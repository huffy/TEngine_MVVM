
using Framework.Binding;
using TEngine;
using UnityEngine;
using UnityEngine.UI;

namespace Framework.Tutorials
{
    public class ListItemDetailView : UIWidget
    {
        public GameObject panel;
        public Text title;
        public Text price;
        public Image image;

        public ListItemViewModel Item
        {
            get { return (ListItemViewModel)transform.GetDataContext(); }
            set { transform.SetDataContext(value); }
        }

        public override void OnCreate()
        {
            base.OnCreate();
            var bindingSet = this.CreateBindingSet<ListItemDetailView, ListItemViewModel>();
            bindingSet.Bind(panel).For(v => v.activeSelf).To(vm => vm.IsSelected);
            bindingSet.Bind(title).For(v => v.text).To(vm => vm.Title);
            bindingSet.Bind(image).For(v => v.sprite).To(vm => vm.Icon).WithSprite().OneWay();
            bindingSet.Bind(this.price).For(v => v.text).ToExpression(vm => string.Format("${0:0.00}", vm.Price)).OneWay();
            bindingSet.Build();
        }
    }
}
