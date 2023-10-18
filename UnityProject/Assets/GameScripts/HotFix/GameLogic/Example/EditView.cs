using UnityEngine.UI;
using TEngine;

using Framework.Binding;
using Framework.Commands;

namespace GameLogic
{
	public class EditView : UIWidget
	{
		private SimpleCommand<bool> _visualCommand; 
		
		#region 脚本工具生成的代码

		public EditViewViewModel ViewModel
		{
			get => (EditViewViewModel) transform.GetDataContext();
			set => transform.SetDataContext(value);
		}

		private Image m_imgIcon;
		private Text m_textTitle;
		private Text m_textPrice;
		private Slider m_slider;
		private Button m_btnChangeIconButton;
		private Button m_btnCancelButton;
		private Button m_btnSubmitButton;

		public override void ScriptGenerator()
		{
			base.ScriptGenerator();
			_visualCommand = new SimpleCommand<bool>(OnVisualChange);
			var bindingSet = this.CreateBindingSet<EditView, EditViewViewModel>();
			m_imgIcon = FindChildComponent<Image>("m_imgIcon");
			m_textTitle = FindChildComponent<Text>("m_textTitle");
			m_textPrice = FindChildComponent<Text>("m_textPrice");
			m_slider = FindChildComponent<Slider>("m_slider");
			m_btnChangeIconButton = FindChildComponent<Button>("m_btnChangeIconButton");
			m_btnCancelButton = FindChildComponent<Button>("m_btnCancelButton");
			m_btnSubmitButton = FindChildComponent<Button>("m_btnSubmitButton");

			bindingSet.Bind(m_imgIcon).For(v => v.sprite).To(vm => vm.Icon).WithSprite();

			bindingSet.Bind(m_textTitle).For(v => v.text).To(vm => vm.Title);
			bindingSet.Bind().For(v => v._visualCommand).To(vm => vm.VisualCommand).OneWayToSource();
			bindingSet.Bind(m_textPrice).For(v => v.text).To(vm => vm.Price);
			bindingSet.Bind(m_slider).For(v => v.onValueChanged).To<float>(vm => vm.OnSliderChange);
			bindingSet.Bind(m_btnChangeIconButton).For(v => v.onClick).To(vm => vm.OnClickChangeIconButtonBtn);
			bindingSet.Bind(m_btnCancelButton).For(v => v.onClick).To(vm => vm.OnClickCancelButtonBtn);
			bindingSet.Bind(m_btnSubmitButton).For(v => v.onClick).To(vm => vm.OnClickSubmitButtonBtn);
			bindingSet.Build();
		}

		#endregion

		private void OnVisualChange(bool active)
		{
			gameObject.SetActive(active);
		}
	}
}
