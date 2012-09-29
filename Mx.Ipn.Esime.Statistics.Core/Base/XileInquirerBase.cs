namespace Mx.Ipn.Esime.Statistics.Core.Base
{
	using System;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Core.Resources;

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
			return string.Format (TaskNames.XileFormat, Xile, NthXile);
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
				throw new StatisticsException (String.Format (ExceptionMessages.Invalid_Xile_Name_Format, xileName), new IndexOutOfRangeException (String.Format (ExceptionMessages.Invalid_Xile_Name_Number_Format, xileName, nTh)));
			}
			
			return new XileInfo{Xile=xile, NthXile=nTh};
		}

		private double GetXile (XileInfo xileInfo)
		{
			double xileResult;
			if (!Properties ["Answers"].ContainsKey (xileInfo.ToString ())) {
				var lx = Properties ["Data"].Count * xileInfo.NthXile / (double)xileInfo.Xile;
				xileResult = CalcXile (lx);

				Properties ["Answers"].Add (xileInfo.ToString (), xileResult);
			} else {
				xileResult = Properties ["Answers"] [xileInfo.ToString ()];
			}
			
			return xileResult;
		}
	}
}