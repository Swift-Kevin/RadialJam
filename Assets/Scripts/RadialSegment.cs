using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasRenderer))]
public class RadialSegment : Graphic
{
    [Seperator]
    [Header("Appearance Settings")]

    [Tooltip("Thickness of the ring (from inner radius to outer radius).")]
    [SerializeField, Range(5f, 500f)]
    private float ringThickness = 40f;

    [Tooltip("Total angle the arc can span (e.g. 360 = full circle, 180 = half circle).")]
    [SerializeField, Range(0f, 360f)]
    private float arcSpanAngle = 360f;

    [Tooltip("Starting angle of the arc in degrees (0 = Up, 90 = Right, 180 = Down, 270 = Left).")]
    [SerializeField, Range(0f, 360f)]
    private float startAngle = 0f;

    [Tooltip("Number of segments used to draw the arc (more = smoother curve).")]
    [SerializeField, Range(3, 128)]
    private int segmentCount = 64;

    [Seperator]
    [Header("Colors")]
    [Tooltip("Starting Color")]
    [SerializeField]
    private Color startColor = Color.white;

    [Tooltip("Ending Color")]
    [SerializeField]
    private Color endColor = Color.black;

    [Seperator]
    [Header("Fill Control")]

    [Tooltip("Current fill amount of the arc (0 = empty, 1 = full arc).")]
    [SerializeField, Range(0f, 1f)]
    private float fillPercent = 1f;

    // Functions Below
    public void InspectorButton()
    {
        SetVerticesDirty();
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        Debug.Log("test");
        base.OnPopulateMesh(vh);
        vh.Clear(); // clear the vertex helper

        RectTransform rectTransform = GetComponent<RectTransform>();

        float arcRad = Mathf.Deg2Rad * arcSpanAngle * fillPercent;
        float radius = Mathf.Min(rectTransform.rect.width, rectTransform.rect.height) * 0.5f;
        float angleOffsetRad = Mathf.Deg2Rad * startAngle;

        Vector2 center = Vector2.one * 0.5f - rectTransform.pivot;
        center = new Vector2(center.x * radius * 2, center.y * radius * 2);
        UIVertex vertex = UIVertex.simpleVert;
        vertex.color = Color.white;
        float deltaAngle = arcRad / segmentCount;
        int vertexCount = segmentCount * 2;

        float angleStep = arcRad / segmentCount;
        int stepsToDraw = Mathf.CeilToInt(segmentCount * fillPercent) + 1;
        int offset = 0;

        for(int i = 0; i < stepsToDraw; i++)
        {
            float angle = angleOffsetRad + i * angleStep;
            float percent = (float)i / (stepsToDraw - 1);
            Color gradientColor = Color.Lerp(startColor, endColor, percent);
            vertex.color = gradientColor;

            // bottom right corner
            vertex.position = new Vector2(Mathf.Sin(angle) * (radius - ringThickness), Mathf.Cos(angle) * (radius - ringThickness)) + center;
            vh.AddVert(vertex);

            // top right corner
            vertex.position = new Vector2(Mathf.Sin(angle) * radius, Mathf.Cos(angle) * radius) + center;
            vh.AddVert(vertex);
            
            offset = i * 2;
            if (i < stepsToDraw - 1)
            {
                vh.AddTriangle(offset + 0, offset + 1, offset + 3);
                vh.AddTriangle(offset + 3, offset + 2, offset + 0);
            }
        }
    }
}