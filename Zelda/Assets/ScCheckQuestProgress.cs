using UnityEngine;

public class ScCheckQuestProgress : MonoBehaviour
{
    public string QuestName = "Quete"; // nom utiliser dans prefab pour savoir l'avancer de la quete
    ScPNJ _scPNJ;
    void Start()
    {
        _scPNJ = GetComponent<ScPNJ>();

        if (PlayerPrefs.HasKey(QuestName))
        {
            ChangeQuestProgress(PlayerPrefs.GetInt(QuestName));
        }
    }

    public void ChangeQuestProgress(int value)
    {
        _scPNJ.ChangeDialogue(value);
    }
}
