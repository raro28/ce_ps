namespace Mx.Ipn.Esime.Statistics.GroupedData
{
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using Mx.Ipn.Esime.Statistics.Libs;

	public class DataDistributionFrequencyInquirer:InquirerBase,IDistributionChartInquirer
	{
		public DataDistributionFrequencyInquirer (IList<double> rawData):base(rawData)
		{			
		}
		
		public DataDistributionFrequencyInquirer (ReadOnlyCollection<double> sortedData, GroupedStatisticsInquirer inquirer):base(sortedData,inquirer)
		{
		}

		public bool IsAuxiliaryDataVisible {
			get {
				throw new System.NotImplementedException ();
			}
			set {
				throw new System.NotImplementedException ();
			}
		}

		public IEnumerable<double> GetFrequencies ()
		{
			throw new System.NotImplementedException ();
		}

		public IEnumerable<double> GetAcumulatedFrequencies ()
		{
			throw new System.NotImplementedException ();
		}

		public IEnumerable<double> GetRelativeFrequencies ()
		{
			throw new System.NotImplementedException ();
		}

		public IEnumerable<double> GetAcumulatedRelativeFrequencies ()
		{
			throw new System.NotImplementedException ();
		}

		public IEnumerable<double> GetClassMarks ()
		{
			throw new System.NotImplementedException ();
		}

		public IEnumerable<Pair> GetClassIntervals ()
		{
			throw new System.NotImplementedException ();
		}

		public IEnumerable<Pair> GetRealIntervals ()
		{
			throw new System.NotImplementedException ();
		}
	}	
}