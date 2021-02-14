using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//ADD THIS TO BE ABLE TO USE SCENEMANAGER

public class GameOverDeath : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (gameObject.transform.position.y < 0)
        {
            Destroy(GameObject.Find("WhisperSource"));//Off the audios playing in the background
            GrabPickups.levelNum = 1; //When the character is dead, reset the level to 1
            SceneManager.LoadScene("GameOver");//transition to another scene (GameOver)
        }
    }
}
