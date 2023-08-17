using UnityEngine;

public class SoundSorce : MonoBehaviour
{
    public GameObject echoPrefab;
    public float strength = 10.0f;
    public AudioClip[] audioClips;
    private int currentClipIndex = 0;

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
            if (audioClips.Length > 0)
            {
                AudioSource audioSource = newEcho.GetComponent<AudioSource>();
                if (audioSource == null)
                {
                    audioSource = newEcho.AddComponent<AudioSource>();
                }
                audioSource.clip = audioClips[currentClipIndex];
                audioSource.Play();
                currentClipIndex = (currentClipIndex + 1) % audioClips.Length;
            }
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