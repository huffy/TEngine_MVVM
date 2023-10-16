﻿

using System;

namespace Framework.Binding.Reflection
{
    public interface IProxyEventInfo : IProxyMemberInfo
    {
        Type HandlerType { get; }

        void Add(object target, Delegate handler);

        void Remove(object target, Delegate handler);
    }
}
