using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnInput : MonoBehaviour {
	void Start () {
		
	}
	
	void Update () {
		if (Input.GetAxis("Submit") == 1) 
		{
			SceneManager.LoadScene("Play");
		}
	}
}
