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
    using System.Dynamic;

    public abstract class StatisticsInquirerBase : DynamicObject
    {
        public readonly IEnumerable<InquirerBase> Inquirers;

        public StatisticsInquirerBase(DataContainer dataContainer, params InquirerBase[] inquirers)
        {                      
            this.Inquirers = inquirers;

            this.Container = dataContainer;
        }

        public DataContainer Container
        {
            get;
            private set;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            return this.Inquire(binder.Name, args, out result);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var success = false;
            result = null;
            foreach (var item in this.Inquirers)
            {
                var member = item.GetType().GetProperty(binder.Name);
                if (member == null)
                {
                    continue;
                }

                result = member.GetValue(item, new object[] { });
                success = true;
            }

            return success;
        }

        public bool Inquire(string inquiry, object[] args, out object result)
        {
            var success = false;
            result = null;

            foreach (var item in this.Inquirers)
            {
                var method = item.GetType().GetMethod(inquiry);
                if (method != null)
                {
                    result = method.Invoke(item, args);
                    success = true;
                    break;
                }
            }

            return success;
        }
    }
}