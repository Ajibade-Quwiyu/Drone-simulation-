using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    PlayerController playerControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GetComponent<PlayerController>();
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerControllerScript.isShooting = true;
            gameObject.SetActive(false);
            
        }
    }
}
