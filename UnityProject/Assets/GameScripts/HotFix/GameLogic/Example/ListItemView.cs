
using Framework.Binding;
using Framework.Binding.Builder;
using TEngine;
using UnityEngine;
using UnityEngine.UI;

namespace Framework.Tutorials
{
    public class ListItemView : UIWidget
    {
        #region 脚本工具生成的代码
        private GameObject m_goBorder;
        private Button m_btnPanel;
        private Image m_imgIcon;
        private Text m_textTitle;
        private Text m_textPrice;
        private Button m_btnButton;
        public override void ScriptGenerator()
        {
            m_goBorder = FindChild("m_goBorder").gameObject;
            m_btnPanel = FindChildComponent<Button>("Panel/m_btnPanel");
            m_imgIcon = FindChildComponent<Image>("Panel/m_btnPanel/m_imgIcon");
            m_textTitle = FindChildComponent<Text>("Panel/m_btnPanel/m_textTitle");
            m_textPrice = FindChildComponent<Text>("Panel/m_btnPanel/m_textPrice");
            m_btnButton = FindChildComponent<Button>("Panel/m_btnButton");
        }
        #endregion

        public override void OnCreate()
        {
            BindingSet<ListItemView, ListItemViewModel> bindingSet = this.CreateBindingSet<ListItemView, ListItemViewModel>();
            bindingSet.Bind(this.m_textTitle).For(v => v.text).To(vm => vm.Title).OneWay();
            bindingSet.Bind(this.m_imgIcon).For(v => v.sprite).To(vm => vm.Icon).WithSprite().OneWay();
            bindingSet.Bind(this.m_textPrice).For(v => v.text).ToExpression(vm => string.Format("${0:0.00}", vm.Price)).OneWay();
            bindingSet.Bind(this.m_goBorder).For(v => v.activeSelf).To(vm => vm.IsSelected).OneWay();
            bindingSet.Bind(this.m_btnPanel).For(v => v.onClick).To(vm => vm.SelectCommand).CommandParameter(transform.GetDataContext);
            bindingSet.Bind(this.m_btnButton).For(v => v.onClick).To(vm => vm.ClickCommand).CommandParameter(transform.GetDataContext);
            bindingSet.Build();
        }
    }
}
