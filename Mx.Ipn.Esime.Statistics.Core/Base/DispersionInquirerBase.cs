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
            var range = this.XileInquirer.GetQuartile(3) - this.XileInquirer.GetQuartile(1);
            this.FireResolvedEvent(this, new InquiryEventArgs(TaskNames.QuartileRange, range));

            return range;
        }

        [AnswerAttribute(Name = "DecileRange", Type = typeof(TaskNames))]
        public double GetInterDecileRange()
        {
            var range = this.XileInquirer.GetDecile(9) - this.XileInquirer.GetDecile(1);
            this.FireResolvedEvent(this, new InquiryEventArgs(TaskNames.DecileRange, range));

            return range;
        }

        [AnswerAttribute(Name = "PercentileRange", Type = typeof(TaskNames))]
        public double GetInterPercentileRange()
        {
            var range = this.XileInquirer.GetPercentile(90) - this.XileInquirer.GetPercentile(10);
            this.FireResolvedEvent(this, new InquiryEventArgs(TaskNames.PercentileRange, range));

            return range;
        }

        [AnswerAttribute(Name = "AbsoluteDeviation", Type = typeof(TaskNames))]
        public abstract double GetAbsoluteDeviation();

        [AnswerAttribute(Name = "Variance", Type = typeof(TaskNames))]
        public abstract double GetVariance();

        [AnswerAttribute(Name = "StandarDeviation", Type = typeof(TaskNames))]
        public double GetStandarDeviation()
        {
            var sde = Math.Sqrt(this.GetVariance());
            this.FireResolvedEvent(this, new InquiryEventArgs(TaskNames.StandarDeviation, sde));

            return sde;
        }

        [AnswerAttribute(Name = "CoefficientOfVariation", Type = typeof(TaskNames))]
        public double GetCoefficientOfVariation()
        {
            var cov = this.GetStandarDeviation() / this.CentralTendecyInquirer.GetMean();
            this.FireResolvedEvent(this, new InquiryEventArgs(TaskNames.CoefficientOfVariation, cov));

            return cov;
        }

        [AnswerAttribute(Name = "CoefficientOfSymmetry", Type = typeof(TaskNames))]
        public double GetCoefficientOfSymmetry()
        {
            var m3 = this.GetMomentum(3);
            var m2 = this.GetMomentum(2);
            var cos = m3 / Math.Pow(m2, 1.5);
            this.FireResolvedEvent(this, new InquiryEventArgs(TaskNames.CoefficientOfSymmetry, cos));

            return cos;
        }

        [AnswerAttribute(Name = "CoefficientOfKourtosis", Type = typeof(TaskNames))]
        public double GetCoefficientOfKourtosis()
        {
            var m4 = this.GetMomentum(4);
            var m2 = this.GetMomentum(2);
            var cok = m4 / Math.Pow(m2, 2);
            this.FireResolvedEvent(this, new InquiryEventArgs(TaskNames.CoefficientOfKourtosis, cok));

            return cok;
        }

        [AnswerAttribute(Name = "MomentumFormat", Type = typeof(TaskNames), Formated = true)]
        protected abstract double GetMomentum(int nMomentum);
    }
}