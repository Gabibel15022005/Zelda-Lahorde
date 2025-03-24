using UnityEngine;
using UnityEngine.SceneManagement;

public class ScTransitionManager : MonoBehaviour
{
    Animator _animator;
    string _sceneToLoad;
    void Start() 
    {
        _animator = GetComponent<Animator>();
    }
    public void StartTransition(string sceneToLoad)
    {
        _animator.Play("StartTransition");
        _sceneToLoad = sceneToLoad;
    }

    public void OnEndOfStartTransition()
    {
        SceneManager.LoadScene(_sceneToLoad);
    }
}
