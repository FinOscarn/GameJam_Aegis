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



    void Start() 
    {
        player = GetComponent<Player>();
        CurLevel();
        PlayerHp.value = player.hp;
    }

    void Update() 
    {

        if (PlayerHp.value <= 0)
            transform.Find("Fill Area").gameObject.SetActive(false);
        else
            transform.Find("Fill Area").gameObject.SetActive(true);
            
        if (DoppelHp.value <= 0)
            transform.Find("Fill Area").gameObject.SetActive(false);
        else
            transform.Find("Fill Area").gameObject.SetActive(true);
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
