using Framework.Binding;
using TEngine;
using UnityEngine.UI;

namespace Framework.Tutorials
{
    public class ListItemEditView : UIWidget
    {
        #region 脚本工具生成的代码
        private Image m_imgIcon;
        private Text m_textTitle;
        private Text m_textPrice;
        private Slider m_slider;
        private Button m_btnChangeIconButton;
        private Button m_btnCancelButton;
        private Button m_btnSubmitButton;
        public override void ScriptGenerator()
        {
            m_imgIcon = FindChildComponent<Image>("m_imgIcon");
            m_textTitle = FindChildComponent<Text>("m_textTitle");
            m_textPrice = FindChildComponent<Text>("m_textPrice");
            m_slider = FindChildComponent<Slider>("m_slider");
            m_btnChangeIconButton = FindChildComponent<Button>("m_btnChangeIconButton");
            m_btnCancelButton = FindChildComponent<Button>("m_btnCancelButton");
            m_btnSubmitButton = FindChildComponent<Button>("m_btnSubmitButton");
        }
        #endregion
        public ListItemEditViewModel ViewModel
        {
            get { return (ListItemEditViewModel)transform.GetDataContext(); }
            set { transform.SetDataContext(value); }
        }

        public override void OnCreate()
        {
            var bindingSet = this.CreateBindingSet<ListItemEditView, ListItemEditViewModel>();
            bindingSet.Bind(m_textTitle).For(v => v.text).To(vm => vm.Title);
            bindingSet.Bind(m_textPrice).For(v => v.text).ToExpression(vm => string.Format("${0:0.00}", vm.Price)).OneWay();
            bindingSet.Bind(m_slider).For(v => v.value, v => v.onValueChanged).To(vm => vm.Price).TwoWay();
            bindingSet.Bind(m_imgIcon).For(v => v.sprite).To(vm => vm.Icon).WithSprite().OneWay();
            bindingSet.Bind(m_btnChangeIconButton).For(v => v.onClick).To(vm => vm.OnChangeIcon);
            bindingSet.Build();

            m_btnCancelButton.onClick.AddListener(Cancel);
            m_btnSubmitButton.onClick.AddListener(Submit);
        }

        private void Cancel()
        {            
            this.ViewModel.Cancelled = true;
            this.gameObject.SetActive(false);
            transform.SetDataContext(null);
        }

        private void Submit()
        {            
            this.ViewModel.Cancelled = false;
            ViewModel.Submit();
            this.gameObject.SetActive(false);
            transform.SetDataContext(null);
        }
    }
}