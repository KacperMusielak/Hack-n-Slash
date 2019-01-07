using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour {

    public GameObject opponent;
    public AnimationClip attack;
    public AnimationClip die;

    public int damage;
    public int health;

    public double impactTime;
    public bool impacted;
    public float range;

    bool started;
    bool ended;
     
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
        Die();
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

    public bool IsDead()
    {
        return health == 0;
    }

    void Die()
    {
        if (IsDead() && !ended)
        {
            if (!started)
            {
                ClickToMove.isDying = true;
                GetComponent<Animation>().CrossFade("die");
                started = true;
            }
            if (started && !GetComponent<Animation>().IsPlaying("die"))
            {
                Debug.Log("You have died");

                //health = 100;
               // ended = true;
                //started = false;
               // ClickToMove.isDying = false;
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
