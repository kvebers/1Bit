using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TutorialText : MonoBehaviour
{
    public TextMeshProUGUI Text;
    void Start()
    {
        Text.text = "Tutorial";
        Debug.Log("Ī was here");
        StartCoroutine(ChangeTextAfterDelay(2f, "Use WASD, and mose to move around"));
        StartCoroutine(ChangeTextAfterDelay(4f, "Press SPACE to find secret passages in the map Using ECHO location"));
        StartCoroutine(ChangeTextAfterDelay(6f, ""));
        DestroyTextAfterDelay(8f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator ChangeTextAfterDelay(float delay, string txt)
    {
        yield return new WaitForSeconds(delay);
        Text.text = txt;
    }

    IEnumerator DestroyTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(Text.gameObject);
    }
}
