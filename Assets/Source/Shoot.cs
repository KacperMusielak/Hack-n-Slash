using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Pool pool;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            pool.activate(1, new Vector3(0f, 7f, -2f), Quaternion.identity);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            pool.activate(2, new Vector3(0f, 7f, -2f), Quaternion.identity);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            pool.activate(0, new Vector3(0f, 7f, -2f), Quaternion.identity);
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            pool.activate(3, new Vector3(0f, 7f, -2f), Quaternion.identity);
        }
    }
}
