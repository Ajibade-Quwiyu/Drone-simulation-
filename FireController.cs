using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    
    public void OnEnable()
    {
        
        Destroyer();
    }
    
    void Destroyer()
    {
        Destroy(this.gameObject,1);
    }
}
