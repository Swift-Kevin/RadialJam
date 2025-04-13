using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[Serializable]
public struct SegmentInfo
{
    public Color startColor;
    public Color endColor;

    [Seperator]
    [Range(5f, 500f)]
    public float ringThickness;
    [Range(3, 128)]
    public int numTris;
	[Range(0f, 1f)]
	public float initialFill;

	[Seperator]
    public EventTrigger.TriggerEvent customCallback;
}

public class RadialMenu : MonoBehaviour
{
    public Camera menuCamera;
    public RadialInputs inputs;
    public GameObject prefabSegment;

    [Seperator]
    public List<SegmentInfo> segmentInfos = new List<SegmentInfo>();

/*    [Range(1, 25), SerializeField, Tooltip("Number of Segments to display.")]
    private int numSegments = 1;*/
    [SerializeField, Range(0f, 360f), Tooltip("Total arc to spread all segments across (e.g. 360 = full circle).")]
    private float totalArcAngle = 360f;
    [SerializeField, Range(0f, 360f), Tooltip("Where the radial menu starts (0 = top, 90 = right, 180 = bottom, etc.).")]
    private float startAngle = 0f;
    private List<RadialSegment> segments = new List<RadialSegment>();

    public List<RadialSegment> Segments
    {
        get
        {
            return segments;
        }
    }

	private void Awake()
    {
        inputs = new RadialInputs();


		CreateSegments();
	}

    // Update is called once per frame
    void Update()
    {
        float angle = CalculateMouseAngle(menuCamera, Vector2.up);
        //Debug.Log($"Mouse Angle: {angle}");
    }

    //returns the angle
    public float CalculateMouseAngle(Camera relativeCamera, Vector2 menuStartDir)
    {
        Vector3 mousePosition = inputs.MousePosition;
        //Debug.Log($"{inputs.MousePosition}");
        Vector3 cameraPosition = relativeCamera.transform.position;
        mousePosition.z = 10.0f;
        mousePosition = relativeCamera.ScreenToWorldPoint(mousePosition) - cameraPosition;

        Vector2 mouseVecRelToSliderCenterVec = mousePosition;//Convert to a vector 2

        Vector3 crossProduct = Vector3.Cross(mouseVecRelToSliderCenterVec, menuStartDir);
        float dotProduct = Vector2.Dot(menuStartDir, mouseVecRelToSliderCenterVec);

        float startDirMag = menuStartDir.magnitude;
        float mouseVecMag = mouseVecRelToSliderCenterVec.magnitude;

        float val = dotProduct / (startDirMag * mouseVecMag);
        float angleDegrees = Mathf.Rad2Deg * Mathf.Acos(val);

        return (crossProduct.z < 0) ? (180 - angleDegrees) + 180 : angleDegrees;

    }

    private void Start()
    {

	}

    private void CreateSegments()
    {
		if(Application.isPlaying)
		{
			for(int i = 0; i < segments.Count; i++)
			{
				Destroy(segments[i].gameObject);
			}
			segments.Clear();

			for(int i = 0; i < segmentInfos.Count; i++)
			{
				RadialSegment comp = Instantiate(prefabSegment, transform).GetComponent<RadialSegment>();

				comp.UpdateSegmentInfo(segmentInfos[i]);

				segments.Add(comp);
			}
			UpdateSegments();
		}
	}

    //private void OnValidate()
    //{
    //    if (Application.isPlaying)
    //    {
    //        Debug.Log("TEST");
    //        for (int i = 0; i < segments.Count; i++)
    //        {
    //            segments[i].UpdateSegmentInfo(segmentInfos[i]);
    //        }
    //    }
    //}

    private void Reset()
    {
        Debug.Log("Reset Called");
        segments.Clear();
        segments.AddRange(GetComponentsInChildren<RadialSegment>());
        UpdateSegments();
    }

    public void UpdateSegments()
    {
        if (segments == null || segments.Count == 0)
            return;

        float anglePerSegment = totalArcAngle / segments.Count;

        for (int i = 0; i < segments.Count; i++)
        {
            var segment = segments[i];
            if (segment == null) continue;

            segment.SetArcAngle(anglePerSegment);
            segment.SetStartAngle(startAngle + i * anglePerSegment);
            segment.SetVerticesDirty();
        }
    }

    public void InspectorButton(bool v)
    {

    }
}