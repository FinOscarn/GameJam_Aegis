using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject Player;

    [System.Serializable]
    public class StartPositionArray
    {
        public List<Transform> StartPosition = new List<Transform>();
    }

    CinemachineConfiner cine;

    public StartPositionArray[] startPositionArrays;


    public List<Transform> StartPositionBattle = new List<Transform>();
    public List<Transform> StartPositionPuzzle = new List<Transform>();


    int curStage;
    int lastStage;

    public Collider2D[] bgColider;
    private void Start() 
    {
        cine = FindObjectOfType<CinemachineConfiner>();    
    }

    public void NextStage()
    {
        curStage++;
        
        int mapIndex = curStage / 10;
        int randomIndex = Random.Range(0, startPositionArrays[mapIndex].StartPosition.Count);

        cine.m_BoundingShape2D = bgColider[randomIndex];

        Player.transform.position = startPositionArrays[mapIndex].StartPosition[randomIndex].position;
        startPositionArrays[mapIndex].StartPosition.RemoveAt(randomIndex);
    }


}
