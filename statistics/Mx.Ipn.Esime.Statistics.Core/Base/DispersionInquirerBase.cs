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
    using Mx.Ipn.Esime.Statistics.Core.Resources;

    public abstract class DispersionInquirerBase : InquirerBase, IDispersionInquirer
    {
        public DispersionInquirerBase(DataContainer dataContainer, params InquirerBase[] dependencies) : base(dataContainer, dependencies)
        {
        }

        protected IXileInquirer XileInquirer
        {
            get;
            set;
        }

        protected ICentralTendencyInquirer CentralTendecyInquirer
        {
            get;
            set;
        }

        public abstract double GetDataRange();

        public double GetInterQuartileRange()
        {
            Func<double> func = () => this.XileInquirer.GetQuartile(3) - this.XileInquirer.GetQuartile(1);

            return this.Container.Register(TaskNames.QuartileRange, func);
        }

        public double GetInterDecileRange()
        {
            Func<double> func = () => this.XileInquirer.GetDecile(9) - this.XileInquirer.GetDecile(1);

            return this.Container.Register(TaskNames.DecileRange, func);
        }

        public double GetInterPercentileRange()
        {
            Func<double> func = () => this.XileInquirer.GetPercentile(90) - this.XileInquirer.GetPercentile(10);

            return this.Container.Register(TaskNames.PercentileRange, func);
        }

        public double GetAbsoluteDeviation()
        {
            Func<double> func = () => this.MeanDifferenceSum(1) / Container.DataCount;

            return this.Container.Register(TaskNames.AbsoluteDeviation, func);
        }

        public double GetVariance()
        {
            Func<double> func = () => this.MeanDifferenceSum(2) / (Container.DataCount - 1);
            
            return this.Container.Register(TaskNames.Variance, func);
        }

        public double GetStandarDeviation()
        {
            Func<double> func = () => Math.Sqrt(this.GetVariance());

            return this.Container.Register(TaskNames.StandarDeviation, func);
        }

        public double GetCoefficientOfVariation()
        {
            Func<double> func = () => this.GetStandarDeviation() / this.CentralTendecyInquirer.GetMean();

            return this.Container.Register(TaskNames.CoefficientOfVariation, func);
        }

        public double GetCoefficientOfSymmetry()
        {
            Func<double> func = () => 
            {
                var m3 = this.GetMomentum(3);
                var m2 = this.GetMomentum(2);
                var cos = m3 / Math.Pow(m2, 1.5);

                return cos;
            };

            return this.Container.Register(TaskNames.CoefficientOfSymmetry, func);
        }

        public double GetCoefficientOfKourtosis()
        {
            Func<double> func = () => 
            {
                var m4 = this.GetMomentum(4);
                var m2 = this.GetMomentum(2);
                var cok = m4 / Math.Pow(m2, 2);
                
                return cok;
            };
            
            return this.Container.Register(TaskNames.CoefficientOfKourtosis, func);
        }

        protected static void AssertValidPower(int power)
        {
            if (power < 1 || power > 4)
            {
                throw new StatisticsException(string.Format(ExceptionMessages.Invalid_Power_Format, power));
            }
        }

        protected double GetMomentum(int nMomentum)
        {
            Func<double> func = () => this.MeanDifferenceSum(nMomentum) / Container.DataCount;
            var keyMomentum = string.Format(TaskNames.MomentumFormat, nMomentum);

            return this.Container.Register(keyMomentum, func);
        }

        protected abstract double MeanDifferenceSum(int power);
    }
}