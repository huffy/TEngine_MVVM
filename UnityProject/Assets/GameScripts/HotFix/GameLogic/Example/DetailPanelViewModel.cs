using Framework.ViewModels;
using UnityEngine;
namespace GameLogic
{
	public class DetailPanelViewModel : ViewModelBase
	{
		#region 脚本工具生成的代码
		private DetailPanelModel _model;
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

		public DetailPanelViewModel ()
		{
			_model = new DetailPanelModel();
		}
		#endregion

		#region 事件
		#endregion

	}
}
