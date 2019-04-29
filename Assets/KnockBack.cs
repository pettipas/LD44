using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoState
{
    public Animator bodyAnimator;
    public ParticleSystem sparkSystem;
    public CharacterController mover;
    public float duration = 1.5f;
    public float timeleft;
    public Vector3 direction;
    public float speed;

    public void OnEnable() {
        bodyAnimator.SafePlay("complain");
        sparkSystem.Emit(10);
        timeleft = 0;
    }

    public void Update() {
        timeleft += Time.smoothDeltaTime;
        mover.Move(direction * speed * Time.smoothDeltaTime);
        if (timeleft > duration) {
            sparkSystem.Emit(5);
            this.enabled = false;
        }
    }
}
