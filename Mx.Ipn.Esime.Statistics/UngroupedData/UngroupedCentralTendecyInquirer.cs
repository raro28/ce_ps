namespace Mx.Ipn.Esime.Statistics.UngroupedData
{
	using System.Linq;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Libs;
	
	public class UngroupedCentralTendecyInquirer:InquirerBase,ICentralTendencyInquirer
	{
		public UngroupedCentralTendecyInquirer (IList<double> rawData):base(rawData)
		{	
		}

		public UngroupedCentralTendecyInquirer (InquirerBase inquirer):base(inquirer)
		{
		}

		public double GetMean ()
		{
			if (!Inquirer.Answers.ContainsKey ("get(mean)")) {

				Inquirer.Answers.Add ("get(mean)", ((IEnumerable<double>)Inquirer.Data).Sum () / Inquirer.Data.Count);
			}
			
			return Inquirer.Answers ["get(mean)"];
		}

		public double GetMedian ()
		{
			if (!Inquirer.Answers.ContainsKey ("get(median)")) {
				var midIndex = (Inquirer.Data.Count / 2) - 1;
				Inquirer.Answers.Add ("get(median)", Inquirer.Data.Count % 2 != 0 ? Inquirer.Data [midIndex + 1] : (Inquirer.Data [midIndex] + Inquirer.Data [midIndex + 1]) / 2);
			}
			
			return Inquirer.Answers ["get(median)"];
		}

		public IList<double> GetMode ()
		{
			if (!Inquirer.Answers.ContainsKey ("get(mode)")) {
				var groups = ((IEnumerable<double>)Inquirer.Data).GroupBy (data => data);
				var modes = from _mode in groups
					where _mode.Count () == groups.Max (grouped => grouped.Count ())
					select _mode.First ();

				Inquirer.Answers.Add ("get(mode)", modes.ToList ());
			}
			
			return Inquirer.Answers ["get(mode)"];
		}
	}
}