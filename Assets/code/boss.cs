using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class boss : MonoBehaviour
{
    public LayerMask enemymask;
    public float speed = 1;
    public float speed2 = 2;
    public float speed3 = 3;
    public float hp = 3;
    Rigidbody2D bosso;
    Transform trans;
    private Transform gc;
    public LayerMask gl;
    float width, height;
   public Image image;

    

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        bosso = GetComponent<Rigidbody2D>();
        gc = transform.Find("gc");
        trans = transform;
        SpriteRenderer mysprite = GetComponent<SpriteRenderer>();
        width = mysprite.bounds.extents.x;
        height = mysprite.bounds.extents.y;
        anim = GetComponent<Animator>();
        Physics2D.IgnoreLayerCollision(14, 10, false);
        Physics2D.IgnoreLayerCollision(14, 8, false);
        image.enabled = false;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 linecastpos = trans.position.toVector2() - trans.right.toVector2() * width + Vector2.up * height;
        bool isground = Physics2D.Linecast(linecastpos, linecastpos + Vector2.down, enemymask);
        bool iswall = Physics2D.Linecast(linecastpos, linecastpos - trans.right.toVector2() * .02f, enemymask);

        if (iswall)
        {
            Vector3 nowrot = trans.eulerAngles;
            nowrot.y += 180;
            trans.eulerAngles = nowrot;
        }

        bool isgrounded = Physics2D.OverlapPoint(gc.position, gl);
        if (hp == 3)
        {
            anim.SetTrigger("run");
            Vector2 vel = bosso.velocity;
            vel.x = -trans.right.x * speed;
            bosso.velocity = vel;
            anim.SetBool("Isgrounded", isgrounded);
        }
        else if (hp == 2)
        {
            anim.SetTrigger("run");
            
          
            if (isgrounded)
            {
                sound.playsound("bossjump");
                bosso.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
                isgrounded = false;
                anim.SetBool("Isgrounded", isgrounded);
            }
           

            Vector2 vel = bosso.velocity;
            vel.x = -trans.right.x * speed2;
            bosso.velocity = vel;
        }
        else if (hp == 1)
        {
            anim.SetTrigger("run");
           
            anim.SetTrigger("berserk");
            Vector2 vel = bosso.velocity;
            vel.x = -trans.right.x * speed3;
            bosso.velocity = vel;
        }
        if(hp<=0)
        {
            Physics2D.IgnoreLayerCollision(14, 10, true);
            Physics2D.IgnoreLayerCollision(14, 8, true);
            anim.SetTrigger("dead");
            image.enabled = true;
            Invoke("bossdead", 2f);
        }
    }
    public void bosshit()
    {
        hp-=0.5f;
    }
    void bossdead()
    {
        SceneManager.LoadScene(0);
    }
}
