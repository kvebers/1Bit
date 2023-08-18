using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColors : MonoBehaviour
{
    public Color targetColor = Color.white;
    public float changeSpeed = 1f;
    public Material material;

    void Update()
    {
        Color currentColor = material.GetColor("_Color");
        material.SetColor("_Color", Color.Lerp(currentColor, targetColor, changeSpeed * Time.deltaTime));
    }
}
