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
        Destroy(this.gameObject,10);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject,3);
        }
    }
}
