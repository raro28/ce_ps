namespace Mx.Ipn.Esime.Statistics.Core.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Mx.Ipn.Esime.Statistics.Core.Resources;

    public abstract class InquirerBase : IInquirer
    {
        public readonly Guid Id;

        protected InquirerBase(DataContainer dataContainer, params InquirerBase[] dependencies)
        {
            if (dataContainer == null)
            {
                throw new StatisticsException(ExceptionMessages.Null_Data_Container, new ArgumentNullException("dataContainer"));
            }
            
            this.Container = dataContainer;

            AssertNotNull(dependencies);
            AssertUniqueDataContainer(dependencies);

            this.Id = Guid.NewGuid();
        }

        public DataContainer Container
        {
            get;
            private set;
        }

        public override string ToString()
        {
            return string.Format("[{0}: Id={1}]", this.GetType().Name, this.Id.ToString().Substring(0, 5));
        }

        bool IInquirer.Inquire(string inquiry, object[] args, out object result)
        {
            var success = false;
            result = null;
            
            var method = this.GetType().GetMethod(inquiry);
            if (method != null)
            {
                result = method.Invoke(this, args);
                success = true;
            }
            
            return success;
        }

        private static void AssertUniqueDataContainer(IEnumerable<InquirerBase> dependencies)
        {
            if (dependencies.Select(inquirer => inquirer.Container).Distinct().Count() > 1)
            {
                throw new StatisticsException(ExceptionMessages.Multiple_DataContainers);
            }
        }

        private static void AssertNotNull(IEnumerable<InquirerBase> dependencies)
        {
            if (dependencies.Count(inquirer => inquirer == null) != 0)
            {
                throw new StatisticsException(ExceptionMessages.Null_Data_Inquirer, new ArgumentNullException("inquirer"));
            }
        }
    }
}