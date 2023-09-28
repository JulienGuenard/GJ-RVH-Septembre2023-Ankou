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
    [SerializeField]
    private Transform root;
    private Vector3 target;

    Vector3 cameraWorldmapPosition;

    public static CameraManager Instance;

    void Awake()
    {
        if (Instance == null) Instance = this;

        cameraWorldmapPosition = Camera.main.transform.localPosition;
        ZoomOut();
    }

    private void Start()
    {
        ship = ShipManager.Instance.playerShip.transform;
    }

    public void ZoomIn()
    {
        Debug.Log("Début ZoomIn");
        cameraShipPivot.SetParent(ship);
        cameraShipPivot.transform.localPosition = Vector3.zero;
        Camera.main.transform.SetParent(cameraShipPivot);
        target = cameraZoomTarget.position;
        Debug.Log("Fin ZoomIn");
    }

    public void ZoomOut()
    {
        Camera.main.transform.SetParent(root);
        cameraShipPivot.SetParent(root);
        target = cameraWorldmapPosition;
    }

    private void Update()
    {
        iTween.MoveTo(Camera.main.gameObject, target, zoomDuration);
    }
}
