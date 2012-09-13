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
		
		public DataDistributionFrequencyInquirer (ReadOnlyCollection<double> sortedData, IInquirer inquirer):base(sortedData,inquirer)
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

		public IEnumerable<double> Frequency {
			get {
				throw new System.NotImplementedException ();
			}
		}

		public IEnumerable<double> AcumulatedFrequency {
			get {
				throw new System.NotImplementedException ();
			}
		}

		public IEnumerable<double> RelativeFrequency {
			get {
				throw new System.NotImplementedException ();
			}
		}

		public IEnumerable<double> AcumulatedRelativeFrequency {
			get {
				throw new System.NotImplementedException ();
			}
		}

		public IEnumerable<double> ClassMark {
			get {
				throw new System.NotImplementedException ();
			}
		}

		public IEnumerable<Pair> ClassIntervals {
			get {
				throw new System.NotImplementedException ();
			}
		}

		public IEnumerable<Pair> RealIntervals {
			get {
				throw new System.NotImplementedException ();
			}
		}
	}	
}