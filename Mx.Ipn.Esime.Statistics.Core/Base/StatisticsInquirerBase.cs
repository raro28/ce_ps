namespace Mx.Ipn.Esime.Statistics.Core.Base
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using Ninject;

    public abstract class StatisticsInquirerBase : DynamicObject
    {
        protected readonly StandardKernel Kernel;
        protected readonly Dictionary<Type, IInquirer> Inquirers;

        public StatisticsInquirerBase(IEnumerable<double> rawData, params IInquirer[] inquirers)
        {
            this.Kernel = new StandardKernel();
            this.Init(rawData);
            this.Inquirers = inquirers.ToDictionary(inquirer => inquirer.GetType());
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            var success = false;
            result = null;

            var inquirer = this.Inquirers
                .Where(item => item.Key
                       .GetMethods()
                       .Where(method => method.Name == binder.Name && method.CanAssignValueSequence(args))
                       .Count() != 0)
                .Select(item => item.Value)
                .SingleOrDefault();

            if (inquirer != null)
            {
                result = inquirer.Inquire(binder.Name, args);
                success = true;
            }

            return success;
        }

        protected virtual void Init()
        {
        }

        private void Init(IEnumerable<double> rawData)
        {
            this.Kernel.Bind<DataContainer>().ToMethod(context => new DataContainer(rawData)).InSingletonScope();
            this.Init();
        }
    }
}