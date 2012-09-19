namespace Mx.Ipn.Esime.Statistics.UngroupedData
{
	using System.Linq;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Core.Base;
	
	public class UngroupedCentralTendecyInquirer:CentralTendecyInquirerBase
	{
		public UngroupedCentralTendecyInquirer (List<double> rawData):base(rawData)
		{	
			var xiles = new UngroupedXileInquirer (this);
			
			Inquirer = new UngroupedRangesInquirer (xiles);
		}

		public UngroupedCentralTendecyInquirer (InquirerBase inquirer):base(inquirer)
		{
		}

		protected override double CalcMean ()
		{
			//TODO:fixme cast of dynamic object to IEnumerable<double>
			var mean = ((IEnumerable<double>)Inquirer.Data).Sum () / Inquirer.Data.Count;

			return mean;
		}

		protected override double CalcMedian ()
		{
			var midIndex = (Inquirer.Data.Count / 2) - 1;
			var median = Inquirer.Data.Count % 2 != 0 ? Inquirer.Data [midIndex + 1] : (Inquirer.Data [midIndex] + Inquirer.Data [midIndex + 1]) / 2;

			return median;
		}

		protected override IList<double> CalcModes ()
		{
			//TODO:fixme cast of dynamic object to IEnumerable<double>
			var groups = ((IEnumerable<double>)Inquirer.Data).GroupBy (data => data);
			var modes = (from _mode in groups
					where _mode.Count () == groups.Max (grouped => grouped.Count ())
						select _mode.First ()).ToList ();

			return modes;
		}
	}
}