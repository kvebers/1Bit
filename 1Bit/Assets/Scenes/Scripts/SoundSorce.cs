using UnityEngine;

public class SoundSorce : MonoBehaviour
{
    public GameObject echoPrefab;
    public float strength = 10.0f;

    private void Start()
    {
        InvokeRepeating("SpawnAndInitializePrefab", 0f, 2f);
    }

    void SpawnAndInitializePrefab()
    {
        Vector3 newPosition = new Vector3(transform.position.x, 8, transform.position.z);
        GameObject newEcho = Instantiate(echoPrefab, newPosition, Quaternion.Euler(90f, 0f, 0f));
        Echo echoScript = newEcho.GetComponent<Echo>();

        if (echoScript != null)
        {
            echoScript.Initialize(strength);
        }
        else
        {
            Debug.LogError("");
        }
    }
    public void SpawnButtonClicked()
    {
        SpawnAndInitializePrefab();
    }
}