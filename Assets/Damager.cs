using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    public LayerMask doDamage;
    public int hits;
    public List<Transform> damagePoints = new List<Transform>();

    void Update()
    {
        for (int i = 0; i < damagePoints.Count;i++) {
            Collider[] colliders = Physics.OverlapSphere(damagePoints[i].position, 0.5f, doDamage);
            if (colliders.Length > 0) {

                for (int j = 0; j < colliders.Length; j++) {
                    Enemy e = colliders[i].GetComponent<Enemy>();
                    if (e != null) {
                        //push enemy away in  the direction of the enemy
                        e.TakeDamage(hits, (e.transform.position - damagePoints[i].transform.position).normalized);
                    }
                }
               
            }
        }
    }
}
