using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GrabPickups : MonoBehaviour 
{
	public static int levelNum = 1; //DECLARE AND INITIALIZE THE GAME LEVEL TO 1
	public Text Text;//DECLARE THE TYPE OF VARIABLE TEXT
	private AudioSource pickupSoundSource;

	void Awake() 
	{
		pickupSoundSource = DontDestroy.instance.GetComponents<AudioSource>()[1];
	
		Text.text = "Level: " + levelNum;// Update the number of the game level displayed on the screen
	}


	void OnControllerColliderHit(ControllerColliderHit hit) {
		if (hit.gameObject.tag == "Pickup") {
			levelNum++;//Increment the number of Level every time you find and grab the pickup
			pickupSoundSource.Play();
			SceneManager.LoadScene("Play");
		}
	}
}
