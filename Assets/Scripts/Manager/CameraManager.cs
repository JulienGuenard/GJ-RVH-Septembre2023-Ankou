using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraManager : MonoBehaviour
{
    [Header("Camera's transition values")]
    public float zoomDuration;

    [Header("References")]
    public Transform cameraZoomTarget;
    public Transform cameraShipPivot;
    Transform ship;

    bool isZooming;
    bool hasZoomed;

    Vector3 cameraWorldmapPosition;

    public static CameraManager Instance;

    void Awake()
    {
        if (Instance == null) Instance = this;

        cameraWorldmapPosition = Camera.main.transform.localPosition;
    }

    private void Start()
    {
        ship = ShipManager.Instance.playerShip.transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) Zoom();

        ZoomUpdate();
    }

    public void Zoom()
    {
        isZooming = true;
    }

    void ZoomUpdate()
    {
        if (isZooming && !hasZoomed)
        {
            cameraShipPivot.SetParent(ship);
            cameraShipPivot.transform.localPosition = Vector3.zero;
            Camera.main.transform.SetParent(cameraShipPivot);
            ZoomOnTarget(cameraZoomTarget.position);
        }

        if (isZooming && hasZoomed)
        {
            Camera.main.transform.SetParent(GameObject.Find("Root").transform);
            cameraShipPivot.SetParent(GameObject.Find("Root").transform);
            ZoomOnTarget(cameraWorldmapPosition);
        }
    }

    void ZoomOnTarget(Vector3 target)
    {
        if (Camera.main.transform.position.magnitude - target.magnitude < 5)
        {
            isZooming = false;
            hasZoomed = !hasZoomed;

            if (!hasZoomed) ShipManager.Instance.shipCanTravel = true;
            else            ShipManager.Instance.playerShip.TravelEndEvent();
        }

        iTween.MoveTo(Camera.main.gameObject, target, zoomDuration);
    }
}
