using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using System.Linq;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject Player;
    SpriteRenderer[] sr;

    [System.Serializable]
    public class StartPositionArray
    {
        public List<Transform> StartPosition = new List<Transform>();
    }

    CinemachineConfiner cine;

    public StartPositionArray[] startPositionArrays;


    public List<Transform> Potal = new List<Transform>();
    public List<Transform> PairPotal = new List<Transform>();

    public List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

    public List<Collider2D> bgColider = new List<Collider2D>();

    public GameObject[] monsterPosition;

    int curStage;
    int lastStage;
    
    GameObject MonsterType;

    private void Start() 
    {
        cine = FindObjectOfType<CinemachineConfiner>();

        int randomIndex = Random.Range(0, 4);

        MonsterType = Instantiate(monsterPosition[randomIndex], startPositionArrays[0].StartPosition[0].transform);

        

        int srCount  = Potal.Count;

        for (int i = 0; i < srCount; i++)
        {
            sr = Potal[i].GetComponents<SpriteRenderer>();
        }
    }

    private void Update()
    {
        if(DataManager.Instance.monsters.Count > 0)
        {
            DataManager.Instance.monsters = DataManager.Instance.monsters.
                OrderBy(x => Vector2.Distance(x.transform.position, Player.transform.position)).ToList();
        }
    }

    public void NextStage()
    {
        curStage++;
        
        int mapIndex = curStage / 10;
        int randomIndex = Random.Range(0, startPositionArrays[mapIndex].StartPosition.Count);

        produceMonster(randomIndex);

        Player.transform.position = startPositionArrays[mapIndex].StartPosition[randomIndex].position;
        startPositionArrays[mapIndex].StartPosition.RemoveAt(randomIndex);

        cine.m_BoundingShape2D = bgColider[randomIndex];
        bgColider.RemoveAt(randomIndex);

        int count = MonsterType.transform.childCount;

        for (int i = 0; i < count; i++)
        {
            GameObject monster = MonsterType.transform.GetChild(i).gameObject;

            DataManager.Instance.monsters.Add(monster);
        }
    }

    public void CurPotalSprite()
    {
        if(DataManager.Instance.monsters.Count == 0)
        {
            int random = Random.Range(0, spriteRenderers.Count);

            sr[0].sprite = spriteRenderers[random].sprite;

            spriteRenderers.RemoveAt(random);
        }
    }

    void produceMonster(int index)
    {
        int random = Random.Range(0, 4);

        Instantiate(monsterPosition[random], startPositionArrays[0].StartPosition[index].transform);
    }
}
