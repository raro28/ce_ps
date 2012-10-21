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
namespace Mx.Ipn.Esime.Statistics.Core.Base
{
    using System;
    using Mx.Ipn.Esime.Statistics.Core.Resources;

    public enum Xiles
    {
        Quartile = 4,
        Decile = 10,
        Percentile = 100
    }

    public abstract class XileInquirerBase : InquirerBase, IXileInquirer
    {       
        public XileInquirerBase(DataContainer dataContainer, params InquirerBase[] dependencies) : base(dataContainer, dependencies)
        {           
        }

        public double GetDecile(int nTh)
        {
            var xileInfo = AssertValidXile(nTh, Xiles.Decile);
            var nThDecile = this.GetXile(xileInfo);
            
            return nThDecile;
        }

        public double GetPercentile(int nTh)
        {
            var xileInfo = AssertValidXile(nTh, Xiles.Percentile);
            var nThPercentile = this.GetXile(xileInfo);
            
            return nThPercentile;
        }

        public double GetQuartile(int nTh)
        {
            var xileInfo = AssertValidXile(nTh, Xiles.Quartile);
            var nThQuartile = this.GetXile(xileInfo);
            
            return nThQuartile;
        }

        protected abstract double CalcXile(double lx);
            
        private static XileInfo AssertValidXile(int nTh, Xiles xile)
        {
            if (nTh < 1 || nTh > (int)xile)
            {
                var xileName = Enum.GetName(typeof(Xiles), xile);
                throw new StatisticsException(string.Format(ExceptionMessages.Invalid_Xile_Name_Format, xileName), new IndexOutOfRangeException(string.Format(ExceptionMessages.Invalid_Xile_Name_Number_Format, xileName, nTh)));
            }
            
            return new XileInfo(xile, nTh);
        }

        public double GetXile(XileInfo xileInfo)
        {
            Func<double> func = () =>
            {
                double xileResult;
                var lx = Container.DataCount * xileInfo.NthXile / (double)xileInfo.Xile;
                xileResult = this.CalcXile(lx);

                return xileResult;
            };

            return this.Container.Register(xileInfo.ToString(), func);
        }
    }
}