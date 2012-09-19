namespace Mx.Ipn.Esime.Statistics.UngroupedData.Tests
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using NUnit.Framework;
	using Mx.Ipn.Esime.Statistics.UngroupedData;
	
	[TestFixture()]
	public class UngroupedCentralTendecyInquirer_Tests:UngroupedInquirerBase_Tests<UngroupedCentralTendecyInquirer>
	{	
		public UngroupedCentralTendecyInquirer_Tests ():base(()=>{return new UngroupedCentralTendecyInquirer (rawData: null);})
		{
		}

		[TestCase(100)]
		public void Inquirer_Gets_Expected_Mean (int size)
		{
			List<double> sortedData;
			var calculator = Helper.NewInquirer (out sortedData, size);

			var expected = SampleMean (sortedData);
			var actual = calculator.GetMean ();
			Assert.AreEqual (expected, actual);
		}

		[TestCase(new double[]{1,2,3,2})]
		[TestCase(new double[]{6,1,2,3,2,4,5,6})]
		public void Inquirer_Gets_Expected_Mode (double[] dataArray)
		{
			var sortedData = dataArray.ToList ();
			var calculator = Helper.NewInquirer (ref sortedData);
			
			var <double> expected = SampleMode (sortedData);
			var actual = calculator.GetMode ();
			Assert.AreEqual (expected, actual);
		}
		
		[TestCase(4)]
		[TestCase(5)]
		public void Inquirer_Gets_Expected_Median (int size)
		{
			List<double> sortedData;
			var calculator = Helper.NewInquirer (out sortedData, size);
			
			var expected = SampleMedian (sortedData);
			var actual = calculator.GetMedian ();
			Assert.AreEqual (expected, actual);
		}

		protected double SampleMedian (IList<double> sortedData)
		{
			double result;
			int middleIndex = (sortedData.Count / 2) - 1;
			if ((sortedData.Count % 2) != 0) {
				result = sortedData [middleIndex + 1];
			} else {
				result = (sortedData [middleIndex] + sortedData [middleIndex + 1]) / 2;
			}

			return result;
		}

		protected List<double> SampleMode (IEnumerable<double> sortedData)
		{
			var groups = sortedData.GroupBy (data => data);
			var modes = (from _mode in groups
			             where _mode.Count () == groups.Max (grouped => grouped.Count ())
			             select _mode.First ()).ToList ();
			
			return modes;
		}
	}
}