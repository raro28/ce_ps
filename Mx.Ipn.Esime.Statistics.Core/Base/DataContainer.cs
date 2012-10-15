namespace Mx.Ipn.Esime.Statistics.Core.Base
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Mx.Ipn.Esime.Statistics.Core.Resources;

    public class DataContainer
    {
        public readonly ReadOnlyCollection<double> Data;
        public readonly int DataPrecision;
        public readonly double DataPrecisionValue;
        public readonly int DataCount;

        public readonly Guid id;

        public DataContainer(IEnumerable<double> data)
        {
            AssertValidData(data);
            
            var cache = data.ToList();
            cache.Sort();
            
            this.Data = cache.AsReadOnly();
            this.DataPrecision = DataContainer.GetDataPrecision(data);
            this.DataPrecisionValue = Math.Pow(10, -1 * this.DataPrecision);
            this.DataCount = data.Count();
            this.Answers = new Dictionary<string, dynamic>();

            this.id = Guid.NewGuid();
        }

        public Dictionary<string, dynamic> Answers
        {
            get;
            private set;
        }

        public T Register <T>(string opName, Func<T> function)
        {
            T result;
            if (!this.Answers.ContainsKey(opName))
            {
                result = function();
                this.Answers.Add(opName, result);
            }
            else
            {
                result = this.Answers[opName];
            }

            return result;
        }

        public void Register(string opName, string resultName, Action action)
        {
            if (!this.Answers.ContainsKey(opName))
            {
                action();
                this.Answers.Add(opName, resultName);
            }
        }

        public override string ToString()
        {
            return string.Format("[{0}: Id={1}]", this.GetType().Name, this.id.ToString().Substring(0, 5));
        }

        private static void AssertValidData(IEnumerable<double> data)
        {
            if (data == null)
            {
                throw new StatisticsException(ExceptionMessages.Null_Data_Set, new ArgumentNullException("data"));
            }

            var count = data.Count();
            
            if (count == 0)
            {
                throw new StatisticsException(ExceptionMessages.Empty_Data_Set);
            }

            if (count == 1)
            {
                throw new StatisticsException(ExceptionMessages.Insufficient_Data);
            }
        }

        private static int GetDataPrecision(IEnumerable<double> data)
        {
            var decimalLengths = data
                .Distinct()
                    .Where(number => number.ToString().Contains('.'))
                    .Select(number => number.ToString().Split('.')[1].Length)
                    .Distinct()
                    .ToList();
            
            var maxLenght = decimalLengths.Count() != 0 ? decimalLengths.Max() : 0;
            
            return maxLenght;
        }
    }
}