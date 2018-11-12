using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ParticleCollision : MonoBehaviour {

    public GameObject desSound; 

    void OnParticleCollision(GameObject obj)
    {
      if(obj.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("MainMenu");
        }
      else if(obj.gameObject.tag == "Enemy")
        {
            GameObject se = Instantiate(desSound) as GameObject;
            se.transform.position = obj.transform.position;
            Object.Destroy(obj);
        }   
    }
    
}
