using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class fullplayerhp : MonoBehaviour
{
    public float speed = 10.0f;
    public float jumpspeed = 0.2f;


    private Rigidbody2D player;
    private Animator m_animator;

    public LayerMask gl;
    private Transform gc;

    public float hp = 3;
    public Slider healthBar;
    Renderer rend;
    Color c;

    private void Start()
    {
        m_animator = GetComponent<Animator>();
        rend = GetComponent<Renderer>();
        c = rend.material.color;
        gc = transform.Find("gc");
        player = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(8, 9, false);
        Physics2D.IgnoreLayerCollision(8, 10, false);
        Physics2D.IgnoreLayerCollision(8, 13, false);
        Physics2D.IgnoreLayerCollision(8, 14, false);

    }
    void death()
    {
        SceneManager.LoadScene(0);
    }
    void hurt()
    {

        hp -= 0.5f;
        if (hp <= 0)
        {
            Physics2D.IgnoreLayerCollision(8, 10, true);
            Physics2D.IgnoreLayerCollision(8, 9, true);
            Invoke("death", 0.5f);
            m_animator.SetTrigger("dead");


        }
        else
        {
            m_animator.SetTrigger("hit");
        }
        healthBar.value = hp;
    }

    void critical()
    {
        hp -= 1000;
        if (hp <= 0)
        {
            Physics2D.IgnoreLayerCollision(8, 10);
            Physics2D.IgnoreLayerCollision(8, 13);
            Invoke("death", 0.5f);
            m_animator.SetTrigger("dead");

        }
        healthBar.value = hp;
    }

    void bosshit()
    {

        hp -= 0.5f;
        if (hp <= 0)
        {
            Physics2D.IgnoreLayerCollision(8, 14, true);
            Physics2D.IgnoreLayerCollision(8, 10, true);
            Invoke("death", 0.5f);
            m_animator.SetTrigger("dead");


        }
        else
        {
            m_animator.SetTrigger("hit");
        }
        healthBar.value = hp;
    }
    IEnumerator immune()
    {
        Physics2D.IgnoreLayerCollision(8, 9, true);
        Physics2D.IgnoreLayerCollision(8, 13, true);
        Physics2D.IgnoreLayerCollision(8, 14, true);
        c.a = 0.5f;
        rend.material.color = c;
        yield return new WaitForSeconds(1f);
        Physics2D.IgnoreLayerCollision(8, 9, false);
        Physics2D.IgnoreLayerCollision(8, 13, false);
        Physics2D.IgnoreLayerCollision(8, 14, false);
        c.a = 1f;
        rend.material.color = c;
    }
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        bool isgrounded = Physics2D.OverlapPoint(gc.position, gl);
        if (Input.GetButton("Jump"))
        {

            if (isgrounded)
            {
                player.AddForce(Vector2.up * jumpspeed, ForceMode2D.Impulse);
                sound.playsound("jump");
                isgrounded = false;
            }


        }
        m_animator.SetBool("Isgrounded", isgrounded);
        float hspeed = Input.GetAxis("Horizontal");

        m_animator.SetFloat("speed", Mathf.Abs(hspeed));
        if (hspeed > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (hspeed < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        this.player.velocity = new Vector2(hspeed * speed, this.player.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "fall")
        {
            sound.playsound("hit");
            Invoke("death", 0.2f);
        }

        enemy1 robot = collision.collider.GetComponent<enemy1>();
        if (robot != null)
        {
            foreach (ContactPoint2D point in collision.contacts)
            {
                if (point.normal.y >= 0.9f)
                {
                    sound.playsound("kick");
                    Vector2 vel = player.velocity;
                    vel.y = jumpspeed;
                    player.velocity = vel;
                    robot.Hurt();
                }
                else
                {
                    sound.playsound("hit");
                    hurt();
                    StartCoroutine("immune");
                }
            }


        }

        enemy2 ghost = collision.collider.GetComponent<enemy2>();
        if (ghost != null)
        {
            sound.playsound("critica");
            critical();
            StartCoroutine("immune");
        }

        boss bosso = collision.collider.GetComponent<boss>();
        if (bosso != null)
        {
            foreach (ContactPoint2D point in collision.contacts)
            {
                if (point.normal.y >= 0.9f)
                {
                    sound.playsound("kick");
                    Vector2 vel = player.velocity;
                    vel.y = jumpspeed;
                    player.velocity = vel;
                    bosso.bosshit();

                }
                else
                {
                    sound.playsound("hit");
                    bosshit();
                    StartCoroutine("immune");
                }
            }
        }

    }
}
