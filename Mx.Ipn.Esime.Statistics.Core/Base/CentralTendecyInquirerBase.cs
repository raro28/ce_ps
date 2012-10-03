namespace Mx.Ipn.Esime.Statistics.Core.Base
{
    using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Core.Resources;

	public abstract class CentralTendecyInquirerBase:InquirerBase,ICentralTendencyInquirer
	{
		public CentralTendecyInquirerBase (IEnumerable<double> rawData):base(rawData)
		{
		}

		public double GetMean ()
		{
			if (!Answers.ContainsKey (TaskNames.Mean)) {
				Answers.Add (TaskNames.Mean, CalcMean ());
			}

			return Answers [TaskNames.Mean];
		}
		
		public double GetMedian ()
		{
			if (!Answers.ContainsKey (TaskNames.Median)) {
				Answers.Add (TaskNames.Median, CalcMedian ());
			}

			return Answers [TaskNames.Median];
		}
		
		public IList<double> GetModes ()
		{
			if (!Answers.ContainsKey (TaskNames.Modes)) {
				Answers.Add (TaskNames.Modes, CalcModes ());
			}

			return Answers [TaskNames.Modes];
		}

		protected abstract double CalcMean ();

		protected abstract double CalcMedian ();

		protected abstract IList<double> CalcModes ();
	}
}