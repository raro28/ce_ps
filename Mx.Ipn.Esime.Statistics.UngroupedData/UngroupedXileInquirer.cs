namespace Mx.Ipn.Esime.Statistics.UngroupedData
{
	using System;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Core.Base;

	public class UngroupedXileInquirer:XileInquirerBase
	{
		public UngroupedXileInquirer (List<double> rawData):base(rawData)
		{			
		}

		public UngroupedXileInquirer (InquirerBase inquirer):base(inquirer)
		{
		}

		protected override double CalcXile (double lx)
		{
			var li = (int)Math.Floor (lx - 0.5);
			var ls = (int)Math.Floor (lx + 0.5);
			if (ls == Inquirer.Data.Count) {
				ls = li;
			}
			
			var iPortion = li + 1 - (lx - 0.5);
			var sPortion = 1 - iPortion;
			
			var xileResult = iPortion * Inquirer.Data [li] + sPortion * Inquirer.Data [ls];
			return xileResult;
		}
	}
}