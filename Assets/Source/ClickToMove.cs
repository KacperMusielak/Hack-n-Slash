using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToMove : MonoBehaviour {

    private Vector3 position;
    public float speed;
    public CharacterController controller;

    public AnimationClip run;
    public AnimationClip idle;

    public static bool attack;

    // Use this for initialization
    void Start () {
        position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (!attack)
        {
            if (Input.GetMouseButton(0))
            {
                //Locate where the player clicked on the terrain
                LocatePosition();
            }

            //Move the player to the position
            MoveToPosition();
        }
        else
        {

        }
        

    }

    void LocatePosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000))
        {
            //if (hit.collider.tag != "Player")
            if (hit.collider.tag != "Player" && hit.collider.tag != "Enemy")
            {
                position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            }
        }
    }

    void MoveToPosition()
    {
        if (Vector3.Distance(transform.position, position)>1)
        {
            Quaternion newRotation = Quaternion.LookRotation(position - transform.position);
            newRotation.x = 0f;
            newRotation.z = 0f;

            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 10);

            controller.SimpleMove(transform.forward * speed);

            GetComponent<Animation>().CrossFade("run");
        }
        else
        {
            GetComponent<Animation>().CrossFade("idle");
        }
    }
}
