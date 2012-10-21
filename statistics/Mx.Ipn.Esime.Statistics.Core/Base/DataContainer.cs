/*
 * Copyright (C) 2012 Hector Eduardo Diaz Campos
 * 
 * This file is part of Mx.DotNet.Statistics.
 * 
 * Mx.DotNet.Statistics is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * any later version.
 * 
 * Mx.DotNet.Statistics is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with Mx.DotNet.Statistics.  If not, see <http://www.gnu.org/licenses/>.
 */
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
        public readonly double Max;
        public readonly double Min;

        private readonly Guid id;
        private readonly Dictionary<string, dynamic> answers;

        public DataContainer(IEnumerable<double> data)
        {
            AssertValidData(data);
            
            var cache = data.ToList();
            cache.Sort();
            
            this.Data = cache.AsReadOnly();
            this.DataPrecision = DataContainer.GetDataPrecision(data);
            var precision = Math.Pow(10, -1 * this.DataPrecision);
            this.DataPrecisionValue = Math.Abs(precision - 1.0) <= 0 ? 0.5 : precision;

            this.DataCount = data.Count();
            this.Max = data.Max();
            this.Min = data.Min();
            this.answers = new Dictionary<string, dynamic>();

            this.id = Guid.NewGuid();
        }

        public T Register<T>(string opName, Func<T> function)
        {
            T result;
            if (!this.answers.ContainsKey(opName))
            {
                result = function();
                this.answers.Add(opName, result);
            }
            else
            {
                result = this.answers[opName];
            }

            return result;
        }

        public void Register(string opName, string resultName, Action action)
        {
            if (!this.answers.ContainsKey(opName))
            {
                action();
                this.answers.Add(opName, resultName);
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