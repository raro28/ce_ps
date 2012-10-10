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
                inquirer.Resolved += this.UpgradeAnswer;
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

            var inquirer = this.Inquirers
                .Where(pair => pair.Key.GetMethod(inquiry) != null)
                .Select(pair => new 
                            {
                        Type = pair.Key,
                        Instance = pair.Value,
                        Method = pair.Key.GetMethod(inquiry)
                    }).SingleOrDefault();

            if (inquirer != null)
            {
                var attr = inquirer.Method.GetCustomAttributes(typeof(AnswerAttribute), true).SingleOrDefault();
                string name = null;
                if (attr != null)
                {
                    var answerAttr = (AnswerAttribute)attr;
                    name = answerAttr.Formated ? answerAttr.Format(args) : answerAttr.Name;
                }

                if ((name != null && !this.Answers.ContainsKey(name)) || name == null)
                {
                    result = inquirer.Method.Invoke(inquirer.Instance, args);
                }
                else
                {
                    result = this.Answers[name];
                }

                success = true;
            }

            return success;
        }

        private void UpgradeAnswer(object sender, InquiryEventArgs args)
        {
            if (this.Answers.ContainsKey(args.Inquiry))
            {
                this.Answers[args.Inquiry] = args.Result;
            }
            else
            {
                this.Answers.Add(args.Inquiry, args.Result);
            }
        }
    }
}