using Framework.Commands;
using Framework.ViewModels;
namespace GameLogic
{
	public class ListViewDatabindingExampleViewModel : ViewModelBase
	{
		#region 脚本工具生成的代码
		private ListViewDatabindingExampleModel _model;
		public ListViewViewModel M_ListViewViewModel;
		public DetailPanelViewModel M_DetailPanelViewModel;
		public EditViewViewModel M_EditViewViewModel;
		private SimpleCommand<EditViewViewModel> itemEditedCommand;
		private SimpleCommand<TemplateViewModel> itemClickCommand;
		private SimpleCommand<TemplateViewModel> itemSelectCommand;
		public ListViewDatabindingExampleViewModel ()
		{
			_model = new ListViewDatabindingExampleModel();
			M_ListViewViewModel = new ListViewViewModel();
			M_DetailPanelViewModel = new DetailPanelViewModel();
			M_EditViewViewModel = new EditViewViewModel();
			itemClickCommand = new SimpleCommand<TemplateViewModel>(OnClickedItemChanged);
			itemSelectCommand = new SimpleCommand<TemplateViewModel>(OnSelectCommandChanged);
			itemEditedCommand = new SimpleCommand<EditViewViewModel>(OnItemEdited);
			M_ListViewViewModel.Initialize(itemClickCommand, itemSelectCommand);
		}

		public void Initialize()
		{
			M_ListViewViewModel.SelectItem(0);
		}

		#endregion

		#region 事件
		public void OnClickAddButtonBtn()
		{
			M_ListViewViewModel.AddItem();
		}
		public void OnClickRemoveButtonBtn()
		{
			M_ListViewViewModel.RemoveItem();
		}
		public void OnClickClearButtonBtn()
		{
			M_ListViewViewModel.ClearItem();
		}
		public void OnClickChangeIconButtonBtn()
		{
			M_ListViewViewModel.ChangeItemIcon();
		}
		public void OnClickChangeItemsBtn()
		{
			M_ListViewViewModel.ChangeItems();
		}
		#endregion

		private void OnSelectCommandChanged(TemplateViewModel item)
		{
			M_DetailPanelViewModel.Icon = item.Icon;
			M_DetailPanelViewModel.Title = item.Title;
			M_DetailPanelViewModel.Price = item.Price;
			M_DetailPanelViewModel.IsSelected = item.IsSelected;
		}
		
		private void OnClickedItemChanged(TemplateViewModel item)
		{
			EditViewModel editModel = new EditViewModel()
			{
				Icon = item.Icon, Price = item.Price, Title = item.Title,
				TemplateViewModel = item, IsSelected = true,
			};
			M_EditViewViewModel.UpdateData(editModel, itemEditedCommand);
		}
		
		private void OnItemEdited(EditViewViewModel model)
		{
			TemplateViewModel item = model.TemplateViewModel;
			item.Icon = model.Icon;
			item.Price = model.Price;
			item.Title = model.Title;
		}
	}
}
