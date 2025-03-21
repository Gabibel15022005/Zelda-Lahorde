using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScInGameUI : MonoBehaviour

{   
    [SerializeField] private ScInventoryUIManager _inventoryManager;
    [SerializeField] private ScDialogueManager _dialogueManager;
    EventSystem _eventManager;
    Animator _animator;
    Animator _transitionAnimator;
    bool _transitionEnded = false;
    bool _resume = true;
    bool _canOpenInventory = true;
    bool _canOpenPauseMenu = true;
    bool _isGamePaused = false;
    [SerializeField] GameObject FirstButtonInventory;
    [SerializeField] GameObject FirstButtonPauseMenu;

#region Basic Behaviour
    void Start()
    {
        _eventManager = GetComponentInChildren<EventSystem>();
        _animator = GetComponent<Animator>();
        _transitionAnimator = GetComponentInChildren<Animator>();
    }
    void StartTransition()
    {
        _transitionAnimator.Play("StartTransition"); 
    }
    public void MainMenu()
    {
        StartCoroutine(CoMainMenu());
    }
    public void Quit()
    {
        StartCoroutine(CoQuit());
    }
    private IEnumerator CoMainMenu()
    {
        StartTransition();
        yield return _transitionEnded;
        SceneManager.LoadScene("Main Menu");
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
    public void Resume()
    {
        StartCoroutine(CoResume());
    }
    private IEnumerator CoResume()
    {
        _resume = false;

        yield return _resume;

        if (!_isGamePaused) 
        {
            Time.timeScale = 1;
        }
    }
    public void CanResume()
    {
        _resume = true;
    }
    public void OCInventory()
    {
        if (!_resume) return;
        //Debug.Log("Condition 1 to open or close the Inventory is met");
        if(!_canOpenInventory) return; // si on peut pas ouvrir l'inventaire on ne fait rien
        //Debug.Log("Condition 2 to open or close the Inventory is met");

        Resume(); // attend la fin de l'animation lancé
        

        _canOpenPauseMenu = !_canOpenPauseMenu; // si on peut ouvrir ou pas le menu pause

        if (!_canOpenPauseMenu)
        {
            _isGamePaused = true;
            Time.timeScale = 0;
            _animator.Play("OpenInventory");
        }
        else
        {
            _isGamePaused = false;
            _animator.Play("CloseInventory");
        }
    }
    public void OCPauseMenu()
    {
        if (!_resume) return;
        //Debug.Log("Condition 1 to open or close the PauseMenu is met");
        if(!_canOpenPauseMenu) return; // si on peut pas ouvrir le menu pause on ne fait rien
        //Debug.Log("Condition 2 to open or close the PauseMenu is met");
        
        Resume(); // attend la fin de l'animation lancé

        _canOpenInventory = !_canOpenInventory; // si on peut ouvrir ou pas le'inventaire

        if (!_canOpenInventory)
        {
            _isGamePaused = true;
            Time.timeScale = 0;
            _animator.Play("OpenPauseMenu");
        }
        else
        {
            _isGamePaused = false;
            _animator.Play("ClosePauseMenu");
        }
    }
    public void SetFirstButtonInventory()
    {
        SelectFirstButton(FirstButtonInventory);
    }
    public void SetFirstButtonPauseMenu()
    {
        SelectFirstButton(FirstButtonPauseMenu);
    }
    public void SelectFirstButton(GameObject button)
    {
        _eventManager.SetSelectedGameObject(button);
    }
    public void ResetSelectedFirstButton()
    {
        _eventManager.SetSelectedGameObject(null);
    }
    public ScInventoryUIManager GetInventoryManager()
    {
        return _inventoryManager;
    }

    public ScDialogueManager GetDialogueManager()
    {
        return _dialogueManager;
    }
    #endregion


}
