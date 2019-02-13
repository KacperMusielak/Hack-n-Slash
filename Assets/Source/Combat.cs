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
    public bool inAction;
    public float range;

    bool started;
    bool ended;

    public float combatEscapeTime;
    public float countdown;

    public bool specialAttack; 
	// Use this for initialization
	void Start ()
    {
        health = maxHealth;
	}

	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !specialAttack)
        {
            inAction = true;
        }
        if (inAction)
        {
            if (attackMob(0, 1, KeyCode.Space, null, 0, true))
            {

            }
            else
            {
                inAction = false;
            }
        }

        Die();
	}

    public bool attackMob(int stunSeconds, double scaledDamage, KeyCode key, GameObject particleEffect, int projectile, bool opponentBased)
    {
        if (opponentBased)
        {
            if (Input.GetKey(key) && InRange())
            {
                GetComponent<Animation>().CrossFade("attack");
                ClickToMove.attack = true;
                transform.LookAt(opponent.transform.position);
            }
        }
        else
        {
            if (Input.GetKey(key) && InRange())
            {
                GetComponent<Animation>().CrossFade("attack");
                ClickToMove.attack = true;

                if (opponent != null)
                {
                    transform.LookAt(ClickToMove.cursorPosition);
                }
            }
        }

        //if (!GetComponent<Animation>().IsPlaying("attack"))
        if (GetComponent<Animation>()[attack.name].time > 0.9 * GetComponent<Animation>()[attack.name].length)
        {
            ClickToMove.attack = false;
            impacted = false;
            if (specialAttack)
            {
                specialAttack = false;
            }
            return false;
        }

        Impact(stunSeconds, scaledDamage, particleEffect, projectile, opponentBased);
        return true;
    }

    public void resetAttack()
    {
        ClickToMove.attack = false;
        impacted = false;
        GetComponent<Animation>().Stop("attack");

    }
    void stunMob()
    {

    }
    void CombatEscapeCountdown()
    {
        countdown--;
        if(countdown == 0)
        {
            CancelInvoke("CombatEscapeCountdown");
        }
    }
    void Impact(int stunSeconds, double scaledDamage, GameObject particleEffect, int projectile, bool opponentBased)
    {
        if ((!opponentBased || opponent != null) && GetComponent<Animation>().IsPlaying("attack") && !impacted)
        {
            //if(GetComponent<Animation>()[attack.name].time)
            if((GetComponent<Animation>()[attack.name].time > GetComponent<Animation>()[attack.name].length * impactTime) && (GetComponent<Animation>()[attack.name].time < 0.9 * GetComponent<Animation>()[attack.name].length))
            {
                countdown = combatEscapeTime;
                CancelInvoke("CombatEscapeCountdown");
                InvokeRepeating("CombatEscapeCountdown", 0, 1);
                opponent.GetComponent<Mob>().GetHit(damage*scaledDamage);
                if (specialAttack)
                {
                    opponent.GetComponent<Mob>().getStun(stunSeconds);
                }
                Quaternion rot = transform.rotation;
                rot.x = 0f;
                rot.z = 0f;

                if (projectile > 0)
                {
                    float rotOffset = 0;
                    for(int i = 0; i<projectile; i++)
                    {
                        //Instantiate(Resources.Load("Projectile"), new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), new Quaternion(rot.x, (rot.y+(float)i*0.1f)-rotOffset, rot.z, rot.w));
                        Instantiate(Resources.Load("Projectile"), new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), new Quaternion(rot.x, rot.y, rot.z, rot.w));
                    }
                    
                }
                if (particleEffect != null)
                {
                    Instantiate(particleEffect, new Vector3(opponent.transform.position.x, opponent.transform.position.y + 1.5f, opponent.transform.position.z), Quaternion.identity);
                }
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
