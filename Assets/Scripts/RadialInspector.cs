using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RadialSegment))]
public class RadialSegmentEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        RadialSegment _script = target as RadialSegment;

        if (GUILayout.Button("Inspector Debug"))
        {
            _script.InspectorButton();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
