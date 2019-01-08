using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour {

    public GameObject opponent;
    public AnimationClip attack;
    public AnimationClip die;

    public int maxHealth;
    public int damage;
    public int health;

    public double impactTime;
    public bool impacted;
    public float range;

    bool started;
    bool ended;

    public float combatEscapeTime;
    public float countdown;
     
	// Use this for initialization
	void Start ()
    {
        health = maxHealth;
	}

	// Update is called once per frame
	void Update ()
    {
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

    void CombatEscapeCountdown()
    {
        countdown--;
        if(countdown == 0)
        {
            CancelInvoke("CombatEscapeCountdown");
        }
    }
    void Impact()
    {
        if (opponent != null && GetComponent<Animation>().IsPlaying("attack") && !impacted)
        {
            //if(GetComponent<Animation>()[attack.name].time)
            if((GetComponent<Animation>()[attack.name].time > GetComponent<Animation>()[attack.name].length * impactTime) && (GetComponent<Animation>()[attack.name].time < 0.9 * GetComponent<Animation>()[attack.name].length))
            {
                countdown = combatEscapeTime;
                CancelInvoke("CombatEscapeCountdown");
                InvokeRepeating("CombatEscapeCountdown", 0, 1);
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
