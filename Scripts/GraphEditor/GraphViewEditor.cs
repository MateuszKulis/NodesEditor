using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System;

namespace Portfolio.GraphEditor
{
    public class GraphViewEditor : GraphView
    {

        private readonly Vector2 deafultSize = new Vector2(x:100,y:150);

        public GraphViewEditor()
        {
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            AddElement(GenerateEntryPointNode());
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            var compatiblePorts = new List<Port>();

            ports.ForEach(funcCall: (port) =>
            {
                if (startPort != port && startPort.node != port.node)
                    compatiblePorts.Add(port);
            });

            return compatiblePorts;
        }

        private Port GeneratePort(NodeEditor node, Direction portDirection, Port.Capacity capacity = Port.Capacity.Single)
        {
            return node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(float));
        }

        private NodeEditor GenerateEntryPointNode()
        {
            var node = new NodeEditor
            {
                title = "Start",
                guiID = Guid.NewGuid().ToString(),
                dialogueText = "Entrypoint",
                entryPoint = true
            };

            var generatedPort = GeneratePort(node, Direction.Output);
            generatedPort.portName = "Next";
            node.outputContainer.Add(generatedPort);

            node.RefreshExpandedState();
            node.RefreshPorts();

            node.SetPosition(new Rect(x: 100, y: 200, width: 100, height: 150));


            return node;
        }

        public void CreateNode(string nodeName)
        {
            AddElement(CreateDialogueNode(nodeName));
        }

        private NodeEditor CreateDialogueNode(string nodeName)
        {
            var dialogueNode = new NodeEditor
            {
                title = nodeName,
                dialogueText = nodeName,
                guiID = Guid.NewGuid().ToString()
            };

            var inputPort = GeneratePort(dialogueNode, Direction.Input, Port.Capacity.Multi);
            inputPort.portName = "Input";
            dialogueNode.inputContainer.Add(inputPort);

            var button = new Button(clickEvent: () => { AddChoicePort(dialogueNode);});
            button.text = "New Choice";
            dialogueNode.titleContainer.Add(button);


            dialogueNode.RefreshExpandedState();
            dialogueNode.RefreshPorts();
            dialogueNode.SetPosition(new Rect(position:Vector2.zero, deafultSize));

            return dialogueNode;
        }

        private void AddChoicePort(NodeEditor nodeEditor)
        {
            var generatedPort = GeneratePort(nodeEditor, Direction.Output);

            var outputPortCount = nodeEditor.outputContainer.Query(name: "connector").ToList().Count;
            generatedPort.portName = $"Choice {outputPortCount}";

            nodeEditor.outputContainer.Add(generatedPort);
            nodeEditor.RefreshPorts();
            nodeEditor.RefreshExpandedState();
        }

    }

}
