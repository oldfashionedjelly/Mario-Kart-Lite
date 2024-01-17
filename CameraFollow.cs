using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    CameraCalculations cameraCalc;

    public Transform target;

    public int playerLayerNumber = 9;

    public float maxDistance = 4.0f;
    public float minDistance = 2.0f;
    public float minHeight = 1.0f;

    public float distanceOffset;
    public float finalDistance;

    public Vector2 cameraSpeed = new Vector2(3.0f, 1.0f);
    public Vector2 cameraYLimits = new Vector2(5.0f, 60.0f);

    public bool resetCamera = true;
    public float x = 0.0f;
    public float y = 0.0f;
    public float yOffset;
    public float newPos;

    private Quaternion rotation;
    private Vector3 position;

    void Start()
    {
        cameraCalc = FindObjectOfType<CameraCalculations>();
        Vector3 angles = transform.eulerAngles;
        x = angles.x;
        y = angles.y;
        Cursor.visible = false;
    }

    void Update()
    {
        distanceOffset = cameraCalc.CalculateOffset(target, playerLayerNumber, maxDistance, distanceOffset);
        
    }

    void LateUpdate()
    {
        x += Input.GetAxis("Mouse X") * cameraSpeed.x;
        y -= Input.GetAxis("Mouse Y") * cameraSpeed.y;

        y = cameraCalc.ClampAngle(y, cameraYLimits.x, cameraYLimits.y);

        yOffset = cameraCalc.FindOffsetY(minHeight);
        newPos = cameraCalc.FindPosition(minHeight);

        if (resetCamera == true)
        {
            x = target.transform.eulerAngles.y;
            y = 0;

            rotation = Quaternion.Euler(y, x, 0.0f);
        }
        else
        {
            finalDistance = Mathf.Min(-minDistance, -maxDistance + distanceOffset);

            rotation = Quaternion.Euler(y, x, 0.0f);
            Vector3 changedPos = target.position - new Vector3(0.0f, newPos, 0.0f);
            position = rotation * new Vector3(0.0f, 0.0f, finalDistance) + changedPos;
            position.y = position.y + yOffset;
        }

        transform.rotation = rotation;
        transform.position = position;
        resetCamera = false;
    }
}
