using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Title : MonoBehaviour
{
    public Text TitleTxt;

    public Image rImage;
    public Image lImage;

    void Start()
    {
        TitleText();
    }

    void Update()
    {
        
    }

    public void SelectPlayer()
    {
        rImage.rectTransform.DOMoveX(480f, 1f);
        lImage.rectTransform.DOMoveX(-480f, 1f).OnComplete(()=>
        {
            TabPanel();
        });
    }

    void TitleText()
    {
        TitleTxt.DOFade(0, 0.6f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    void TabPanel()
    {
        SceneManager.LoadScene("InGame");
    }
}
