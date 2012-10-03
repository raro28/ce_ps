namespace Mx.Ipn.Esime.Statistics.GroupedData
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Core;
	using Mx.Ipn.Esime.Statistics.Core.Base;
	using Mx.Ipn.Esime.Statistics.Core.Resources;

	public class GroupedDispersionInquirer:DispersionInquirerBase
	{
		public GroupedDispersionInquirer (IEnumerable<double> rawData):base(rawData)
		{			
			DistributionInquirer = new DataDistributionFrequencyInquirer (rawData);
			XileInquirer = new GroupedXileInquirer (rawData);
			CentralTendecyInquirer = new GroupedCentralTendecyInquirer (rawData);
		}

		private DataDistributionFrequencyInquirer DistributionInquirer {
			get;
			set;
		}
		
		public void AddMeanDifference (int power)
		{		
			if (power < 1 || power > 4) {
				throw new StatisticsException (String.Format (ExceptionMessages.Invalid_Power_Format, power));
			}

			var keyProperty = String.Format (TaskNames.MeanDiff_Property_Format, power);
			var keyDifference = String.Format (TaskNames.MeanDifference_Format, keyProperty);
			if (!Answers.ContainsKey (keyDifference)) {
				DistributionInquirer.AddClassMarks ();
				DistributionInquirer.AddFrequencies ();
				var frequencyTable = DistributionInquirer.GetTable ();
				
				Answers.Add (keyDifference, TaskNames.DispersionTable);
				var mean = CentralTendecyInquirer.GetMean ();
				foreach (var item in frequencyTable) {
					var difference = power != 1 ? item.ClassMark - mean : Math.Abs (item.ClassMark - mean);
					((IDictionary<String,Object>)item).Add (keyProperty, item.Frequency * Math.Pow (difference, power));
				}
			}
		}

		protected override double CalcAbsoluteDeviation ()
		{
			var mad = MeanDifferenceSum (1) / Data.Count;

			return mad;
		}

		protected override double CalcVariance ()
		{
			var variance = MeanDifferenceSum (2) / (Data.Count - 1);
			
			return variance;
		}

		protected override double CalcMomentum (int nMomentum)
		{
			var momentum = MeanDifferenceSum (nMomentum) / Data.Count;
			
			return momentum;
		}

		protected override double CalcDataRange ()
		{
			DistributionInquirer.AddClassIntervals ();
			var table = DistributionInquirer.GetTable ().ToList ();
			var range = table [0].ClassInterval.To - table [table.Count - 1].ClassInterval.From;
			
			return range;
		}

		private double MeanDifferenceSum (int power)
		{
			AddMeanDifference (power);
			double sum = 0;
			var table = DistributionInquirer.GetTable ();
			foreach (var item in table) {
				sum += ((IDictionary<String, dynamic>)item) [String.Format (TaskNames.MeanDiff_Property_Format, power)];
			}

			return sum;
		}
	}
}