using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Animator cam;
    public bool click = true;
   
    
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void Cam()
    {
        cam.Play("cam1 to cam2");
        Debug.Log("playing");
    }
}
