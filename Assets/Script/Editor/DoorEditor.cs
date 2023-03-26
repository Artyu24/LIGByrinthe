using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(Door))]
public class DoorEditor : Editor
{
    private Door myObject = null;

    private void OnEnable()
    {
        myObject = (Door) target;
    }

    public override void OnInspectorGUI()
    {
        SerializedObject soDoor = new SerializedObject(myObject);
        soDoor.Update();

        SerializedProperty firstPos = soDoor.FindProperty("firstPos");
        SerializedProperty firstRot = soDoor.FindProperty("firstRot");
        SerializedProperty secondPos = soDoor.FindProperty("secondPos");
        SerializedProperty secondRot = soDoor.FindProperty("secondRot");

        EditPosDrawer("First", firstPos, firstRot);
        EditorGUILayout.LabelField("------------------------------------------------------------");
        EditPosDrawer("Second", secondPos, secondRot);

        GameObject buttonPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/DoNotTouch/Button.prefab");
        if (buttonPrefab == null)
        {
            EditorGUILayout.HelpBox("MISSING BUTTON PREFAB", MessageType.Error);
            soDoor.ApplyModifiedProperties();
            return;
        }

        EditorGUILayout.LabelField("");
        EditorGUILayout.LabelField("");
        EditorGUILayout.LabelField("To create a Button, click here");

        ColorDrawer(Color.green, Color.white);
        if (GUILayout.Button("Instantiate Button"))
        {
            Button prefabTemp = Instantiate(buttonPrefab).GetComponent<Button>();
            prefabTemp.door = myObject;
        }

        soDoor.ApplyModifiedProperties();
    }

    private void EditPosDrawer(string position, SerializedProperty posProperty, SerializedProperty rotProperty)
    {
        ColorDrawer(Color.red, Color.white);
        if (GUILayout.Button("Edit Value " + position + " Pos"))
        {
            posProperty.vector3Value = myObject.transform.position;
            rotProperty.vector3Value = myObject.transform.eulerAngles;
        }

        ColorDrawer(Color.red, Color.white);
        GUI.enabled = false;
        EditorGUILayout.Vector3Field(position + " Position :", posProperty.vector3Value);
        EditorGUILayout.Vector3Field(position + " Rotation :", rotProperty.vector3Value);
        GUI.enabled = true;

        ColorDrawer(Color.cyan, Color.white);
        if (GUILayout.Button("Test " + position + " Pos"))
        {
            myObject.transform.position = posProperty.vector3Value;
            myObject.transform.eulerAngles = rotProperty.vector3Value;
        }
    }

    private void ColorDrawer(Color bg, Color text)
    {
        GUI.backgroundColor = bg;
        GUI.contentColor = text;
    }
}
