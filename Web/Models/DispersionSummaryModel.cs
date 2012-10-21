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

    public class DispersionSummaryModel
    {
        [DisplayName("Data Range")]
        public double DataRange
        {
            get;
            set;
        }

        [DisplayName("InterQuartile Range")]
        public double InterQuartileRange
        {
            get;
            set;
        }

        [DisplayName("InterPercentile Range")]
        public double InterPercentileRange
        {
            get;
            set;
        }

        [DisplayName("InterDecile Range")]
        public double InterDecileRange
        {
            get;
            set;
        }

        [DisplayName("Absolute Deviation")]
        public double AbsoluteDeviation
        {
            get;
            set;
        }

        [DisplayName("Variance")]
        public double Variance
        {
            get;
            set;
        }

        [DisplayName("Standar Deviation")]
        public double StandarDeviation
        {
            get;
            set;
        }

        [DisplayName("Coefficient Of Variation")]
        public double CoefficientOfVariation
        {
            get;
            set;
        }

        [DisplayName("Coefficient Of Kourtosis")]
        public double CoefficientOfKourtosis
        {
            get;
            set;
        }

        [DisplayName("Coefficient Of Symmetry")]
        public double CoefficientSymmetry
        {
            get;
            set;
        }
    }
}
