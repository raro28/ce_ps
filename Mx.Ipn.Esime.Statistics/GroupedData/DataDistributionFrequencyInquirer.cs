namespace Mx.Ipn.Esime.Statistics.GroupedData
{
	using System;
	using System.Dynamic;
	using System.Linq;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Libs;

	public class Interval
	{
		public double From {
			get;
			set;
		}
		
		public double To {
			get;
			set;
		}
		
		public override string ToString ()
		{
			return string.Format ("[{0},{1}]", From, To);
		}
	}

	public class DataDistributionFrequencyInquirer:InquirerBase,IDistributionChartInquirer
	{
		public DataDistributionFrequencyInquirer (InquirerBase inquirer):base(inquirer)
		{
			InitProperties ();
		}

		public DataDistributionFrequencyInquirer (List<double> rawData):base(rawData)
		{
			InitProperties ();
		}

		public IEnumerable<Interval> GetClassIntervalsTable ()
		{
			var frequencyTable = AddClassIntervals ();

			foreach (var item in frequencyTable) {
				yield return item.ClassInterval;
			}
		}

		public IEnumerable<int> GetFrequencyTable ()
		{
			var frequencyTable = AddFrequencies ();

			foreach (var item in frequencyTable) {
				yield return item.Frequency;
			}
		}

		public IEnumerable<int> GetAcumulatedFrequencyTable ()
		{
			var frequencyTable = AddAcumulatedFrequencies ();

			foreach (var item in frequencyTable) {
				yield return item.AcumulatedFrequency;
			}
		}

		public IEnumerable<double> GetRelativeFrequencyTable ()
		{
			var frequencyTable = AddRelativeFrequencies ();

			foreach (var item in frequencyTable) {
				yield return item.RelativeFrequency;
			}
		}

		public IEnumerable<double> GetAcumulatedRelativeFrequencyTable ()
		{
			var frequencyTable = AddAcumulatedRelativeFrequencies ();

			foreach (var item in frequencyTable) {
				yield return item.AcumulatedRelativeFrequency;
			}			
		}

		public IEnumerable<double> GetClassMarksTable ()
		{
			var frequencyTable = AddClassMarks ();

			foreach (var item in frequencyTable) {
				yield return item.ClassMark;
			}				
		}

		public IEnumerable<Interval> GetRealClassIntervalsTable ()
		{
			var frequencyTable = AddRealClassIntervals ();
			
			foreach (var item in frequencyTable) {
				yield return item.RealInterval;
			}		
		}

		public IEnumerable<double> GetFrequenciesTimesClassMarksTable ()
		{
			var frequencyTable = AddFrequenciesTimesClassMarks ();
			
			foreach (var item in frequencyTable) {
				yield return item.fX;
			}		
		}

		public IEnumerable<dynamic> AddClassIntervals ()
		{
			if (!Inquirer.Answers.ContainsKey ("add(table,CI)")) {
				var frequencyTable = new List<dynamic> (Inquirer.Groups);
				Inquirer.Answers.Add ("add(table,CI)", frequencyTable.AsReadOnly ());
				var inferiorClassLimit = Enumerable.Min (Inquirer.Data);
				var superiorClassLimit = inferiorClassLimit + Inquirer.Amplitude - Inquirer.DataPresicionValue;
				for (int i = 1; i <= Inquirer.Groups; i++) {
					var interval = new Interval {
						From = inferiorClassLimit,
						To = superiorClassLimit
					};

					dynamic distElement = new ExpandoObject ();
					distElement.ClassInterval = interval;
					frequencyTable.Add (distElement);
					inferiorClassLimit += Inquirer.Amplitude;
					superiorClassLimit += Inquirer.Amplitude;
				}
			}

			return Inquirer.Answers ["add(table,CI)"];
		}

		public IEnumerable<dynamic> AddFrequencies ()
		{
			if (!Inquirer.Answers.ContainsKey ("add(table,f)")) {
				var frequencyTable = AddClassIntervals ();
				Inquirer.Answers.Add ("add(table,f)", frequencyTable);
				//TODO: cast of dynamic object
				var data = ((IEnumerable<double>)Inquirer.Data);
				foreach (var tableItem in frequencyTable) {
					var frequency = data.Count (item => item >= tableItem.ClassInterval.From && item <= tableItem.ClassInterval.To);
					tableItem.Frequency = frequency;
				}
			}

			return Inquirer.Answers ["add(table,f)"];
		}

		public IEnumerable<dynamic> AddAcumulatedFrequencies ()
		{
			if (!Inquirer.Answers.ContainsKey ("add(table,F)")) {
				var frequencyTable = AddFrequencies ();
				Inquirer.Answers.Add ("add(table,F)", frequencyTable);
				var lastFrequency = 0;
				foreach (var item in frequencyTable) {
					item.AcumulatedFrequency = item.Frequency + lastFrequency;
					lastFrequency = item.AcumulatedFrequency;
				}
			}

			return Inquirer.Answers ["add(table,F)"];
		}

		public IEnumerable<dynamic> AddRelativeFrequencies ()
		{
			if (!Inquirer.Answers.ContainsKey ("add(table,fr)")) {
				var frequencyTable = AddFrequencies ();
				Inquirer.Answers.Add ("add(table,fr)", frequencyTable);
				foreach (var item in frequencyTable) {
					item.RelativeFrequency = (double)item.Frequency / Inquirer.Data.Count;
				}
			}

			return Inquirer.Answers ["add(table,fr)"];
		}

		public IEnumerable<dynamic> AddAcumulatedRelativeFrequencies ()
		{
			if (!Inquirer.Answers.ContainsKey ("add(table,Fr)")) {
				var frequencyTable = AddRelativeFrequencies ();
				Inquirer.Answers.Add ("add(table,Fr)", frequencyTable);
				var lastRelativeFrequency = 0.0;
				foreach (var item in frequencyTable) {
					item.AcumulatedRelativeFrequency = item.RelativeFrequency + lastRelativeFrequency;
					lastRelativeFrequency = item.AcumulatedRelativeFrequency;
				}
			}

			return Inquirer.Answers ["add(table,Fr)"];
		}

		public IEnumerable<dynamic>  AddClassMarks ()
		{
			if (!Inquirer.Answers.ContainsKey ("add(table,X)")) {
				var frequencyTable = AddClassIntervals ();
				Inquirer.Answers.Add ("add(table,X)", frequencyTable);
				foreach (var item in frequencyTable) {
					var classMark = (item.ClassInterval.From + item.ClassInterval.To) / 2;
					item.ClassMark = classMark;
				}
			}

			return Inquirer.Answers ["add(table,X)"];
		}

		public IEnumerable<dynamic>  AddRealClassIntervals ()
		{
			if (!Inquirer.Answers.ContainsKey ("add(table,RI)")) {
				var frequencyTable = AddClassIntervals ();
				Inquirer.Answers.Add ("add(table,RI)", frequencyTable);
				var midPresicion = Inquirer.DataPresicionValue / 2;
				foreach (var item in frequencyTable) {
					var realInterval = new Interval {
						From = item.ClassInterval.From - midPresicion,
						To = item.ClassInterval.To + midPresicion
					};

					item.RealInterval = realInterval;
				}
			}

			return Inquirer.Answers ["add(table,RI)"];
		}

		public IEnumerable<dynamic>  AddFrequenciesTimesClassMarks ()
		{
			if (!Inquirer.Answers.ContainsKey ("add(table,fX)")) {
				var frequencyTable = AddFrequencies ();
				AddClassMarks ();
				Inquirer.Answers.Add ("add(table,fX)", frequencyTable);
				foreach (var item in frequencyTable) {
					item.fX = item.Frequency * item.ClassMark;
				}
			}
			
			return Inquirer.Answers ["add(table,fX)"];
		}

		void InitProperties ()
		{
			Inquirer.Max = Enumerable.Max (Inquirer.Data);
			Inquirer.Min = Enumerable.Min (Inquirer.Data);
			Inquirer.Range = Inquirer.Max - Inquirer.Min;
			Inquirer.Groups = (int)Math.Round (Math.Sqrt (Inquirer.Data.Count));
			Inquirer.Amplitude = Math.Round (Inquirer.Range / Inquirer.Groups, Inquirer.DataPresicion);
			Inquirer.DataPresicionValue = (1 / Math.Pow (10, Inquirer.DataPresicion));
			if ((Inquirer.Min + Inquirer.Amplitude * Inquirer.Groups - Inquirer.DataPresicionValue) <= Inquirer.Max) {
				Inquirer.Amplitude += Inquirer.DataPresicionValue;
			}
		}
	}	
}