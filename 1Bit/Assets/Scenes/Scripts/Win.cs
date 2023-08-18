using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public void changeScene()
    {
        SceneManager.LoadScene("Intro");
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            SceneManager.LoadScene(i);
        }
    }
}
