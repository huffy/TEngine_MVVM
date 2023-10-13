using Framework.Binding;
using TEngine;
using UnityEngine.UI;

namespace Framework.Tutorials
{
    public class ListItemEditView : UIWidget
    {
        public Text title;
        public Text price;
        public Slider priceSlider;
        public Button changeIcon;
        public Image image;
        public Button submit;
        public Button cancel;

        public ListItemEditViewModel ViewModel
        {
            get { return (ListItemEditViewModel)transform.GetDataContext(); }
            set { transform.SetDataContext(value); }
        }

        public override void OnCreate()
        {
            var bindingSet = this.CreateBindingSet<ListItemEditView, ListItemEditViewModel>();
            bindingSet.Bind(title).For(v => v.text).To(vm => vm.Title);
            bindingSet.Bind(price).For(v => v.text).ToExpression(vm => string.Format("${0:0.00}", vm.Price)).OneWay();
            bindingSet.Bind(priceSlider).For(v => v.value, v => v.onValueChanged).To(vm => vm.Price).TwoWay();
            bindingSet.Bind(image).For(v => v.sprite).To(vm => vm.Icon).WithSprite().OneWay();
            bindingSet.Bind(changeIcon).For(v => v.onClick).To(vm => vm.OnChangeIcon);
            bindingSet.Build();

            this.cancel.onClick.AddListener(Cancel);
            this.submit.onClick.AddListener(Submit);
        }

        private void Cancel()
        {            
            this.ViewModel.Cancelled = true;
            this.gameObject.SetActive(false);
            //this.Visibility = false;
            transform.SetDataContext(null);
        }

        private void Submit()
        {            
            this.ViewModel.Cancelled = false;
            ViewModel.Submit();
            this.gameObject.SetActive(false);
            //this.Visibility = false;           
            transform.SetDataContext(null);
        }
    }

}