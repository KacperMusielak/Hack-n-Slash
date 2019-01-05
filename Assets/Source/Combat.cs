using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour {

    public GameObject opponent;
    public AnimationClip attack;

    public int damage;

    public double impactTime;
	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            GetComponent<Animation>().CrossFade("attack");
            ClickToMove.attack = true;

            if (opponent != null)
            {
                transform.LookAt(opponent.transform.position);
                opponent.GetComponent<Mob>().GetHit(damage);
            }
           

        }
        if (!GetComponent<Animation>().IsPlaying("attack"))
        { 
            ClickToMove.attack = false;
        }

        Impact();
	}

    void Impact()
    {
        if (opponent != null && GetComponent<Animation>().IsPlaying("attack"))
        {
            //if(GetComponent<Animation>()[attack.name].time)
        }
    }
}
