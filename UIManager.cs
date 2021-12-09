using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    PlayerController playerControllerScript;
    [SerializeField] private TextMeshProUGUI _ammoText;

    private void Start()
    {
        playerControllerScript = GetComponent<PlayerController>();
    }

    public void UpdateFireCount(int count,int max)
    {
        //count = playerControllerScript.currentAmmo;
        _ammoText.text = "AMMO: " + count + "/" + max;
    }
}
