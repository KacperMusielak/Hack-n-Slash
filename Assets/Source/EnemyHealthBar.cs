using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour {

    public Combat player;
    public Texture2D healthFrame;
    public Rect healthFramePosition;

    public Texture2D healthBar;
    public Rect healthBarPosition;

    public float horizontalOffset;
    public float verticalOffset;
    public float widthScaling;
    public float heightScaling;

    public Mob target;
    public float healthPercentage;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (player.opponent != null)
        {
            target = player.opponent.GetComponent<Mob>();
            healthPercentage = (float)target.health / (float)target.maxHealth;
        }
        else
        {
            target = null;
            healthPercentage = 0;
        }

	}

    void OnGUI()
    {
        if (target != null && player.countdown>0)
        {
            DrawFrame();
            DrawBar();
        }

    }

    void DrawFrame()
    {
        healthFramePosition.x = (Screen.width - healthFramePosition.width) / 2;
        float width = 0.375f;
        float height = 0.06f;
        healthFramePosition.width = Screen.width * width;
        healthFramePosition.height = Screen.height * height;
        GUI.DrawTexture(healthFramePosition, healthFrame);
    }

    void DrawBar()
    {
        healthBarPosition.x = healthFramePosition.x+horizontalOffset * healthBarPosition.width;
        healthBarPosition.y = healthFramePosition.y+verticalOffset * healthBarPosition.height;
        healthBarPosition.width = healthFramePosition.width * widthScaling * healthPercentage;
        healthBarPosition.height = healthFramePosition.height * heightScaling;
        GUI.DrawTexture(healthBarPosition, healthBar);
    }
}
