using Unity.VisualScripting;
using UnityEngine;

public class ScCameraMoveTowards : MonoBehaviour
{
    Camera cam;
    [SerializeField] private float _cameraSpeed = 1f;
    private Transform _target = null;
    private float _zoomTarget;
    private float _originalZoom;
    
    void Start() {
        cam = GetComponent<Camera>();
        _originalZoom = cam.orthographicSize ;
        _zoomTarget = _originalZoom;
    }
    void Update()
    {
        if (_target == null) return;

        MoveToward();
        ZoomToward();
    }

    private void MoveToward()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(_target.position.x, _target.position.y, transform.position.z), _cameraSpeed * Time.deltaTime);
    }
    private void ZoomToward()
    {
        cam.orthographicSize  =  Mathf.MoveTowards(cam.orthographicSize , _zoomTarget, _cameraSpeed * Time.deltaTime);
    }
    public Transform CheckTarget()
    {
        return _target;
    }
    public void NewTarget(Transform newTarget)
    {
        _target = newTarget;
    }
    public void NewZoom(float newTarget)
    {
        _zoomTarget = newTarget;
    }
    public void OriginalZoom()
    {
        _zoomTarget = _originalZoom;
    }
}
