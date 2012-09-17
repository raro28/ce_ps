namespace Mx.Ipn.Esime.Statistics.GroupedData
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Libs;

	public class DataDistributionFrequencyInquirer:InquirerBase,IDistributionChartInquirer
	{
		public DataDistributionFrequencyInquirer (List<double> rawData):base(rawData)
		{			
			Inquirer.Range = Enumerable.Max (Inquirer.Data) - Enumerable.Min (Inquirer.Data);
			Inquirer.Groups = (int)Math.Round (Math.Sqrt (Inquirer.Data.Count), 0);
			Inquirer.Amplitude = Inquirer.Range / Inquirer.Groups;
		}
	}	
}