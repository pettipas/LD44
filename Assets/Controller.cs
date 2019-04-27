using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Vector3 LastestCardinalDirection;
    public Vector3 LastGoodCardinalDirection;
    public Vector3 LastGoodDirection;
    public Vector3 LatestDirection;

    public float Speed;
    public List<Transform> wheelsTransforms = new List<Transform>();
    public List<Spin> wheels = new List<Spin>();
    Vector3 Velocity;
    public float deadzone;
    public Animator body;
    public string animationString;

    public Dictionary<string, string> animationDictionary = new Dictionary<string, string>();


    public void Awake() {

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
        if (animationDictionary.ContainsKey(animationString)) {
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
        transform.Translate(Velocity);
    }
}
