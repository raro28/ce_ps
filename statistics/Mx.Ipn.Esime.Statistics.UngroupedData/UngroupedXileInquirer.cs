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
namespace Mx.Ipn.Esime.Statistics.UngroupedData
{
	using System;
    using Mx.Ipn.Esime.Statistics.Core.Base;

    public class UngroupedXileInquirer : XileInquirerBase
    {
        public UngroupedXileInquirer(DataContainer dataContainer) : base(dataContainer)
        {			
        }

        protected override double CalcXile(double lx)
        {
            var li = (int)Math.Floor(lx - 0.5);
            var ls = (int)Math.Floor(lx + 0.5);
            if (ls == Container.DataCount)
            {
                ls = li;
            }
			
            var iPortion = li + 1 - (lx - 0.5);
            var sPortion = 1 - iPortion;
			
            var xileResult = (iPortion * Container.Data[li]) + (sPortion * Container.Data[ls]);
            return xileResult;
        }
    }
}