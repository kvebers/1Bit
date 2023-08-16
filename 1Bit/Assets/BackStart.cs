using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackStart : MonoBehaviour
{

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            changeScene();
        }
    }

    public void changeScene()
    {
        SceneManager.LoadScene("Intro");
    }
}
