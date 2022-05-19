using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy2 : MonoBehaviour
{
    public LayerMask enemymask;
    public float speed = 1;
    Rigidbody2D ghost;
    Transform trans;
    float width, height;
    // Start is called before the first frame update
    void Start()
    {
        ghost = GetComponent<Rigidbody2D>();
        trans = transform;
        SpriteRenderer mysprite = GetComponent<SpriteRenderer>();
        width = mysprite.bounds.extents.x;
        height = mysprite.bounds.extents.y;
        Physics2D.IgnoreLayerCollision(13, 9);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 linecastpos = trans.position.toVector2() - trans.right.toVector2() * width + Vector2.up * height;
        bool isground = Physics2D.Linecast(linecastpos, linecastpos + Vector2.down, enemymask);
        bool iswall = Physics2D.Linecast(linecastpos, linecastpos - trans.right.toVector2() * .02f, enemymask);


        if (!isground || iswall)
        {
            Vector3 nowrot = trans.eulerAngles;
            nowrot.y += 180;
            trans.eulerAngles = nowrot;
        }

        Vector2 vel = ghost.velocity;
        vel.x = -trans.right.x * speed;
        ghost.velocity = vel;
    }
}
