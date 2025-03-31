using UnityEngine;
using UnityEngine.SceneManagement;

public class ScCheckIfAlreadyTaken : MonoBehaviour
{
    // si deja récup : désactive le gameobject
    private void Start() 
    {
        if (PlayerPrefs.HasKey($"{gameObject.name}InScene({SceneManager.GetActiveScene().name})"))
        {
            gameObject.SetActive(false);
        }
    }

    public void HasBeenTaken()
    {
        Debug.Log("Item won't be shown on reload");
        PlayerPrefs.SetInt($"{gameObject.name}InScene({SceneManager.GetActiveScene().name})",1);
        PlayerPrefs.Save();
    }
}
