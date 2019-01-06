using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour {

    public GameObject opponent;
    public AnimationClip attack;

    public int damage;
    public int health;

    public double impactTime;
    public bool impacted;
    public float range;
	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log(health + " Player");
        if (Input.GetKey(KeyCode.Space) && InRange())
        {
            GetComponent<Animation>().CrossFade("attack");
            ClickToMove.attack = true;

            if (opponent != null)
            {
                transform.LookAt(opponent.transform.position);

            }
           

        }
        //if (!GetComponent<Animation>().IsPlaying("attack"))
        if (GetComponent<Animation>()[attack.name].time > 0.9 * GetComponent<Animation>()[attack.name].length)
        { 
            ClickToMove.attack = false;
            impacted = false;
        }

        Impact();
	}

    void Impact()
    {
        if (opponent != null && GetComponent<Animation>().IsPlaying("attack") && !impacted)
        {
            //if(GetComponent<Animation>()[attack.name].time)
            if((GetComponent<Animation>()[attack.name].time > GetComponent<Animation>()[attack.name].length * impactTime) && (GetComponent<Animation>()[attack.name].time < 0.9 * GetComponent<Animation>()[attack.name].length))
            {
                opponent.GetComponent<Mob>().GetHit(damage);
                impacted = true;
            }
        }
    }

    bool InRange()
    {
        return Vector3.Distance(opponent.transform.position, transform.position) <= range;
    }

    public void GetHit(int damage)
    {
        health = health - damage;
        if (health < 0)
        {
            health = 0;
        }
    }
}
