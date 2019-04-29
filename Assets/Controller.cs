using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public int life = 8;

    public Vector3 LastestCardinalDirection;
    public Vector3 LastGoodCardinalDirection;
    public Vector3 LastGoodDirection;
    public Vector3 LatestDirection;
    public Transform targeter;
    public float Speed;
    public List<Transform> wheelsTransforms = new List<Transform>();
    public List<Spin> wheels = new List<Spin>();
    Vector3 Velocity;
    public float deadzone;
    public Animator body;
    public string animationString;
    public ParticleSystem sparks;

    public KnockBack knocback;
    public Dictionary<string, string> animationDictionary = new Dictionary<string, string>();
    public SpinnyThing spinnyThing;
    public Vector3 velocity;
    public Attack Attack;
    public Transform spinlp;
    public static Controller INstance;

    public CharacterController mover;

    public void Awake() {
        if (INstance == null) {
            INstance = this;
        }

        animationDictionary.Add("(0.0, 0.0, -1.0)", "down");
        animationDictionary.Add("(0.0, 0.0, -0.5)", "down");

        animationDictionary.Add("(0.0, 0.0, 0.5)", "away");
        animationDictionary.Add("(0.0, 0.0, 1.0)", "away");

        animationDictionary.Add("(-1.0, 0.0, 0.0)", "left");
        animationDictionary.Add("(-0.5, 0.0, 0.0)", "left");

        animationDictionary.Add("(1.0, 0.0, 0.0)", "right");
        animationDictionary.Add("(0.5, 0.0, 0.0)", "right");

        animationDictionary.Add("(1.0, 0.0, 1.0)", "leftaway");
        animationDictionary.Add("(0.5, 0.0, 0.5)", "leftaway");

        animationDictionary.Add("(-1.0, 0.0, 1.0)", "rightaway");
        animationDictionary.Add("(-0.5, 0.0, 0.5)", "rightaway");

        animationDictionary.Add("(-1.0, 0.0, -1.0)", "downright");
        animationDictionary.Add("(-0.5, 0.0, -0.5)", "downright");

        animationDictionary.Add("(1.0, 0.0, -1.0)", "downleft");
        animationDictionary.Add("(0.5, 0.0, -0.5)", "downleft");
    }

    public void Update() {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        x = Mathf.Abs(x) < deadzone ? 0 : x;
        z = Mathf.Abs(z) < deadzone ? 0 : z;
        this.LatestDirection = new Vector3(x, 0, z);

        if (this.LatestDirection != Vector3.zero) {
            this.LastGoodDirection = this.LatestDirection;
            this.LastGoodCardinalDirection = this.LastGoodDirection.NSEWXZ();
            for (int i = 0; i < wheels.Count; i++) {
                wheelsTransforms[i].transform.forward = LastGoodCardinalDirection;
            }
        }

        animationString = LastGoodCardinalDirection.ToString();
        if (animationDictionary.ContainsKey(animationString) && !knocback.enabled) {
            body.SafePlay(animationDictionary[animationString]);
        }

        this.LastestCardinalDirection = this.LatestDirection.NSEWXZ();
       
        for (int i = 0; i < wheels.Count; i++) {
            if (LatestDirection != Vector3.zero) {
                wheels[i].speed = 3;
            } else {
                wheels[i].speed = 0;
            }
        }

        Velocity = LatestDirection * Speed * Time.smoothDeltaTime;

        if (!knocback.enabled) {
            mover.Move(Velocity);
        }

        if (!Attack.enabled && Input.GetMouseButtonDown(0) && Vector3.Distance(spinnyThing.transform.position, spinlp.position) < 1.0f) {
            Attack.SafeEnable();
        }

        if (!Attack.enabled) {
            spinnyThing.transform.position = Vector3.SmoothDamp(spinnyThing.transform.position, spinlp.position, ref velocity, 0.1f);
        }

        if (Input.GetMouseButtonDown(1) && spinnyThing.captured != null &&  Vector3.Distance(spinnyThing.transform.position, spinlp.position) < 1.0f) {
            Projectile projectile = spinnyThing.captured.transform.GetComponent<Projectile>();
            projectile.Launch(10, (targeter.position - transform.position).normalized);
            spinnyThing.captured.transform.SetParent(null);
            spinnyThing.captured = null;
            projectile.enabled = true;
        }
    }

    public void TakeDamage(int damage, Vector3 dir) {
        knocback.direction = dir;
        if (!knocback.enabled) {
            life += damage;
            knocback.GotoState();
        }
    }
}
