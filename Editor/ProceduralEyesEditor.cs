using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(ProceduralEyes))]
public class ProceduralEyesEditor : Editor
{
    private ReorderableList list;

    private void OnEnable()
    {
        list = new ReorderableList(serializedObject,
                serializedObject.FindProperty("Eyes"),
                true, true, true, true);

        list.drawElementCallback =
    (Rect rect, int index, bool isActive, bool isFocused) => {
        var element = list.serializedProperty.GetArrayElementAtIndex(index);
        rect.y += 2;
        EditorGUI.PropertyField(
            new Rect(rect.x, rect.y, rect.width - 30, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("eye"), GUIContent.none);
        EditorGUI.PropertyField(
            new Rect(rect.x + rect.width - 30, rect.y, 30, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("radius"), GUIContent.none);
    };
    }

    public override void OnInspectorGUI()
    {
        ProceduralEyes myTarget = (ProceduralEyes)target;
        myTarget.preview = EditorGUILayout.Toggle("Preview", myTarget.preview);
        myTarget.maxZFar = EditorGUILayout.FloatField("Max Z Far", myTarget.maxZFar);

        serializedObject.Update();
        list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}