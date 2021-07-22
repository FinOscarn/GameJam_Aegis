using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject Player;

    [System.Serializable]
    public class StartPositionArray
    {
        public List<Transform> StartPosition = new List<Transform>();
    }

    public StartPositionArray[] startPositionArrays;


    public List<Transform> StartPositionBattle = new List<Transform>();
    public List<Transform> StartPositionPuzzle = new List<Transform>();


    int curStage;
    int lastStage;

    public void NextStage()
    {
        curStage++;
        
        int mapIndex = curStage / 10;
        int randomIndex = Random.Range(0, startPositionArrays[mapIndex].StartPosition.Count);

        Player.transform.position = startPositionArrays[mapIndex].StartPosition[randomIndex].position;
        startPositionArrays[mapIndex].StartPosition.RemoveAt(randomIndex);
    }
}
