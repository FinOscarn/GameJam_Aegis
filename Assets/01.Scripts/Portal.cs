using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject black;
    public GameObject player;

    public StageManager stageManager;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.R) && DataManager.Instance.monsters.Count == 0)
        {
            stageManager.NextStage();
            Debug.LogWarning("���� ��������!!!!!");
            black.transform.position = player.transform.position;
        }
    }
}
