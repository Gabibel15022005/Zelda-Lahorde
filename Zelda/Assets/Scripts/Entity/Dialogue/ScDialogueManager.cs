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
    private Queue<string> _names;
    private Queue<Sprite> _faces;
    private Queue<string> _sentences;

    [SerializeField] private TMP_Text _textName;
    [SerializeField] private Image _imageFace;
    [SerializeField] private TMP_Text _textSentence;
    [SerializeField] private float _delayBetweenLetter = 0.5f;

    private bool _isAlreadyDialoging = false;

    Animator _animator;

    void Start()
    {
        _names = new Queue<string>();
        _faces = new Queue<Sprite>();
        _sentences = new Queue<string>();

        _animator = GetComponent<Animator>();
        cam = Camera.main.GetComponent<ScCameraMoveTowards>();
    }

    public void StartDialogue(ScDialogue dialogue ,ScPlayerMovement player, Transform target)
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
            _faces.Clear();
            _names.Clear();
            foreach (string name in dialogue.Name)
            {
                _names.Enqueue(name);
            }
            foreach (Sprite face in dialogue.Face)
            {
                _faces.Enqueue(face);
            }
            foreach (string sentence in dialogue.Sentence)
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

        _textName.text = _names.Dequeue();
        _imageFace.sprite = _faces.Dequeue();
        string sentence = _sentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        
    }

    IEnumerator TypeSentence(string sentence)
    {
        _textSentence.text = "";

        foreach (char letter in sentence.ToCharArray())
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
