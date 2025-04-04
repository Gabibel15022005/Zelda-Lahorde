using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScCadna : MonoBehaviour
{
    [SerializeField] List<ScDoor> _doors;
    Animator _animator;
    BoxCollider2D _collider;
    ScShowInteraction _interaction;
    void Start()
    {
        _interaction = GetComponent<ScShowInteraction>();

        if (PlayerPrefs.HasKey($"{gameObject.transform.parent.name}InScene({SceneManager.GetActiveScene().name})"))
        {
            OpenAllDoor();
            gameObject.SetActive(false);
        }
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();
    }

    private void HideUI()
    {
        _interaction.StopChecking();
    }

    public void Unlock()
    {
        HideUI();
        HasBeenUnlocked();
        _collider.enabled = false;
        _animator.Play("Unlock");
        OpenAllDoor();
    }
    private void OpenAllDoor()
    {
        foreach (ScDoor door in _doors)
        {
            door.OpenDoor();
        }
    }
    public void HasBeenUnlocked()
    {
        Debug.Log($"{gameObject.transform.parent.name}InScene({SceneManager.GetActiveScene().name}) has been unlocked");
        PlayerPrefs.SetInt($"{gameObject.transform.parent.name}InScene({SceneManager.GetActiveScene().name})",1);
        PlayerPrefs.Save();
    }
}
