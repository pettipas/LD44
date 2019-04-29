using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    public float height = 3.0f;
    public float spinnythingspeed;
    public NavMeshAgent agent;
    public LayerMask capturedLayer;
    public LayerMask flyinglayer;
    public int life;
    public TakingDamage damnagedState;
    public Damager damager;
    public AttackPlayer attack;
    public Transform damagePoint;
 

    public void Update() {
        if (agent.enabled) {
            agent.updateRotation = false;
            agent.SetDestination(Controller.INstance.transform.position);
        }

        Vector3 dampDir = (Controller.INstance.transform.position - transform.position).normalized;
        damagePoint.transform.position = transform.position + dampDir * 1.1f;
    }

    public void ParentToSpinnyThing(SpinnyThing thing) {
        agent.isStopped = true;
        agent.enabled = false;
        this.enabled = false;
        transform.SetParent(thing.transform, true);
        transform.position += Vector3.up * height;
        thing.speed = spinnythingspeed;
        this.gameObject.layer = Extensions.ToLayer(capturedLayer.value);
        damager.enabled = true;
        attack.enabled = false;
    }

    public void TakeDamage(int hits, Vector3 awayFromWeapon) {
        this.life -= hits;
        if (this.life <= 0) {
            Projectile projectile = this.GetComponent<Projectile>();
            projectile.Launch(13, awayFromWeapon);
            this.gameObject.layer = Extensions.ToLayer(flyinglayer.value);
        } else {
            damnagedState.direction = awayFromWeapon;
            damnagedState.GotoState();
        }
    }

    public void OnDrawGizmos() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(damagePoint.position, 0.3f);
    }
}