using Unity.VisualScripting;
using UnityEngine;

public class ScCameraMoveTowards : MonoBehaviour
{
    Camera cam;
    [SerializeField] private float _cameraSpeed = 1f;
    [SerializeField] private float _zoomSpeed = 1f;
    [SerializeField] private float _cameraDialogueSpeed = 1f;
    [SerializeField] private float _zoomDialogueSpeed = 1f;
    private Transform _target = null;
    private float _zoomTarget;
    private float _originalZoom;

    private Transform _dialogueTarget = null;
    [SerializeField] private float _zoomDialogueTarget;
    
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

    public void SetDialogueTarget(Transform target)
    {
        _dialogueTarget = target;
    }
    private void MoveToward()
    {
        if (_dialogueTarget == null)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3
            (_target.position.x, _target.position.y, transform.position.z),
            _cameraSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3
            (_dialogueTarget.position.x, _dialogueTarget.position.y, transform.position.z), 
            _cameraDialogueSpeed * Time.deltaTime);
        }
    }
    private void ZoomToward()
    {
        if (_dialogueTarget == null)
        {
            cam.orthographicSize  =  Mathf.MoveTowards
            (cam.orthographicSize , _zoomTarget, _zoomSpeed * Time.deltaTime);
        }
        else
        {
            cam.orthographicSize  =  Mathf.MoveTowards
            (cam.orthographicSize , _zoomDialogueTarget, _zoomDialogueSpeed * Time.deltaTime);
        }
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
        
