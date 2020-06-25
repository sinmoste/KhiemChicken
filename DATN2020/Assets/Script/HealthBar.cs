using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    public Vector3 scale;
    public opposumwalker creep;
    
    public void SetMaxHealth(int health)
    {
        scale = gameObject.transform.localScale;
        slider.maxValue = health;
        slider.value = health;
    }
    public void SetHealth(int health)
    {
        slider.value = health;
    }

    //kiểm tra hướng thanh máu  
    public void checkScale()
    {

        scale.x *= -1;
        transform.localScale = scale;

    }
}
