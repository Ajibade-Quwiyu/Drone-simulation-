using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _ammoText;
    
    public void UpdateFireCount(int count)
    {
        _ammoText.text = "AMMO: " + count;
    }
}
