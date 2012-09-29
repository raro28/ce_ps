namespace Mx.Ipn.Esime.Statistics.GroupedData
{
	using System;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Core;
	using Mx.Ipn.Esime.Statistics.Core.Base;
	using Mx.Ipn.Esime.Statistics.Core.Resources;

	public class GroupedDispersionInquirer:DispersionInquirerBase
	{
		public GroupedDispersionInquirer (List<double> rawData):base(rawData)
		{			
			var distribution = new DataDistributionFrequencyInquirer (this);
			Inquirer = new GroupedXileInquirer (distribution);
		}
		
		public void AddMeanDifference (int power, double mean)
		{		
			if (power < 1 || power > 4) {
				throw new StatisticsException (String.Format (ExceptionMessages.Invalid_Power_Format, power));
			}

			var keyProperty = String.Format (TaskNames.MeanDiff_Property_Format, power);
			var keyDifference = String.Format (TaskNames.MeanDifference_Format, keyProperty);
			if (!Properties["Answers"].ContainsKey (keyDifference)) {
				Inquirer.AddClassMarks ();
				Inquirer.AddFrequencies ();
				var frequencyTable = Inquirer.GetTable ();
				
				Properties["Answers"].Add (keyDifference, TaskNames.DispersionTable);
				foreach (var item in frequencyTable) {
					var difference = power != 1 ? item.ClassMark - mean : Math.Abs (item.ClassMark - mean);
					((IDictionary<String,Object>)item).Add (keyProperty, item.Frequency * Math.Pow (difference, power));
				}
			}
		}

		protected override double CalcAbsoluteDeviation (double mean)
		{
			var mad = MeanDifferenceSum (1, mean) / Properties["Data"].Count;

			return mad;
		}

		protected override double CalcVariance (double mean)
		{
			var variance = MeanDifferenceSum (2, mean) / (Properties["Data"].Count - 1);
			
			return variance;
		}

		protected override double CalcMomentum (int nMomentum, double mean)
		{
			var momentum = MeanDifferenceSum (nMomentum, mean) / Properties["Data"].Count;
			
			return momentum;
		}

		protected override double CalcDataRange ()
		{
			Inquirer.AddClassIntervals ();
			var table = Inquirer.GetTable ();
			var range = table [0].ClassInterval.To - table [table.Count - 1].ClassInterval.From;
			
			return range;
		}

		private double MeanDifferenceSum (int power, double mean)
		{
			AddMeanDifference (power, mean);
			double sum = 0;
			var table = Inquirer.GetTable ();
			foreach (var item in table) {
				sum += ((IDictionary<String, dynamic>)item) [String.Format (TaskNames.MeanDiff_Property_Format, power)];
			}

			return sum;
		}
	}
}