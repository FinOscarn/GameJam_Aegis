using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text PlayerLevel;
    public Text DoppelgangerLevel;



    void Start() 
    {
        CurLevel();
    }

    

    void CurLevel()
    {
        PlayerLevel.text = "LV_" + DataManager.Instance.PlayerLv;
        DoppelgangerLevel.text = "LV_" + DataManager.Instance.DoppelgangerLv;
    }
}
