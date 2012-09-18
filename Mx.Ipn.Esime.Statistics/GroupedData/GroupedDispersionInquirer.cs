namespace Mx.Ipn.Esime.Statistics.GroupedData
{
	using System;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Libs;

	public class GroupedDispersionInquirer:InquirerBase,IDispersionInquirer
	{
		public GroupedDispersionInquirer (List<double> rawData):base(rawData)
		{			
			var distribution = new DataDistributionFrequencyInquirer(this);
			var xiles = new GroupedXileInquirer(distribution);
			var ranges = new GroupedRangesInquirer(xiles);

			Inquirer = new GroupedCentralTendecyInquirer (ranges);
		}

		public double GetAbsoluteDeviation ()
		{
			throw new System.NotImplementedException ();
		}

		public double GetVariance ()
		{
			throw new System.NotImplementedException ();
		}

		public double GetStandarDeviation ()
		{
			throw new System.NotImplementedException ();
		}

		public double GetCoefficientOfVariation ()
		{
			throw new System.NotImplementedException ();
		}

		public double GetCoefficientOfSymmetry ()
		{
			throw new System.NotImplementedException ();
		}

		public double GetCoefficientOfKourtosis ()
		{
			throw new System.NotImplementedException ();
		}

		public IEnumerable<double> GetMeanDifference (int nthDifference)
		{
			var frequencyTable = AddMeanDifference (nthDifference);
			
			foreach (var item in frequencyTable) {
				//TODO:WTF!
				yield return (double)((IDictionary<String,Object>)item) ["fMeanDiffE" + nthDifference];
			}
		}
		
		public IEnumerable<dynamic> AddMeanDifference (int power)
		{		
			var keyProperty = String.Format ("fMeanDiffE{0}", power);
			var keyDifference = String.Format ("add(table,{0})", keyProperty);
			if (!Inquirer.Answers.ContainsKey (keyDifference)) {
				var mean = Inquirer.GetMean ();
				var frequencyTable = Inquirer.AddClassMarks ();
				Inquirer.AddFrequencies ();
				
				Inquirer.Answers.Add (keyDifference, frequencyTable);
				foreach (var item in frequencyTable) {
					((IDictionary<String,Object>)item).Add (keyProperty, item.Frequency * Math.Pow (item.ClassMark - mean, power));
				}
			}
			
			return Inquirer.Answers [keyDifference];
		}
	}
}