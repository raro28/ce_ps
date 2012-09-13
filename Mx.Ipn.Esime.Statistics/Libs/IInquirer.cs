namespace Mx.Ipn.Esime.Statistics.Libs
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