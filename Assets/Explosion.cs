using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public Animator animator;

    public void Update()
    {
        if (animator.AtEndOfAnimation()) {
            Destroy(this.gameObject);
        }
    }
}
