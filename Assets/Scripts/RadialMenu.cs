using UnityEngine;

public class RadialMenu : MonoBehaviour
{
    public Camera menuCamera;
    public RadialInputs inputs;

    [Range(0, 50)]
    public int segmentCount = 10;

    private void Awake()
    {
        inputs = new RadialInputs();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float angle = CalculateMouseAngle(menuCamera, Vector2.up);
        Debug.Log($"Mouse Angle: {angle}");
        //Debug.Log($"Mouse Position: {Input.mousePosition}");
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
}