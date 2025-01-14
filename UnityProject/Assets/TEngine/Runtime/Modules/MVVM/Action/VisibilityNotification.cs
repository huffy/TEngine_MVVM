﻿
namespace Framework.Interactivity
{
    public class VisibilityNotification
    {
        public static VisibilityNotification CreateShowNotification()
        {
            return new VisibilityNotification(true, null);
        }

        public static VisibilityNotification CreateShowNotification(object viewModel)
        {
            return new VisibilityNotification(true, viewModel);
        }

        public static VisibilityNotification CreateHideNotification()
        {
            return new VisibilityNotification(false);
        }

        public bool Visible { get; private set; }
        public object ViewModel { get; private set; }
        public bool WaitDisabled { get; private set; }

        public VisibilityNotification(bool visible) : this(visible, null)
        {
        }

        public VisibilityNotification(bool visible, object viewModel)
        {
            this.Visible = visible;
            this.ViewModel = viewModel;
        }
    }
}
