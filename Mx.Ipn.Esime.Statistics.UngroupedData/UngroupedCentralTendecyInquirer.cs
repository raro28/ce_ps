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
			var mean = Enumerable.Sum (Properties ["Data"]) / Properties ["Data"].Count;

			return mean;
		}

		protected override double CalcMedian ()
		{
			var midIndex = (Properties ["Data"].Count / 2) - 1;
			var median = Properties ["Data"].Count % 2 != 0 ? Properties ["Data"] [midIndex + 1] : (Properties ["Data"] [midIndex] + Properties ["Data"] [midIndex + 1]) / 2;

			return median;
		}

		protected override IList<double> CalcModes ()
		{
			List<double> data = Enumerable.ToList (Properties ["Data"]);
			var groups = data.GroupBy (item => item);
			var modes = (from _mode in groups
					where _mode.Count () == groups.Max (grouped => grouped.Count ())
						select _mode.First ()).ToList ();

			return modes;
		}
	}
}