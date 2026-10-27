
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class BearPlacement : MonoBehaviour
{
    public GameObject bearPrefab;

    private ARRaycastManager raycastManager;
    private GameObject placedBear;

    private static readonly List<ARRaycastHit> hits =
        new List<ARRaycastHit>();

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();

        if (raycastManager == null)
            UnityEngine.Debug.LogError("AR Raycast Manager алга!");

        if (bearPrefab == null)
            UnityEngine.Debug.LogError("Bear Prefab холбоогүй байна!");
    }

    private void Update()
    {
        if (raycastManager == null || bearPrefab == null)
            return;

        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase != TouchPhase.Began)
            return;

        if (raycastManager.Raycast(
            touch.position,
            hits,
            TrackableType.PlaneWithinPolygon))
        {
            Pose pose = hits[0].pose;

            if (placedBear == null)
                placedBear = Instantiate(
                    bearPrefab, pose.position, pose.rotation);
            else
                placedBear.transform.SetPositionAndRotation(
                    pose.position, pose.rotation);
        }
    }
}
