using UnityEngine;
using UnityEngine.SceneManagement;

public class ScEndOfLevel : MonoBehaviour
{
    ScStartTransition _cameraScript;
    bool _hasBeenCalled = false;

    public string _nextScene;

    void Start() 
    {
        _cameraScript = Camera.main.GetComponent<ScStartTransition>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_hasBeenCalled && collision.gameObject.CompareTag("Player"))
        {
            _hasBeenCalled = true;
            DeletePlayerPrefsForSpawn();
            _cameraScript.StartTransition(_nextScene);
        }
    }

    void DeletePlayerPrefsForSpawn()
    {
        PlayerPrefs.DeleteKey("PlayerPosX");
        PlayerPrefs.DeleteKey("PlayerPosY");
        PlayerPrefs.Save();
    }
}
