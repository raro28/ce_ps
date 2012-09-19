namespace Mx.Ipn.Esime.Statistics.UngroupedData.Tests
{
	using NUnit.Framework;
	using Mx.Ipn.Esime.Statistics.UngroupedData;
	using Mx.Ipn.Esime.Statistics.BaseData.Tests;

	[TestFixture]
	public class UngroupedXileInquirer_Tests:XileInquirerBase_Tests<UngroupedXileInquirer,UngroupedHelperMethods<UngroupedXileInquirer>>
	{
		public UngroupedXileInquirer_Tests ():base(()=>{return new UngroupedXileInquirer (rawData: null);})
		{
		}
	}
}