

using Framework.Binding.Paths;

namespace Framework.Binding.Proxy.Sources.Object
{
    public interface INodeProxyFactory
    {
        ISourceProxy Create(object source, PathToken token);
    }
}
