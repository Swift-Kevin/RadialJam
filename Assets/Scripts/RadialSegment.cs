using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasRenderer))]
public class RadialSegment : Graphic
{
    // Member fields
    [SerializeField] private float thickness = 40;

    private Vector3 position;
    private float curveAmt;
    private float segmentWidth;
    private float arc;

    // Don't Change Values
    private const int divisions = 64;

    // Functions Below
    public void InspectorButton()
    {
        Debug.Log("Clicked!");
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        base.OnPopulateMesh(vh);
        vh.Clear(); // clear the vertex helper

        RectTransform rectTransform = GetComponent<RectTransform>();
        float majorRadius = rectTransform.rect.width * 0.5f;
        float minorRadius = rectTransform.rect.height * 0.5f;

        Vector2 center = Vector2.one * 0.5f - rectTransform.pivot;
        center = new Vector2(center.x * majorRadius * 2, center.y * minorRadius * 2);
        UIVertex vertex = UIVertex.simpleVert;
        float deltaAngle = Mathf.PI * 2 / divisions;
        int vertexCount = divisions * 2;

        for (int i = 0; i < divisions; i++)
        {
            float angle = i * deltaAngle;
            // bottom right corner
            vertex.position = new Vector2(Mathf.Sin(angle) * (majorRadius - thickness), Mathf.Cos(angle) * (minorRadius - thickness)) + center;
            vh.AddVert(vertex);

            // top right corner
            vertex.position = new Vector2(Mathf.Sin(angle) * majorRadius, Mathf.Cos(angle) * minorRadius) + center;
            vh.AddVert(vertex);

            int offset = i * 2;
            vh.AddTriangle(offset + 0, offset + 1, (offset + 3) % vertexCount);
            vh.AddTriangle((offset + 3) % vertexCount, (offset + 2) % vertexCount, offset + 0);
        }
    }
}