using UnityEngine;
using UnityEngine.UI;
using TEngine;

using Framework.Binding;

namespace GameLogic
{
	public class DetailPanel : UIWidget
	{
		#region 脚本工具生成的代码
		public DetailPanelViewModel ViewModel
		{
			get => (DetailPanelViewModel)transform.GetDataContext(); 
			set => transform.SetDataContext(value); 
		}
		private GameObject m_goItemDetailGo;
		private Image m_imgIcon;
		private Text m_textTitle;
		private Text m_textPrice;
		public override void ScriptGenerator()
		{
			base.ScriptGenerator();
			var bindingSet = this.CreateBindingSet<DetailPanel, DetailPanelViewModel>();
			m_goItemDetailGo = FindChild("m_goItemDetail").gameObject;
			m_imgIcon = FindChildComponent<Image>("m_goItemDetail/m_imgIcon");
			m_textTitle = FindChildComponent<Text>("m_goItemDetail/m_textTitle");
			m_textPrice = FindChildComponent<Text>("m_goItemDetail/m_textPrice");
			
			bindingSet.Bind(m_goItemDetailGo).For(v => v.activeSelf).To(vm => vm.IsSelected);
			bindingSet.Bind(m_imgIcon).For(v => v.sprite).To(vm => vm.Icon).WithSprite();

			bindingSet.Bind(m_textTitle).For(v => v.text).To(vm => vm.Title);

			bindingSet.Bind(m_textPrice).For(v => v.text).To(vm => vm.Price);
			bindingSet.Build();
		}
		#endregion

}
	}
