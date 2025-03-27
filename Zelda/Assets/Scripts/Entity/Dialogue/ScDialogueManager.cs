using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScDialogueManager : MonoBehaviour
{
    ScCameraMoveTowards cam;
    ScPlayerMovement _player = null;
    private Queue<ScDialogue> _sentences;

    [SerializeField] private TMP_Text _textName;
    [SerializeField] private Image _imageFace;
    [SerializeField] private TMP_Text _textSentence;
    [SerializeField] private float _delayBetweenLetter = 0.5f;

    private bool _isAlreadyDialoging = false;

    Animator _animator;

    void Start()
    {
        _sentences = new Queue<ScDialogue>();

        _animator = GetComponent<Animator>();
        cam = Camera.main.GetComponent<ScCameraMoveTowards>();
    }

    public void StartDialogue(ScDialogue[] dialogue ,ScPlayerMovement player, Transform target)
    {
        if (_player == null) _player = player;

        if (!_isAlreadyDialoging)
        {
            // imobiliser le joueur
            // dire à la cam de zoomer au bonne endroit
            _player.CantMove();
            _player.GetComponent<ScPlayerUseItem>().CantUseItem();
            cam.SetDialogueTarget(target);

            _isAlreadyDialoging = true;
            _animator.SetBool("IsOpen",true);
            _sentences.Clear();

            foreach (ScDialogue sentence in dialogue)
            {
                _sentences.Enqueue(sentence);
            }

            DisplayNextSentence();
        }
        else
        {
            DisplayNextSentence();
        }
    }

    void DisplayNextSentence()
    {
        if (_sentences.Count == 0)
        {
            EndOfDialogue();
            return;
        }

        ScDialogue sentence = _sentences.Dequeue();

        _textName.text = sentence.Name;
        _imageFace.sprite = sentence.Face;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        
    }

    IEnumerator TypeSentence(ScDialogue sentence)
    {
        _textSentence.text = "";

        foreach (char letter in sentence.Sentence.ToCharArray())
        {
            _textSentence.text += letter;
            yield return new WaitForSeconds(_delayBetweenLetter);
        }
    }
    void EndOfDialogue()
    {
        // refaire bouger le joueur
        _player.CanMove();
        _player.GetComponent<ScPlayerUseItem>().CanUseItem();
        _player = null;
        cam.SetDialogueTarget(null);

        // dire à la cam de zoomer au bonne endroit
        _isAlreadyDialoging = false;
        _animator.SetBool("IsOpen",false);
    }
}
