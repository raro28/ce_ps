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
			if (!Inquirer.Answers.ContainsKey ("add(CI)")) {
				AddClassIntervals ();
			}

			return Inquirer.Answers ["add(CI)"];
		}

		public void AddClassIntervals ()
		{
			if (!Inquirer.Answers.ContainsKey ("add(CI)")) {
				var frequencyTable = new List<dynamic> (Inquirer.Groups);
				Inquirer.Answers.Add ("add(CI)", frequencyTable.AsReadOnly ());
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
			if (!Inquirer.Answers.ContainsKey ("add(f)")) {
				AddClassIntervals ();
				var frequencyTable = GetTable ();
				Inquirer.Answers.Add ("add(f)", frequencyTable);
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
			if (!Inquirer.Answers.ContainsKey ("add(F)")) {
				AddFrequencies ();
				var frequencyTable = GetTable ();
				Inquirer.Answers.Add ("add(F)", frequencyTable);
				var lastFrequency = 0;
				foreach (var item in frequencyTable) {
					item.AcumulatedFrequency = item.Frequency + lastFrequency;
					lastFrequency = item.AcumulatedFrequency;
				}
			}
		}

		public void AddRelativeFrequencies ()
		{
			if (!Inquirer.Answers.ContainsKey ("add(fr)")) {
				AddFrequencies ();
				var frequencyTable = GetTable ();
				Inquirer.Answers.Add ("add(fr)", frequencyTable);
				foreach (var item in frequencyTable) {
					item.RelativeFrequency = (double)item.Frequency / Inquirer.Data.Count;
				}
			}
		}

		public void AddAcumulatedRelativeFrequencies ()
		{
			if (!Inquirer.Answers.ContainsKey ("add(Fr)")) {
				AddRelativeFrequencies ();
				var frequencyTable = GetTable ();
				Inquirer.Answers.Add ("add(Fr)", frequencyTable);
				var lastRelativeFrequency = 0.0;
				foreach (var item in frequencyTable) {
					item.AcumulatedRelativeFrequency = item.RelativeFrequency + lastRelativeFrequency;
					lastRelativeFrequency = item.AcumulatedRelativeFrequency;
				}
			}
		}

		public void  AddClassMarks ()
		{
			if (!Inquirer.Answers.ContainsKey ("add(X)")) {
				AddClassIntervals ();
				var frequencyTable = GetTable ();
				Inquirer.Answers.Add ("add(X)", frequencyTable);
				foreach (var item in frequencyTable) {
					var classMark = (item.ClassInterval.From + item.ClassInterval.To) / 2;
					item.ClassMark = classMark;
				}
			}
		}

		public void  AddRealClassIntervals ()
		{
			if (!Inquirer.Answers.ContainsKey ("add(RI)")) {
				AddClassIntervals ();
				var frequencyTable = GetTable ();
				Inquirer.Answers.Add ("add(RI)", frequencyTable);
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
			if (!Inquirer.Answers.ContainsKey ("add(fX)")) {
				AddFrequencies ();
				AddClassMarks ();
				var frequencyTable = GetTable ();
				Inquirer.Answers.Add ("add(fX)", frequencyTable);
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