using System.Text;
using UnityEngine;

public class GenerateScriptLogic
{
    private const string Gap = "/";
    
    private static string GetRelativePath(Transform child, Transform root)
    {
        StringBuilder path = new StringBuilder();
        path.Append(child.name);
        while (child.parent != null && child.parent != root)
        {
            child = child.parent;
            path.Insert(0, Gap);
            path.Insert(0, child.name);
        }

        return path.ToString();
    }

    public static string GetBtnFuncName(string varName)
    {
        return "OnClick" + varName.Replace("m_btn", string.Empty) + "Btn";
    }

    public static string GetToggleFuncName(string varName)
    {
        return "OnToggle" + varName.Replace("m_toggle", string.Empty) + "Change";
    }

    public static string GetSliderFuncName(string varName)
    {
        return "OnSlider" + varName.Replace("m_slider", string.Empty) + "Change";
    }
    
    public static string GetInputFieldFuncName(string varName)
    {
        return "OnInputField" + varName.Replace("m_inputField", string.Empty) + "Change";
    }
    
    public static string GetDropdownFuncName(string varName)
    {
        return "OnDropdown" + varName.Replace("m_dropdown", string.Empty) + "Change";
    }
    
    public static string GetScrollbarFuncName(string varName)
    {
        return "OnScrollbar" + varName.Replace("m_scrollbar", string.Empty) + "Change";
    }
    
    public static string GetScrollRectFuncName(string varName)
    {
        return "OnScrollRect" + varName.Replace("m_scrollRect", string.Empty) + "Change";
    }

    public static void WriteScript(Transform root, Transform child, ref StringBuilder strVar,
        ref StringBuilder strBind, ref StringBuilder strOnCreate)
    {
        string varName = child.name;
        string componentName = string.Empty;

        var rule = SettingsUtils.GetScriptGenerateRule().Find(t => varName.StartsWith(t.uiElementRegex));

        if (rule != null)
        {
            componentName = rule.componentName;
        }

        if (componentName == string.Empty)
        {
            return;
        }

        string varPath = GetRelativePath(child, root);
        if (!string.IsNullOrEmpty(varName))
        {
            switch (componentName)
            {
                case "Transform":
                    strVar.Append("\t\tprivate " + componentName + " " + varName + ";\n");
                    strBind.Append($"\t\t\t{varName} = FindChild(\"{varPath}\");\n");
                    break;
                case "GameObject":
                    strVar.Append("\t\tprivate " + componentName + " " + varName + "Go;\n");
                    strBind.Append($"\t\t\t{varName}Go = FindChild(\"{varPath}\").gameObject;\n");
                    break;
                case "AnimationCurve":
                    strVar.Append("\t\tprivate " + componentName + " " + varName + ";\n");
                    strBind.Append(
                        $"\t\t\t{varName} = FindChildComponent<AnimCurveObject>(\"{varPath}\").m_animCurve;\n");
                    break;
                default:
                    strVar.Append("\t\tprivate " + componentName + " " + varName + ";\n");
                    strBind.Append($"\t\t\t{varName} = FindChildComponent<{componentName}>(\"{varPath}\");\n");
                    break;
            }

            if (componentName == "Button")
            {
                string varFuncName = GetBtnFuncName(varName);
                strOnCreate.Append("\n");
                strOnCreate.Append("\t\t\tbindingSet.Bind(" + varName + ").For(v => v.onClick).To(vm => vm." + varFuncName +
                                   ");\n");
            }
            else if (componentName == "Toggle")
            {
                string varFuncName = GetToggleFuncName(varName);
                strOnCreate.Append("\n");
                strOnCreate.Append("\t\t\tbindingSet.Bind(" + varName + ").For(v => v.onValueChanged).To<bool>(vm => vm." +
                                   varFuncName + ");\n");
            }
            else if (componentName == "Slider")
            {
                string varFuncName = GetSliderFuncName(varName);
                strOnCreate.Append("\n");
                strOnCreate.Append("\t\t\tbindingSet.Bind(" + varName + ").For(v => v.onValueChanged).To<float>(vm => vm." +
                                   varFuncName + ");\n");
            }
            else if (componentName == "InputField")
            {
                string varFuncName = GetInputFieldFuncName(varName);
                strOnCreate.Append("\n");
                strOnCreate.Append("\t\t\tbindingSet.Bind(" + varName + ").For(v => v.onValueChanged).To<string>(vm => vm." +
                                   varFuncName + ");\n");
            }
            else if (componentName == "Text")
            {
                strOnCreate.Append("\n");
                strOnCreate.Append("\t\t\tbindingSet.Bind(" + varName + ").For(v => v.text).To(vm => vm." +
                                   varName.Replace(rule.uiElementRegex, "") + ");\n");
            }
            else if (componentName == "Image")
            {
                strOnCreate.Append("\n");
                strOnCreate.Append("\t\t\tbindingSet.Bind(" + varName + ").For(v => v.sprite).To(vm => vm." +
                                   varName.Replace(rule.uiElementRegex, "") + ").WithSprite();\n");
            }
            else if (componentName == "RawImage")
            {
                strOnCreate.Append("\n");
                strOnCreate.Append("\t\t\tbindingSet.Bind(" + varName + ").For(v => v.texture).To(vm => vm." +
                                   varName.Replace(rule.uiElementRegex, "") + ").WithTexture();\n");
            }
            else if (componentName == "Dropdown")
            {
                string varFuncName = GetDropdownFuncName(varName);
                strOnCreate.Append("\n");
                strOnCreate.Append("\t\t\tbindingSet.Bind(" + varName + ").For(v => v.onValueChanged).To<int>(vm => vm." +
                                   varFuncName + ");\n");
            }
            else if (componentName == "Scrollbar")
            {
                string varFuncName = GetScrollbarFuncName(varName);
                strOnCreate.Append("\n");
                strOnCreate.Append("\t\t\tbindingSet.Bind(" + varName + ").For(v => v.onValueChanged).To<float>(vm => vm." +
                                   varFuncName + ");\n");
            }
            else if (componentName == "ScrollRect")
            {
                string varFuncName = GetScrollRectFuncName(varName);
                strOnCreate.Append("\n");
                strOnCreate.Append("\t\t\tbindingSet.Bind(" + varName + ").For(v => v.onValueChanged).To<Vector2>(vm => vm." +
                                   varFuncName + ");\n");
            }
        }
    }
    
