namespace Mx.Ipn.Esime.Statistics.Libs
{
	using System;
	using System.Collections.Generic;

	public abstract class DispersionInquirerBase:InquirerBase,IDispersionInquirer
	{
		public DispersionInquirerBase (List<double> rawData):base(rawData)
		{
		}
		
		public DispersionInquirerBase (InquirerBase inquirer):base(inquirer)
		{
		}

		public double GetAbsoluteDeviation ()
		{
			if (!Inquirer.Answers.ContainsKey ("get(mad)")) {								
				Inquirer.Answers.Add ("get(mad)", CalcAbsoluteDeviation ());
			}
			
			return Inquirer.Answers ["get(mad)"];
		}
		
		public double GetVariance ()
		{
			if (!Inquirer.Answers.ContainsKey ("get(ssquare)")) {
				Inquirer.Answers.Add ("get(ssquare)", CalcVariance ());
			}
			
			return Inquirer.Answers ["get(ssquare)"];
		}
		
		public double GetStandarDeviation ()
		{
			if (!Inquirer.Answers.ContainsKey ("get(s)")) {
				Inquirer.Answers.Add ("get(s)", Math.Sqrt (GetVariance ()));
			}
			
			return Inquirer.Answers ["get(s)"];
		}
		
		public double GetCoefficientOfVariation ()
		{
			if (!Inquirer.Answers.ContainsKey ("get(cv)")) {
				var strDev = GetStandarDeviation ();
				var mean = Inquirer.GetMean ();

				var cov = strDev / mean;

				Inquirer.Answers.Add ("get(cv)", cov);
			}
			
			return Inquirer.Answers ["get(cv)"];
		}
		
		public double GetCoefficientOfSymmetry ()
		{
			if (!Inquirer.Answers.ContainsKey ("get(symmetry)")) {
				var m3 = GetMomentum (3);
				var m2 = GetMomentum (2);
				
				Inquirer.Answers.Add ("get(symmetry)", m3 / Math.Pow (m2, 1.5));
			}
			
			return Inquirer.Answers ["get(symmetry)"];
		}
		
		public double GetCoefficientOfKourtosis ()
		{
			if (!Inquirer.Answers.ContainsKey ("get(kourtosis)")) {
				var m4 = GetMomentum (4);
				var m2 = GetMomentum (2);
				
				Inquirer.Answers.Add ("get(kourtosis)", m4 / Math.Pow (m2, 2));
			}
			
			return Inquirer.Answers ["get(kourtosis)"];
		}
		
		private double GetMomentum (int nMomentum)
		{
			var keyMomentum = String.Format ("get(momentum,{0})", nMomentum);
			if (!Inquirer.Answers.ContainsKey (keyMomentum)) {
				Inquirer.Answers.Add (keyMomentum, CalcMomentum (nMomentum));
			}
			
			return Inquirer.Answers [keyMomentum];
		}

		protected abstract double CalcAbsoluteDeviation ();

		protected abstract double CalcVariance ();

		protected abstract double CalcMomentum (int nMomentum);
	}
}

