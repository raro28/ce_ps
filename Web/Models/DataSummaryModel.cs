namespace Web.Models
{
    using System.ComponentModel;
    public class DataSummaryModel
    {
        [DisplayName("Original Data")]
        public string Data
        {
            get;
            set;
        }

        [DisplayName("Data Count")]
        public int DataCount
        {
            get;
            set;
        }

        [DisplayName("Max Data")]
        public double Max
        {
            get;
            set;
        }

        [DisplayName("Min Data")]
        public double Min
        {
            get;
            set;
        }

        [DisplayName("Decimal Count")]
        public int DecimalCount
        {
            get;
            set;
        }

        [DisplayName("Data Precision")]
        public double Precision
        {
            get;
            set;
        }
    }
}

