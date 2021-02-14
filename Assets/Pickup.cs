using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		transform.Rotate(0, 5f, 0, Space.World);
	}
}
