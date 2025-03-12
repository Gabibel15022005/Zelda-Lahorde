using UnityEngine;

public class ScPlayerStats : ScStats
{
    override public void Start()
    {
        if (!PlayerPrefs.HasKey("Player_hpMax"))
        {
            PlayerPrefs.SetInt("Player_hpMax",_hpMax);
            Debug.Log("Didn't have Player_hpMax");
        }
        
        _hpMax = PlayerPrefs.GetInt("Player_hpMax");
    }
}
