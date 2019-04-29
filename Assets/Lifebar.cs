using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifebar : MonoBehaviour
{
    public List<SpriteRenderer> bars = new List<SpriteRenderer>();

    public Controller ctrl;

    public void Update()
    {
        if (ctrl.life >= 0 && ctrl.life <= 7) {
            for (int i = 0; i < 8; i++) {
                if (i == ctrl.life) {
                    bars[i].enabled = true;
                } else {
                    bars[i].enabled = false;
                }
            }
        } else {
            for (int i = 0; i < 8; i++) {
                bars[i].enabled = false;
            }
            bars[8].enabled = true;
        }
    }
}
