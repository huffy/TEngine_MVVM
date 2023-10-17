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
            Ergodic(root, root, ref strVar, ref strBind, ref strOnCreate, isUniTask);
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
                strFile.Append("using Framework.Binding;\n");
                strFile.Append("using Framework.Binding.Builder;\n");
                strFile.Append("using Framework.Binding.Contexts;\n");
                strFile.Append("using Framework.Contexts;\n");
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
            strFile.Append($"\t\tprivate {root.name}ViewModel _viewModel;\n");
            strFile.Append(strVar);
            strFile.Append("\t\tpublic override void ScriptGenerator()\n");
            strFile.Append("\t\t{\n");
            strFile.Append("\t\t\tbase.ScriptGenerator();\n");
            strFile.Append($"\t\t\t_viewModel = new {root.name}ViewModel();\n");
            strFile.Append("\t\t\tIBindingContext bindingContext = transform.BindingContext();\n");
            strFile.Append("\t\t\tbindingContext.DataContext = _viewModel;\n");
            strFile.Append("\t\t\tBindingSet<" + root.name + ", " + root.name + "ViewModel> bindingSet = this.CreateBindingSet<" + root.name + ", " + root.name + "ViewModel>();\n");
            strFile.Append(strBind);
            strFile.Append(strOnCreate);
            strFile.Append("\t\t\tbindingSet.Build();\n");
            strFile.Append("\t\t}\n");
            strFile.Append("\t\t#endregion");

            if (includeListener)
            {
                strFile.Append("\n\n");

                strFile.Append("\t}\n");
                strFile.Append("}\n");
                SaveScript(root.name, strFile.ToString());
                GenerateViewModel(root);
                GenerateModel(root);
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
            Ergodic(root, root, ref strVar, ref strBind, ref strOnCreate, isUniTask);
            StringBuilder strFile = new StringBuilder();
            string widgetName = root.name.Replace("m_item", "");
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
            strFile.Append("using Framework.Binding;\n\n");
            strFile.Append($"namespace {SettingsUtils.GetUINameSpace()}\n");
            strFile.Append("{\n");
            strFile.Append("\tpublic class " + widgetName + " : UIWidget\n");
            strFile.Append("\t{\n");

            // 脚本工具生成的代码
            strFile.Append("\t\t#region 脚本工具生成的代码\n");
            strFile.Append("\t\tpublic " + widgetName + "ViewModel ViewModel\n");
            strFile.Append("\t\t{\n");
            strFile.Append("\t\t\tget => (" + widgetName + "ViewModel)transform.GetDataContext(); \n");
            strFile.Append("\t\t\tset => transform.SetDataContext(value); \n");
            strFile.Append("\t\t}\n");
            strFile.Append(strVar);
            strFile.Append("\t\tpublic override void ScriptGenerator()\n");
            strFile.Append("\t\t{\n");
            strFile.Append("\t\t\tbase.ScriptGenerator();\n");
            strFile.Append("\t\t\tvar bindingSet = this.CreateBindingSet<" + widgetName + ", " + widgetName + "ViewModel>();\n");
            strFile.Append(strBind);
            strFile.Append(strOnCreate);
            strFile.Append("\t\t\tbindingSet.Build();\n");
            strFile.Append("\t\t}\n");
            strFile.Append("\t\t#endregion");

            strFile.Append("\n\n");
            strFile.Append("}\n");
            strFile.Append("\t}\n");
            SaveScript(widgetName, strFile.ToString());
            return widgetName;
        }

        return "";
    }

     public static string GenerateViewModel(Transform root)
     {
         if (root != null)
         {
             StringBuilder strVar = new StringBuilder();
             StringBuilder strOnCreate = new StringBuilder();
             StringBuilder strCallback = new StringBuilder();
             ErgodicViewModel(root, ref strVar,  ref strOnCreate, ref strCallback);
             StringBuilder strFile = new StringBuilder();
             string widgetName = root.name.Replace("m_item", "");
             strFile.Append("using Framework.ViewModels;\n");
             strFile.Append("using UnityEngine;\n");
             strFile.Append($"namespace {SettingsUtils.GetUINameSpace()}\n");
             strFile.Append("{\n");
             strFile.Append("\tpublic class " + widgetName + "ViewModel : ViewModelBase\n");
             strFile.Append("\t{\n");

             // 脚本工具生成的代码
             strFile.Append("\t\t#region 脚本工具生成的代码\n");
             strFile.Append($"\t\tprivate {widgetName}Model _model;\n");
             strFile.Append(strVar);
             strFile.Append($"\t\tpublic {widgetName}ViewModel ()\n");
             strFile.Append("\t\t{\n"); 
             strFile.Append($"\t\t\t_model = new {widgetName}Model();\n");
             strFile.Append("\t\t}\n");
             strFile.Append("\t\t#endregion");

             strFile.Append("\n\n");
             // #region 事件
             strFile.Append("\t\t#region 事件\n");
             strFile.Append(strCallback);
             strFile.Append("\t\t#endregion\n\n");
             strFile.Append("\t}\n");
             strFile.Append("}\n");
             SaveScript(widgetName + "ViewModel", strFile.ToString());

             return widgetName + "ViewModel";
         }

         return "";
     }
     
     public static void GenerateModel(Transform root)
     {
         if (root != null)
         {
             StringBuilder strVar = new StringBuilder();
             ErgodicModel(root, ref strVar);
             StringBuilder strFile = new StringBuilder();
             string widgetName = root.name.Replace("m_item", "");
             strFile.Append("using UnityEngine;\n");
             strFile.Append($"namespace {SettingsUtils.GetUINameSpace()}\n");
             strFile.Append("{\n");
             strFile.Append("\tpublic class " + widgetName + "Model\n");
             strFile.Append("\t{\n");

             // 脚本工具生成的代码
             strFile.Append("\t\t#region 脚本工具生成的代码\n");
             strFile.Append(strVar);

             strFile.Append("\t\t#endregion");

             strFile.Append("\n\n");

             strFile.Append("\t}\n");
             strFile.Append("}\n");
             SaveScript(widgetName + "Model", strFile.ToString());
         }
     }

     private static void Ergodic(Transform root, Transform transform, ref StringBuilder strVar,
        ref StringBuilder strBind, ref StringBuilder strOnCreate, bool isUniTask)
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            Transform child = transform.GetChild(i);
            GenerateScriptLogic.WriteScript(root, child, ref strVar, ref strBind, ref strOnCreate);
            if (child.name.StartsWith("m_item"))
            {
                // 子 Item 不再往下遍历
                string componentName = GenerateWidget(child, isUniTask);
                string varName = "m_item" + componentName;
                strVar.Append("\t\tprivate " + componentName + " " + varName + ";\n");
                strBind.Append($"\t\t\t{varName} = CreateWidget<{componentName}>({child.name}Go);\n");
                strOnCreate.Append($"\t\t\tbindingSet.Bind({varName}).For(v => v.ViewModel).To(vm => vm.M_{componentName}ViewModel);\n");
            
                continue;
            }

            Ergodic(root, child, ref strVar, ref strBind, ref strOnCreate, isUniTask);
        }
    }
     
     private static void ErgodicViewModel(Transform transform, ref StringBuilder strVar, ref StringBuilder strOnCreate,
         ref StringBuilder strCallback)
     {
         for (int i = 0; i < transform.childCount; ++i)
         {
             Transform child = transform.GetChild(i);
             GenerateScriptLogic.WriteScriptViewModel(child, ref strVar, ref strOnCreate, ref strCallback);
             if (child.name.StartsWith("m_item"))
             {
                 string componentName = GenerateViewModel(child);
                 string varName = "M_" + componentName;
                 GenerateModel(child);
                 strVar.Append("\t\tpublic " + componentName + " " + varName + ";\n");
                 continue;
             }

             ErgodicViewModel(child, ref strVar, ref strOnCreate, ref strCallback);
         }
     }
     
     private static void ErgodicModel(Transform transform, ref StringBuilder strVar)
     {
         for (int i = 0; i < transform.childCount; ++i)
         {
             Transform child = transform.GetChild(i);
             GenerateScriptLogic.WriteScriptModel(child, ref strVar);
             if (child.name.StartsWith("m_item"))
             {
                 continue;
             }

             ErgodicModel(child, ref strVar);
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
