using System.Collections.Generic;
using Ninject;
using System.Dynamic;
using System;
using System.Linq;

namespace Mx.Ipn.Esime.Statistics.Core.Base
{
    public class StatisticsInquirerBase: DynamicObject
    {
        protected readonly StandardKernel Kernel;
        protected readonly Dictionary<Type,IInquirer> Inquirers;

        public StatisticsInquirerBase(IEnumerable<double> rawData, params IInquirer[] inquirers)
        {
            Kernel = new StandardKernel();
            Init(rawData);
            Inquirers = inquirers.ToDictionary(inquirer => inquirer.GetType());
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            var success = false;

            var inquirer = Inquirers
                .Where(item => item.Key.GetMethods().Where(method => method.Name == binder.Name).Count() != 0)
                .Select(item => item.Value)
                .SingleOrDefault();

            if (inquirer != null)
            {
                result = inquirer.Inquire(binder.Name, args);
            }

            return success;
        }

        protected virtual void Init()
        {
        }

        private void Init(IEnumerable<double> rawData)
        {
            Kernel.Bind<DataContainer>().ToMethod(context => new DataContainer(rawData)).InSingletonScope();
            Init();
        }
    }
}