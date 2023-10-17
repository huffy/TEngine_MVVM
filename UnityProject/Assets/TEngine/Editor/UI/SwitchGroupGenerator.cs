using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace TEngine.Editor.UI
{
    public class SwitchGroupGenerator
    {
        private const string Condition = "m_switchGroup";

        public static readonly SwitchGroupGenerator Instance = new SwitchGroupGenerator();

        public string Process(Transform root)
        {
            var sbd = new StringBuilder();
            var list = new List<Transform>();
            Collect(root, list);
            foreach (var node in list)
            {
                sbd.AppendLine(Process(root, node)).AppendLine();
            }

            return sbd.ToString();
        }

        public void Collect(Transform node, List<Transform> nodeList)
        {
            if (node.name.StartsWith(Condition))
            {
                nodeList.Add(node);
                return;
            }

            var childCnt = node.childCount;
            for (var i = 0; i < childCnt; i++)
            {
                var child = node.GetChild(i);
                Collect(child, nodeList);
            }
        }

        private string Process(Transform root, Transform groupTf)
        {
            var parentPath = GetPath(root, groupTf);
            var name = groupTf.name;
            var sbd = new StringBuilder(@"
                        var _namePath = ""#parentPath"";
                        var _nameTf = FindChild(_namePath);
                        var childCnt = _nameTf.childCount;
                        SwitchTabItem[] _name;
                        _name = new SwitchTabItem[childCnt];
                        for (var i = 0; i < childCnt; i++)
                        {
                            var child = _nameTf.GetChild(i);
                            _name[i] = CreateWidget<SwitchTabItem>(_namePath + ""/"" + child.name);
                        }");
            sbd.Replace("_name", name);
            sbd.Replace("#parentPath", parentPath);
            return sbd.ToString();
        }

        public string GetPath(Transform root, Transform childTf)
        {
            if (childTf == null)
            {
                return string.Empty;
            }

            if (childTf == root)
            {
                return childTf.name;
            }

            var parentPath = GetPath(root, childTf.parent);
            if (parentPath == string.Empty)
            {
                return childTf.name;
            }

            return parentPath + "/" + childTf.name;
        }
    }
}