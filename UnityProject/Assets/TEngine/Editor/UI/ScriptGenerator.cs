using UnityEditor;

namespace TEngine.Editor.UI
{
    public class ScriptGenerator
    {
        [MenuItem("GameObject/ScriptGenerator/UIProperty", priority = 41)]
        public static void MemberProperty()
        {
            PrefabErgodic.Generate(false);
        }
        
        [MenuItem("GameObject/ScriptGenerator/UIPropertyAndListener", priority = 42)]
        public static void MemberPropertyAndListener()
        {
            PrefabErgodic.Generate(true);
        }

        [MenuItem("GameObject/ScriptGenerator/UIProperty - UniTask", priority = 43)]
        public static void MemberPropertyUniTask()
        {
            PrefabErgodic.Generate(false, true);
        }

        [MenuItem("GameObject/ScriptGenerator/UIPropertyAndListener - UniTask", priority = 44)]
        public static void MemberPropertyAndListenerUniTask()
        {
            PrefabErgodic.Generate(true, true);
        }
    }
}