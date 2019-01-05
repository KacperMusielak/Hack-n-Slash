using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour {

    public float speed;
    public float range;

    public Transform player;
    public CharacterController controller;

    public AnimationClip idle;
    public AnimationClip run;

    private int health;

    // Use this for initialization
    void Start ()
    {
        health = 100;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!InRange())
        {
            Chase();
        }
        else
        {
            GetComponent<Animation>().CrossFade("idle");
        }
        Debug.Log(health);
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

    public void GetHit(int damage)
    {
        health = health - damage;
    }
}
