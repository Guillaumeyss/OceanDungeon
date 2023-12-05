using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//class to create a healthbar
public class HealthBarBehavior : MonoBehaviour
{
    public Slider Slider;
    public Color low;
    public Color high;
    public Vector3 Offset = new Vector3(0,1,0);

    //function to call each time that the health of the player drop
    public void SetHealth(float health, float maxHealth)
    {
        Slider.gameObject.SetActive(health < maxHealth);
        Slider.value = health;
        Slider.maxValue = maxHealth;

        Slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low,high,Slider.normalizedValue); 
    }

    //put the slider on the top of the player at each frame
    void Update()
    {
        Slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset);
    }
}
