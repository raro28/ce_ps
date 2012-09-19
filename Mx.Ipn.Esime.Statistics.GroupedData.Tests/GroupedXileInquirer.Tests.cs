namespace Mx.Ipn.Esime.Statistics.GroupedData.Tests
{
	using NUnit.Framework;
	using Mx.Ipn.Esime.Statistics.GroupedData;
	using Mx.Ipn.Esime.Statistics.BaseData.Tests;

	[TestFixture()]
	public class GroupedXileInquirer_Tests:XileInquirerBase_Tests<GroupedXileInquirer,GroupedHelperMethods<GroupedXileInquirer>>
	{
		public GroupedXileInquirer_Tests ():base(()=>{return new GroupedXileInquirer (rawData: null);})
		{
		}
	}
}