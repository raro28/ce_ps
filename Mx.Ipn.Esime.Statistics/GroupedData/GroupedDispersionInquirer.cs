namespace Mx.Ipn.Esime.Statistics.GroupedData
{
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using Mx.Ipn.Esime.Statistics.Libs;

	public class GroupedDispersionInquirer:InquirerBase,IDispersionInquirer
	{
		public GroupedDispersionInquirer (IList<double> rawData):base(rawData)
		{			
		}
		
		public GroupedDispersionInquirer (ReadOnlyCollection<double> sortedData, ICentralTendencyInquirer inquirer):base(sortedData,inquirer)
		{
		}

		public double GetAbsoluteDeviation ()
		{
			throw new System.NotImplementedException ();
		}

		public double GetVariance ()
		{
			throw new System.NotImplementedException ();
		}

		public double GetStandarDeviation ()
		{
			throw new System.NotImplementedException ();
		}

		public double GetCoefficientOfVariation ()
		{
			throw new System.NotImplementedException ();
		}

		public double GetCoefficientOfSymmetry ()
		{
			throw new System.NotImplementedException ();
		}

		public double GetCoefficientOfKourtosis ()
		{
			throw new System.NotImplementedException ();
		}
	}
}