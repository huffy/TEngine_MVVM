using System;
using Framework.Interactivity;
using Cysharp.Threading.Tasks;
using Framework.Binding;
using TEngine;

namespace Framework.Views.InteractionActions
{
    public class AsyncViewInteractionAction : AsyncInteractionActionBase<VisibilityNotification>
    {
        private UIBase _view;
        private Func<UniTask<UIBase>>  _viewAction;

        public AsyncViewInteractionAction(Func<UniTask<UIBase>> viewAction)
        {
            _viewAction = viewAction;
        }

        public override async UniTask Action(VisibilityNotification notification)
        {
            if (notification.Visible)
            {
                _view = await _viewAction.Invoke();
                Show(notification.ViewModel);
            }
            else
            {
                Hide();
            }
        }

        protected UniTask Show(object viewModel)
        {
            if (viewModel != null)
            {
                _view.transform.SetDataContext(viewModel);
            }

            _view.gameObject.SetActive(true);
            return UniTask.CompletedTask;
        }

        protected UniTask Hide()
        {
            _view.gameObject.SetActive(false);
            return UniTask.CompletedTask;
        }
    }
}
