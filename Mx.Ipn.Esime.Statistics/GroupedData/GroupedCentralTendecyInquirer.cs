namespace Mx.Ipn.Esime.Statistics.GroupedData
{
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using Mx.Ipn.Esime.Statistics.Libs;

	public class GroupedCentralTendecyInquirer:InquirerBase, ICentralTendencyInquirer
	{
		public GroupedCentralTendecyInquirer (IList<double> rawData):base(rawData)
		{			
		}
		
		public GroupedCentralTendecyInquirer (ReadOnlyCollection<double> sortedData, IXileInquirer inquirer):base(sortedData, inquirer)
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