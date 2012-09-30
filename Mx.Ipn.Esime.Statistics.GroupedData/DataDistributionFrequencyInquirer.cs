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
			if (!Properties ["Answers"].ContainsKey (TaskNames.DispersionTable)) {
				AddClassIntervals ();
			}

			return Properties ["Answers"] [TaskNames.DispersionTable];
		}

		public void AddClassIntervals ()
		{
			if (!Properties ["Answers"].ContainsKey (TaskNames.ClassIntervals)) {
				var frequencyTable = new List<dynamic> (Properties ["Groups"]);
				Properties ["Answers"].Add (TaskNames.ClassIntervals, TaskNames.DispersionTable);
				Properties ["Answers"].Add (TaskNames.DispersionTable, frequencyTable.AsReadOnly ());
				var inferiorClassLimit = Enumerable.Min (Properties ["Data"]);
				var superiorClassLimit = inferiorClassLimit + Properties ["Amplitude"] - Properties ["DataPrecisionValue"];
				for (int i = 1; i <= Properties["Groups"]; i++) {
					var interval = new Interval {
						From = inferiorClassLimit,
						To = superiorClassLimit
					};

					dynamic distElement = new ExpandoObject ();
					distElement.ClassInterval = interval;
					frequencyTable.Add (distElement);
					inferiorClassLimit += Properties ["Amplitude"];
					superiorClassLimit += Properties ["Amplitude"];
				}
			}
		}

		public void AddFrequencies ()
		{
			if (!Properties ["Answers"].ContainsKey (TaskNames.Frequencies)) {
				AddClassIntervals ();
				var frequencyTable = GetTable ();
				Properties ["Answers"].Add (TaskNames.Frequencies, TaskNames.DispersionTable);
				List<double> data = Enumerable.ToList (Properties ["Data"]);
				foreach (var tableItem in frequencyTable) {
					var frequency = data.Count (item => item >= tableItem.ClassInterval.From && item <= tableItem.ClassInterval.To);
					tableItem.Frequency = frequency;
				}
			}
		}

		public void AddAcumulatedFrequencies ()
		{
			if (!Properties ["Answers"].ContainsKey (TaskNames.AcumulatedFrequencies)) {
				AddFrequencies ();
				var frequencyTable = GetTable ();
				Properties ["Answers"].Add (TaskNames.AcumulatedFrequencies, TaskNames.DispersionTable);
				var lastFrequency = 0;
				foreach (var item in frequencyTable) {
					item.AcumulatedFrequency = item.Frequency + lastFrequency;
					lastFrequency = item.AcumulatedFrequency;
				}
			}
		}

		public void AddRelativeFrequencies ()
		{
			if (!Properties ["Answers"].ContainsKey (TaskNames.RelativeFrequencies)) {
				AddFrequencies ();
				var frequencyTable = GetTable ();
				Properties ["Answers"].Add (TaskNames.RelativeFrequencies, TaskNames.DispersionTable);
				foreach (var item in frequencyTable) {
					item.RelativeFrequency = (double)item.Frequency / Properties ["Data"].Count;
				}
			}
		}

		public void AddAcumulatedRelativeFrequencies ()
		{
			if (!Properties ["Answers"].ContainsKey (TaskNames.AcumulatedRelativeFrequencies)) {
				AddRelativeFrequencies ();
				var frequencyTable = GetTable ();
				Properties ["Answers"].Add (TaskNames.AcumulatedRelativeFrequencies, TaskNames.DispersionTable);
				var lastRelativeFrequency = 0.0;
				foreach (var item in frequencyTable) {
					item.AcumulatedRelativeFrequency = item.RelativeFrequency + lastRelativeFrequency;
					lastRelativeFrequency = item.AcumulatedRelativeFrequency;
				}
			}
		}

		public void  AddClassMarks ()
		{
			if (!Properties ["Answers"].ContainsKey (TaskNames.ClassMarks)) {
				AddClassIntervals ();
				var frequencyTable = GetTable ();
				Properties ["Answers"].Add (TaskNames.ClassMarks, TaskNames.DispersionTable);
				foreach (var item in frequencyTable) {
					var classMark = (item.ClassInterval.From + item.ClassInterval.To) / 2;
					item.ClassMark = classMark;
				}
			}
		}

		public void  AddRealClassIntervals ()
		{
			if (!Properties ["Answers"].ContainsKey (TaskNames.RealClassIntervals)) {
				AddClassIntervals ();
				var frequencyTable = GetTable ();
				Properties ["Answers"].Add (TaskNames.RealClassIntervals, TaskNames.DispersionTable);
				var midPrecision = Properties ["DataPrecisionValue"] / 2;
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
			if (!Properties ["Answers"].ContainsKey (TaskNames.FrequenciesTimesClassMarks)) {
				AddFrequencies ();
				AddClassMarks ();
				var frequencyTable = GetTable ();
				Properties ["Answers"].Add (TaskNames.FrequenciesTimesClassMarks, TaskNames.DispersionTable);
				foreach (var item in frequencyTable) {
					item.fX = item.Frequency * item.ClassMark;
				}
			}
		}

		private void InitProperties ()
		{
			var max = Enumerable.Max (Properties ["Data"]);
			Properties.Add ("Max", max);

			var min = Enumerable.Min (Properties ["Data"]);
			Properties.Add ("Min", min);

			var range = max - min;
			Properties.Add ("Range", range);

			var groups = (int)Math.Round (Math.Sqrt (Properties ["Data"].Count));
			Properties.Add ("Groups", groups);

			var amplitude = Math.Round (range / Properties ["Groups"], Properties ["DataPrecision"]);
			Properties.Add ("Amplitude", amplitude);

			var dataPrecisionValue = (1 / Math.Pow (10, Properties ["DataPrecision"]));
			Properties.Add ("DataPrecisionValue", dataPrecisionValue);

			if ((min + amplitude * groups - dataPrecisionValue) <= max) {
				Properties ["Amplitude"] += dataPrecisionValue;
			}
		}
	}	
}