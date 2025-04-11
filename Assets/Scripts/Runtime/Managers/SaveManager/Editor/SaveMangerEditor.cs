using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SaveManager))]
public class SaveMangerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("SET DATA"))
        {
            SaveManager saveManager = (SaveManager)target;
            var saveSystem = saveManager.GetComponent<ISaveSystem>();
            saveSystem.SetData();
        }

        if (GUILayout.Button("RESET DATA"))
        {
            SaveManager saveManager = (SaveManager)target;
            var saveSystem = saveManager.GetComponent<ISaveSystem>();
            saveSystem.ResetData();
        }

    }
}
