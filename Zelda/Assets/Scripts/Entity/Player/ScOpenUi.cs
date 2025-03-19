using UnityEngine;
using UnityEngine.InputSystem;

public class ScOpenUi : MonoBehaviour
{
    ScInGameUI _UIManager;
    void Start()
    {
        _UIManager = Camera.main.GetComponent<ScInGameUI>();
    }
    public void OpenInventory(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            //Debug.Log("Pressed Select");
            _UIManager.OCInventory();
        }
    }

    public void OpenPauseMenu(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            //Debug.Log("Pressed Start");
            _UIManager.OCPauseMenu();
        }
    }
}
