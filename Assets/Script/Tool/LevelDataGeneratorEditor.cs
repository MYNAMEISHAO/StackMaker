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

        if (GUILayout.Button("Generate Level"))
        {
            generator.GenerateLevel();
        }

        if(GUILayout.Button("Save Level"))
        {
            generator.SaveToJson();
        }
    }
}
