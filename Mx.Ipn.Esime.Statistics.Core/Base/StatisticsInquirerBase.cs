namespace Mx.Ipn.Esime.Statistics.Core.Base
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;

    public abstract class StatisticsInquirerBase : DynamicObject, IInquirer
    {
        public readonly Dictionary<Type, InquirerBase> Inquirers;
        protected readonly Dictionary<string, dynamic> Answers;

        public StatisticsInquirerBase(DataContainer dataContainer, params InquirerBase[] inquirers)
        {                      
            this.Inquirers = inquirers
                .ToDictionary(inquirer => 
            {
                inquirer.Resolved += this.RegisterAnswer;
                return inquirer.GetType();
            });

            this.Answers = new Dictionary<string, dynamic>();
            this.DataContainer = dataContainer;
        }

        public DataContainer DataContainer
        {
            get;
            private set;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            return this.Inquire(binder.Name, args, out result);
        }

        public bool Inquire(string inquiry, object[] args, out object result)
        {
            var success = false;
            result = null;
//            var answer = inquiry;
//            if (!this.Answers.ContainsKey(answer))
//            {
            foreach (var inquirer in this.Inquirers)
            {
                if (success = ((IInquirer)inquirer.Value).Inquire(inquiry, args, out result))
                {
                    break;
                }
            }
//            }
//            else
//            {
//                result = this.Answers[answer];
//                success = true;
//            }

            return success;
        }

        private void RegisterAnswer(object sender, InquiryEventArgs args)
        {
            if (!this.Answers.ContainsKey(args.Inquiry))
            {
                this.Answers.Add(args.Inquiry, args.Result);
            }
        }
    }
}