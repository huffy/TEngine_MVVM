using Framework.Services;

namespace Framework.Contexts
{
    /// <summary>
    /// ApplicationContext
    /// </summary>
    public class ApplicationContext : Context
    {
        public ApplicationContext() : this(null)
        {
        }

        public ApplicationContext(IServiceContainer container) : base(container, null)
        {
        }
    }
}
