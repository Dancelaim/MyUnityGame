using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class ProgressBarScript : MonoBehaviour
{
    public Image cooldown;
    bool coolingDown;
    public float waitTime = 30.0f;

    
    void Update()
    {
        if (coolingDown == true)
        {
            cooldown.fillAmount -= 1.0f / waitTime * Time.deltaTime;
        }
    }
}