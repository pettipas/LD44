using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    public LayerMask doDamage;
    public LayerMask world;
    public int hits;
    public List<Transform> damagePoints = new List<Transform>();
    public Enemy enemy;
    public Explosion explode;
    public CharacterController CTRL;
    public BoxCollider COLIDER;
    void Update()
    {
        CTRL.enabled = false;
        COLIDER.enabled = false;
        for (int i = 0; i < damagePoints.Count;i++) {
            Collider[] colliders = Physics.OverlapSphere(damagePoints[i].position, 1, doDamage);
            if (colliders.Length > 0) {

                for (int j = 0; j < colliders.Length; j++) {
                    Enemy e = colliders[i].GetComponent<Enemy>();
                    if (e != null) {
                        //push enemy away in  the direction of the enemy
                        e.TakeDamage(hits, (e.transform.position - damagePoints[i].transform.position).normalized);
                        enemy.life--;
                        if (enemy.life <= 0) {
                            for (int k = 0; k < damagePoints.Count; k++) {
                                explode.Duplicate(damagePoints[k].position);
                            }
                            Destroy(this.gameObject);
                        }
                    }
                }
            }
        }
    }

    public void LateUpdate() {
        for (int i = 0; i < damagePoints.Count; i++) {
            Collider[] colliders = Physics.OverlapSphere(damagePoints[i].position, 1, world);
            if (colliders.Length > 0) {
                for (int j = 0; j < damagePoints.Count; j++) {
                    explode.Duplicate(damagePoints[j].position);
                }
                Destroy(this.gameObject);
            }
        }
    }
}
