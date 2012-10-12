namespace Mx.Ipn.Esime.Statistics.Core
{
    using System;
    using System.Linq;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AnswerAttribute : Attribute
    {
        private readonly object[] defaults;

        private string name;

        public AnswerAttribute(params object[] defaults)
        {
            this.Formated = false;
            this.defaults = defaults;
        }

        public string Name
        {
            get
            {
                return Type.GetProperty(this.name).GetValue(null, null).ToString();
            }

            set
            {
                this.name = value;
            }
        }

        public Type Type
        {
            get;
            set;
        }

        public bool Formated
        {
            get;
            set;
        }

        public string Format(params object[] args)
        {
            var argsList = args.ToList();
            argsList.InsertRange(0, this.defaults.ToList());

            return string.Format(this.Name, argsList.ToArray());
        }

        public override string ToString()
        {
            return string.Format("[Name=({0},{1}), Type={2}, Formated={3}]", this.Name, this.name, this.Type.Name, this.Formated);
        }
    }
}