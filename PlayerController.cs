using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    protected Joystick moveJoystick;

    Rigidbody Drone;
    Vector3 DronePosition;
    
    public Rigidbody fire;
    public Transform fireEnd;
    public Vector3 recoil;
    public float recoilRate;
    Vector3 originalRotation;
    public GameObject gunTank;
    public float bulletRate;
    public AudioClip bulletSound;
    public AudioSource AudioSource;

    public int fireCount;
    private UIManager _uiManager;
    public int maxAmmo = 50;

    public Transform Blade1;
    public Transform Blade2;
    public Transform Blade3;
    public Transform Blade4;
    public Transform Blade5;
    public Transform Blade6;
    public Transform rBlade1;
    public Transform rBlade2;
    public Transform rBlade3;
    public Transform rBlade4;
    public Transform rBlade5;
    public Transform rBlade6;

    float Z_Axis;
    float Y_Axis;
    float X_Axis;

    public Transform tyre1;
    public Transform tyre2;
    public Transform tyre3;
    public Transform tyre4;
    public Vector3 UpwardForce = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        Drone = GetComponent<Rigidbody>();
        bulletSound = GetComponent<AudioClip>();
        AudioSource = GetComponent<AudioSource>();
        DronePosition = transform.position;
        fireCount = maxAmmo;
        _uiManager = GameObject.Find("UI Manager").GetComponent<UIManager>();
        moveJoystick = FindObjectOfType<Joystick>();
    }

    // Update is called once per frame
    void Update()
    {
        // LookAtMouse();
        //  InputSystemForPc();
        DronePosition = new Vector3(moveJoystick.Vertical * -2, Y_Axis, moveJoystick.Horizontal * 2);
    }

    private void FixedUpdate()
    {
        float movespeed = 5;
        Drone.MovePosition(transform.position + DronePosition * Time.fixedDeltaTime * movespeed);
        ForceAdd(UpwardForce);
        Rotate();
        //Drone.AddRelativeForce(UpwardForce);
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
    void InputSystemForPc()
    {
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

        if (Input.GetKey(KeyCode.Mouse0) && fire != null)
        {
            AddRecoil();
            Rigidbody fireInstance;
            fireCount--;
            maxAmmo -= fireCount;
            fireInstance = Instantiate(fire, fireEnd.position, fireEnd.rotation) as Rigidbody;
            fireInstance.AddForce(fireEnd.forward * -bulletRate);
            StartCoroutine(Coilstop());
            AudioSource.PlayOneShot(bulletSound,1.0f);
        }
        IEnumerator Coilstop()
        {
            yield return new WaitForSeconds(recoilRate);
            StopRecoil();
        }    
    }
    public void MobileInputSystem()
    {
            if(fire != null)
            {
                AddRecoil();
                Rigidbody fireInstance;
                fireCount--;
                maxAmmo -= fireCount;
                fireInstance = Instantiate(fire, fireEnd.position, fireEnd.rotation) as Rigidbody;
                fireInstance.AddForce(fireEnd.forward * -bulletRate);
                StartCoroutine(Coilstop());
                AudioSource.PlayOneShot(bulletSound, 1.0f);
            }
            IEnumerator Coilstop()
            {
                yield return new WaitForSeconds(recoilRate);
                StopRecoil();
            }
    }
    void Rotate()
    {
        tyre1.transform.Rotate(10, 0, 0);
        tyre2.transform.Rotate(10, 0, 0);
        tyre3.transform.Rotate(10, 0, 0);
        tyre4.transform.Rotate(10, 0, 0);
        rBlade1.transform.Rotate(0, 100, 0);
        rBlade2.transform.Rotate(0, -100, 0);
        rBlade3.transform.Rotate(0, 100, 0);
        rBlade4.transform.Rotate(0, -100, 0);
        rBlade5.transform.Rotate(0, 100, 0);
        rBlade6.transform.Rotate(0, -100, 0);

    }
    void LookAtMouse()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDist;

        if (playerPlane.Raycast(ray, out hitDist))
        {
            Vector3 targetPoint = ray.GetPoint(hitDist);
            Quaternion targetRotation = Quaternion.LookRotation(transform.position - targetPoint);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2 * Time.deltaTime);
        }
    }
    private void AddRecoil()
    {
        gunTank.transform.localEulerAngles += recoil;
    }
    private void StopRecoil()
    {
        gunTank.transform.localEulerAngles = originalRotation;
    }
}
