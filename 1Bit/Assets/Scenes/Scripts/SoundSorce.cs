using UnityEngine;

public class SoundSorce : MonoBehaviour
{
    public GameObject echoPrefab; // Assign the prefab in the Inspector
    public float strength = 10.0f; // Set the desired strength value

    void SpawnAndInitializePrefab()
    {
        Vector3 newPosition = new Vector3(transform.position.x, 5, transform.position.z);
        GameObject newEcho = Instantiate(echoPrefab, newPosition, Quaternion.Euler(90f, 0f, 0f));
        Echo echoScript = newEcho.GetComponent<Echo>();

        if (echoScript != null)
        {
            echoScript.Initialize(strength);
        }
        else
        {
            Debug.LogError("Echo script not found on the spawned prefab.");
        }
    }
    public void SpawnButtonClicked()
    {
        SpawnAndInitializePrefab();
    }
}