namespace Mx.Ipn.Esime.Statistics.Core.Base
{
    using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Core.Resources;

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
			if (!Inquirer.Answers.ContainsKey (TaskNames.Mean)) {
				Inquirer.Answers.Add (TaskNames.Mean, CalcMean ());
			}

			return Inquirer.Answers [TaskNames.Mean];
		}
		
		public double GetMedian ()
		{
			if (!Inquirer.Answers.ContainsKey (TaskNames.Median)) {
				Inquirer.Answers.Add (TaskNames.Median, CalcMedian ());
			}

			return Inquirer.Answers [TaskNames.Median];
		}
		
		public IList<double> GetModes ()
		{
			if (!Inquirer.Answers.ContainsKey (TaskNames.Modes)) {
				Inquirer.Answers.Add (TaskNames.Modes, CalcModes ());
			}

			return Inquirer.Answers [TaskNames.Modes];
		}

		protected abstract double CalcMean ();

		protected abstract double CalcMedian ();

		protected abstract IList<double> CalcModes ();
	}
}