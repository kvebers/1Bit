using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TutorialText1 : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public string TextToDsiplay;
    void Start()
    {
        StartCoroutine(ChangeTextAfterDelay(2f, TextToDsiplay));
        StartCoroutine(ChangeTextAfterDelay(4f, "Use WASD, and mose to move around"));
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
