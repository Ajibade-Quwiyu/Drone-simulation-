using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text activeFireText;
    private void Start()
    {
        activeFireText.text = "AMMO: " + 100;
    }
    public void UpdateFireCount()
    {
        activeFireText.text = "AMMO: " + PlayerController.fireCount;
    }
}
