using UnityEngine;
using UnityEngine.UI;
using TEngine;

using Framework.Binding;

namespace GameLogic
{
	public class Template : UIWidget
	{
		#region 脚本工具生成的代码
		public TemplateViewModel ViewModel
		{
			get => (TemplateViewModel)transform.GetDataContext(); 
			set => transform.SetDataContext(value); 
		}
		private GameObject m_goBorderGo;
		private Button m_btnPanel;
		private Image m_imgIcon;
		private Text m_textTitle;
		private Text m_textPrice;
		private Button m_btnButton;
		public override void ScriptGenerator()
		{
			base.ScriptGenerator();
			var bindingSet = this.CreateBindingSet<Template, TemplateViewModel>();
			m_goBorderGo = FindChild("m_goBorder").gameObject;
			m_btnPanel = FindChildComponent<Button>("Panel/m_btnPanel");
			m_imgIcon = FindChildComponent<Image>("Panel/m_btnPanel/m_imgIcon");
			m_textTitle = FindChildComponent<Text>("Panel/m_btnPanel/m_textTitle");
			m_textPrice = FindChildComponent<Text>("Panel/m_btnPanel/m_textPrice");
			m_btnButton = FindChildComponent<Button>("Panel/m_btnButton");

			bindingSet.Bind(m_btnPanel).For(v => v.onClick).To(vm => vm.OnClickPanelBtn);

			bindingSet.Bind(m_imgIcon).For(v => v.sprite).To(vm => vm.Icon).WithSprite();

			bindingSet.Bind(m_textTitle).For(v => v.text).To(vm => vm.Title);

			bindingSet.Bind(m_textPrice).For(v => v.text).To(vm => vm.Price);

			bindingSet.Bind(m_btnButton).For(v => v.onClick).To(vm => vm.OnClickButtonBtn);
			bindingSet.Build();
		}
		#endregion

}
	}
