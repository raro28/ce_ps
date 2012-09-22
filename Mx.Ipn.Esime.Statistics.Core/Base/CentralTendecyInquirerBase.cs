namespace Mx.Ipn.Esime.Statistics.Core.Base
{
	using System.Collections.Generic;

	public abstract class CentralTendecyInquirerBase:InquirerBase,ICentralTendencyInquirer
	{
		public CentralTendecyInquirerBase (List<double> rawData):base(rawData)
		{
		}
		
		public CentralTendecyInquirerBase (InquirerBase inquirer):base(inquirer)
		{
		}

		public double GetMean ()
		{
			if (!Inquirer.Answers.ContainsKey ("get(mean)")) {			
				Inquirer.Answers.Add ("get(mean)", CalcMean ());
			}
			
			return Inquirer.Answers ["get(mean)"];
		}
		
		public double GetMedian ()
		{
			if (!Inquirer.Answers.ContainsKey ("get(median)")) {
				Inquirer.Answers.Add ("get(median)", CalcMedian ());
			}
			
			return Inquirer.Answers ["get(median)"];
		}
		
		public IList<double> GetModes ()
		{
			if (!Inquirer.Answers.ContainsKey ("get(modes)")) {							
				Inquirer.Answers.Add ("get(modes)", CalcModes ());
			}
			
			return Inquirer.Answers ["get(modes)"];
		}

		protected abstract double CalcMean ();

		protected abstract double CalcMedian ();

		protected abstract IList<double> CalcModes ();
	}
}

