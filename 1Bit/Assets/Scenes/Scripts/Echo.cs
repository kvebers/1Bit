using TMPro.EditorUtilities;
using UnityEngine;

public class Echo : MonoBehaviour
{
    private float _strength;

    public void Initialize(float strength)
    {
            _strength = strength * 120;
    }

    private void Update()
    {
        if (_strength >= 60)
        {
            float scaleFactor = 1.004f;
            
            transform.localScale = new Vector3(transform.localScale.x * scaleFactor,
                                               transform.localScale.y * scaleFactor,
                                               transform.localScale.z * scaleFactor);
            
        }
        else if (_strength >= 0)
        {
            float scaleFactor = 1.003f;
            transform.localScale = new Vector3(transform.localScale.x * scaleFactor,
                                               transform.localScale.y * scaleFactor,
                                               transform.localScale.z * scaleFactor);
        }
        else
        {
            Destroy(gameObject);
        }
        _strength--;

    }
}