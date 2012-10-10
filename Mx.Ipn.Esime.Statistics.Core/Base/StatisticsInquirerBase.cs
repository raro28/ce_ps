namespace Mx.Ipn.Esime.Statistics.Core.Base
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using Ninject;

    public abstract class StatisticsInquirerBase : DynamicObject
    {
        public readonly Dictionary<Type, InquirerBase> Inquirers;
        protected static readonly StandardKernel Kernel;

        static StatisticsInquirerBase()
        {
            Kernel = new StandardKernel();
        }

        public StatisticsInquirerBase(DataContainer dataContainer, params InquirerBase[] inquirers)
        {                      
            this.Inquirers = inquirers.ToDictionary(inquirer => inquirer.GetType());
            this.DataContainer = dataContainer;
        }

        public DataContainer DataContainer
        {
            get;
            private set;
        }

        public static TInquirer CreateInstance<TInquirer>(IEnumerable<double> rawData) where TInquirer : StatisticsInquirerBase
        {           
            if (Kernel.GetBindings(typeof(TInquirer)).Where(bind => bind.Metadata.Name == "NewInstance").Count() == 0)
            {            
                Kernel.Bind<DataContainer>().ToMethod(context => new DataContainer(rawData)).InSingletonScope();
                Kernel.Bind<TInquirer>().ToSelf().Named("NewInstance");
                Kernel.Bind<TInquirer>().ToMethod(context => Kernel.Get<TInquirer>("NewInstance")).InSingletonScope().Named("Singleton");
            }
            
            return Kernel.Get<TInquirer>("NewInstance");
        }
        
        public static TInquirer GetInstance<TInquirer>()
        {           
            return Kernel.Get<TInquirer>("Singleton");
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            return this.Inquire(binder.Name, args, out result);
        }

        public bool Inquire(string inquiry, object[] args, out object result)
        {
            var success = false;
            result = null;

            var inquirer = this.Inquirers
                .Where(pair => pair.Key.ResolveFor(inquiry, args) != null)
                .Select(pair => new 
                            {
                        Method = pair.Key.ResolveFor(inquiry, args), Instance = pair.Value
                    }).SingleOrDefault();

            if (inquirer != null)
            {
                result = inquirer.Method.Invoke(inquirer.Instance, args);
                success = true;
            }
            
            return success;
        }
    }
}