namespace Web.Models
{
    using System.ComponentModel;

    public class DispersionSummaryModel
    {
        [DisplayName("Data Range")]
        public double DataRange
        {
            get;
            set;
        }

        [DisplayName("InterQuartile Range")]
        public double InterQuartileRange
        {
            get;
            set;
        }

        [DisplayName("InterPercentile Range")]
        public double InterPercentileRange
        {
            get;
            set;
        }

        [DisplayName("InterDecile Range")]
        public double InterDecileRange
        {
            get;
            set;
        }

        [DisplayName("Absolute Deviation")]
        public double AbsoluteDeviation
        {
            get;
            set;
        }

        [DisplayName("Variance")]
        public double Variance
        {
            get;
            set;
        }

        [DisplayName("Standar Deviation")]
        public double StandarDeviation
        {
            get;
            set;
        }

        [DisplayName("Coefficient Of Variation")]
        public double CoefficientOfVariation
        {
            get;
            set;
        }

        [DisplayName("Coefficient Of Kourtosis")]
        public double CoefficientOfKourtosis
        {
            get;
            set;
        }

        [DisplayName("Coefficient Of Symmetry")]
        public double CoefficientSymmetry
        {
            get;
            set;
        }
    }
}
