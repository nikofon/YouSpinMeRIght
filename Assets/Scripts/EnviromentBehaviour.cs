using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentBehaviour : MonoBehaviour
{
    public List<GameObject> targets;
    void Start()
    {
        targets = new List<GameObject>();
        targets.AddRange(GameObject.FindGameObjectsWithTag("Target"));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.R))
        {
            transform.rotation = transform.rotation*Quaternion.Euler(0, 0, -1);
            foreach(GameObject t in targets)
            {
                try
                {
                    t.GetComponentInChildren<SpriteRenderer>().transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, -transform.rotation.z);
                }
                catch (MissingReferenceException)
                {
                    targets = new List<GameObject>();
                    targets.AddRange(GameObject.FindGameObjectsWithTag("Target"));
                    break;
                }
            }
        }
    }
}
