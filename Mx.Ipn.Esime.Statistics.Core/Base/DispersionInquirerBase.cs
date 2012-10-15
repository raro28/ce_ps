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

        [AnswerAttribute(Name = "DataRange", Type = typeof(TaskNames))]
        public abstract double GetDataRange();

        [AnswerAttribute(Name = "QuartileRange", Type = typeof(TaskNames))]
        public double GetInterQuartileRange()
        {
            Func<double> func = () => this.XileInquirer.GetQuartile(3) - this.XileInquirer.GetQuartile(1);

            return this.Container.Register(TaskNames.QuartileRange, func);
        }

        [AnswerAttribute(Name = "DecileRange", Type = typeof(TaskNames))]
        public double GetInterDecileRange()
        {
            Func<double> func = () => this.XileInquirer.GetDecile(9) - this.XileInquirer.GetDecile(1);

            return this.Container.Register(TaskNames.DecileRange, func);
        }

        [AnswerAttribute(Name = "PercentileRange", Type = typeof(TaskNames))]
        public double GetInterPercentileRange()
        {
            Func<double> func = () => this.XileInquirer.GetPercentile(90) - this.XileInquirer.GetPercentile(1);

            return this.Container.Register(TaskNames.PercentileRange, func);
        }

        [AnswerAttribute(Name = "AbsoluteDeviation", Type = typeof(TaskNames))]
        public double GetAbsoluteDeviation()
        {
            Func<double> func = () => this.MeanDifferenceSum(1) / Container.DataCount;

            return this.Container.Register(TaskNames.AbsoluteDeviation, func);
        }

        [AnswerAttribute(Name = "Variance", Type = typeof(TaskNames))]
        public double GetVariance()
        {
            Func<double> func = () => this.MeanDifferenceSum(2) / (Container.DataCount - 1);
            
            return this.Container.Register(TaskNames.Variance, func);
        }

        [AnswerAttribute(Name = "StandarDeviation", Type = typeof(TaskNames))]
        public double GetStandarDeviation()
        {
            Func<double> func = () => Math.Sqrt(this.GetVariance());

            return this.Container.Register(TaskNames.StandarDeviation, func);
        }

        [AnswerAttribute(Name = "CoefficientOfVariation", Type = typeof(TaskNames))]
        public double GetCoefficientOfVariation()
        {
            Func<double> func = () => this.GetStandarDeviation() / this.CentralTendecyInquirer.GetMean();

            return this.Container.Register(TaskNames.CoefficientOfVariation, func);
        }

        [AnswerAttribute(Name = "CoefficientOfSymmetry", Type = typeof(TaskNames))]
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

        [AnswerAttribute(Name = "CoefficientOfKourtosis", Type = typeof(TaskNames))]
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

        [AnswerAttribute(Name = "MomentumFormat", Type = typeof(TaskNames), Formated = true)]
        protected double GetMomentum(int nMomentum)
        {
            Func<double> func = () => this.MeanDifferenceSum(nMomentum) / Container.DataCount;
            var keyMomentum = string.Format(TaskNames.MomentumFormat, nMomentum);

            return this.Container.Register(keyMomentum, func);
        }

        protected abstract double MeanDifferenceSum(int power);
    }
}