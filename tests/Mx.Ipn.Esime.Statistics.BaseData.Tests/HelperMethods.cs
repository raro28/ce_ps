/*
 * Copyright (C) 2012 Hector Eduardo Diaz Campos
 * 
 * This file is part of Mx.DotNet.Statistics.
 * 
 * Mx.DotNet.Statistics is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * any later version.
 * 
 * Mx.DotNet.Statistics is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with Mx.DotNet.Statistics.  If not, see <http://www.gnu.org/licenses/>.
 */
namespace Mx.Ipn.Esime.Statistics.BaseData.Tests
{
	using System;
    using System.Linq;
    using System.Collections.Generic;
    using Mx.Ipn.Esime.Statistics.Core;
    using Mx.Ipn.Esime.Statistics.Core.Base;
    using Ninject;

    public class HelperMethods
    {
        private readonly static Random rnd;

        static HelperMethods()
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

        public Func<List<double>,int, int,double> CalcNthXile
        {
            get;
            protected set;
        }

        public Func<List<double>,double> SampleMean
        {
            get;
            protected set;
        }

        public TInquirer NewInquirer<TInquirer>(int size)
        {

            var inquirer = NewInquirer<TInquirer>(GetRandomDataSample(size).ToList());
			
            return inquirer;
        }

        public TInquirer NewInquirer<TInquirer>(List<double> data)
        {
            var Kernel = new StandardKernel();
            Kernel.Bind<DataContainer>().ToMethod(context => {
                var container = new DataContainer(data);
                data.Sort();

                return container;
            }).InSingletonScope();

            return NewInquirer<TInquirer>(Kernel);
        }

        public virtual TInquirer NewInquirer<TInquirer>(StandardKernel kernel)
        {
            return kernel.Get<TInquirer>();
        }
    }

//    internal static class SequenceCastExtension
//    {
//        public static bool TypeSequenceIsAssignableFrom(this IEnumerable<Type> source, IEnumerable<Object> sequence)
//        {
//            var canCast = true;
//
//            if (source.Count() == sequence.Count())
//            {
//                if (source.Count() > 0)
//                {
//                    var srcEnum = source.GetEnumerator();
//                    var seqEnum = sequence.GetEnumerator();
//                    do
//                    {
//                        if (seqEnum.Current == null)
//                        {
//                            continue;
//                        }
//
//                        canCast = srcEnum.Current.IsAssignableFrom(seqEnum.Current.GetType());
//                    }
//                    while(srcEnum.MoveNext() && seqEnum.MoveNext() && canCast);
//                }
//            }
//            else
//            {
//                canCast = false;
//            }
//
//            return canCast;
//        }
//    }
}