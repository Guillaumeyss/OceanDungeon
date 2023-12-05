using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class who detect if the player is in the detection zone of the enemy
public class DetectionZone : MonoBehaviour
{
    public string tagPlayer = "Player";
    public List<Collider2D> detectedObjs = new List<Collider2D>();
    public Collider2D coll;

    //if the player enter in the zone, verify that he has the tag "Player" and add him to a list
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == tagPlayer)
        {
            detectedObjs.Add(other);
        }
        
    }

    //remove him if he leaves
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == tagPlayer)
        {
            detectedObjs.Remove(other);
        }
    }
}
