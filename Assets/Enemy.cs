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

    public bool notcaptured() {
        return agent.isActiveAndEnabled;
    }

    public TakingDamage damnagedState;
    public Damager damager;
    public AttackPlayer attack;
    public Transform damagePoint;
 

    public void Update() {
        if (agent.enabled && Vector3.Distance(Controller.INstance.transform.position, transform.position) < 20) {
            agent.updateRotation = false;
            agent.SetDestination(Controller.INstance.transform.position);
        }

        Vector3 dampDir = (Controller.INstance.transform.position - transform.position).normalized;
        damagePoint.transform.position = transform.position + dampDir * 1.1f;
    }

    public void ParentToSpinnyThing(SpinnyThing thing) {
        if (life > 0 && agent.isActiveAndEnabled) {
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
    }

    public void TakeDamage(int hits, Vector3 awayFromWeapon) {
        this.life -= hits;
        damnagedState.direction = awayFromWeapon;
        damnagedState.GotoState();
    }

    public void OnDrawGizmos() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(damagePoint.position, 0.3f);
    }
}