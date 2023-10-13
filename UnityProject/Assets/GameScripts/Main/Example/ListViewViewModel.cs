using Framework.Commands;
using Framework.Interactivity;
using Framework.Observables;
using Framework.ViewModels;
using UnityEngine;

namespace Framework.Tutorials
{
    public class ListViewViewModel : ViewModelBase
    {
        private ListItemViewModel selectedItem;
        private SimpleCommand<ListItemViewModel> itemSelectCommand;
        private SimpleCommand<ListItemViewModel> itemClickCommand;
        private SimpleCommand<ListItemEditModel> itemEditedCommand;
        private AsyncInteractionRequest<VisibilityNotification> itemEditRequest;
        private ObservableList<ListItemViewModel> items;

        public ListViewViewModel()
        {
            itemEditRequest = new AsyncInteractionRequest<VisibilityNotification>(this);
            itemClickCommand = new SimpleCommand<ListItemViewModel>(OnItemClick);
            itemSelectCommand = new SimpleCommand<ListItemViewModel>(OnItemSelect);
            itemEditedCommand = new SimpleCommand<ListItemEditModel>(OnItemEdited);
            items = CreateList();
        }

        public ObservableList<ListItemViewModel> Items
        {
            get { return this.items; }
            set { this.Set(ref items, value); }
        }

        public ListItemViewModel SelectedItem
        {
            get { return this.selectedItem; }
            set { Set(ref selectedItem, value); }
        }

        public IInteractionRequest ItemEditRequest
        {
            get { return this.itemEditRequest; }
        }

        public ListItemViewModel SelectItem(int index)
        {
            if (index < 0 || index >= items.Count)
                throw new System.Exception();

            var item = items[index];
            item.IsSelected = true;
            this.SelectedItem = item;

            if (items != null && item.IsSelected)
            {
                foreach (var i in items)
                {
                    if (i == item)
                        continue;
                    i.IsSelected = false;
                }
            }

            return item;
        }

        private async void OnItemClick(ListItemViewModel item)
        {
            ListItemEditModel editModel = new ListItemEditModel()
            {
                Icon = item.Icon, Price = item.Price, Title = item.Title,
                ItemViewModel = item
            };
            ListItemEditViewModel editViewModel = new ListItemEditViewModel(editModel, itemEditedCommand);
            await itemEditRequest.Raise(VisibilityNotification.CreateShowNotification(editViewModel));
        }

        private void OnItemSelect(ListItemViewModel item)
        {
            item.IsSelected = !item.IsSelected;
            if (items != null && item.IsSelected)
            {
                foreach (var i in items)
                {
                    if (i == item)
                        continue;
                    i.IsSelected = false;
                }
            }

            if (item.IsSelected)
                this.SelectedItem = item;
        }

        private void OnItemEdited(ListItemEditModel model)
        {
            ListItemViewModel item = model.ItemViewModel;
            item.Icon = model.Icon;
            item.Price = model.Price;
            item.Title = model.Title;
        }

        public void AddItem()
        {
            int i = this.items.Count;
            this.items.Add(NewItem(i));
        }

        public void RemoveItem()
        {
            if (this.items.Count <= 0)
                return;

            int index = Random.Range(0, this.items.Count - 1);
            var item = this.items[index];
            if (item.IsSelected)
                this.SelectedItem = null;

            this.items.RemoveAt(index);
        }

        public void ClearItem()
        {
            if (this.items.Count <= 0)
                return;

            this.items.Clear();
            this.SelectedItem = null;
        }

        public void ChangeItemIcon()
        {
            if (this.items.Count <= 0)
                return;

            foreach (var item in this.items)
            {
                int iconIndex = Random.Range(1, 30);
                item.Icon = string.Format("EquipImages_{0}", iconIndex);
            }
        }

        public void ChangeItems()
        {
            this.SelectedItem = null;
            this.Items = CreateList();
        }

        private ObservableList<ListItemViewModel> CreateList()
        {
            var items = new ObservableList<ListItemViewModel>();
            for (int i = 0; i < 3; i++)
            {
                items.Add(NewItem(i));
            }
            return items;
        }

        private ListItemViewModel NewItem(int id)
        {
            int iconIndex = Random.Range(1, 30);
            float price = Random.Range(0f, 100f);
            return new ListItemViewModel(this.itemSelectCommand, this.itemClickCommand) { Title = "Equip " + id, Icon = string.Format("EquipImages_{0}", iconIndex), Price = price };
        }
    }
}
