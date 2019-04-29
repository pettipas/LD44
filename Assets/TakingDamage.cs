using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

public class TakingDamage : MonoState
{
    public LayerMask flyingState;
    public LayerMask enemyState;
    public Vector3 direction;
    public NavMeshAgent agent;
    public CharacterController ctrller;
    public float timehit;
    public float duration;
    public Animator bodyAnimator;
    public TvGuy tvguy;

    public void OnEnable() {
        gameObject.layer = Extensions.ToLayer(flyingState);
        agent.isStopped = true;
        agent.enabled = false;
        timehit = 0;
        direction = new Vector3(direction.x,0, direction.z);
        bodyAnimator.SafePlay("badguy_hurt");
        tvguy.enabled = false;
    }

    public void Update()
    {
        timehit += Time.smoothDeltaTime;
        ctrller.Move(direction * 3.0f * Time.smoothDeltaTime);

        if (timehit > duration ) {
            transform.position = new Vector3(transform.position.x,0, transform.position.z);
            this.enabled = false;
            gameObject.layer = Extensions.ToLayer(enemyState);
            agent.enabled = true;
            agent.isStopped = false;
            tvguy.enabled = true;
        }
    }
}
