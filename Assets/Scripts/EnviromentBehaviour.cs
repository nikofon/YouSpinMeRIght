using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentBehaviour : MonoBehaviour
{
    public List<GameObject> targets;
    public Transform background;
    void Start()
    {
        background = GameObject.Find("Background").GetComponent<Transform>();
        targets = new List<GameObject>();
        targets.AddRange(GameObject.FindGameObjectsWithTag("Target"));
        background.transform.position = (Vector2) GameObject.Find("GridPivot").GetComponent<Transform>().position;
        background.transform.position -= Vector3.back;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        background.localScale = Camera.main.orthographicSize * new Vector2(1, 1);
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
