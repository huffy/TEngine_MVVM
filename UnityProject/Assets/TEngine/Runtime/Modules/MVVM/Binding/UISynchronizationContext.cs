

using System.Threading;
using UnityEngine;

namespace Framework.Binding
{
    public class UISynchronizationContext
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void OnInitialize()
        {
            content = SynchronizationContext.Current;
            threadId = Thread.CurrentThread.ManagedThreadId;
        }

        private static int threadId;
        private static SynchronizationContext content;

        public static void Post(SendOrPostCallback callback, object state)
        {
            if (threadId == Thread.CurrentThread.ManagedThreadId)
                callback(state);
            else
                content.Post(callback, state);
        }
        public static void Send(SendOrPostCallback callback, object state)
        {
            if (threadId == Thread.CurrentThread.ManagedThreadId)
                callback(state);
            else
                content.Send(callback, state);
        }
    }
}
