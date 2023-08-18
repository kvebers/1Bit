using System;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEditor.Rendering.PostProcessing;
using UnityEngine;
//using static UnityEngine.Rendering.DebugUI;
using UnityEngine.Timeline;

public class Echo : MonoBehaviour
{
    private float _strength;

    public void Initialize(float strength)
    {
        _strength = strength * 60;
    }

    private void Update()
    {
        if (_strength > 0)
        {
            float scaleFactor = 5;

            transform.localScale = new Vector3(transform.localScale.x + scaleFactor,
                                               transform.localScale.y + scaleFactor,
                                               transform.localScale.z + scaleFactor);

            _strength--;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}




