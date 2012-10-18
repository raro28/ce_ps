namespace Web.Models
{
    using System.Collections.Generic;

    public class GroupedReportModel
    {
        public GroupedReportModel()
        {
            Table = new List<dynamic>();
        }

        public double Range
        {
            get;
            set;
        }

        public int GroupsCount
        {
            get;
            set;
        }

        public double Amplitude
        {
            get;
            set;
        }

        public List<dynamic> Table
        {
            get;
            set;
        }
    }
}