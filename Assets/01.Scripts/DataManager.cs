using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    
    private static DataManager instance;
    public static DataManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DataManager>();

                if (instance == null)
                {
                    Debug.LogError("게임메니져가 존재하지 않습니다!");
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)                       // 만약 instance가 비어있다면
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if(instance != this)                   // 비어있진 않은데 instance가 자신이 아니라면
        {
            Destroy(this.gameObject);
        }
    }

    public List<GameObject> monsters = new List<GameObject>();

    [Header("플레이어 관련")]
    public int PlayerLv = 1;
    public float PlayerEx = 0f;

    [Header("도플갱어 관련")]
    public int DoppelgangerLv = 1;
    public float DoppelEx = 0f;

    public int deadMonsterCount= 0;

    public int curStage;
}
