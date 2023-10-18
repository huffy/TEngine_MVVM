using Framework.Commands;
using Framework.Interactivity;
using Framework.Observables;
using Framework.ViewModels;
using Random = UnityEngine.Random;

namespace GameLogic
{
	public class ListViewViewModel : ViewModelBase
	{
		#region 脚本工具生成的代码
		private ListViewModel _model;
        public TemplateViewModel M_TemplateViewModel;
        private TemplateViewModel _clickedTemplateViewModel;
        #endregion

		#region 事件
		#endregion

        private SimpleCommand<TemplateViewModel> itemSelectCommand;
        private ICommand _itemClickCommand, _selectItemCommand;
        private ObservableList<TemplateViewModel> items;
        private IInteractionAction _interactionAction;

        public ListViewViewModel()
        {
            _model = new ListViewModel();
            itemSelectCommand = new SimpleCommand<TemplateViewModel>(OnItemSelect);
        }
        
        public void Initialize(ICommand clickCommand, ICommand selectCommand)
        {
            _itemClickCommand = clickCommand;
            _selectItemCommand = selectCommand;
            items = CreateList();
        }

        public ObservableList<TemplateViewModel> Items
        {
            get { return this.items; }
            set { this.Set(ref items, value); }
        }

        public TemplateViewModel SelectedItem
        {
            get { return this.M_TemplateViewModel; }
            set
            {
                Set(ref M_TemplateViewModel, value);
                _selectItemCommand.Execute(value);
            }
        }

        public TemplateViewModel SelectItem(int index)
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

        private void OnItemSelect(TemplateViewModel item)
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

        private ObservableList<TemplateViewModel> CreateList()
        {
            var items = new ObservableList<TemplateViewModel>();
            for (int i = 0; i < 3; i++)
            {
                items.Add(NewItem(i));
            }
            return items;
        }

        private TemplateViewModel NewItem(int id)
        {
            int iconIndex = Random.Range(1, 30);
            float price = Random.Range(0f, 100f);
            return new TemplateViewModel(this.itemSelectCommand, _itemClickCommand) { Title = "Equip " + id, Icon = string.Format("EquipImages_{0}", iconIndex), Price = price.ToString() };
        }

	}
}
