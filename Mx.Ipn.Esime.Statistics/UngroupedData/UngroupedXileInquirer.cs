namespace Mx.Ipn.Esime.Statistics.UngroupedData
{
	using System;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Libs;

	public class UngroupedXileInquirer:InquirerBase,IXileInquirer
	{
		public UngroupedXileInquirer (IList<double> rawData):base(rawData)
		{			
		}

		public UngroupedXileInquirer (InquirerBase inquirer):base(inquirer)
		{
		}

		public double GetDecile (int nTh)
		{
			var xileInfo = AssertValidXile (nTh, Xiles.Decile);
			var nThDecile = GetXile (xileInfo);

			return nThDecile;
		}

		public double GetPercentile (int nTh)
		{
			var xileInfo = AssertValidXile (nTh, Xiles.Percentile);
			var nThPercentile = GetXile (xileInfo);
			
			return nThPercentile;
		}

		public double GetQuartile (int nTh)
		{
			var xileInfo = AssertValidXile (nTh, Xiles.Quartile);
			var nThQuartile = GetXile (xileInfo);
			
			return nThQuartile;
		}

		private double GetXile (XileInfo xileInfo)
		{
			double xileResult = Double.MinValue;
			if (!Inquirer.Answers.ContainsKey (xileInfo.ToString ())) {
				var lx = Inquirer.Data.Count * xileInfo.NthXile / (double)xileInfo.Xile;
				var li = (int)Math.Floor (lx - 0.5);
				var ls = (int)Math.Floor (lx + 0.5);
				if (ls == Inquirer.Data.Count) {
					ls = li;
				}
				
				var iPortion = li + 1 - (lx - 0.5);
				var sPortion = 1 - iPortion;
				
				xileResult = iPortion * Inquirer.Data [li] + sPortion * Inquirer.Data [ls];

				Inquirer.Answers.Add (xileInfo.ToString (), xileResult);
			} else {
				xileResult = Inquirer.Answers [xileInfo.ToString ()];
			}

			return xileResult;
		}

		private XileInfo AssertValidXile (int nTh, Xiles xile)
		{
			if (nTh < 1 || nTh > (int)xile) {
				var xileName = Enum.GetName (typeof(Xiles), xile);
				throw new StatisticsException (String.Format ("Invalid {0}", xileName), new IndexOutOfRangeException (String.Format ("{0} {0}", xileName, nTh)));
			}

			return new XileInfo{Xile=xile, NthXile=nTh};
		}

		private class XileInfo
		{
			public Xiles Xile {
				get;
				set;
			}

			public int NthXile {
				get;
				set;
			}

			public override string ToString ()
			{
				return string.Format ("get({0},{1})", Xile, NthXile);
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