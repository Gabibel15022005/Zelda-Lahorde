using UnityEditor.Rendering;
using UnityEngine;

public class ScPNJ : MonoBehaviour
{
    ScDialogueManager _dialogueManager;
    public ScDialogue Dialogue;
    void Start()
    {
        _dialogueManager = Camera.main.GetComponentInChildren<ScInGameUI>().GetDialogueManager();
    }

    public void TriggerDialogue(ScPlayerMovement player)
    {

        _dialogueManager.StartDialogue(Dialogue, player, transform);
    }
}
