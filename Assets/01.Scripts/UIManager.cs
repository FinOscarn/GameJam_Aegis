using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Player player;

    public Slider PlayerHp;
    public Slider DoppelHp;

    public Text PlayerLevel;
    public Text DoppelgangerLevel;

    public GameObject OptionPanel;
    public GameObject OptionImage;


    private void Awake() 
    {
        if (PlayerHp.value <= 0)
            GameObject.Find("PlayerFill").gameObject.SetActive(false);
        else
            GameObject.Find("PlayerFill").gameObject.SetActive(true);

        if (DoppelHp.value <= 0)
            GameObject.Find("DoppelFill").gameObject.SetActive(false);
        else
            GameObject.Find("DoppelFill").gameObject.SetActive(true);
    }

    void Start() 
    {
       PlayerHp.maxValue = player.hp;
    }

    void Update() 
    {
        CurLevel();
        PlayerHp.value = player.hp;
    }

    void CurLevel()
    {
        PlayerLevel.text = "LV_" + DataManager.Instance.PlayerLv;
        DoppelgangerLevel.text = "LV_" + DataManager.Instance.DoppelgangerLv;
    }

    public void Option()
    {
        OptionPanel.SetActive(true);
        OptionImage.SetActive(true);
    }
}
