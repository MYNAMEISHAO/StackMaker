using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelDataGenerator))]
public class LevelDataGeneratorEditor : Editor
{
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LevelDataGenerator generator =
            (LevelDataGenerator)target;


        if(GUILayout.Button("Save Level"))
        {
            if(generator.levelSaveName == null)
            {
                Debug.LogError("Level Save Name cannot be empty!");
                return;
            }
            generator.SaveLevel();
        }


        EditorGUILayout.Space();
        if (GUILayout.Button("Load Level"))
        {
            generator.ConvertDataToScene();
        }

        EditorGUILayout.Space();
        if (GUILayout.Button("Clear Scene"))
        {
            generator.ClearScene();
        }

    }
}
