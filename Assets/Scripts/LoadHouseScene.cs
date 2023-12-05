using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] string sceneName;
    
    private void OnTriggerEnter2D(Collider2D other) {
        //when a player enter in collision with the door, load an other scene
        if(other.gameObject.name == "Player")
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
