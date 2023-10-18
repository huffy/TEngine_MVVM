using Framework.Commands;
using Framework.ViewModels;
namespace GameLogic
{
	public class TemplateViewModel : ViewModelBase
	{
		#region 脚本工具生成的代码
		private TemplateModel _model;
		public string Icon
		{
			get { return _model.Icon; }
			set { this.Set(ref _model.Icon, value); }
		}
		public string Title
		{
			get { return _model.Title; }
			set { this.Set(ref _model.Title, value); }
		}
		public string Price
		{
			get { return _model.Price; }
			set { this.Set(ref _model.Price, value); }
		}
		
		public bool IsSelected
		{
			get { return _model.IsSelected; }
			set { this.Set(ref _model.IsSelected, value); }
		}
		
		private ICommand _clickCommand;
		private ICommand _selectCommand;
		public TemplateViewModel (ICommand selectCommand, ICommand clickCommand)
		{
			_selectCommand = selectCommand;
			_clickCommand = clickCommand;
			_model = new TemplateModel();
		}
		#endregion

		#region 事件
		public void OnClickPanelBtn()
		{
			_selectCommand.Execute(this);
		}
		public void OnClickButtonBtn()
		{
			_clickCommand.Execute(this);
		}
		#endregion

	}
}
