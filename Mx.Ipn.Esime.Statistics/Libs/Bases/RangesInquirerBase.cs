namespace Mx.Ipn.Esime.Statistics.Libs
{	
	using System.Collections.Generic;

	public abstract class RangesInquirerBase:InquirerBase,IRangesInquirer
	{
		public RangesInquirerBase (List<double> rawData):base(rawData)
		{	
		}
		
		public RangesInquirerBase (InquirerBase inquirer):base(inquirer)
		{
		}

		public double GetDataRange ()
		{
			if (!Inquirer.Answers.ContainsKey ("get(range)")) {
				Inquirer.Answers.Add ("get(range)", CalcRange ());
			}
			
			return Inquirer.Answers ["get(range)"];
		}
		
		public double GetInterquartileRange ()
		{
			if (!Inquirer.Answers.ContainsKey ("get(qrange)")) {
				Inquirer.Answers.Add ("get(qrange)", Inquirer.GetQuartile (3) - Inquirer.GetQuartile (1));
			}
			
			return Inquirer.Answers ["get(qrange)"];
		}
		
		public double GetInterdecileRange ()
		{
			if (!Inquirer.Answers.ContainsKey ("get(drange)")) {
				Inquirer.Answers.Add ("get(drange)", Inquirer.GetDecile (9) - Inquirer.GetDecile (1));
			}
			
			return Inquirer.Answers ["get(drange)"];
		}
		
		public double GetInterpercentileRange ()
		{
			if (!Inquirer.Answers.ContainsKey ("get(prange)")) {
				Inquirer.Answers.Add ("get(prange)", Inquirer.GetPercentile (90) - Inquirer.GetPercentile (10));
			}
			
			return Inquirer.Answers ["get(prange)"];
		}

		protected abstract double CalcRange ();
	}
}

