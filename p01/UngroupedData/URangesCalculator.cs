namespace Mx.Ipn.Esime.Statistics.UngroupedData
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using Mx.Ipn.Esime.Statistics.Libs;

	public class URangesCalculator:IRangesCalculator
	{
		public ReadOnlyCollection<double> Data {
			get;
			set;
		}

		private double? range;

		private double? qRange;

		private double? dRange;

		private double? pRange;

		public URangesCalculator (List<double> data)
		{
			if (data == null) 
			{

				throw new StatisticsException ("Null data set.",new ArgumentNullException("data"));
			}

			if(data.Count==0)
			{
				throw new StatisticsException ("Empty data set.");
			}

			if(data.Count==1)
			{
				throw new StatisticsException ("Insuficient data.");
			}

			var cache = data.ToList();
			cache.Sort();
			Data=cache.AsReadOnly();
		}

		public double CalcDataRange ()
		{
			if(range==null){
				range=Data.Max()-Data.Min();
			}

			return (double)range;
		}		

		public double CalcInterquartileRange ()
		{
			if(qRange==null){
				qRange=CalcX(XOptions.Quartil,3)-CalcX(XOptions.Quartil,1);
			}
			
			return (double)qRange;
		}		

		public double CalcInterdecileRange ()
		{
			if(dRange==null){
				dRange=CalcX(XOptions.Decil,9)-CalcX(XOptions.Decil,1);
			}
			
			return (double)dRange;
		}		

		public double CalcInterpercentileRange ()
		{
			if(pRange==null){
				pRange=CalcX(XOptions.Percentil,90)-CalcX(XOptions.Percentil,10);
			}
			
			return (double)pRange;
		}

		private double CalcX(XOptions option, int number)
		{

			var lx = Data.Count*number/(double)option;
			var li=(int)Math.Floor(lx-0.5);
			var ls=(int)Math.Floor(lx+0.5);

			var iPortion=li+1-(lx-0.5);
			var sPortion=1-iPortion;

			var xRange=iPortion*Data[li]+sPortion*Data[ls];

			return xRange;
		}

		private enum XOptions
		{
			Quartil=4,
			Decil=10,
			Percentil=100
		}
	}
}
