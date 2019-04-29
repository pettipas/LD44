using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{

    public Transform attackPoint;
    public LayerMask playerMask;
    public int damage;
    public float duration = 1.0f;
    public float timeexpired;
    void Update()
    {
        timeexpired += Time.smoothDeltaTime;
        if (timeexpired > duration) {
            timeexpired = 0;
            Collider[] colliders = Physics.OverlapSphere(attackPoint.position, 0.3f, playerMask);
            if (colliders.Length > 0) {
                for (int j = 0; j < colliders.Length; j++) {
                    Controller player = colliders[j].GetComponent<Controller>();
                    if (player != null) {

                        Vector3 kb = (player.transform.position - transform.position).normalized;
                        player.TakeDamage(damage, new Vector3(kb.x,0,kb.z));
                    }
                }
            }
        }
    }
}
