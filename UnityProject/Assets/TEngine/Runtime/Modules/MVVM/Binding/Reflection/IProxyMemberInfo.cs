﻿

using System;

namespace Framework.Binding.Reflection
{
    public interface IProxyMemberInfo
    {
        Type DeclaringType { get; }

        string Name { get; }

        bool IsStatic { get; }
    }
}
