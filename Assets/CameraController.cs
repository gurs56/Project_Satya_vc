using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour {
    private CinemachineCamera cam;

    /// <summary>
    /// For inspector use only. Use property instead.
    /// </summary>
    [SerializeField]
    private Transform trackingTarget;

    public Transform TrackingTarget {
        get { return trackingTarget; }
        set {
            cam.Target.TrackingTarget = value;
            trackingTarget = value;
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        cam = GetComponentInChildren<CinemachineCamera>();

    }

    // Update is called once per frame
    void Update() {

    }
}
