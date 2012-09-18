namespace Mx.Ipn.Esime.Statistics.Libs
{
	using System;
	using System.Collections.Generic;

	public class XileInfo
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
	
	public enum Xiles
	{
		Quartile=4,
		Decile=10,
		Percentile=100
	}

	public abstract class XileInquirerBase:InquirerBase,IXileInquirer
	{		

		public XileInquirerBase (InquirerBase inquirer):base(inquirer)
		{			
		}
		
		public XileInquirerBase (List<double> rawData):base(rawData)
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

		protected abstract double CalcXile (double lx);
			
		private XileInfo AssertValidXile (int nTh, Xiles xile)
		{
			if (nTh < 1 || nTh > (int)xile) {
				var xileName = Enum.GetName (typeof(Xiles), xile);
				throw new StatisticsException (String.Format ("Invalid {0}", xileName), new IndexOutOfRangeException (String.Format ("{0} {0}", xileName, nTh)));
			}
			
			return new XileInfo{Xile=xile, NthXile=nTh};
		}

		private double GetXile (XileInfo xileInfo)
		{
			double xileResult = Double.MinValue;
			if (!Inquirer.Answers.ContainsKey (xileInfo.ToString ())) {
				var lx = Inquirer.Data.Count * xileInfo.NthXile / (double)xileInfo.Xile;
				xileResult = CalcXile (lx);

				Inquirer.Answers.Add (xileInfo.ToString (), xileResult);
			} else {
				xileResult = Inquirer.Answers [xileInfo.ToString ()];
			}
			
			return xileResult;
		}
	}
}

