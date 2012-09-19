namespace Mx.Ipn.Esime.Statistics.GroupedData
{
	using System;
	using System.Dynamic;
	using System.Linq;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Core;
	using Mx.Ipn.Esime.Statistics.Core.Base;

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

	public class DataDistributionFrequencyInquirer:InquirerBase,IDataDistributionFrequencyInquirer
	{
		public DataDistributionFrequencyInquirer (InquirerBase inquirer):base(inquirer)
		{
			InitProperties ();
		}

		public DataDistributionFrequencyInquirer (List<double> rawData):base(rawData)
		{
			InitProperties ();
		}

		public IEnumerable<dynamic> GetTable ()
		{
			if (!Inquirer.Answers.ContainsKey ("add(table,CI)")) {
				AddClassIntervals ();
			}

			return Inquirer.Answers ["add(table,CI)"];
		}

		public IEnumerable<Interval> GetClassIntervals ()
		{
			AddClassIntervals ();
			var frequencyTable = GetTable ();

			foreach (var item in frequencyTable) {
				yield return item.ClassInterval;
			}
		}

		public IEnumerable<int> GetFrequencies ()
		{
			AddFrequencies ();
			var frequencyTable = GetTable ();

			foreach (var item in frequencyTable) {
				yield return item.Frequency;
			}
		}

		public IEnumerable<int> GetAcumulatedFrequencies ()
		{
			AddAcumulatedFrequencies ();
			var frequencyTable = GetTable ();

			foreach (var item in frequencyTable) {
				yield return item.AcumulatedFrequency;
			}
		}

		public IEnumerable<double> GetRelativeFrequencies ()
		{
			AddRelativeFrequencies ();
			var frequencyTable = GetTable ();

			foreach (var item in frequencyTable) {
				yield return item.RelativeFrequency;
			}
		}

		public IEnumerable<double> GetAcumulatedRelativeFrequencies ()
		{
			AddAcumulatedRelativeFrequencies ();
			var frequencyTable = GetTable ();

			foreach (var item in frequencyTable) {
				yield return item.AcumulatedRelativeFrequency;
			}			
		}

		public IEnumerable<double> GetClassMarks ()
		{
			AddClassMarks ();
			var frequencyTable = GetTable ();

			foreach (var item in frequencyTable) {
				yield return item.ClassMark;
			}				
		}

		public IEnumerable<Interval> GetRealClassIntervals ()
		{
			AddRealClassIntervals ();
			var frequencyTable = GetTable ();
			
			foreach (var item in frequencyTable) {
				yield return item.RealInterval;
			}		
		}

		public IEnumerable<double> GetFrequenciesTimesClassMarks ()
		{
			AddFrequenciesTimesClassMarks ();
			var frequencyTable = GetTable ();
			
			foreach (var item in frequencyTable) {
				yield return item.fX;
			}		
		}

		public void AddClassIntervals ()
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
		}

		public void AddFrequencies ()
		{
			if (!Inquirer.Answers.ContainsKey ("add(table,f)")) {
				AddClassIntervals ();
				var frequencyTable = GetTable ();
				Inquirer.Answers.Add ("add(table,f)", frequencyTable);
				//FIXME cast of dynamic object to IEnumerable<double>
				var data = ((IEnumerable<double>)Inquirer.Data);
				foreach (var tableItem in frequencyTable) {
					var frequency = data.Count (item => item >= tableItem.ClassInterval.From && item <= tableItem.ClassInterval.To);
					tableItem.Frequency = frequency;
				}
			}
		}

		public void AddAcumulatedFrequencies ()
		{
			if (!Inquirer.Answers.ContainsKey ("add(table,F)")) {
				AddFrequencies ();
				var frequencyTable = GetTable ();
				Inquirer.Answers.Add ("add(table,F)", frequencyTable);
				var lastFrequency = 0;
				foreach (var item in frequencyTable) {
					item.AcumulatedFrequency = item.Frequency + lastFrequency;
					lastFrequency = item.AcumulatedFrequency;
				}
			}
		}

		public void AddRelativeFrequencies ()
		{
			if (!Inquirer.Answers.ContainsKey ("add(table,fr)")) {
				AddFrequencies ();
				var frequencyTable = GetTable ();
				Inquirer.Answers.Add ("add(table,fr)", frequencyTable);
				foreach (var item in frequencyTable) {
					item.RelativeFrequency = (double)item.Frequency / Inquirer.Data.Count;
				}
			}
		}

		public void AddAcumulatedRelativeFrequencies ()
		{
			if (!Inquirer.Answers.ContainsKey ("add(table,Fr)")) {
				AddRelativeFrequencies ();
				var frequencyTable = GetTable ();
				Inquirer.Answers.Add ("add(table,Fr)", frequencyTable);
				var lastRelativeFrequency = 0.0;
				foreach (var item in frequencyTable) {
					item.AcumulatedRelativeFrequency = item.RelativeFrequency + lastRelativeFrequency;
					lastRelativeFrequency = item.AcumulatedRelativeFrequency;
				}
			}
		}

		public void  AddClassMarks ()
		{
			if (!Inquirer.Answers.ContainsKey ("add(table,X)")) {
				AddClassIntervals ();
				var frequencyTable = GetTable ();
				Inquirer.Answers.Add ("add(table,X)", frequencyTable);
				foreach (var item in frequencyTable) {
					var classMark = (item.ClassInterval.From + item.ClassInterval.To) / 2;
					item.ClassMark = classMark;
				}
			}
		}

		public void  AddRealClassIntervals ()
		{
			if (!Inquirer.Answers.ContainsKey ("add(table,RI)")) {
				AddClassIntervals ();
				var frequencyTable = GetTable ();
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
		}

		public void  AddFrequenciesTimesClassMarks ()
		{
			if (!Inquirer.Answers.ContainsKey ("add(table,fX)")) {
				AddFrequencies ();
				AddClassMarks ();
				var frequencyTable = GetTable ();
				Inquirer.Answers.Add ("add(table,fX)", frequencyTable);
				foreach (var item in frequencyTable) {
					item.fX = item.Frequency * item.ClassMark;
				}
			}
		}

		private void InitProperties ()
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