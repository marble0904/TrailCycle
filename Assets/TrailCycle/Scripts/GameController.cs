using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject enemyPrefab;
    public GameObject[] generatePoint;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject enemy = Instantiate(enemyPrefab) as GameObject;
            //int px = Random.Range(-6,7);
            //px = px * 4;
            int genPoint = Random.Range(0,generatePoint.Length);
            enemy.transform.position = generatePoint[genPoint].transform.position;
            enemy.transform.rotation = generatePoint[genPoint].transform.rotation;

        }
	}
}
