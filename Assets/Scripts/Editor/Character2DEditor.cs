using System;
using CharacterSystem;
using Lunari.Tsuki.Editor.Extenders;
using UnityEditor;
using UnityEngine;
using Types = Lunari.Tsuki.Runtime.Types;

[CustomEditor(typeof(Character2D))]
public class Character2DEditor : Editor {
    
    private SerializedObject m_sourceRef;
    private SerializedProperty m_actions, m_input;
    private TypeSelectorButton m_actionsButton, m_inputButton;

    private void OnEnable() {
        m_sourceRef = serializedObject;
        m_actions = m_sourceRef.FindProperty("m_actions");
        m_input = m_sourceRef.FindProperty("m_input");
        m_actionsButton = TypeSelectorButton.Of<CharacterAction>(new GUIContent("Add action"), OnActionTypeSelected);
        m_inputButton = TypeSelectorButton.Of<InputSource>(new GUIContent("Input source"), OnInputTypeSelected);
    }

    private void OnInputTypeSelected(Type type) {
        m_input.managedReferenceValue = Activator.CreateInstance(type);
        m_input.serializedObject.ApplyModifiedProperties();
        Undo.RegisterCreatedObjectUndo(target, "Input type selected");
    }

    private void OnActionTypeSelected(Type type) {
        m_actions.arraySize++;
        var element = m_actions.GetArrayElementAtIndex(m_actions.arraySize - 1);
        element.managedReferenceValue = Activator.CreateInstance(type);
        m_actions.serializedObject.ApplyModifiedProperties();
        Undo.RegisterCreatedObjectUndo(target, "Action type selected");
    }

    public override void OnInspectorGUI() {
        
        DisplayProperties();
        m_sourceRef.ApplyModifiedProperties();
    }

    void DisplayProperties() {

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(m_input.type);
        m_inputButton.OnInspectorGUI();
        EditorGUILayout.EndHorizontal();
        
        m_actionsButton.OnInspectorGUI();
        for (int i = 0; i < m_actions.arraySize; i++) {
            var element = m_actions.GetArrayElementAtIndex(i);
            EditorGUILayout.PropertyField(element, new GUIContent(element.type), true);
        }
    }
}
