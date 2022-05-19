using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound : MonoBehaviour
{
    public static AudioClip jump, hit, critical, kick, bossj;
    static AudioSource audiosfx;
    // Start is called before the first frame update
    void Start()
    {
        jump = Resources.Load<AudioClip>("jump");
        hit = Resources.Load<AudioClip>("hit");
        critical = Resources.Load<AudioClip>("critica");
        kick = Resources.Load<AudioClip>("kick");
        bossj = Resources.Load<AudioClip>("bossjump");

        audiosfx = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void playsound(string clip)
    {
        switch(clip)
        {
            case "jump":
                audiosfx.PlayOneShot(jump);
                break;
            case "hit":
                audiosfx.PlayOneShot(hit);
                break;
            case "critica":
                audiosfx.PlayOneShot(critical);
                break;
            case "kick":
                audiosfx.PlayOneShot(kick);
                break;
            case "bossjump":
                audiosfx.PlayOneShot(bossj);
                break;
        }
    }
}
