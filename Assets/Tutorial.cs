using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject dog;
    [SerializeField] GameObject girl;
    [SerializeField] GameObject duck;
    [SerializeField] GameObject waypoint;
    [SerializeField] GameObject bush;
    [SerializeField] Interactable interactable;
    [SerializeField] Camera cam;
    [SerializeField] GameObject hyena;
    [SerializeField] TextMeshProUGUI text;

    private void Start()
    {
        girl.GetComponent<PlayerMovement>().canMove = false;

        StartCoroutine(DoTutorial());
    }

    private IEnumerator DoTutorial()
    {
        float elapsedTime = 0f;
        float lerpDuration = 2f;
        Vector3 start = dog.transform.position;

        while (elapsedTime < lerpDuration)
        {
            float t = elapsedTime / lerpDuration;

            dog.transform.position = Vector3.Lerp(start, duck.transform.position, t);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
        dog.transform.position = duck.transform.position;

        yield return new WaitForSeconds(1);

        text.text = "I can see you!";

        yield return new WaitForSeconds(2);

        text.text = " ";

        elapsedTime = 0f;
        start = dog.transform.position;

        while (elapsedTime < 1)
        {
            float t = elapsedTime / 1;

            dog.transform.position = Vector3.Lerp(start, waypoint.transform.position, t);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        elapsedTime = 0f;
        start = dog.transform.position;

        while (elapsedTime < 1)
        {
            float t = elapsedTime / 1;

            dog.transform.position = Vector3.Lerp(start, bush.transform.position, t);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
        dog.transform.position = bush.transform.position;

        yield return new WaitForSeconds(2);

        text.text = "Where did you go?";

        yield return new WaitForSeconds(2);

        elapsedTime = 0f;
        float camStart = cam.orthographicSize;

        while (elapsedTime < lerpDuration)
        {
            float t = elapsedTime / lerpDuration;

            cam.orthographicSize = Mathf.Lerp(camStart, 15, t);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
        cam.orthographicSize = 15;
        text.text = "I have to find him...";

        girl.GetComponent<PlayerMovement>().canMove = true;

        yield return new WaitForSeconds(2);
        text.text = " ";

    }

    private void Update()
    {
        if (!interactable.canInteract)
        {
            hyena.SetActive(true);
            Invoke(nameof(SwitchScene), 3);
        }
    }

    private void SwitchScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
