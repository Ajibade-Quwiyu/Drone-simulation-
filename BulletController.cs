using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    PlayerController playerControllerScript;
    public float bulletRate;
    public float recoilRate;
    public Rigidbody fire;
    public AudioClip bulletSound;
    public GameObject bullet;
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -10)
        {
            Destroy(gameObject);
        }if (transform.position.x > 10)
        {
            Destroy(gameObject);
        }if (transform.position.z > 10)
        {
            Destroy(gameObject);
        }if (transform.position.z < -10)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("collided");
            playerControllerScript = other.gameObject.GetComponent<PlayerController>();
            playerControllerScript.bulletSound = bulletSound;
            playerControllerScript.bulletRate = bulletRate;
            playerControllerScript.recoilRate = recoilRate;
            playerControllerScript.fire = fire;
            Destroy(gameObject);
            
        }
    }
}
