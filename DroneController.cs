using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    public float turnSpeed = 100;

    Rigidbody Drone;
    public Transform Blade_N;
    public Transform Blade_S;
    public Transform Blade_E;
    public Transform Blade_W;


    public Transform RBlade_N;
    public Transform RBlade_S;
    public Transform RBlade_E;
    public Transform RBlade_W;
    public Vector3 UpwardForce = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        Drone = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //Drone.AddRelativeForce(UpwardForce);
        Drone.AddForceAtPosition(UpwardForce, Blade_N.position);
        Drone.AddForceAtPosition(UpwardForce, Blade_S.position);
        Drone.AddForceAtPosition(UpwardForce, Blade_E.position);
        Drone.AddForceAtPosition(UpwardForce, Blade_W.position);

        RBlade_N.transform.Rotate(0, turnSpeed, 0);
        RBlade_S.transform.Rotate(0, turnSpeed, 0);
        RBlade_E.transform.Rotate(0, turnSpeed, 0);
        RBlade_W.transform.Rotate(0, turnSpeed, 0);
    }
}
