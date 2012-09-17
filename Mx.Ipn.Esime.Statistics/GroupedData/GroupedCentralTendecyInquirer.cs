namespace Mx.Ipn.Esime.Statistics.GroupedData
{
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Libs;

	public class GroupedCentralTendecyInquirer:InquirerBase, ICentralTendencyInquirer
	{
		public GroupedCentralTendecyInquirer (List<double> rawData):base(rawData)
		{			
		}

		public double GetMean ()
		{
			throw new System.NotImplementedException ();
		}

		public double GetMedian ()
		{
			throw new System.NotImplementedException ();
		}

		public IList<double> GetMode ()
		{
			throw new System.NotImplementedException ();
		}
	}
}