using UnityEngine;

public class ScQuest : MonoBehaviour
{
    [SerializeField] protected ScCheckQuestProgress _quest;
    [SerializeField] protected string _questName; // if the _quest is not on this scene , it must be the exact name of the quest so be careful
    [SerializeField] protected int _questProgress = 1;

    public virtual void Start()
    {
        if (_quest != null)
        {
            Debug.Log($"Changed the name of _questName to : {_quest.QuestName}");
            _questName = _quest.QuestName;
        }
    }

    public void ChangeQuestProgress()
    {
        PlayerPrefs.SetInt(_questName, _questProgress);
        PlayerPrefs.Save();
        if (_quest != null) _quest.ChangeQuestProgress(_questProgress);
    }
}
