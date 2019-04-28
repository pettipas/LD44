using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoState
{
    public float travelTime;
    public Transform targeter;
    public LayerMask enemyMAsk;
    public Vector3 direction;
    public Transform launchPoint;
    public SpinnyThing spinnyTHing;
    public float noirmalspinspeed;

    public float speed;
    public float maxDistance;
    public Vector3 startPosition;
    public bool startedwithing;

    public bool HasSomething {
        get {
            return spinnyTHing.captured != null;
        }
    }

  

    public void OnEnable() {
        direction = (targeter.position - launchPoint.position).normalized;
        maxDistance = Vector3.Distance(targeter.position, launchPoint.position);
        startPosition = spinnyTHing.transform.position;
        startedwithing = HasSomething;
    }

    public void Update()
    {
        spinnyTHing.transform.position += direction * speed * Time.smoothDeltaTime; ;
        if (Vector3.Distance(startPosition, spinnyTHing.transform.position) > maxDistance || !startedwithing && HasSomething) {
            spinnyTHing.SetSpeed(noirmalspinspeed);
            this.SafeDisable();
            startedwithing = false;
        }
    }
}
