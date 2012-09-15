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

	public interface IDistributionChartInquirer
	{
		bool IsAuxiliaryDataVisible {
			get;
			set;
		}

		IEnumerable<double> GetFrequencies ();

		IEnumerable<double> GetAcumulatedFrequencies ();

		IEnumerable<double> GetRelativeFrequencies ();

		IEnumerable<double> GetAcumulatedRelativeFrequencies ();

		IEnumerable<double> GetClassMarks ();

		IEnumerable<Pair> GetClassIntervals ();

		IEnumerable<Pair> GetRealIntervals ();
	}
}