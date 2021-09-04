using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Title : MonoBehaviour
{
    public Text TitleTxt;
    
    void Start()
    {
        TitleText();
    }

    void Update()
    {
        
    }

    void TitleText()
    {
        TitleTxt.DOFade(0, 0.6f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    void TabPanel()
    {
        SceneManager.LoadScene("LoadingScene");
    }
}
