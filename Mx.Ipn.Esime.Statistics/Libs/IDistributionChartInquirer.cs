namespace Mx.Ipn.Esime.Statistics.Libs
{
	using System.Collections.Generic;

	public struct Pair
	{
		public double A {
			get;
			set;
		}

		public double B {
			get;
			set;
		}
	}

	public interface IDistributionChartInquirer:IInquirer
	{
		bool IsAuxiliaryDataVisible {
			get;
			set;
		}

		IEnumerable<double> Frequency {
			get;
		}

		IEnumerable<double> AcumulatedFrequency {
			get;
		}

		IEnumerable<double> RelativeFrequency {
			get;
		}

		IEnumerable<double> AcumulatedRelativeFrequency {
			get;
		}

		IEnumerable<double> ClassMark {
			get;
		}

		IEnumerable<Pair> ClassIntervals {
			get;
		}

		IEnumerable<Pair> RealIntervals {
			get;
		}
	}
}