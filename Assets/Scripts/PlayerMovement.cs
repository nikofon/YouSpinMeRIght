using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator am;
    public int gravityscale;
    public float movespeed;
    public Vector2 size;
    BoxCollider2D checker;
    List<Collider2D> res = new List<Collider2D>();
    private bool Frozen { get { return frozen; } set { rb.gravityScale = value ? 0 : gravityscale; rb.velocity = value ? Vector2.zero : rb.velocity; am.SetBool("Frozen", value) ; frozen = value; } }
    private bool frozen;

    // Start is called before the first frame update
    void Start()
    {
        am = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        checker = GetComponents<BoxCollider2D>()[0].isTrigger == false ? GetComponents<BoxCollider2D>()[1] : GetComponents<BoxCollider2D>()[0];
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LevelLoader.instance.LoadMainMenu();
        }
        if (Frozen)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Frozen = false;
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Frozen = true;
            return;
        }
    }
    void FixedUpdate()
    {
        if (!Frozen)
        {
            checker.OverlapCollider(new ContactFilter2D(), res);
            if (res.Count > 1)
            {
                transform.position = (Vector2)transform.position + movespeed * new Vector2(1, 0);
                return;
            }
        }
        
    }
}
