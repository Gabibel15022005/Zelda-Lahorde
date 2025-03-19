using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScMainMenu : MonoBehaviour
{
    Animator _animator;
    bool _transitionEnded = false;
    [SerializeField] List<Button> buttons;
    void Start()
    {
        Time.timeScale = 1;
        _animator = GetComponent<Animator>();
    }
    void StartTransition()
    {
        _animator.Play("StartTransition"); 
    }
    public void Continue()
    {
        StartCoroutine(CoContinue());
    }
    public void NewGame()
    {
        StartCoroutine(CoNewGame());
    }
    public void Quit()
    {
        StartCoroutine(CoQuit());
    }
    private IEnumerator CoContinue()
    {
        StartTransition();
        yield return _transitionEnded;
        SceneManager.LoadScene(PlayerPrefs.GetString("Scene"));
    }
    private IEnumerator CoNewGame()
    {
        StartTransition();
        yield return _transitionEnded;
        ResetPlayerPrefs();
        SceneManager.LoadScene("Level 1");
    }
    private void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
    private IEnumerator CoQuit()
    {
        StartTransition();
        yield return _transitionEnded;
        Application.Quit();
    }
    public void TransitionEnded()
    {
        _transitionEnded = true;
    }

}
