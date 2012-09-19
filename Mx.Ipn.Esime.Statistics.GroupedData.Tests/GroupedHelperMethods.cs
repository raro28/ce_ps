namespace Mx.Ipn.Esime.Statistics.GroupedData.Tests
{
	using System;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Core.Base;
	using Mx.Ipn.Esime.Statistics.BaseData.Tests;
	
	public class GroupedHelperMethods<T>:HelperMethodsBase<T> where T:InquirerBase
	{
		public override double CalcNthXile (IList<double> data, int xile, int nTh)
		{
			throw new NotImplementedException ();
		}		

		public override double SampleMean (List<double> sortedData)
		{
			throw new NotImplementedException ();
		}
	}
}