using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour {

    public Combat player;
    public KeyCode key;
    public double damagePercentage;
    public int stunTime;
    public bool inAction;
    public GameObject particleEffect;
    public int projectile;
    public bool opponentBased;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(key) && !player.specialAttack)
        {
            player.resetAttack();
            player.specialAttack = true;
            inAction = true;
        }
        if (inAction)
        {
            if(player.attackMob(stunTime, damagePercentage, key, particleEffect, projectile, opponentBased))
            {

            }
            else
            {
                inAction = false;
            }
        }

        
	}
}
