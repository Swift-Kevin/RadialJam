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

[CustomEditor(typeof(RadialMenu))]
public class RadialMenuEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        RadialMenu _script = target as RadialMenu;

        //if (GUILayout.Button("Add Segment"))
        //{
        //    _script.InspectorButton(true);
        //}

        _script.UpdateSegmentsInfo();

        serializedObject.ApplyModifiedProperties();
    }
}
