using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class PrefabErgodic
{
    public static void Generate(bool includeListener, bool isUniTask = false)
    {
        var root = Selection.activeTransform;
        if (root != null)
        {
            StringBuilder strVar = new StringBuilder();
            StringBuilder strBind = new StringBuilder();
            StringBuilder strOnCreate = new StringBuilder();
            StringBuilder strCallback = new StringBuilder();
            Ergodic(root, root, ref strVar, ref strBind, ref strOnCreate, ref strCallback, isUniTask);
            StringBuilder strFile = new StringBuilder();

            if (includeListener)
            {
#if ENABLE_TEXTMESHPRO
                    strFile.Append("using TMPro;\n");
#endif
                if (isUniTask)
                {
                    strFile.Append("using Cysharp.Threading.Tasks;\n");
                }

                strFile.Append("using UnityEngine;\n");
                strFile.Append("using UnityEngine.UI;\n");
                strFile.Append("using TEngine;\n\n");
                strFile.Append($"namespace {SettingsUtils.GetUINameSpace()}\n");
                strFile.Append("{\n");
                strFile.Append("\t[Window(UILayer.UI)]\n");
                strFile.Append("\tpublic class " + root.name + " : UIWindow\n");
                strFile.Append("\t{\n");
            }

            // 脚本工具生成的代码
            strFile.Append("\t\t#region 脚本工具生成的代码\n");
            strFile.Append(strVar);
            strFile.Append("\t\tpublic override void ScriptGenerator()\n");
            strFile.Append("\t\t{\n");
            strFile.Append(strBind);
            strFile.Append(strOnCreate);
            strFile.Append("\t\t}\n");
            strFile.Append("\t\t#endregion");

            if (includeListener)
            {
                strFile.Append("\n\n");
                // #region 事件
                strFile.Append("\t\t#region 事件\n");
                strFile.Append(strCallback);
                strFile.Append("\t\t#endregion\n\n");

                strFile.Append("\t}\n");
                strFile.Append("}\n");
                SaveScript(root.name, strFile.ToString());
            }
            else
            {
                TextEditor te = new TextEditor();
                te.text = strFile.ToString();
                te.SelectAll();
                te.Copy();
            }
        }
    }
    
     private static string GenerateWidget(Transform root, bool isUniTask = false)
    {
        if (root != null)
        {
            StringBuilder strVar = new StringBuilder();
            StringBuilder strBind = new StringBuilder();
            StringBuilder strOnCreate = new StringBuilder();
            StringBuilder strCallback = new StringBuilder();
            Ergodic(root, root, ref strVar, ref strBind, ref strOnCreate, ref strCallback, isUniTask);
            StringBuilder strFile = new StringBuilder();
            string widgetName = root.name.Replace("m_item", "") + "ItemWidget";
#if ENABLE_TEXTMESHPRO
                    strFile.Append("using TMPro;\n");
#endif
            if (isUniTask)
            {
                strFile.Append("using Cysharp.Threading.Tasks;\n");
            }
            
            strFile.Append("using UnityEngine;\n");
            strFile.Append("using UnityEngine.UI;\n");
            strFile.Append("using TEngine;\n\n");
            strFile.Append($"namespace {SettingsUtils.GetUINameSpace()}\n");
            strFile.Append("{\n");
            strFile.Append("\tpublic class " + widgetName + " : UIWidget\n");
            strFile.Append("\t{\n");

            // 脚本工具生成的代码
            strFile.Append("\t\t#region 脚本工具生成的代码\n");
            strFile.Append(strVar);
            strFile.Append("\t\tpublic override void ScriptGenerator()\n");
            strFile.Append("\t\t{\n");
            strFile.Append(strBind);
            strFile.Append(strOnCreate);
            strFile.Append("\t\t}\n");
            strFile.Append("\t\t#endregion");

            strFile.Append("\n\n");
            // #region 事件
            strFile.Append("\t\t#region 事件\n");
            strFile.Append(strCallback);
            strFile.Append("\t\t#endregion\n\n");

            strFile.Append("\t}\n");
            strFile.Append("}\n");
         
            SaveScript(widgetName, strFile.ToString());
            return widgetName;
        }

        return "";
    }

    private static void Ergodic(Transform root, Transform transform, ref StringBuilder strVar,
        ref StringBuilder strBind, ref StringBuilder strOnCreate,
        ref StringBuilder strCallback, bool isUniTask)
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            Transform child = transform.GetChild(i);
            GenerateScriptLogic.WriteScript(root, child, ref strVar, ref strBind, ref strOnCreate, ref strCallback,
                isUniTask);
            if (child.name.StartsWith("m_item"))
            {
                // 子 Item 不再往下遍历
                string componentName = GenerateWidget(child, isUniTask);
                string varName = "m_item" + componentName;
                strVar.Append("\t\tprivate " + componentName + " " + varName + ";\n");
                strBind.Append($"\t\t\t{varName} = CreateWidget<{componentName}>(\"{child.name}\");\n");
                continue;
            }

            Ergodic(root, child, ref strVar, ref strBind, ref strOnCreate, ref strCallback, isUniTask);
        }
    }

    private static void SaveScript(string prefabName, string content)
    {
        string folderPath = Application.dataPath + "/Scripts/UI/Generated/";
        if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

        StreamWriter writer;
        var t = new FileInfo(folderPath + prefabName + ".cs");

        if (t.Exists) t.Delete();

        writer = t.CreateText();

        writer.Write(content);
        writer.Close();
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
