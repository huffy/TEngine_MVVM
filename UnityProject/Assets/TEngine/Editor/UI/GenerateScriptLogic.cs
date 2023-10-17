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

    public static void WriteScript(Transform root, Transform child, ref StringBuilder strVar,
        ref StringBuilder strBind, ref StringBuilder strOnCreate,
        ref StringBuilder strCallback, bool isUniTask)
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
            strVar.Append("\t\tprivate " + componentName + " " + varName + ";\n");
            switch (componentName)
            {
                case "Transform":
                    strBind.Append($"\t\t\t{varName} = FindChild(\"{varPath}\");\n");
                    break;
                case "GameObject":
                    strBind.Append($"\t\t\t{varName} = FindChild(\"{varPath}\").gameObject;\n");
                    break;
                case "AnimationCurve":
                    strBind.Append(
                        $"\t\t\t{varName} = FindChildComponent<AnimCurveObject>(\"{varPath}\").m_animCurve;\n");
                    break;
                case "RichItemIcon":
                case "CommonFightWidget":
                case "PlayerHeadWidget":
                    strBind.Append($"\t\t\t{varName} = CreateWidgetByType<{componentName}>(\"{varPath}\");\n");
                    break;
                case "RedNoteBehaviour":
                case "TextButtonItem":
                case "SwitchTabItem":
                case "UIActorWidget":
                case "UIEffectWidget":
                case "UISpineWidget":
                    strBind.Append($"\t\t\t{varName} = CreateWidget<{componentName}>(\"{varPath}\");\n");
                    break;
                case "ActorNameBinderText":
                    strBind.Append($"\t\t\t{varName} = FindTextBinder(\"{varPath}\");\n");
                    break;
                case "ActorNameBinderEffect":
                    strBind.Append($"\t\t\t{varName} = FindEffectBinder(\"{varPath}\");\n");
                    break;
                default:
                    strBind.Append($"\t\t\t{varName} = FindChildComponent<{componentName}>(\"{varPath}\");\n");
                    break;
            }

            if (componentName == "Button")
            {
                string varFuncName = GetBtnFuncName(varName);
                if (isUniTask)
                {
                    strOnCreate.Append($"\t\t\t{varName}.onClick.AddListener(UniTask.UnityAction({varFuncName}));\n");
                    strCallback.Append($"\t\tprivate async UniTaskVoid {varFuncName}()\n");
                    strCallback.Append("\t\t{\n await UniTask.Yield();\n\t\t}\n");
                }
                else
                {
                    strOnCreate.Append($"\t\t\t{varName}.onClick.AddListener({varFuncName});\n");
                    strCallback.Append($"\t\tprivate void {varFuncName}()\n");
                    strCallback.Append("\t\t{\n\t\t}\n");
                }
            }
            else if (componentName == "Toggle")
            {
                string varFuncName = GetToggleFuncName(varName);
                strOnCreate.Append($"\t\t\t{varName}.onValueChanged.AddListener({varFuncName});\n");
                strCallback.Append($"\t\tprivate void {varFuncName}(bool isOn)\n");
                strCallback.Append("\t\t{\n\t\t}\n");
            }
            else if (componentName == "Slider")
            {
                string varFuncName = GetSliderFuncName(varName);
                strOnCreate.Append($"\t\t\t{varName}.onValueChanged.AddListener({varFuncName});\n");
                strCallback.Append($"\t\tprivate void {varFuncName}(float value)\n");
                strCallback.Append("\t\t{\n\t\t}\n");
            }
        }
    }
}
