using Framework.Binding;
using Framework.Binding.Builder;
using Framework.Binding.Contexts;
using Framework.Contexts;
using UnityEngine;
using UnityEngine.UI;
using TEngine;

namespace GameLogic
{
	[Window(UILayer.UI)]
	public class ListViewDatabindingExample : UIWindow
	{
		#region 脚本工具生成的代码
		private ListViewDatabindingExampleViewModel _viewModel;
		private Button m_btnAddButton;
		private Button m_btnRemoveButton;
		private Button m_btnClearButton;
		private Button m_btnChangeIconButton;
		private Button m_btnChangeItems;
		private GameObject m_itemListViewGo;
		private ListView m_itemListView;
		private GameObject m_itemDetailPanelGo;
		private DetailPanel m_itemDetailPanel;
		private GameObject m_itemEditViewGo;
		private EditView m_itemEditView;
		
		public override void ScriptGenerator()
		{
			base.ScriptGenerator();
			
			ApplicationContext context = Context.GetApplicationContext();
			BindingServiceBundle bindingService = new BindingServiceBundle(context.GetContainer());
			bindingService.Start();
			
			_viewModel = new ListViewDatabindingExampleViewModel();
			IBindingContext bindingContext = transform.BindingContext();
			bindingContext.DataContext = _viewModel;
			BindingSet<ListViewDatabindingExample, ListViewDatabindingExampleViewModel> bindingSet = this.CreateBindingSet<ListViewDatabindingExample, ListViewDatabindingExampleViewModel>();
			m_btnAddButton = FindChildComponent<Button>("ListViewExample/m_btnAddButton");
			m_btnRemoveButton = FindChildComponent<Button>("ListViewExample/m_btnRemoveButton");
			m_btnClearButton = FindChildComponent<Button>("ListViewExample/m_btnClearButton");
			m_btnChangeIconButton = FindChildComponent<Button>("ListViewExample/m_btnChangeIconButton");
			m_btnChangeItems = FindChildComponent<Button>("ListViewExample/m_btnChangeItems");
			m_itemListViewGo = FindChild("ListViewExample/m_itemListView").gameObject;
			m_itemListView = CreateWidget<ListView>(m_itemListViewGo);
			m_itemDetailPanelGo = FindChild("ListViewExample/m_itemDetailPanel").gameObject;
			m_itemDetailPanel = CreateWidget<DetailPanel>(m_itemDetailPanelGo);
			m_itemEditViewGo = FindChild("ListViewExample/m_itemEditView").gameObject;
			m_itemEditView = CreateWidget<EditView>(m_itemEditViewGo);

			bindingSet.Bind(m_btnAddButton).For(v => v.onClick).To(vm => vm.OnClickAddButtonBtn);

			bindingSet.Bind(m_btnRemoveButton).For(v => v.onClick).To(vm => vm.OnClickRemoveButtonBtn);

			bindingSet.Bind(m_btnClearButton).For(v => v.onClick).To(vm => vm.OnClickClearButtonBtn);

			bindingSet.Bind(m_btnChangeIconButton).For(v => v.onClick).To(vm => vm.OnClickChangeIconButtonBtn);

			bindingSet.Bind(m_btnChangeItems).For(v => v.onClick).To(vm => vm.OnClickChangeItemsBtn);
			bindingSet.Bind(m_itemListView).For(v => v.ViewModel).To(vm => vm.M_ListViewViewModel);
			bindingSet.Bind(m_itemDetailPanel).For(v => v.ViewModel).To(vm => vm.M_DetailPanelViewModel);
			bindingSet.Bind(m_itemEditView).For(v => v.ViewModel).To(vm => vm.M_EditViewViewModel);
			bindingSet.Build();
			
			_viewModel.Initialize();
		}
		#endregion
	}
}
