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

		public double Max {
			get;
			private set;
		}

		public double Min {
			get;
			private set;
		}

		public double Range {
			get;
			private set;
		}

		public int GroupsCount {
			get;
			private set;
		}

		public double Amplitude {
			get;
			private set;
		}

		public int DecimalsCount {
			get;
			private set;
		}

		public double DataPrecisionValue {
			get;
			private set;
		}

		public IEnumerable<dynamic> GetTable ()
		{
			if (!Answers.ContainsKey (TaskNames.DispersionTable)) {
				AddClassIntervals ();
			}

			return Answers [TaskNames.DispersionTable];
		}

		public void AddClassIntervals ()
		{
			if (!Answers.ContainsKey (TaskNames.ClassIntervals)) {
				var frequencyTable = new List<dynamic> (GroupsCount);
				Answers.Add (TaskNames.ClassIntervals, TaskNames.DispersionTable);
				Answers.Add (TaskNames.DispersionTable, frequencyTable.AsReadOnly ());
				var inferiorClassLimit = Min;
				var superiorClassLimit = inferiorClassLimit + Amplitude - DataPrecisionValue;
				for (int i = 1; i <= GroupsCount; i++) {
					var interval = new Interval {
						From = inferiorClassLimit,
						To = superiorClassLimit
					};

					dynamic distElement = new ExpandoObject ();
					distElement.ClassInterval = interval;
					frequencyTable.Add (distElement);
					inferiorClassLimit += Amplitude;
					superiorClassLimit += Amplitude;
				}
			}
		}

		public void AddFrequencies ()
		{
			if (!Answers.ContainsKey (TaskNames.Frequencies)) {
				AddClassIntervals ();
				var frequencyTable = GetTable ();
				Answers.Add (TaskNames.Frequencies, TaskNames.DispersionTable);
				foreach (var tableItem in frequencyTable) {
					var frequency = Data.Count (item => item >= tableItem.ClassInterval.From && item <= tableItem.ClassInterval.To);
					tableItem.Frequency = frequency;
				}
			}
		}

		public void AddAcumulatedFrequencies ()
		{
			if (!Answers.ContainsKey (TaskNames.AcumulatedFrequencies)) {
				AddFrequencies ();
				var frequencyTable = GetTable ();
				Answers.Add (TaskNames.AcumulatedFrequencies, TaskNames.DispersionTable);
				var lastFrequency = 0;
				foreach (var item in frequencyTable) {
					item.AcumulatedFrequency = item.Frequency + lastFrequency;
					lastFrequency = item.AcumulatedFrequency;
				}
			}
		}

		public void AddRelativeFrequencies ()
		{
			if (!Answers.ContainsKey (TaskNames.RelativeFrequencies)) {
				AddFrequencies ();
				var frequencyTable = GetTable ();
				Answers.Add (TaskNames.RelativeFrequencies, TaskNames.DispersionTable);
				foreach (var item in frequencyTable) {
					item.RelativeFrequency = (double)item.Frequency / Data.Count;
				}
			}
		}

		public void AddAcumulatedRelativeFrequencies ()
		{
			if (!Answers.ContainsKey (TaskNames.AcumulatedRelativeFrequencies)) {
				AddRelativeFrequencies ();
				var frequencyTable = GetTable ();
				Answers.Add (TaskNames.AcumulatedRelativeFrequencies, TaskNames.DispersionTable);
				var lastRelativeFrequency = 0.0;
				foreach (var item in frequencyTable) {
					item.AcumulatedRelativeFrequency = item.RelativeFrequency + lastRelativeFrequency;
					lastRelativeFrequency = item.AcumulatedRelativeFrequency;
				}
			}
		}

		public void  AddClassMarks ()
		{
			if (!Answers.ContainsKey (TaskNames.ClassMarks)) {
				AddClassIntervals ();
				var frequencyTable = GetTable ();
				Answers.Add (TaskNames.ClassMarks, TaskNames.DispersionTable);
				foreach (var item in frequencyTable) {
					var classMark = (item.ClassInterval.From + item.ClassInterval.To) / 2;
					item.ClassMark = classMark;
				}
			}
		}

		public void  AddRealClassIntervals ()
		{
			if (!Answers.ContainsKey (TaskNames.RealClassIntervals)) {
				AddClassIntervals ();
				var frequencyTable = GetTable ();
				Answers.Add (TaskNames.RealClassIntervals, TaskNames.DispersionTable);
				var midPrecision = DataPrecisionValue / 2;
				foreach (var item in frequencyTable) {
					var realInterval = new Interval {
						From = item.ClassInterval.From - midPrecision,
						To = item.ClassInterval.To + midPrecision
					};

					item.RealInterval = realInterval;
				}
			}
		}

		public void  AddFrequenciesTimesClassMarks ()
		{
			if (!Answers.ContainsKey (TaskNames.FrequenciesTimesClassMarks)) {
				AddFrequencies ();
				AddClassMarks ();
				var frequencyTable = GetTable ();
				Answers.Add (TaskNames.FrequenciesTimesClassMarks, TaskNames.DispersionTable);
				foreach (var item in frequencyTable) {
					item.fX = item.Frequency * item.ClassMark;
				}
			}
		}

		private void InitProperties ()
		{
			Max = Data.Max ();
			Min = Data.Min ();

			Range = Max - Min;

			GroupsCount = (int)Math.Round (Math.Sqrt (Data.Count));

			Amplitude = Math.Round (Range / GroupsCount, DecimalsCount);

			DataPrecisionValue = (1 / Math.Pow (10, DecimalsCount));

			if ((Min + Amplitude * GroupsCount - DataPrecisionValue) <= Max) {
				Amplitude += DataPrecisionValue;
			}
		}
	}	
}