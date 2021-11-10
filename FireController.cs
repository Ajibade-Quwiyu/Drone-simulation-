using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    private UIManager _ui;
    public void OnEnable()
    {
        PlayerController.fireCount--;
        _ui = GameObject.Find("UI Manager").GetComponent<UIManager>();
        _ui.UpdateFireCount();
        Destroyer();
    }
    
    void Destroyer()
    {
        Destroy(this.gameObject,1);
    }
}
