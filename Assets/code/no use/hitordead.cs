using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitordead : MonoBehaviour
{
    private Animator m_animator2;
    private void Start()
    {
        m_animator2 = GetComponent<Animator>();
     }
    public void Triggerhurt(float hurttime)
    {
        StartCoroutine(hurtblink(hurttime));
    }
    IEnumerator hurtblink(float hurttime)
    {
        int enemylayer = LayerMask.NameToLayer("enemy");
        int playerlayer = LayerMask.NameToLayer("player");
        Physics2D.IgnoreLayerCollision(enemylayer, playerlayer);

        m_animator2.SetLayerWeight(1, 1);

        yield return new WaitForSeconds(hurttime);

        Physics2D.IgnoreLayerCollision(enemylayer, playerlayer, false);
        m_animator2.SetLayerWeight(1, 0);
    }
}
