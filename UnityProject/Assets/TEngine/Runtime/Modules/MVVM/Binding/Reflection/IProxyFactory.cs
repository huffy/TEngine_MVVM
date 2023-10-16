

using System;

namespace Framework.Binding.Reflection
{
    public interface IProxyFactory
    {
        IProxyType Create(Type type);
    }
}
