using System.Collections;
using UnityEngine;

public class ScStartTransition : MonoBehaviour
{
    [SerializeField] ScTransitionManager _transiManager;
    public void StartTransition(string sceneToLoad)
    {
        _transiManager.StartTransition(sceneToLoad);
    }
}
