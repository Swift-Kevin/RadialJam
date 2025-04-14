using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasRenderer))]
public class RadialSegment : Graphic
{
    [Tooltip("Current fill amount of the arc (0 = empty, 1 = full arc).")]
    [SerializeField, Range(0f, 1f)]
    private float fillPercent = 1f;

    SegmentInfo segmentInfo;

    private float segmentAngle = 360f;
    private float startAngle = 0f;
    private Rect rectHandle;

    public float SegmentAngle => segmentAngle;
    public float StartAngle => startAngle;
    public float Thickness => OuterRadius - InnerRadius;
    public Rect RectHandle => rectHandle;
    public float OuterRadius => segmentInfo.outerRadius;
    public float InnerRadius => segmentInfo.innerRadius;

    public Image overlayImageComponent;
    public TextMeshProUGUI costLabel;
    

    private float rectSize;
    private bool updateRectSize = true;

    protected override void Start()
    {
        rectHandle = GetComponent<RectTransform>().rect;
    }

    protected override void OnPopulateMesh(VertexHelper _vh)
    {
        base.OnPopulateMesh(_vh);
        _vh.Clear();

        // Use the serialized radii instead of rect size
        float arcRad = Mathf.Deg2Rad * segmentAngle * fillPercent;
        float angleOffsetRad = Mathf.Deg2Rad * startAngle;
        Vector2 center = Vector2.zero;

        UIVertex vertex = UIVertex.simpleVert;
        float angleStep = arcRad / segmentInfo.numTris;
        int stepsToDraw = Mathf.CeilToInt(segmentInfo.numTris * fillPercent) + 1;
        int offset = 0;

        for (int i = 0; i < stepsToDraw; i++)
        {
            float angle = angleOffsetRad + i * angleStep;
            float percent = (float)i / (stepsToDraw - 1);

            if (float.IsNaN(percent))
                percent = 0;

            Color gradientColor = segmentInfo.colors.Evaluate(percent);
            vertex.color = gradientColor;

            // Inner edge (toward center)
            float innerX = Mathf.Sin(angle) * InnerRadius;
            float innerY = Mathf.Cos(angle) * InnerRadius;
            vertex.position = new Vector2(innerX, innerY) + center;
            _vh.AddVert(vertex);

            // Outer edge (away from center)
            float outerX = Mathf.Sin(angle) * OuterRadius;
            float outerY = Mathf.Cos(angle) * OuterRadius;
            vertex.position = new Vector2(outerX, outerY) + center;
            _vh.AddVert(vertex);

            offset = i * 2;
            if (i < stepsToDraw - 1)
            {
                _vh.AddTriangle(offset + 0, offset + 1, offset + 3);
                _vh.AddTriangle(offset + 3, offset + 2, offset + 0);
            }
        }

        UpdateSpritePosition();
    }

    public void SetArcAngle(float _segmentAngle)
    {
        segmentAngle = _segmentAngle;
    }

    public void UpdateSegmentInfo(SegmentInfo _info)
    {
        segmentInfo = _info;

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

    public void SetSprite(Sprite _sprite)
    {
        overlayImageComponent.sprite = _sprite;

        if (_sprite == null)
        {
            overlayImageComponent.gameObject.SetActive(false);
        }
    }

    public void UpdateSpritePosition()
    {
        // Set position of component so its in segment
        float desiredAngle = (segmentAngle * 0.5f) + startAngle;
        float desiredDistance = OuterRadius - (InnerRadius * 0.5f);
        rectSize = desiredDistance * 0.5f;

        float x = Mathf.Sin(desiredAngle * Mathf.Deg2Rad) * (desiredDistance + segmentInfo.spriteOffset);
        float y = Mathf.Cos(desiredAngle * Mathf.Deg2Rad) * (desiredDistance + segmentInfo.spriteOffset);

        overlayImageComponent.rectTransform.localPosition = new Vector2(x, y);
        updateRectSize = true;
    }

    private void LateUpdate()
    {
        if (updateRectSize)
        {
            overlayImageComponent.rectTransform.sizeDelta = new Vector2(rectSize, rectSize); 
            updateRectSize = false;
        }
    }

}