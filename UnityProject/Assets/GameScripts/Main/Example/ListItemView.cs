
using Framework.Binding;
using Framework.Binding.Builder;
using TEngine;
using UnityEngine;
using UnityEngine.UI;

namespace Framework.Tutorials
{
    public class ListItemView : UIWidget
    {
        public Text title;
        public Text price;
        public Image image;
        public GameObject border;
        public Button selectButton;
        public Button clickButton;

        public override void OnCreate()
        {
            BindingSet<ListItemView, ListItemViewModel> bindingSet = this.CreateBindingSet<ListItemView, ListItemViewModel>();
            bindingSet.Bind(this.title).For(v => v.text).To(vm => vm.Title).OneWay();
            bindingSet.Bind(this.image).For(v => v.sprite).To(vm => vm.Icon).WithSprite().OneWay();
            bindingSet.Bind(this.price).For(v => v.text).ToExpression(vm => string.Format("${0:0.00}", vm.Price)).OneWay();
            bindingSet.Bind(this.border).For(v => v.activeSelf).To(vm => vm.IsSelected).OneWay();
            bindingSet.Bind(this.selectButton).For(v => v.onClick).To(vm => vm.SelectCommand).CommandParameter(transform.GetDataContext);
            bindingSet.Bind(this.clickButton).For(v => v.onClick).To(vm => vm.ClickCommand).CommandParameter(transform.GetDataContext);
            bindingSet.Build();
        }
    }
}
