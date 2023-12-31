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




