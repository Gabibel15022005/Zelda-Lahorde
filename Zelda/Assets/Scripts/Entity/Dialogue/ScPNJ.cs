using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScPNJ : MonoBehaviour
{
    ScDialogueManager _dialogueManager;
    public ScDialogues[] Dialogues;
    int _index = 0;
    void Start()
    {
        _dialogueManager = Camera.main.GetComponentInChildren<ScInGameUI>().GetDialogueManager();
    }

    public void TriggerDialogue(ScPlayerMovement player)
    {
        _dialogueManager.StartDialogue(Dialogues[_index].Dialogue, player, transform);
    }

    public void ChangeDialogue(int value)
    {
        _index = value;
        if (Dialogues.Length - 1> _index) _index = Dialogues.Length - 1;
    }
}
