using Framework.Commands;
using Framework.ViewModels;
using UnityEngine;

namespace Framework.Tutorials
{
    public class ListItemEditModel
    {
        public string Title;
        public string Icon;
        public float Price;
        public ListItemViewModel ItemViewModel;
    }
    public class ListItemEditViewModel : ViewModelBase
    {
        private ListItemEditModel _model;
        private ICommand _submitCommand;
        private bool cancelled;

        public ListItemEditViewModel(ListItemEditModel vm, ICommand submitCommand)
        {
            _model = vm;
            _submitCommand = submitCommand;
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

        public bool Cancelled
        {
            get { return cancelled; }
            set { this.Set(ref this.cancelled, value); }
        }

        public void OnChangeIcon()
        {
            int iconIndex = Random.Range(1, 30);
            this.Icon = string.Format("EquipImages_{0}", iconIndex);
        }
        
        public void Submit()
        {
            _submitCommand.Execute(_model);
        }
    }
}
