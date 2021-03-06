﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour {

    public float speed;
    public float range;
    private int stunTime;

    public Transform player;
    public CharacterController controller;
    public LevelSystem playerLevel;

    public AnimationClip attack;
    public AnimationClip idle;
    public AnimationClip run;
    public AnimationClip die;

    public double impactTime = 0.35;
    public int maxHealth;
    public int health;
    public int damage;

    private bool impacted;
    private Combat opponent;

    // Use this for initialization
    void Start ()
    {
        health = maxHealth;
        opponent = player.GetComponent<Combat>();
	}
	
	// Update is called once per frame
	void Update ()
    {

        if (!IsDead())
        {
            if (stunTime <= 0)
            {
                if (!InRange())
                {
                    Chase();
                }
                else
                {
                    //GetComponent<Animation>().CrossFade("idle");
                    GetComponent<Animation>().CrossFade("attack");
                    Attack();

                    if ((GetComponent<Animation>()[attack.name].time > 0.9 * GetComponent<Animation>()[attack.name].length))
                    {
                        impacted = false;
                    }
                }
            }
            else
            {

            }

        }
        else
        {
            Die();
        }

	}
    public void getStun(int seconds)
    {
        CancelInvoke("stunCountDown");
        stunTime += seconds + 1;
        InvokeRepeating("stunCountDown", 0f, 1f);
    }

    void stunCountDown()
    {
        stunTime--;
        if(stunTime == 0)
        {
            CancelInvoke("stunCountDown");
        }
    }

    void Attack()
    {
        if((GetComponent<Animation>()[attack.name].time > GetComponent<Animation>()[attack.name].length * impactTime && !impacted) && (GetComponent<Animation>()[attack.name].time < 0.9 * GetComponent<Animation>()[attack.name].length))
        {
            opponent.GetHit(damage);
            impacted = true;
        }
    }
    bool InRange()
    {
        if (Vector3.Distance(transform.position, player.position) < range)
        {
            return true;
        }
        else return false;
    }

    void Chase()
    {
        transform.LookAt(player.position);
        controller.SimpleMove(transform.forward * speed);
        GetComponent<Animation>().CrossFade("run");

    }

    void OnMouseOver()
    {
        player.GetComponent<Combat>().opponent = gameObject;
    }
    
    void Die()
    {
        GetComponent<Animation>().CrossFade("die");

        if(GetComponent<Animation>()[die.name].time > GetComponent<Animation>()[die.name].length * 0.9)
        {
            playerLevel.exp += 100; 
            Destroy(gameObject);
        }
    }

    bool IsDead()
    {
        if (health <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    public void GetHit(double damage)
    {
        health = health - (int)damage;
        if (health < 0)
        {
            health = 0;
        }
    }
}
