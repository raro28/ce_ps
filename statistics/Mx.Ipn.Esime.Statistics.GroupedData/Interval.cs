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
namespace Mx.Ipn.Esime.Statistics.GroupedData
{
    using Mx.Ipn.Esime.Statistics.Core.Resources;

    public class Interval
    {
        public readonly double From;
        public readonly double To;
        
        public Interval(double fromValue, double toValue)
        {
            this.From = fromValue;
            this.To = toValue;
        }
        
        public override string ToString()
        {
            return string.Format(TaskNames.Interval_Format, this.From, this.To);
        }
    }		
}