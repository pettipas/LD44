using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPLayer : MonoBehaviour
{

    public Transform target;
    public Vector3 velocity;
    public void Update()
    {
        if (target != null) {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(target.position.x,transform.position.y, target.position.z), ref velocity, 0.8f);
        }
    }
}
