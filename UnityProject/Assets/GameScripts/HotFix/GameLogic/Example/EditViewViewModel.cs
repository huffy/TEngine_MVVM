using Framework.Commands;
using Framework.ViewModels;
using UnityEngine;
namespace GameLogic
{
	public class EditViewViewModel : ViewModelBase
	{
		private ICommand _submitCommand;
		private ICommand _visualCommand;
		#region 脚本工具生成的代码
		private EditViewModel _model;
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
		
		public ICommand VisualCommand
		{
			get { return _visualCommand; }
			set { this.Set(ref _visualCommand, value); }
		}
		
		public TemplateViewModel TemplateViewModel
		{
			get { return _model.TemplateViewModel; }
			set { this.Set(ref _model.TemplateViewModel, value); }
		}

		public EditViewViewModel ()
		{
			_model = new EditViewModel();
		}

		public void  UpdateData(EditViewModel vm, ICommand submitCommand)
		{
			_visualCommand.Execute(vm.IsSelected);
			Icon = vm.Icon;
			Title = vm.Title;
			Price = vm.Price;
			_submitCommand = submitCommand;
			_model = vm;
		}
		#endregion

		#region 事件
		public void OnSliderChange(float value)
		{
			Price = value.ToString();
		}
		public void OnClickChangeIconButtonBtn()
		{
			int iconIndex = Random.Range(1, 30);
			Icon = string.Format("EquipImages_{0}", iconIndex);
		}
		public void OnClickCancelButtonBtn()
		{
			_visualCommand.Execute(false);
		}
		public void OnClickSubmitButtonBtn()
		{
			_visualCommand.Execute(false);
			_submitCommand.Execute(this);
		}
		#endregion

	}
}
