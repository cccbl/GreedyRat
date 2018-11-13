using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beginRoomRemove : MonoBehaviour {


    public GameObject gameObject;

	void Start () {

        Destroy(gameObject,10f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
}
