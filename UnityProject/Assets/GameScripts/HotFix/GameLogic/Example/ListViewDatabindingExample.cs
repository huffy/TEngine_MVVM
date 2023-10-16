using Cysharp.Threading.Tasks;
using Framework.Binding;
using Framework.Binding.Builder;
using Framework.Binding.Contexts;
using Framework.Contexts;
using Framework.Views.InteractionActions;
using TEngine;
using UnityEngine;
using UnityEngine.UI;

namespace Framework.Tutorials
{
    [Window(UILayer.UI)]
    public class ListViewDatabindingExample : UIWindow
    {
        private ListViewViewModel viewModel;
        private ListView listView;
        private ListItemDetailView detailView;

        private AsyncViewInteractionAction editViewInteractionAction;

        #region 脚本工具生成的代码

        private Button m_btnAddButton;
        private Button m_btnRemoveButton;
        private Button m_btnClearButton;
        private Button m_btnChangeIconButton;
        private Button m_btnChangeItems;
        private GameObject m_itemListView;
        private GameObject m_itemDetailPanel;
        private GameObject m_itemEditView;

        public override void ScriptGenerator()
        {
            m_btnAddButton = FindChildComponent<Button>("ListViewExample/m_btnAddButton");
            m_btnRemoveButton = FindChildComponent<Button>("ListViewExample/m_btnRemoveButton");
            m_btnClearButton = FindChildComponent<Button>("ListViewExample/m_btnClearButton");
            m_btnChangeIconButton = FindChildComponent<Button>("ListViewExample/m_btnChangeIconButton");
            m_btnChangeItems = FindChildComponent<Button>("ListViewExample/m_btnChangeItems");
            m_itemListView = FindChild("ListViewExample/m_itemListView").gameObject;
            m_itemDetailPanel = FindChild("ListViewExample/m_itemDetailPanel").gameObject;
            m_itemEditView = FindChild("ListViewExample/m_itemEditView").gameObject;
        }

        #endregion

        public override void BindMemberProperty()
        {
            base.BindMemberProperty();
            ApplicationContext context = Context.GetApplicationContext();
            BindingServiceBundle bindingService = new BindingServiceBundle(context.GetContainer());
            bindingService.Start();
        }

        public override void OnCreate()
        {
            base.OnCreate();
            viewModel = new ListViewViewModel();
            IBindingContext bindingContext = transform.BindingContext();
            bindingContext.DataContext = viewModel;
            
            listView = CreateWidget<ListView>(m_itemListView);
            detailView = CreateWidget<ListItemDetailView>(m_itemDetailPanel);
            
            BindingSet<ListViewDatabindingExample, ListViewViewModel> bindingSet = this.CreateBindingSet<ListViewDatabindingExample, ListViewViewModel>();
            bindingSet.Bind(this.listView).For(v => v.Items).To(vm => vm.Items).OneWay();
            bindingSet.Bind(this.detailView).For(v => v.Item).To(vm => vm.SelectedItem);
            bindingSet.Bind(this.m_btnAddButton).For(v => v.onClick).To(vm => vm.AddItem);
            bindingSet.Bind(this.m_btnRemoveButton).For(v => v.onClick).To(vm => vm.RemoveItem);
            bindingSet.Bind(this.m_btnClearButton).For(v => v.onClick).To(vm => vm.ClearItem);
            bindingSet.Bind(this.m_btnChangeIconButton).For(v => v.onClick).To(vm => vm.ChangeItemIcon);
            bindingSet.Bind(this.m_btnChangeItems).For(v => v.onClick).To(vm => vm.ChangeItems);

            editViewInteractionAction = new AsyncViewInteractionAction(CreateItemEditView);
            bindingSet.Bind().For(v => v.editViewInteractionAction).To(vm => vm.ItemEditRequest);

            bindingSet.Build();

            Start();
        }

        private void Start()
        {
            viewModel.SelectItem(0);
        }

        private UniTask<UIBase> CreateItemEditView()
        {
            ListItemEditView editView = CreateWidget<ListItemEditView>(m_itemEditView);
            return UniTask.FromResult<UIBase>(editView);
        }
    }
}