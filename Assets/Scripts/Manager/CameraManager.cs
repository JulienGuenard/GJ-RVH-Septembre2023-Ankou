using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform cameraZoomTarget;
    public Transform cameraShipPivot;

    public AnimationCurve zoomCurve;
    public float zoomSpeed;
    public float zoomDuration;

    bool isZooming;
    bool hasZoomed;

    float evaluateTime = 0;

    Vector3 cameraWorldmapPosition;

    ShipManager sm;

    public static CameraManager Instance;

    void Awake()
    {
        if (Instance == null) Instance = this;

        cameraWorldmapPosition = Camera.main.transform.localPosition;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && !isZooming) isZooming = true;

        if (isZooming && !hasZoomed)
        {
            Camera.main.transform.SetParent(cameraShipPivot);
            ZoomOnTarget(cameraZoomTarget.localPosition);
        }
 
        if (isZooming && hasZoomed)     
        {
            Camera.main.transform.SetParent(GameObject.Find("Root").transform);
            ZoomOnTarget(cameraWorldmapPosition);
        }

    }

    void ZoomOnTarget(Vector3 target)
    {
        evaluateTime += 0.1f / zoomDuration;

        if (evaluateTime >= 2)
        {
            isZooming = false;
            hasZoomed = !hasZoomed;
            evaluateTime = 0;
        }

        Camera.main.transform.localPosition = Vector3.MoveTowards(Camera.main.transform.localPosition, target, zoomCurve.Evaluate(evaluateTime) * zoomSpeed);
    }
}
