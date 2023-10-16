

using Framework.Binding.Reflection;
using System;
using UnityEngine;

namespace Framework.Binding.Proxy.Targets
{
    public class MethodTargetProxy : TargetProxyBase, IObtainable, IProxyInvoker
    {
        protected readonly IProxyMethodInfo methodInfo;
        public MethodTargetProxy(object target, IProxyMethodInfo methodInfo) : base(target)
        {
            this.methodInfo = methodInfo;
            if (!methodInfo.ReturnType.Equals(typeof(void)))
                throw new ArgumentException("methodInfo");
        }

        public override BindingMode DefaultMode { get { return BindingMode.OneWayToSource; } }

        public override Type Type { get { return typeof(IProxyInvoker); } }

        public IProxyMethodInfo ProxyMethodInfo { get { return this.methodInfo; } }

        public object GetValue()
        {
            return this;
        }

        public TValue GetValue<TValue>()
        {
            return (TValue)this.GetValue();
        }

        public object Invoke(params object[] args)
        {
            if (this.methodInfo.IsStatic)
                return this.methodInfo.Invoke(null, args);

            var obj = this.Target;
            if (obj == null)
                return null;

            if (obj is Behaviour behaviour && !behaviour.isActiveAndEnabled)
                return null;

            return this.methodInfo.Invoke(obj, args);
        }
    }
}
