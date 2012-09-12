namespace Mx.Ipn.Esime.Statistics.Libs.Inquiries
{
	using System;
	using System.Collections.Generic;

	public interface IInquirer
	{
		Dictionary<String,object> Asked {
			get;
		}
	}
}