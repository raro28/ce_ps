namespace Mx.Ipn.Esime.Statistics.Core.Base
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;

    public abstract class StatisticsInquirerBase : DynamicObject, IInquirer
    {
        public readonly Dictionary<Type, InquirerBase> Inquirers;

        public StatisticsInquirerBase(DataContainer dataContainer, params InquirerBase[] inquirers)
        {                      
            this.Inquirers = inquirers
                .ToDictionary(inquirer => inquirer.GetType());

            this.Container = dataContainer;
        }

        public DataContainer Container
        {
            get;
            private set;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            return this.Inquire(binder.Name, args, out result);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var success = false;
            result = null;
            foreach (var item in this.Inquirers)
            {
                var member = item.Value.GetType().GetProperty(binder.Name);
                if (member == null)
                {
                    continue;
                }

                result = member.GetValue(item.Value, new object[]{});
                success = true;
            }

            return success;
        }

        public bool Inquire(string inquiry, object[] args, out object result)
        {
            var success = false;
            result = null;

            foreach (var item in this.Inquirers)
            {
                if (success = ((IInquirer)item.Value).Inquire(inquiry, args, out result))
                {
                    break;
                }
            }

            return success;
        }
    }
}