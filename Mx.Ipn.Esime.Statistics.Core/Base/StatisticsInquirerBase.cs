namespace Mx.Ipn.Esime.Statistics.Core.Base
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;

    public abstract class StatisticsInquirerBase : DynamicObject
    {
        public readonly IEnumerable<InquirerBase> Inquirers;

        public StatisticsInquirerBase(DataContainer dataContainer, params InquirerBase[] inquirers)
        {                      
            this.Inquirers = inquirers;

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
                var member = item.GetType().GetProperty(binder.Name);
                if (member == null)
                {
                    continue;
                }

                result = member.GetValue(item, new object[] { });
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
                var method = item.GetType().GetMethod(inquiry);
                if (method != null)
                {
                    result = method.Invoke(item, args);
                    success = true;
                    break;
                }
            }

            return success;
        }
    }
}