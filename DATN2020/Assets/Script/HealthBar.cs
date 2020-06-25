using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    public Vector3 scale;
    public opposumwalker creep;
    public void Start()
    {
        creep = GameObject.FindGameObjectWithTag("Enemy").GetComponent<opposumwalker>();

    }
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void SetHealth(int health)
    {
        slider.value = health;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ChangeDirection();
    }
    public void ChangeDirection()
    {
        Vector3 temp = transform.localScale;

        if (creep.faceright == false)
        {
            temp.x = 1f;
        }
        else
        {
            temp.x = -1f;
        }
        transform.localScale = temp;
    }

}
