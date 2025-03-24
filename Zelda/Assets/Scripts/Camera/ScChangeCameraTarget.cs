using UnityEngine;

public class ScChangeCameraTarget : MonoBehaviour
{
    ScCameraMoveTowards _mainCamera;
    int _checkNbTrigger = 0;
    void Start()
    {
        _mainCamera = Camera.main.GetComponent<ScCameraMoveTowards>();

        Vector3 cameraStartPoint = new Vector3(transform.position.x,transform.position.y,Camera.main.transform.position.z);
        Camera.main.transform.position = cameraStartPoint;

        if (_mainCamera.CheckTarget() == null) _mainCamera.NewTarget(transform);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NewCameraPosition")) // ce sera juste un empty avec ce tag et une zone de trigger
        {
            _mainCamera.NewTarget(collision.gameObject.transform);
            _mainCamera.NewZoom(collision.gameObject.GetComponent<ScZoomForCamera>().ZoomToApply);

            _checkNbTrigger++;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NewCameraPosition")) // ce sera juste un empty avec ce tag et une zone de trigger
        {
            _checkNbTrigger--;

            if (_checkNbTrigger == 0)
            {
                _mainCamera.NewTarget(transform);
                _mainCamera.OriginalZoom();
            }
        }
    }
}
