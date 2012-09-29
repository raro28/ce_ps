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
			if (!Properties["Answers"].ContainsKey (TaskNames.Mean)) {
				Properties["Answers"].Add (TaskNames.Mean, CalcMean ());
			}

			return Properties["Answers"] [TaskNames.Mean];
		}
		
		public double GetMedian ()
		{
			if (!Properties["Answers"].ContainsKey (TaskNames.Median)) {
				Properties["Answers"].Add (TaskNames.Median, CalcMedian ());
			}

			return Properties["Answers"] [TaskNames.Median];
		}
		
		public IList<double> GetModes ()
		{
			if (!Properties["Answers"].ContainsKey (TaskNames.Modes)) {
				Properties["Answers"].Add (TaskNames.Modes, CalcModes ());
			}

			return Properties["Answers"] [TaskNames.Modes];
		}

		protected abstract double CalcMean ();

		protected abstract double CalcMedian ();

		protected abstract IList<double> CalcModes ();
	}
}