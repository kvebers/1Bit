using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackStart : MonoBehaviour
{
    public void changeScene()
    {
        SceneManager.LoadScene("Intro");
    }
}
