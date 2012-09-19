namespace Mx.Ipn.Esime.Statistics.GroupedData
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Core;
	using Mx.Ipn.Esime.Statistics.Core.Base;

	public class GroupedDispersionInquirer:DispersionInquirerBase
	{
		public GroupedDispersionInquirer (List<double> rawData):base(rawData)
		{			
			var distribution = new DataDistributionFrequencyInquirer (this);
			var xiles = new GroupedXileInquirer (distribution);
			var ranges = new GroupedRangesInquirer (xiles);

			Inquirer = new GroupedCentralTendecyInquirer (ranges);
		}

		public IEnumerable<double> GetMeanDifference (int power)
		{
			var frequencyTable = AddMeanDifference (power);
			
			foreach (var item in frequencyTable) {
				//FIXME better way to acces dynamic properties of an ExpandoObject
				yield return (double)((IDictionary<String,Object>)item) [String.Format ("fMeanDiffE{0}", power)];
			}
		}
		
		public IEnumerable<dynamic> AddMeanDifference (int power)
		{		
			if (power < 1 || power > 4) {
				throw new StatisticsException (String.Format ("Invalid power:{0}", power));
			}

			var keyProperty = String.Format ("fMeanDiffE{0}", power);
			var keyDifference = String.Format ("add(table,{0})", keyProperty);
			if (!Inquirer.Answers.ContainsKey (keyDifference)) {
				var mean = Inquirer.GetMean ();
				Inquirer.AddClassMarks ();
				Inquirer.AddFrequencies ();
				var frequencyTable = Inquirer.GetTable ();
				
				Inquirer.Answers.Add (keyDifference, frequencyTable);
				foreach (var item in frequencyTable) {
					var difference = power == 1 ? item.ClassMark - mean : Math.Abs (item.ClassMark - mean);
					//FIXME better way to acces dynamic properties of an ExpandoObject
					((IDictionary<String,Object>)item).Add (keyProperty, item.Frequency * Math.Pow (difference, power));
				}
			}
			
			return Inquirer.Answers [keyDifference];
		}

		protected override double CalcAbsoluteDeviation ()
		{
			var mad = GetMeanDifference (1).Sum () / Inquirer.Data.Count;

			return mad;
		}

		protected override double CalcVariance ()
		{
			var variance = GetMeanDifference (2).Sum () / (Inquirer.Data.Count - 1);
			
			return variance;
		}

		protected override double CalcMomentum (int nMomentum)
		{
			var momentum = GetMeanDifference (nMomentum).Sum () / Inquirer.Data.Count;
			
			return momentum;
		}
	}
}