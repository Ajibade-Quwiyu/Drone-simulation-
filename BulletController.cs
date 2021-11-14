using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    PlayerController playerControllerScript;
    private UIManager _uiManager;
    public float bulletRate;
    public float recoilRate;
    public Rigidbody fire;
    public AudioClip bulletSound;
    public GameObject bullet;
    public int maxAmmo;
    public int fireCount;
   
 
    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("UI Manager").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
            playerControllerScript.fireCount = fireCount;
          // playerControllerScript.maxAmmo = maxAmmo;
            playerControllerScript.currentAmmo = maxAmmo;

           
            Destroy(gameObject);
            _uiManager.UpdateFireCount(playerControllerScript.fireCount);

        }
    }
}
