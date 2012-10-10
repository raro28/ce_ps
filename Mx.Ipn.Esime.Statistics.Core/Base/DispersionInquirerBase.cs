namespace Mx.Ipn.Esime.Statistics.Core.Base
{
    using System;

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
            var range = this.XileInquirer.GetQuartile(3) - this.XileInquirer.GetQuartile(1);

            return range;
        }
        
        public double GetInterDecileRange()
        {
            var range = this.XileInquirer.GetDecile(9) - this.XileInquirer.GetDecile(1);

            return range;
        }
        
        public double GetInterPercentileRange()
        {
            var range = this.XileInquirer.GetPercentile(90) - this.XileInquirer.GetPercentile(10);

            return range;
        }

        public abstract double GetAbsoluteDeviation();
        
        public abstract double GetVariance();
        
        public double GetStandarDeviation()
        {
            var sde = Math.Sqrt(this.GetVariance());

            return sde;
        }

        public double GetCoefficientOfVariation()
        {
            var cov = this.GetStandarDeviation() / this.CentralTendecyInquirer.GetMean();

            return cov;
        }
        
        public double GetCoefficientOfSymmetry()
        {
            var m3 = this.GetMomentum(3);
            var m2 = this.GetMomentum(2);
            var cos = m3 / Math.Pow(m2, 1.5);

            return cos;
        }
        
        public double GetCoefficientOfKourtosis()
        {
            var m4 = this.GetMomentum(4);
            var m2 = this.GetMomentum(2);
            var cok = m4 / Math.Pow(m2, 2);

            return cok;
        }

        protected abstract double GetMomentum(int nMomentum);
    }
}