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

                if ((name != null && !this.Container.Answers.ContainsKey(name)) || name == null)
                {
                    result = inquirer.Method.Invoke(inquirer.Instance, args);
                }
                else
                {
                    result = this.Container.Answers[name];
                }

                success = true;
            }

            return success;
        }
    }
}