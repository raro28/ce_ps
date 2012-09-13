namespace Mx.Ipn.Esime.Statistics.GroupedData
{
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using Mx.Ipn.Esime.Statistics.Libs;

	public class GroupedRangesInquirer:InquirerBase,IRangesInquirer
	{
		public GroupedRangesInquirer (IList<double> rawData):base(rawData)
		{			
		}
		
		public GroupedRangesInquirer (ReadOnlyCollection<double> sortedData, IXileInquirer inquirer):base(sortedData,inquirer)
		{
		}

		public double GetDataRange ()
		{
			throw new System.NotImplementedException ();
		}

		public double GetInterquartileRange ()
		{
			throw new System.NotImplementedException ();
		}

		public double GetInterdecileRange ()
		{
			throw new System.NotImplementedException ();
		}

		public double GetInterpercentileRange ()
		{
			throw new System.NotImplementedException ();
		}
	}
}