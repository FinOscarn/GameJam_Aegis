using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolTip : MonoBehaviour
{
    public TextMeshProUGUI roleTxt;

    public void PSelectTxt(string role)
    {
        roleTxt.text = role;
    }
    /*public void ItemTxt1(string a, float atk, float def, float hp)
    {

    }
    public void ItemTxt2(string a, float atk, float def)
    {

    }
    public void ItemTxt3(string a, float atk, )*/

}
