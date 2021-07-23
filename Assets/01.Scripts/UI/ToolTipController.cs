using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ToolTipController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI txt1;
    public GameObject mPlayerPanel;
    public GameObject rPlayerPanel;
    public ToolTip tipPanel;

    [TextArea]public string a = "asd";
    public void SetUpTxt(string role)
    {
        txt1.text = role;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetUpTxt(a);
        print("asd");
        tipPanel.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tipPanel.gameObject.SetActive(false);
    }
}
