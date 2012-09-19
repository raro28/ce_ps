namespace Mx.Ipn.Esime.Statistics.UngroupedData
{
	using System.Linq;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Core.Base;

	public class UngroupedRangesInquirer:RangesInquirerBase
	{
		public UngroupedRangesInquirer (List<double> rawData):base(rawData)
		{	
			Inquirer = new UngroupedXileInquirer (this);
		}

		public UngroupedRangesInquirer (InquirerBase inquirer):base(inquirer)
		{
		}

		protected override double CalcDataRange ()
		{
			return Enumerable.Max (Inquirer.Data) - Enumerable.Min (Inquirer.Data);
		}
	}
}