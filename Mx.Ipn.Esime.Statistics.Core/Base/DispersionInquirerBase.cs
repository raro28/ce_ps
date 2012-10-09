namespace Mx.Ipn.Esime.Statistics.Core.Base
{
    using System;
    using Mx.Ipn.Esime.Statistics.Core.Resources;

    public abstract class DispersionInquirerBase : InquirerBase, IDispersionInquirer
    {
        protected readonly XileInquirerBase XileInquirer;
        protected readonly CentralTendecyInquirerBase CentralTendecyInquirer;

        public DispersionInquirerBase(DataContainer dataContainer, params InquirerBase[] dependencies) : base(dataContainer, dependencies)
        {
        }

        public double GetDataRange()
        {
            if (!DataContainer.Answers.ContainsKey(TaskNames.DataRange))
            {
                DataContainer.Answers.Add(TaskNames.DataRange, this.CalcDataRange());
            }
            
            return DataContainer.Answers[TaskNames.DataRange];
        }
        
        public double GetInterQuartileRange()
        {
            if (!DataContainer.Answers.ContainsKey(TaskNames.QuartileRange))
            {
                DataContainer.Answers.Add(TaskNames.QuartileRange, this.XileInquirer.GetQuartile(3) - this.XileInquirer.GetQuartile(1));
            }
            
            return DataContainer.Answers[TaskNames.QuartileRange];
        }
        
        public double GetInterDecileRange()
        {
            if (!DataContainer.Answers.ContainsKey(TaskNames.DecileRange))
            {
                DataContainer.Answers.Add(TaskNames.DecileRange, this.XileInquirer.GetDecile(9) - this.XileInquirer.GetDecile(1));
            }
            
            return DataContainer.Answers[TaskNames.DecileRange];
        }
        
        public double GetInterPercentileRange()
        {
            if (!DataContainer.Answers.ContainsKey(TaskNames.PercentileRange))
            {
                DataContainer.Answers.Add(TaskNames.PercentileRange, this.XileInquirer.GetPercentile(90) - this.XileInquirer.GetPercentile(10));
            }
            
            return DataContainer.Answers[TaskNames.PercentileRange];
        }

        public double GetAbsoluteDeviation()
        {
            if (!DataContainer.Answers.ContainsKey(TaskNames.AbsoluteDeviation))
            {
                DataContainer.Answers.Add(TaskNames.AbsoluteDeviation, this.CalcAbsoluteDeviation());
            }

            return DataContainer.Answers[TaskNames.AbsoluteDeviation];
        }
        
        public double GetVariance()
        {
            if (!DataContainer.Answers.ContainsKey(TaskNames.Variance))
            {
                DataContainer.Answers.Add(TaskNames.Variance, this.CalcVariance());
            }

            return DataContainer.Answers[TaskNames.Variance];
        }
        
        public double GetStandarDeviation()
        {
            if (!DataContainer.Answers.ContainsKey(TaskNames.StandarDeviation))
            {
                DataContainer.Answers.Add(TaskNames.StandarDeviation, Math.Sqrt(this.GetVariance()));
            }

            return DataContainer.Answers[TaskNames.StandarDeviation];
        }

        public double GetCoefficientOfVariation()
        {
            if (!DataContainer.Answers.ContainsKey(TaskNames.CoefficientOfVariation))
            {
                var strDev = this.GetStandarDeviation();
                var cov = strDev / this.CentralTendecyInquirer.GetMean();

                DataContainer.Answers.Add(TaskNames.CoefficientOfVariation, cov);
            }

            return DataContainer.Answers[TaskNames.CoefficientOfVariation];
        }
        
        public double GetCoefficientOfSymmetry()
        {
            if (!DataContainer.Answers.ContainsKey(TaskNames.CoefficientOfSymmetry))
            {
                var m3 = this.GetMomentum(3);
                var m2 = this.GetMomentum(2);
                var cos = m3 / Math.Pow(m2, 1.5);

                DataContainer.Answers.Add(TaskNames.CoefficientOfSymmetry, cos);
            }

            return DataContainer.Answers[TaskNames.CoefficientOfSymmetry];
        }
        
        public double GetCoefficientOfKourtosis()
        {
            if (!DataContainer.Answers.ContainsKey(TaskNames.CoefficientOfKourtosis))
            {
                var m4 = this.GetMomentum(4);
                var m2 = this.GetMomentum(2);
                var cok = m4 / Math.Pow(m2, 2);

                DataContainer.Answers.Add(TaskNames.CoefficientOfKourtosis, cok);
            }

            return DataContainer.Answers[TaskNames.CoefficientOfKourtosis];
        }

        protected abstract double CalcAbsoluteDeviation();

        protected abstract double CalcVariance();

        protected abstract double CalcMomentum(int nMomentum);

        protected abstract double CalcDataRange();

        private double GetMomentum(int nMomentum)
        {
            var keyMomentum = string.Format(TaskNames.MomentumFormat, nMomentum);
            if (!DataContainer.Answers.ContainsKey(keyMomentum))
            {
                DataContainer.Answers.Add(keyMomentum, this.CalcMomentum(nMomentum));
            }
            
            return DataContainer.Answers[keyMomentum];
        }
    }
}