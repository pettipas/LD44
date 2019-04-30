using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnyThing : MonoBehaviour
{
    public float speed;
    Transform mTrans;
    public Enemy captured;
    public Transform guy;
        
    void Start() {
        mTrans = transform;
    }


    void Update() {

        if (captured != null && captured.notcaptured()) {
            captured = null;
        }

        ApplyDelta(Time.deltaTime);
    }

    public void SetSpeed(float speed) {
        if (captured == null) {
            this.speed = speed;
        }
    }

    public void ApplyDelta(float delta) {

        delta *= Mathf.Rad2Deg * Mathf.PI * 2f;
        Quaternion offset = Quaternion.Euler(new Vector3(0f, speed, 0f) * delta);
        mTrans.rotation = mTrans.rotation * offset;

        Collider[] enemies = Physics.OverlapSphere(transform.position, 0.3f);
        for (int i = 0; i < enemies.Length; i ++) {
            Enemy enemy = enemies[i].transform.GetComponent<Enemy>();
            if (captured == null && enemy != null && Vector3.Distance(guy.transform.position, transform.position) > 1.0f) {
                captured = enemy;
                enemy.ParentToSpinnyThing(this);
            }
        }
    }
}
