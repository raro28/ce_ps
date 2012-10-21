/*
 * Copyright (C) 2012 Hector Eduardo Diaz Campos
 * 
 * This file is part of Mx.DotNet.Statistics.
 * 
 * Mx.DotNet.Statistics is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * any later version.
 * 
 * Mx.DotNet.Statistics is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with Mx.DotNet.Statistics.  If not, see <http://www.gnu.org/licenses/>.
 */
namespace Web.Models
{
    using System.ComponentModel;
    public class DataSummaryModel
    {
        [DisplayName("Original Data")]
        public string Data
        {
            get;
            set;
        }

        [DisplayName("Data Count")]
        public int DataCount
        {
            get;
            set;
        }

        [DisplayName("Max Data")]
        public double Max
        {
            get;
            set;
        }

        [DisplayName("Min Data")]
        public double Min
        {
            get;
            set;
        }

        [DisplayName("Decimal Count")]
        public int DecimalCount
        {
            get;
            set;
        }

        [DisplayName("Data Precision")]
        public double Precision
        {
            get;
            set;
        }
    }
}

