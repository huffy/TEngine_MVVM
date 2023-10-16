

using Framework.Binding.Contexts;

namespace Framework.Binding
{
    public interface IBindingFactory
    {
        IBinding Create(IBindingContext bindingContext, object source, object target, BindingDescription bindingDescription);
    }
}
