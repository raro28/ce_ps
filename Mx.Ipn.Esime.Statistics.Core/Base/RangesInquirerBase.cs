namespace Mx.Ipn.Esime.Statistics.Core.Base
{	
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Core.Resources;

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
			if (!Inquirer.Answers.ContainsKey (TaskNames.DataRange)) {
				Inquirer.Answers.Add (TaskNames.DataRange, CalcDataRange ());
			}

			return Inquirer.Answers [TaskNames.DataRange];
		}
		
		public double GetInterQuartileRange ()
		{
			if (!Inquirer.Answers.ContainsKey (TaskNames.QuartileRange)) {
				Inquirer.Answers.Add (TaskNames.QuartileRange, Inquirer.GetQuartile (3) - Inquirer.GetQuartile (1));
			}
			
			return Inquirer.Answers [TaskNames.QuartileRange];
		}
		
		public double GetInterDecileRange ()
		{
			if (!Inquirer.Answers.ContainsKey (TaskNames.DecileRange)) {
				Inquirer.Answers.Add (TaskNames.DecileRange, Inquirer.GetDecile (9) - Inquirer.GetDecile (1));
			}
			
			return Inquirer.Answers [TaskNames.DecileRange];
		}
		
		public double GetInterPercentileRange ()
		{
			if (!Inquirer.Answers.ContainsKey (TaskNames.PercentileRange)) {
				Inquirer.Answers.Add (TaskNames.PercentileRange, Inquirer.GetPercentile (90) - Inquirer.GetPercentile (10));
			}
			
			return Inquirer.Answers [TaskNames.PercentileRange];
		}

		protected abstract double CalcDataRange ();
	}
}