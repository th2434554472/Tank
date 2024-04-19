using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 
/// </summary>

public class Option : MonoBehaviour{
    private int choice = 1;
    public Transform posOne;
    public Transform posTwo;

    private void Update(){
        if (Input.GetKeyDown(KeyCode.W)) {
            choice = 1;
            transform.position = posOne.position;
        }else if (Input.GetKeyDown(KeyCode.S)) {
            choice = 2;
            transform.position = posTwo.position;
        }

        if (choice == 1 && Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene(1);
        }
    }
    
}
