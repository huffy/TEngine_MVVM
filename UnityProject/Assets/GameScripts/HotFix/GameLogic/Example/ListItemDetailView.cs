
using Framework.Binding;
using TEngine;
using UnityEngine;
using UnityEngine.UI;

namespace Framework.Tutorials
{
    public class ListItemDetailView : UIWidget
    {
        #region 脚本工具生成的代码
        private GameObject m_goItemDetail;
        private Image m_imgIcon;
        private Text m_textTitle;
        private Text m_textPrice;
        public override void ScriptGenerator()
        {
            m_goItemDetail = FindChild("m_goItemDetail").gameObject;
            m_imgIcon = FindChildComponent<Image>("m_goItemDetail/m_imgIcon");
            m_textTitle = FindChildComponent<Text>("m_goItemDetail/m_textTitle");
            m_textPrice = FindChildComponent<Text>("m_goItemDetail/m_textPrice");
        }
        #endregion

        public ListItemViewModel Item
        {
            get { return (ListItemViewModel)transform.GetDataContext(); }
            set { transform.SetDataContext(value); }
        }

        public override void OnCreate()
        {
            base.OnCreate();
            var bindingSet = this.CreateBindingSet<ListItemDetailView, ListItemViewModel>();
            bindingSet.Bind(m_goItemDetail).For(v => v.activeSelf).To(vm => vm.IsSelected);
            bindingSet.Bind(m_textTitle).For(v => v.text).To(vm => vm.Title);
            bindingSet.Bind(m_imgIcon).For(v => v.sprite).To(vm => vm.Icon).WithSprite().OneWay();
            bindingSet.Bind(m_textPrice).For(v => v.text).ToExpression(vm => string.Format("${0:0.00}", vm.Price)).OneWay();
            bindingSet.Build();
        }
    }
}
