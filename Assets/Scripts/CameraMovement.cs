using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public BoxCollider2D outChecker;
    public BoxCollider2D inChecker;
    private Transform pivotPosition;
    // Start is called before the first frame update
    void Awake()
    {
        pivotPosition = GameObject.Find("GridPivot").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(outChecker.OverlapCollider(new ContactFilter2D(), new List<Collider2D>()) > 0)
        {
            Camera.main.orthographicSize += 0.1f;
            outChecker.offset += new Vector2(0, 0.1f);
            inChecker.offset += new Vector2(0, 0.1f);
        }
        else if(inChecker.OverlapCollider(new ContactFilter2D(), new List<Collider2D>()) == 0)
        {
            Camera.main.orthographicSize -= 0.1f;
            outChecker.offset -= new Vector2(0, 0.1f);
            inChecker.offset -= new Vector2(0, 0.1f);
        }
        Camera.main.transform.position = pivotPosition.position;
    }
}
