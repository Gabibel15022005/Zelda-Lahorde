using UnityEngine;
using UnityEngine.SceneManagement;

public class ScSavePoint : MonoBehaviour
{
    ScStartTransition _cameraScript;
    [SerializeField] Transform _respawnPos;
    void Start() 
    {
        _cameraScript = Camera.main.GetComponent<ScStartTransition>();
    }
    public void UseSavePoint(Transform playerPos)
    {
        // save la pos du joueur
        // remettre la vie au max dans les PlayerPrefs
        Vector3 pos = _respawnPos.position; 

        PlayerPrefs.SetFloat("PlayerPosX", pos.x);
        PlayerPrefs.SetFloat("PlayerPosY", pos.y);
        PlayerPrefs.SetString("Scene",SceneManager.GetActiveScene().name);

        // remet full hp dans player prefs en mettant la valeur de hp max dans player prefs

        PlayerPrefs.Save();

        // lancer la transition à l'écran noir 
        _cameraScript.StartTransition(SceneManager.GetActiveScene().name);

        // reload la scene

    }
}
