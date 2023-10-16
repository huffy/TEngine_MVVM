
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace Framework.Interactivity
{
    public abstract class AsyncInteractionActionBase<TNotification> : IInteractionAction
    {
        public async void OnRequest(object sender, InteractionEventArgs args)
        {
            AsyncInteractionEventArgs asyncArgs = args as AsyncInteractionEventArgs;
            TaskCompletionSource<object> source = asyncArgs.Source;
            TNotification notification = (TNotification)asyncArgs.Context;
           await Action(notification);
           source.TrySetResult(null);
        }

        public abstract UniTask Action(TNotification notification);
    }
}
