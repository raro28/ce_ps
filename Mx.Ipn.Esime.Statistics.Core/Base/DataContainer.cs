namespace Mx.Ipn.Esime.Statistics.Core.Base
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Mx.Ipn.Esime.Statistics.Core.Resources;

    public class DataContainer
    {
        public readonly Dictionary<string, dynamic> Answers;
        public readonly ReadOnlyCollection<double> Data;
        public readonly int DataPrecision;

        public readonly double DataPrecisionValue;

        public readonly int DataCount;

        public DataContainer(IEnumerable<double> data)
        {
            AssertValidData(data);
            this.Answers = new Dictionary<string, dynamic>();
            
            var cache = data.ToList();
            cache.Sort();
            
            this.Data = cache.AsReadOnly();
            this.DataPrecision = DataContainer.GetDataPrecision(data);
            this.DataPrecisionValue = Math.Pow(1, -1 * this.DataPrecision);
            this.DataCount = data.Count();
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