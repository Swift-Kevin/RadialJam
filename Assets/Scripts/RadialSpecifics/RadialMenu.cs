using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public struct SegmentInfo
{
    public Gradient colors;

    [Seperator]
    [Range(5f, 500f)]
    public float innerRadius;
    [Range(5f, 500f)]
    public float outerRadius;
    [Range(3, 128)]
    public int numTris;
    [Range(0f, 1f)]
    public float initialFill;

    [Seperator]
    public UnityEvent customCallback;

    [Seperator]
    public Sprite overlaySprite;
    [Range(-250f, 250f)]
    public float spriteOffset;
}

public class RadialMenu : MonoBehaviour
{
    Camera menuCamera;
    public RadialInputs inputs;
    public GameObject prefabSegment;

    [Seperator]
    public List<SegmentInfo> segmentInfos = new List<SegmentInfo>();

    [SerializeField, Range(0f, 360f), Tooltip("Total arc to spread all segments across (e.g. 360 = full circle).")]
    private float totalArcAngle = 360f;
    [SerializeField, Range(0f, 360f), Tooltip("Where the radial menu starts (0 = top, 90 = right, 180 = bottom, etc.).")]
    private float startAngle = 0f;
    private List<RadialSegment> segments = new List<RadialSegment>();

    public List<RadialSegment> Segments => segments;
    private Vector2 center = new Vector2(Screen.width / 2f, Screen.height / 2f);

    private void Awake()
    {
        inputs = new RadialInputs();
        CreateSegments();
    }

    private void Start()
    {
        menuCamera = GameManager.Instance.GameCamera;
        UpdateSegments();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < segments.Count; i++)
            {
                if (ClickCheck(segments[i]))
                {
                    segmentInfos[i].customCallback?.Invoke();
                }
            }
        }
    }

    bool ClickCheck(RadialSegment _segment)
    {
        float angle = CalculateMouseAngle(menuCamera, Vector2.up);
        float distance = Vector2.Distance(center, Input.mousePosition);
        bool inRangeAngle = IsAngleInRange(angle, _segment.StartAngle, _segment.StartAngle + _segment.SegmentAngle);
        bool inRangePosition = distance > _segment.InnerRadius && distance < _segment.OuterRadius;

        return inRangeAngle && inRangePosition;
    }

    bool IsAngleInRange(float angle, float _entry, float _exit)
    {
        angle = NormalizeAngle(angle);
        _entry = NormalizeAngle(_entry);
        _exit = NormalizeAngle(_exit);

        if (_entry < _exit)
        {
            return angle >= _entry && angle <= _exit;
        }
        else
        {
            return angle >= _entry || angle <= _exit;
        }
    }

    float NormalizeAngle(float angle)
    {
        angle %= 360f;
        return angle < 0 ? angle + 360f : angle;
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

    private void CreateSegments()
    {
        for (int i = 0; i < segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }
        segments.Clear();

        for (int i = 0; i < segmentInfos.Count; i++)
        {
            RadialSegment comp = Instantiate(prefabSegment, transform).GetComponent<RadialSegment>();

            comp.SetSprite(segmentInfos[i].overlaySprite);
            comp.UpdateSegmentInfo(segmentInfos[i]);

            segments.Add(comp);
        }
    }

    private void Reset()
    {
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
            segment.UpdateSpritePosition();
            segment.SetVerticesDirty();
        }
    }

    public void InspectorButton(bool v)
    {
        UpdateSegmentsInfo();
    }

    public void UpdateSegmentsInfo()
    {
        for (int i = 0; i < segments.Count; i++)
        {
            segments[i].UpdateSegmentInfo(segmentInfos[i]);
        }
    }
}