    public static void WriteScriptViewModel(Transform child, ref StringBuilder strVar, ref StringBuilder strOnCreate,
        ref StringBuilder strCallback)
    {
        string varName = child.name;
        string componentName = string.Empty;

        var rule = SettingsUtils.GetScriptGenerateRule().Find(t => varName.StartsWith(t.uiElementRegex));

        if (rule != null)
        {
            componentName = rule.componentName;
        }

        if (componentName == string.Empty)
        {
            return;
        }

        if (!string.IsNullOrEmpty(varName))
        {
            if (componentName == "Button")
            {
                string varFuncName = GetBtnFuncName(varName);
                strCallback.Append($"\t\tpublic void {varFuncName}()\n");
                strCallback.Append("\t\t{\n\t\t}\n");
            }
            else if (componentName == "Toggle")
            {
                string varFuncName = GetToggleFuncName(varName);
                strCallback.Append($"\t\tpublic void {varFuncName}(bool isToggle)\n");
                strCallback.Append("\t\t{\n\t\t}\n");
            }
            else if (componentName == "Slider")
            {
                string varFuncName = GetSliderFuncName(varName);
                strCallback.Append($"\t\tpublic void {varFuncName}(float value)\n");
                strCallback.Append("\t\t{\n\t\t}\n");
            }
            else if (componentName == "InputField")
            {
                string varFuncName = GetInputFieldFuncName(varName);
                strCallback.Append($"\t\tpublic void {varFuncName}(string text)\n");
                strCallback.Append("\t\t{\n\t\t}\n");
            }
            else if (componentName == "Text" || componentName == "Image" || componentName == "RawImage")
            {
                strOnCreate.Append("\n");
                strVar.Append("\t\tpublic string " +  varName.Replace(rule.uiElementRegex, "") + "\n");
                strVar.Append("\t\t{\n");
                strVar.Append("\t\t\tget { return _model." + varName.Replace(rule.uiElementRegex, "") + "; }\n");
                strVar.Append("\t\t\tset { this.Set(ref _model." + varName.Replace(rule.uiElementRegex, "") + ", value); }\n");
                strVar.Append("\t\t}\n");
                
            }
            else if (componentName == "Dropdown")
            {
                string varFuncName = GetDropdownFuncName(varName);
                strCallback.Append($"\t\tpublic void {varFuncName}(int value)\n");
                strCallback.Append("\t\t{\n\t\t}\n");
            }
            else if (componentName == "Scrollbar")
            {
                string varFuncName = GetScrollbarFuncName(varName);
                strCallback.Append($"\t\tpublic void {varFuncName}(float progress)\n");
                strCallback.Append("\t\t{\n\t\t}\n");
            }
            else if (componentName == "ScrollRect")
            {
                string varFuncName = GetScrollRectFuncName(varName);
                strCallback.Append($"\t\tpublic void {varFuncName}(Vecter2 valueDelta)\n");
                strCallback.Append("\t\t{\n\t\t}\n");
            }
        }
    }
        
    public static void WriteScriptModel(Transform child, ref StringBuilder strVar)
    {
        string varName = child.name;
        string componentName = string.Empty;

        var rule = SettingsUtils.GetScriptGenerateRule().Find(t => varName.StartsWith(t.uiElementRegex));

        if (rule != null)
        {
            componentName = rule.componentName;
        }

        if (componentName == string.Empty)
        {
            return;
        }

        if (!string.IsNullOrEmpty(varName))
        {
            if (componentName == "Text" || componentName == "Image" || componentName == "RawImage")
            {
                strVar.Append("\t\tpublic string " + varName.Replace(rule.uiElementRegex, "") + ";\n");
            }
        }
    }
}
