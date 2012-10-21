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
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    internal static class SequenceCastExtension
    {
        public static MethodInfo ResolveFor(this Type type, string name, object[] args)
        {
            var result = type.GetMethods()
                .SingleOrDefault(method => method.GetParameters()
                                 .Select(parameter => parameter.ParameterType)
                .TypeSequenceIsAssignableFrom(args));

            return result;
        }

        public static bool TypeSequenceIsAssignableFrom(this IEnumerable<Type> source, IEnumerable<object> sequence)
        {
            var canCast = true;
            
            if (source.Count() == sequence.Count())
            {
                if (source.Count() > 0)
                {
                    var srcEnum = source.GetEnumerator();
                    var seqEnum = sequence.GetEnumerator();
                    do
                    {
                        if (seqEnum.Current == null)
                        {
                            continue;
                        }
                        
                        canCast = srcEnum.Current.IsAssignableFrom(seqEnum.Current.GetType());
                    }
                    while (srcEnum.MoveNext() && seqEnum.MoveNext() && canCast);
                }
            }
            else
            {
                canCast = false;
            }
            
            return canCast;
        }
    }
}