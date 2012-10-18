namespace Web.Models
{
    using System.Collections.Generic;
    using System.ComponentModel;

    public class CentralTendencySummaryModel
    {
        public CentralTendencySummaryModel()
        {
            Modes = new List<double>();
        }

        [DisplayName("Mean")]
        public double Mean
        {
            get;
            set;
        }

        [DisplayName("Median")]
        public double Median
        {
            get;
            set;
        }

        [DisplayName("Modes")]
        public IEnumerable<double> Modes
        {
            get;
            set;
        }
    }
    
}
