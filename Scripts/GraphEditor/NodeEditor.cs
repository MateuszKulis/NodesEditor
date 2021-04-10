using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace Portfolio.GraphEditor
{
    public class NodeEditor : Node
    {
        public string guiID;

        public string dialogueText;

        public bool entryPoint = false;
    }
}
