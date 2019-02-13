using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToMove : MonoBehaviour {

    private static Vector3 position;
    public static Vector3 cursorPosition;
    public float speed;
    public CharacterController controller;

    public AnimationClip run;
    public AnimationClip idle;

    public static bool attack;
    public static bool isDying;

    // Use this for initialization
    void Start () {
        position = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        locateCursor();
        if (!attack && !isDying)
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
                position = hit.point;
            }
        }
    }

    void locateCursor()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000))
        {
            //if (hit.collider.tag != "Player")
            if (hit.collider.tag != "Player" && hit.collider.tag != "Enemy")
            {
                cursorPosition = hit.point;
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
