using System;
using CharacterSystem;
using Lunari.Tsuki.Editor.Extenders;
using UnityEditor;
using UnityEngine;
using Types = Lunari.Tsuki.Runtime.Types;

[CustomEditor(typeof(Character2D))]
public class Character2DEditor : Editor {
    
    private SerializedObject m_sourceRef;
    private SerializedProperty m_actions;
    private TypeSelectorButton m_button;

    private void OnEnable() {
        m_sourceRef = serializedObject;
        m_actions = m_sourceRef.FindProperty("m_actions");
        m_button = TypeSelectorButton.Of<CharacterAction>(new GUIContent("Add action"), OnTypeSelected);
    }

    private void OnTypeSelected(Type type) {
        m_actions.arraySize++;
        var element = m_actions.GetArrayElementAtIndex(m_actions.arraySize - 1);
        element.managedReferenceValue = Activator.CreateInstance(type);
        
        
        m_sourceRef.ApplyModifiedProperties();
    }

    public override void OnInspectorGUI() {
        
        DisplayProperties();
        m_sourceRef.ApplyModifiedProperties();
    }

    void DisplayProperties() {
        var properties = m_sourceRef.GetIterator().GetChildren();
        foreach (var property in properties) {
            if (SerializedProperty.EqualContents(property, m_actions)) {
                m_button.OnInspectorGUI();
                for (int i = 0; i < m_actions.arraySize; i++) {
                    var element = m_actions.GetArrayElementAtIndex(i);
                    EditorGUILayout.PropertyField(element, new GUIContent(element.displayName), true);
                }                
            }
            else {
                EditorGUILayout.PropertyField(property);
            }
        }
    }
}
