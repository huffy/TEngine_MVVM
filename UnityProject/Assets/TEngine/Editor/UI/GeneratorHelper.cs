using UnityEditor;
using UnityEngine;

namespace TEngine.Editor.UI
{
    public class GeneratorHelper : EditorWindow
    {
        [MenuItem("GameObject/ScriptGenerator/About", priority = 49)]
        public static void About()
        {
            GeneratorHelper welcomeWindow =
                (GeneratorHelper) EditorWindow.GetWindow(typeof(GeneratorHelper), false, "About");
        }

        public void Awake()
        {
            minSize = new Vector2(400, 600);
        }

        protected void OnGUI()
        {
            GUILayout.BeginVertical();
            foreach (var item in SettingsUtils.GetScriptGenerateRule())
            {
                GUILayout.Label(item.uiElementRegex + "：\t" + item.componentName);
            }

            GUILayout.EndVertical();
        }
    }
}