using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour {

    public bool canLeave;

    public string NextScene;
    void Update()
    {
        if (canLeave == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(NextScene);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        canLeave = true;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        canLeave = false;
    }
}
