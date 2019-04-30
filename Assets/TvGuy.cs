using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TvGuy : MonoBehaviour
{
    public string animationString;
    public NavMeshAgent agent;
    public Dictionary<string, string> animationDictionary = new Dictionary<string, string>();
    public Animator body;
    public float HEIGHT;
    // Start is called before the first frame update
    void Awake()
    {
        animationDictionary.Add("(0.0, 0.0, -1.0)", "badguy_down");
        animationDictionary.Add("(0.0, 0.0, -0.5)", "badguy_down");

        animationDictionary.Add("(0.0, 0.0, 0.5)", "badguy_away");
        animationDictionary.Add("(0.0, 0.0, 1.0)", "badguy_away");

        animationDictionary.Add("(-1.0, 0.0, 0.0)", "badguy_right");
        animationDictionary.Add("(-0.5, 0.0, 0.0)", "badguy_right");

        animationDictionary.Add("(1.0, 0.0, 0.0)", "badguy_left");
        animationDictionary.Add("(0.5, 0.0, 0.0)", "badguy_left");

        animationDictionary.Add("(1.0, 0.0, 1.0)", "badguy_leftaway");
        animationDictionary.Add("(0.5, 0.0, 0.5)", "badguy_leftaway");

        animationDictionary.Add("(-1.0, 0.0, 1.0)", "badguy_rightaway");
        animationDictionary.Add("(-0.5, 0.0, 0.5)", "badguy_rightaway");

        animationDictionary.Add("(-1.0, 0.0, -1.0)", "badguy_downleft");
        animationDictionary.Add("(-0.5, 0.0, -0.5)", "badguy_downleft");

        animationDictionary.Add("(1.0, 0.0, -1.0)", "badguy_downright");
        animationDictionary.Add("(0.5, 0.0, -0.5)", "badguy_downright");
    }

    void Update()
    {

        if (agent.enabled) {
           Vector3 v = agent.velocity.normalized.NSEWXZ();
            v = new Vector3(v.x,0,v.z);
            animationString = (v).ToString();
            if (animationDictionary.ContainsKey(animationString)) {
                body.SafePlay(animationDictionary[animationString]);
            }  
        }
    }
}
