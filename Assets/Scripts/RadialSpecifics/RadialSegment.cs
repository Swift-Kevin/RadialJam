using System;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.HableCurve;

[RequireComponent(typeof(CanvasRenderer))]
public class RadialSegment : Graphic
{
    [Tooltip("Current fill amount of the arc (0 = empty, 1 = full arc).")]
    [SerializeField, Range(0f, 1f)]
    private float fillPercent = 1f;

    private Color startColor = Color.white;
    private Color endColor = Color.black;
    private int numTris = 64;
    private float ringThickness = 40f;
    private float segmentAngle = 360f;
    private float startAngle = 0f;

    public float SegmentAngle => segmentAngle;
    public float StartAngle => startAngle;
    public float Thickness => ringThickness;

    // other info needed elsewhere
    private Rect rectHandle;
    public Rect RectHandle => rectHandle;

    public float OuterRadius => Mathf.Min(rectHandle.width, rectHandle.height) * 0.5f;
    public float InnerRadius => OuterRadius - ringThickness;


    protected override void Start()
    {
        rectHandle = GetComponent<RectTransform>().rect;
    }

    // Functions Below
    public void InspectorButton()
    {
        SetVerticesDirty();
    }

    protected override void OnPopulateMesh(VertexHelper _vh)
    {
        Debug.Log("test");
        base.OnPopulateMesh(_vh);
        _vh.Clear(); // clear the vertex helper

        RectTransform rectTransform = GetComponent<RectTransform>();

        float arcRad = Mathf.Deg2Rad * segmentAngle * fillPercent;
        float radius = Mathf.Min(rectTransform.rect.width, rectTransform.rect.height) * 0.5f;
        float angleOffsetRad = Mathf.Deg2Rad * startAngle;

        Vector2 center = Vector2.one * 0.5f - rectTransform.pivot;
        center = new Vector2(center.x * radius * 2, center.y * radius * 2);
        UIVertex vertex = UIVertex.simpleVert;
        vertex.color = Color.white;
        float deltaAngle = arcRad / numTris;
        int vertexCount = numTris * 2;

        float angleStep = arcRad / numTris;
        int stepsToDraw = Mathf.CeilToInt(numTris * fillPercent) + 1;
        int offset = 0;

        for(int i = 0; i < stepsToDraw; i++)
        {
            float angle = angleOffsetRad + i * angleStep;
            float percent = (float)i / (stepsToDraw - 1);
            Color gradientColor = Color.Lerp(startColor, endColor, percent);
            vertex.color = gradientColor;

            // bottom right corner
            vertex.position = new Vector2(Mathf.Sin(angle) * (radius - ringThickness), Mathf.Cos(angle) * (radius - ringThickness)) + center;
            _vh.AddVert(vertex);

            // top right corner
            vertex.position = new Vector2(Mathf.Sin(angle) * radius, Mathf.Cos(angle) * radius) + center;
            _vh.AddVert(vertex);
            
            offset = i * 2;
            if (i < stepsToDraw - 1)
            {
                _vh.AddTriangle(offset + 0, offset + 1, offset + 3);
                _vh.AddTriangle(offset + 3, offset + 2, offset + 0);
            }
        }
    }

    public void SetArcAngle(float _segmentAngle)
    {
        segmentAngle = _segmentAngle;
    }

    public void UpdateSegmentInfo(SegmentInfo _info)
    {
        numTris = _info.numTris;
        ringThickness = _info.ringThickness;
        endColor = _info.endColor;
        startColor = _info.startColor;
        fillPercent = _info.initialFill;
        SetVerticesDirty();
    }

    public void SetStartAngle(float _angle)
    {
        startAngle = _angle;
    }

	public void SetFill(float _fill)
	{
		fillPercent = _fill;
		SetVerticesDirty();
	}
}