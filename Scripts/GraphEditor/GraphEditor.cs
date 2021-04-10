using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;

namespace Portfolio.GraphEditor
{

    public class GraphEditor : EditorWindow
    {
        private GraphViewEditor _graphView;

        [MenuItem("Graph/Dialogue Graph")]
        public static void OpenGraphWindow()
        {
            var window = GetWindow<GraphEditor>();
            window.titleContent = new GUIContent(text: "Dialogue Graph");
        }

        private void OnEnable()
        {
            ConstructGraphView();
            GenerateToolbar();
        }

        private void ConstructGraphView()
        {
            _graphView = new GraphViewEditor
            {
                name = "Dialogue Graph"
            };

            _graphView.StretchToParentSize();
            rootVisualElement.Add(_graphView);
        }

        private void GenerateToolbar()
        {
            var toolbar = new Toolbar();

            var nodeCreateButton = new Button(clickEvent: () =>
            {
                _graphView.CreateNode("Dialogue Node");
            });

            nodeCreateButton.text = "Create Node:";
            toolbar.Add(nodeCreateButton);

            rootVisualElement.Add(toolbar);

        }

        private void OnDisable()
        {
            rootVisualElement.Remove(_graphView);
        }
    }
}
