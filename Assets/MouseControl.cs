using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    public Camera maincam;
    public Transform reticule;
    public LayerMask targetingMask;
    public LayerMask interests;
    public Animator retAnimator;

    void Update() {
        Ray ray = maincam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, interests)) {
            reticule.position = new Vector3(hit.point.x, 0, hit.point.z);
            retAnimator.SafePlay("hitsomething");
            return;
        }

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, targetingMask)) {
            reticule.position = new Vector3(hit.point.x, 0, hit.point.z);
            retAnimator.SafePlay("reticleflash");
        }
    }
}