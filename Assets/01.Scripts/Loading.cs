using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Loading : MonoBehaviour
{
    public Image rImage;
    public Image lImage;

    void Start()
    {
        rImage.rectTransform.DOMoveX(480f, 1f);
        lImage.rectTransform.DOMoveX(-480f, 1f).OnComplete(()=>
        {
            SceneManager.UnloadSceneAsync("LoadingScene");
        });


    }
}
