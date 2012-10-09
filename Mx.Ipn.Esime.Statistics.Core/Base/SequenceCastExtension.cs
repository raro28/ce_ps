namespace Mx.Ipn.Esime.Statistics.Core.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    internal static class SequenceCastExtension
    {
        public static bool CanAssignValueSequence(this MethodInfo method, IEnumerable<object> args)
        {
            return method.GetParameters().Select(parameter => parameter.ParameterType).TypeSequenceIsAssignableFrom(args);
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