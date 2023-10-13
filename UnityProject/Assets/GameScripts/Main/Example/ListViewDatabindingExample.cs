using Cysharp.Threading.Tasks;
using Framework.Binding;
using Framework.Binding.Builder;
using Framework.Binding.Contexts;
using Framework.Contexts;
using Framework.Views.InteractionActions;
using TEngine;
using UnityEngine.UI;

namespace Framework.Tutorials
{
    public class ListViewDatabindingExample : UIWindow
    {
        private ListViewViewModel viewModel;

        public Button addButton;

        public Button removeButton;

        public Button clearButton;

        public Button changeIconButton;

        public Button changeItems;

        public ListView listView;

        public ListItemDetailView detailView;

        public ListItemEditView editView;

        private AsyncViewInteractionAction editViewInteractionAction;

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

            BindingSet<ListViewDatabindingExample, ListViewViewModel> bindingSet = this.CreateBindingSet<ListViewDatabindingExample, ListViewViewModel>();
            bindingSet.Bind(this.listView).For(v => v.Items).To(vm => vm.Items).OneWay();
            bindingSet.Bind(this.detailView).For(v => v.Item).To(vm => vm.SelectedItem);
            bindingSet.Bind(this.addButton).For(v => v.onClick).To(vm => vm.AddItem);
            bindingSet.Bind(this.removeButton).For(v => v.onClick).To(vm => vm.RemoveItem);
            bindingSet.Bind(this.clearButton).For(v => v.onClick).To(vm => vm.ClearItem);
            bindingSet.Bind(this.changeIconButton).For(v => v.onClick).To(vm => vm.ChangeItemIcon);
            bindingSet.Bind(this.changeItems).For(v => v.onClick).To(vm => vm.ChangeItems);
            
            editViewInteractionAction = new AsyncViewInteractionAction(CreateItemEditView);
            bindingSet.Bind().For(v => v.editViewInteractionAction).To(vm => vm.ItemEditRequest);

            bindingSet.Build();

            Start();
        }

        void Start()
        {
            viewModel.SelectItem(0);
        }

        private UniTask<UIBase> CreateItemEditView()
        {
            return CreateWidgetByPathAsync<ListItemEditView>(transform, nameof(ListItemEditView));
        }
    }
}