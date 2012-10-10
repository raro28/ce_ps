namespace Mx.Ipn.Esime.Statistics.BaseData.Tests
{
	using System;
    using System.Linq;
    using System.Collections.Generic;
    using Mx.Ipn.Esime.Statistics.Core;
    using Mx.Ipn.Esime.Statistics.Core.Base;
    using Ninject;

    public abstract class HelperMethodsBase
    {
        private readonly static Random rnd;

        static HelperMethodsBase()
        {
            rnd = new Random(DateTime.Now.Millisecond);
        }

        public IEnumerable<double> GetRandomDataSample(int size)
        {
            for (int i = 1; i <= size; i++)
            {
                yield return rnd.Next(57, 180) + Math.Round(rnd.NextDouble(), 2);
            }
        }

        public TInquirer NewInquirer<TInquirer>(int size) where TInquirer : IInquirer
        {

            var inquirer = NewInquirer<TInquirer>(GetRandomDataSample(size).ToList());
			
            return inquirer;
        }

        public TInquirer NewInquirer<TInquirer>(List<double> data) where TInquirer : IInquirer
        {
            var Kernel = new StandardKernel();
            Kernel.Bind<DataContainer>().ToMethod(context => {
                var container = new DataContainer(data);
                data.Sort();

                return container;
            }).InSingletonScope();

            return NewInquirer<TInquirer>(Kernel);
        }

        public virtual TInquirer NewInquirer<TInquirer>(StandardKernel kernel) where TInquirer : IInquirer
        {
            return kernel.Get<TInquirer>();
        }
					
        public abstract double CalcNthXile(IList<double> data, int xile, int nTh);
		
        public abstract double SampleMean(List<double> data);
    }

    internal static class SequenceCastExtension
    {
        public static bool TypeSequenceIsAssignableFrom(this IEnumerable<Type> source, IEnumerable<Object> sequence)
        {
            var canCast = true;

            if (source.Count() == sequence.Count())
            {
                if (source.Count() > 0)
                {
                    var srcEnum = source.GetEnumerator();
                    var seqEnum = sequence.GetEnumerator();
                    do
                    {
                        if (seqEnum.Current == null)
                        {
                            continue;
                        }

                        canCast = srcEnum.Current.IsAssignableFrom(seqEnum.Current.GetType());
                    }
                    while(srcEnum.MoveNext() && seqEnum.MoveNext() && canCast);
                }
            }
            else
            {
                canCast = false;
            }

            return canCast;
        }
    }
}