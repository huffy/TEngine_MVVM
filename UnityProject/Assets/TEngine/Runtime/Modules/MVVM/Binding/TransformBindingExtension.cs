using UnityEngine;
using System.Collections.Generic;
using Framework.Binding.Binders;
using Framework.Binding.Contexts;
using Framework.Binding.Builder;
using Framework.Contexts;
using System;
using TEngine;

namespace Framework.Binding
{
    public static class TransformBindingExtension
    {
        private static IBinder binder;
        public static IBinder Binder
        {
            get
            {
                if (binder == null)
                    binder = Context.GetApplicationContext().GetService<IBinder>();

                if (binder == null)
                    throw new Exception("Data binding service is not initialized,please create a BindingServiceBundle service before using it.");

                return binder;
            }
        }

        public static IBindingContext BindingContext(this Transform transform)
        {
            if (transform == null || transform.gameObject == null)
                return null;

            BindingContextLifecycle bindingContextLifecycle = transform.GetComponent<BindingContextLifecycle>();
            if (bindingContextLifecycle == null)
                bindingContextLifecycle = transform.gameObject.AddComponent<BindingContextLifecycle>();

            IBindingContext bindingContext = bindingContextLifecycle.BindingContext;
            if (bindingContext == null)
            {
                bindingContext = new BindingContext(transform, Binder);
                bindingContextLifecycle.BindingContext = bindingContext;
            }
            return bindingContext;
        }

        public static BindingSet<TTransform, TSource> CreateBindingSet<TTransform, TSource>(this TTransform transform) where TTransform : UIBase
        {
            IBindingContext context = transform.transform.BindingContext();
            return new BindingSet<TTransform, TSource>(context, transform);
        }

        public static BindingSet<TTransform, TSource> CreateBindingSet<TTransform, TSource>(this TTransform transform, TSource dataContext) where TTransform : UIBase
        {
            IBindingContext context = transform.transform.BindingContext();
            context.DataContext = dataContext;
            return new BindingSet<TTransform, TSource>(context, transform);
        }

        public static BindingSet<TTransform> CreateBindingSet<TTransform>(this TTransform transform) where TTransform : UIBase
        {
            IBindingContext context = transform.transform.BindingContext();
            return new BindingSet<TTransform>(context, transform);
        }

        public static BindingSet CreateSimpleBindingSet(this Transform transform)
        {
            IBindingContext context = transform.BindingContext();
            return new BindingSet(context, transform);
        }

        public static void SetDataContext(this Transform transform, object dataContext)
        {
            transform.BindingContext().DataContext = dataContext;
        }

        public static object GetDataContext(this Transform transform)
        {
            return transform.BindingContext().DataContext;
        }

        public static void AddBinding(this Transform transform, BindingDescription bindingDescription)
        {
            transform.BindingContext().Add(transform, bindingDescription);
        }

        public static void AddBindings(this Transform transform, IEnumerable<BindingDescription> bindingDescriptions)
        {
            transform.BindingContext().Add(transform, bindingDescriptions);
        }

        public static void AddBinding(this Transform transform, IBinding binding)
        {
            transform.BindingContext().Add(binding);
        }

        public static void AddBinding(this Transform transform, IBinding binding, object key = null)
        {
            transform.BindingContext().Add(binding, key);
        }

        public static void AddBindings(this Transform transform, IEnumerable<IBinding> bindings, object key = null)
        {
            if (bindings == null)
                return;

            transform.BindingContext().Add(bindings, key);
        }

        public static void AddBinding(this Transform transform, object target, BindingDescription bindingDescription, object key = null)
        {
            transform.BindingContext().Add(target, bindingDescription, key);
        }

        public static void AddBindings(this Transform transform, object target, IEnumerable<BindingDescription> bindingDescriptions, object key = null)
        {
            transform.BindingContext().Add(target, bindingDescriptions, key);
        }

        public static void AddBindings(this Transform transform, IDictionary<object, IEnumerable<BindingDescription>> bindingMap, object key = null)
        {
            if (bindingMap == null)
                return;

            IBindingContext context = transform.BindingContext();
            foreach (var kvp in bindingMap)
            {
                context.Add(kvp.Key, kvp.Value, key);
            }
        }

        public static void ClearBindings(this Transform transform, object key)
        {
            transform.BindingContext().Clear(key);
        }

        public static void ClearAllBindings(this Transform transform)
        {
            transform.BindingContext().Clear();
        }
    }
}
