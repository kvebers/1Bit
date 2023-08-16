using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] objectsToDestroy;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(DestroyObjectsWithDelay());
        }
    }
    public  IEnumerator DestroyObjectsWithDelay()
    {
        foreach (GameObject obj in objectsToDestroy)
        {
            Destroy(obj);
            yield return new WaitForSeconds(1);
        }

        // Change the scene after destroying objects
        SceneManager.LoadScene("Tutorial");
    }
}
