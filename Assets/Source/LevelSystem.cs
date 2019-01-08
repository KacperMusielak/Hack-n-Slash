using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour {

    public int level;
    public int exp;

    public Combat player;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        LevelUp();
	}

    void LevelUp()
    {
        if (exp >= (int)(Mathf.Pow(level, 2)+100))
        {
            level++;
            exp = exp - (int)(Mathf.Pow(level, 2) + 100);
            LevelEffect();
        }
    }

    void LevelEffect()
    {
        player.maxHealth += 100;
        player.damage += (int)(Mathf.Pow(level, 2));
        player.health = player.maxHealth;
    }
}
