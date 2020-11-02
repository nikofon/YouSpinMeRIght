using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWinCondition : MonoBehaviour
{
    private List<GameObject> targets;
    private bool levelEnded = false;
    private void Start()
    {
        targets = new List<GameObject>();
        targets.AddRange(GameObject.FindGameObjectsWithTag("Target"));
        foreach (GameObject t in targets)
        {
            t.GetComponentInChildren<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = "Foreground";
        }

    }
    private void Update()
    {
        GameObject toRemove = null;
        foreach(GameObject t in targets)
        {
            if(Vector2.Distance(transform.position, t.transform.position) < 1.5f)
            {
                t.GetComponentInChildren<ParticleSystem>().Play();
                AudioManager.instance.PlaySound("PKU");
                toRemove = t;
            }
        }
        if (toRemove != null)
        {
            targets.Remove(toRemove);
            DestroyDessert(toRemove);
            toRemove = null;
        }
        if (targets.Count == 0 && !levelEnded)
        {
            levelEnded = true;
            AudioManager.instance.PlaySound("win");
            LevelLoader.instance.DelayedLoadNext();
        }
    }
    private void DestroyDessert(GameObject dessert)
    {
        StartCoroutine(DestroyDesert(dessert));
    }
    private IEnumerator DestroyDesert(GameObject dessert)
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(dessert);
        yield break;
    }
}
