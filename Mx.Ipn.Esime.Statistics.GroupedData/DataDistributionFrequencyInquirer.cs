namespace Mx.Ipn.Esime.Statistics.GroupedData
{
    using System;
	using System.Collections.Generic;
	using System.Dynamic;
	using System.Linq;
	using Mx.Ipn.Esime.Statistics.Core;
	using Mx.Ipn.Esime.Statistics.Core.Base;
	using Mx.Ipn.Esime.Statistics.Core.Resources;

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
			return string.Format (TaskNames.Interval_Format, From, To);
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
			if (!Inquirer.Answers.ContainsKey (TaskNames.ClassIntervals)) {
				AddClassIntervals ();
			}

			return Inquirer.Answers [TaskNames.ClassIntervals];
		}

		public void AddClassIntervals ()
		{
			if (!Inquirer.Answers.ContainsKey (TaskNames.ClassIntervals)) {
				var frequencyTable = new List<dynamic> (Inquirer.Groups);
				Inquirer.Answers.Add (TaskNames.ClassIntervals, frequencyTable.AsReadOnly ());
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
			if (!Inquirer.Answers.ContainsKey (TaskNames.Frequencies)) {
				AddClassIntervals ();
				var frequencyTable = GetTable ();
				Inquirer.Answers.Add (TaskNames.Frequencies, frequencyTable);
				List<double> data = Enumerable.ToList (Inquirer.Data);
				foreach (var tableItem in frequencyTable) {
					var frequency = data.Count (item => item >= tableItem.ClassInterval.From && item <= tableItem.ClassInterval.To);
					tableItem.Frequency = frequency;
				}
			}
		}

		public void AddAcumulatedFrequencies ()
		{
			if (!Inquirer.Answers.ContainsKey (TaskNames.AcumulatedFrequencies)) {
				AddFrequencies ();
				var frequencyTable = GetTable ();
				Inquirer.Answers.Add (TaskNames.AcumulatedFrequencies, frequencyTable);
				var lastFrequency = 0;
				foreach (var item in frequencyTable) {
					item.AcumulatedFrequency = item.Frequency + lastFrequency;
					lastFrequency = item.AcumulatedFrequency;
				}
			}
		}

		public void AddRelativeFrequencies ()
		{
			if (!Inquirer.Answers.ContainsKey (TaskNames.RelativeFrequencies)) {
				AddFrequencies ();
				var frequencyTable = GetTable ();
				Inquirer.Answers.Add (TaskNames.RelativeFrequencies, frequencyTable);
				foreach (var item in frequencyTable) {
					item.RelativeFrequency = (double)item.Frequency / Inquirer.Data.Count;
				}
			}
		}

		public void AddAcumulatedRelativeFrequencies ()
		{
			if (!Inquirer.Answers.ContainsKey (TaskNames.AcumulatedRelativeFrequencies)) {
				AddRelativeFrequencies ();
				var frequencyTable = GetTable ();
				Inquirer.Answers.Add (TaskNames.AcumulatedRelativeFrequencies, frequencyTable);
				var lastRelativeFrequency = 0.0;
				foreach (var item in frequencyTable) {
					item.AcumulatedRelativeFrequency = item.RelativeFrequency + lastRelativeFrequency;
					lastRelativeFrequency = item.AcumulatedRelativeFrequency;
				}
			}
		}

		public void  AddClassMarks ()
		{
			if (!Inquirer.Answers.ContainsKey (TaskNames.ClassMarks)) {
				AddClassIntervals ();
				var frequencyTable = GetTable ();
				Inquirer.Answers.Add (TaskNames.ClassMarks, frequencyTable);
				foreach (var item in frequencyTable) {
					var classMark = (item.ClassInterval.From + item.ClassInterval.To) / 2;
					item.ClassMark = classMark;
				}
			}
		}

		public void  AddRealClassIntervals ()
		{
			if (!Inquirer.Answers.ContainsKey (TaskNames.RealClassIntervals)) {
				AddClassIntervals ();
				var frequencyTable = GetTable ();
				Inquirer.Answers.Add (TaskNames.RealClassIntervals, frequencyTable);
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
			if (!Inquirer.Answers.ContainsKey (TaskNames.FrequenciesTimesClassMarks)) {
				AddFrequencies ();
				AddClassMarks ();
				var frequencyTable = GetTable ();
				Inquirer.Answers.Add (TaskNames.FrequenciesTimesClassMarks, frequencyTable);
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