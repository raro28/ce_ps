namespace Mx.Ipn.Esime.Statistics.GroupedData
{
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Libs;

	public class GroupedXileInquirer:InquirerBase,IXileInquirer
	{
		public GroupedXileInquirer (IList<double> rawData):base(rawData)
		{			
		}

		public double GetDecile (int nTh)
		{
			throw new System.NotImplementedException ();
		}

		public double GetPercentile (int nTh)
		{
			throw new System.NotImplementedException ();
		}

		public double GetQuartile (int nTh)
		{
			throw new System.NotImplementedException ();
		}
	}
}