using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public int gravityscale;
    public float movespeed;
    public Vector2 size;
    BoxCollider2D checker;
    List<Collider2D> res = new List<Collider2D>();
    private bool Frozen { get { return frozen; } set { rb.gravityScale = value ? 0 : gravityscale; rb.velocity = value ? Vector2.zero : rb.velocity; frozen = value; } }
    private bool frozen;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        checker = GetComponents<BoxCollider2D>()[0].isTrigger == false ? GetComponents<BoxCollider2D>()[1] : GetComponents<BoxCollider2D>()[0];
    }

    // Update is called once per frame
    private void Update()
    {
        if (Frozen)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log("KeyDown");
                Frozen = false;
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("KeyDown");
            Frozen = true;
            return;
        }
    }
    void FixedUpdate()
    {
        if (!Frozen)
        {
            checker.OverlapCollider(new ContactFilter2D(), res);
            Debug.Log(res.Count);
            if (res.Count > 1)
            {
                transform.position = (Vector2)transform.position + movespeed * new Vector2(1, 0);
                return;
            }
        }
        
    }
}
