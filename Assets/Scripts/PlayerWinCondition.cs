using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWinCondition : MonoBehaviour
{
    List<GameObject> targets;
    private void Start()
    {
        targets = new List<GameObject>();
        targets.AddRange(GameObject.FindGameObjectsWithTag("Target"));
    }
    private void Update()
    {
        foreach(GameObject t in targets)
        {
            if(Vector3.Distance(transform.position, t.transform.position) < 0.1)
            {
                targets.Remove(t);
            }
        }
        if(targets.Count == 0)
        {
            LevelLoader.instance.LoadNextLevel();
        }
    }
}
