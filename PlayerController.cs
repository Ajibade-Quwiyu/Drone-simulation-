using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    protected Joystick moveJoystick;

    private bool isFiring;
    public bool isFlying = false;

    Rigidbody Drone;
    Vector3 DronePosition;
    Vector3 DroneRotation;
    
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
    public int currentAmmo;

    public Transform Blade1;
    public Transform Blade2;
    public Transform Blade3;
    public Transform Blade4;
   
    public Transform rBlade1;
    public Transform rBlade2;
    public Transform rBlade3;
    public Transform rBlade4;
   

    float Z_Axis;
    float Y_Axis;
    float X_Axis;

    public Transform tyre1;
    public Transform tyre2;
    public Transform tyre3;
    public Transform tyre4;
    public Vector3 UpwardForce = Vector3.zero;
    public float tiltAngle;

    [Header("Drone Movement Properties")]
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float rotateSpeed = 40;
    [SerializeField] private float flySpeed = 40;

    // Start is called before the first frame update
    void Start()
    {
        Drone = GetComponent<Rigidbody>();
        bulletSound = GetComponent<AudioClip>();
        AudioSource = GetComponent<AudioSource>();
        DronePosition = transform.position;
        currentAmmo = maxAmmo;
        _uiManager = GameObject.Find("UI Manager").GetComponent<UIManager>();
        moveJoystick = FindObjectOfType<Joystick>();
    }

    // Update is called once per frame
    void Update()
    {
        // LookAtMouse();
        //  InputSystemForPc();
        InputSystemForMobile();
    }

    private void FixedUpdate()
    {
        Drone.velocity = DronePosition * moveSpeed;
        Rotate();

        ForceAdd(UpwardForce);
        //Drone.AddRelativeForce(UpwardForce);
    }
    void ForceAdd(Vector3 Force)
    {
        Drone.AddForceAtPosition(Force, Blade1.position);
        Drone.AddForceAtPosition(Force, Blade2.position);
        Drone.AddForceAtPosition(Force, Blade3.position);
        Drone.AddForceAtPosition(Force, Blade4.position);
       
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
    // PC inputs stops here .......All the codes below are for Mobile input
    void InputSystemForMobile()
    {
        DroneRotation = new Vector3(0, Mathf.Atan2(moveJoystick.Horizontal, moveJoystick.Vertical) * 180 / Mathf.PI, 0);

        if (!isFlying)
        {
            UpwardForce.y = 24.525f;
            Drone.drag = 10;
        }

        if (moveJoystick.Horizontal != 0 || moveJoystick.Vertical != 0)
        {
            DronePosition = transform.forward;
            transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.Euler(DroneRotation), Time.deltaTime * rotateSpeed);
        }
        else
        {
            DronePosition = Vector3.zero;
        }
    }
    public void Firing()
    {
        isFiring = true;
        if (fire != null && isFiring == true)
        {
            AddRecoil();
            Rigidbody fireInstance;
          
            currentAmmo -= fireCount;
            fireInstance = Instantiate(fire, fireEnd.position, fireEnd.rotation) as Rigidbody;
            fireInstance.AddForce(fireEnd.forward * bulletRate);
            StartCoroutine(Coilstop());
            AudioSource.PlayOneShot(bulletSound, 1.0f);

            _uiManager.UpdateFireCount(currentAmmo,maxAmmo);
        }
        IEnumerator Coilstop()
        {
            yield return new WaitForSeconds(recoilRate);
            StopRecoil();
        }
    }
    public void NotFiring()
    {
        isFiring = false;
    }
    public void MoveUpOn()
    {
        isFlying = true;
        Drone.drag = 0.5f;
        UpwardForce.y += 0.1f* flySpeed;
    }
    public void MoveUpOff()
    {
        isFlying = false;
    }
    public void MoveDownOn()
    {
        isFlying = true;
        Drone.drag = 0.5f;
        UpwardForce.y -= 0.1f* flySpeed;
        
    }
    public void MoveDownOff()
    {
        isFlying = false;
    }

}
