namespace Mx.Ipn.Esime.Statistics.Core
{
    using System;

    public class AnswerAttribute : Attribute
    {
        public readonly string Name;

        public AnswerAttribute(string name)
        {
            this.Name = name;
        }
    }
}