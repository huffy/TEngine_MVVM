using Framework.Commands;
using Framework.ViewModels;

namespace Framework.Tutorials
{
    public class ListItemModel
    {
        public string Title;
        public string Icon;
        public float Price;
        public bool IsSelected;
    }

    public class ListItemViewModel : ViewModelBase
    {
        private ICommand clickCommand;
        private ICommand selectCommand;
        private ListItemModel _model;

        public ListItemViewModel(ICommand selectCommand, ICommand clickCommand)
        {
            this.selectCommand = selectCommand;
            this.clickCommand = clickCommand;
            _model = new ListItemModel();
        }

        public ICommand ClickCommand
        {
            get { return this.clickCommand; }
        }

        public ICommand SelectCommand
        {
            get { return this.selectCommand; }
        }

        public string Title
        {
            get { return _model.Title; }
            set { this.Set(ref _model.Title, value); }
        }
        public string Icon
        {
            get { return _model.Icon; }
            set { this.Set(ref _model.Icon, value); }
        }

        public float Price
        {
            get { return _model.Price; }
            set { this.Set(ref _model.Price, value); }
        }

        public bool IsSelected
        {
            get { return _model.IsSelected; }
            set { this.Set(ref _model.IsSelected, value); }
        }
    }
}
