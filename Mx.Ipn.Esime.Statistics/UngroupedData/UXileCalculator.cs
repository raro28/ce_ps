namespace Mx.Ipn.Esime.Statistics.UngroupedData
{
	using System;
	using System.Dynamic;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using Mx.Ipn.Esime.Statistics.Libs;

	public class UXileCalculator:InquirerBase,IXileCalculator
	{
		public UXileCalculator (IList<double> rawData):base(rawData)
		{			
		}

		public UXileCalculator (ReadOnlyCollection<double> sortedData, InquirerBase inquirer):base(sortedData, inquirer)
		{
		}

		public double GetDecile (int nTh)
		{
			AssertValidXile (nTh, Xiles.Decile);
			var nThDecile = GetNthXile (Xiles.Decile, nTh);

			return nThDecile;
		}

		public double GetPercentile (int nTh)
		{
			AssertValidXile (nTh, Xiles.Percentile);
			var nThPercentile = GetNthXile (Xiles.Percentile, nTh);
			
			return nThPercentile;
		}

		public double GetQuartile (int nTh)
		{
			AssertValidXile (nTh, Xiles.Quartile);
			var nThQuartile = GetNthXile (Xiles.Quartile, nTh);
			
			return nThQuartile;
		}

		private double GetNthXile (Xiles xile, int nTh)
		{
			
			var lx = Data.Count * nTh / (double)xile;
			var li = (int)Math.Floor (lx - 0.5);
			var ls = (int)Math.Floor (lx + 0.5);
			if (ls == Data.Count) {
				ls = li;
			}
			var iPortion = li + 1 - (lx - 0.5);
			var sPortion = 1 - iPortion;
			
			var xRange = iPortion * Data [li] + sPortion * Data [ls];
			
			return xRange;
		}

		private void AssertValidXile (int nTh, Xiles xile)
		{
			if (nTh < 1 || nTh > (int)xile) {
				var xileName = Enum.GetName (typeof(Xiles), xile);
				throw new StatisticsException (String.Format ("Invalid {0}", xileName), new IndexOutOfRangeException (String.Format ("{0} {0}", xileName, nTh)));
			}
		}
		
		private enum Xiles
		{
			Quartile=4,
			Decile=10,
			Percentile=100
		}
	}
}