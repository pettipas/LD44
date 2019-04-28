using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public Vector3 direction;
    public float speed;

    public void Launch(float speed, Vector3 dir) {
        this.speed = speed;
        this.direction = dir;
    }

    void Update() {
        transform.position += direction * Time.smoothDeltaTime * speed;
    }
}
