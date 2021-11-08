using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody Drone;
    Vector3 DronePosition;

    public Transform Blade1;
    public Transform Blade2;
    public Transform Blade3;
    public Transform Blade4;
    public Transform Blade5;
    public Transform Blade6;

    public float turnSpeed;
    public float Z_Axis;
    public float Y_Axis;
    public float X_Axis;

    public Transform tyre1;
    public Transform tyre2;
    public Transform tyre3;
    public Transform tyre4;
    public Vector3 UpwardForce = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        Drone = GetComponent<Rigidbody>();
        DronePosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        LookAtMouse();
        //move up
        if (Input.GetKey("o"))
        {
            Drone.drag = 0.5f;
            UpwardForce.y += 0.1f;
        }
        //move down
        else if (Input.GetKey("p"))
        {
            Drone.drag = 0.5f;
            UpwardForce.y -= 0.1f;
        }
        else
        {
            UpwardForce.y = 16.35f;
            Drone.drag = 10;
        }
            

        Z_Axis = Input.GetAxis("Horizontal");
        X_Axis = Input.GetAxis("Vertical");
        DronePosition = new Vector3(-X_Axis, Y_Axis, Z_Axis);
    }

    private void FixedUpdate()
    {
        float movespeed = 5;
        Drone.MovePosition(transform.position + DronePosition * Time.fixedDeltaTime * movespeed);
        ForceAdd(UpwardForce);
        //Drone.AddRelativeForce(UpwardForce);

        tyre1.transform.Rotate(turnSpeed, 0, 0);
        tyre2.transform.Rotate(turnSpeed, 0, 0);
        tyre3.transform.Rotate(turnSpeed, 0, 0);
        tyre4.transform.Rotate(turnSpeed, 0, 0);
    }
    void ForceAdd(Vector3 Force)
    {
        Drone.AddForceAtPosition(Force, Blade1.position);
        Drone.AddForceAtPosition(Force, Blade2.position);
        Drone.AddForceAtPosition(Force, Blade3.position);
        Drone.AddForceAtPosition(Force, Blade4.position);
        Drone.AddForceAtPosition(Force, Blade5.position);
        Drone.AddForceAtPosition(Force, Blade6.position);
    }
    void LookAtMouse()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDist;

        if (playerPlane.Raycast(ray, out hitDist))
        {
            Vector3 targetPoint = ray.GetPoint(hitDist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }

}
