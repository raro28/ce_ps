namespace Mx.Ipn.Esime.Statistics.UngroupedData
{
	using System.Linq;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Core.Base;
	
	public class UngroupedCentralTendecyInquirer:CentralTendecyInquirerBase
	{
		public UngroupedCentralTendecyInquirer (List<double> rawData):base(rawData)
		{	
		}

		protected override double CalcMean ()
		{
			var mean = Data.Sum () / Data.Count;

			return mean;
		}

		protected override double CalcMedian ()
		{
			var midIndex = (Data.Count / 2) - 1;
			var median = Data.Count % 2 != 0 ? Data [midIndex + 1] : (Data [midIndex] + Data [midIndex + 1]) / 2;

			return median;
		}

		protected override IList<double> CalcModes ()
		{
			var groups = Data.GroupBy (item => item);
			var modes = (from _mode in groups
					where _mode.Count () == groups.Max (grouped => grouped.Count ())
						select _mode.First ()).ToList ();

			return modes;
		}
	}
}