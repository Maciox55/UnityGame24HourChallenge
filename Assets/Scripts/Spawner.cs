using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject[] objects;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float random = Random.Range(0f, 1f);
        {
            if (random > .95f)
            {
                Spawn(Random.Range(0,objects.Length));
            }
        }


		
	}





    public void Spawn(int index) {

        Instantiate(objects[index], new Vector3(this.transform.position.x, Random.Range(-9.7f, 9.7f), 0), Quaternion.identity);

    }
}
