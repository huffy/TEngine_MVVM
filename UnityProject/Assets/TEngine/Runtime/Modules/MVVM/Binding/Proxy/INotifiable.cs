

using System;

namespace Framework.Binding.Proxy
{

    public interface INotifiable
    {
        event EventHandler ValueChanged;
    }
}
