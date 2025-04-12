using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasRenderer))]
public class RadialSegment : Graphic
{
    // Member fields
    [SerializeField, Range(25, 350)] 
    private float thickness = 40;

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
        float radius = Mathf.Min(rectTransform.rect.width, rectTransform.rect.height) * 0.5f;

        Vector2 center = Vector2.one * 0.5f - rectTransform.pivot;
        center = new Vector2(center.x * radius * 2, center.y * radius * 2);
        UIVertex vertex = UIVertex.simpleVert;
        float deltaAngle = Mathf.PI * 2 / divisions;
        int vertexCount = divisions * 2;

        for (int i = 0; i < divisions; i++)
        {
            float angle = i * deltaAngle;
            // bottom right corner
            vertex.position = new Vector2(Mathf.Sin(angle) * (radius - thickness), Mathf.Cos(angle) * (radius - thickness)) + center;
            vh.AddVert(vertex);

            // top right corner
            vertex.position = new Vector2(Mathf.Sin(angle) * radius, Mathf.Cos(angle) * radius) + center;
            vh.AddVert(vertex);

            int offset = i * 2;
            vh.AddTriangle(offset + 0, offset + 1, (offset + 3) % vertexCount);
            vh.AddTriangle((offset + 3) % vertexCount, (offset + 2) % vertexCount, offset + 0);
        }
    }
}