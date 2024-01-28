using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject dog;
    [SerializeField] GameObject girl;
    [SerializeField] GameObject duck;
    [SerializeField] GameObject castle;
    [SerializeField] GameObject dogWaypoint;
    [SerializeField] GameObject girlWaypoint;
    [SerializeField] GameObject hyenaWaypoint1;
    [SerializeField] GameObject hyenaWaypoint2;
    [SerializeField] GameObject bush;
    [SerializeField] Interactable interactable;
    [SerializeField] Camera cam;
    [SerializeField] GameObject hyena;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Image panel;
    [SerializeField] AudioSource audioSource;

    [SerializeField] AudioClip[] voicelines;
    [SerializeField] AudioClip hyenaSound;
    [SerializeField] AudioClip whistle;
    [SerializeField] AudioClip scream;

    private bool done;

    private void Start()
    {
        girl.GetComponent<PlayerMovement>().canMove = false;

        StartCoroutine(DoTutorial());
    }

    private IEnumerator DoTutorial()
    {

        yield return new WaitForSeconds(1);

        audioSource.PlayOneShot(voicelines[0]);

        text.text = "Lets play hide and seek!";
        yield return new WaitForSeconds(2);

        float elapsedTime = 0f;
        float lerpDuration = 2f;
        float startAlpha = 0;

        while (elapsedTime < lerpDuration)
        {
            float t = elapsedTime / lerpDuration;

            panel.color = new Color(0, 0, 0, Mathf.Lerp(startAlpha, 1, t));

            elapsedTime += Time.deltaTime;

            yield return null;
        }
        panel.color = new Color(0, 0, 0, 1);
        text.text = " ";

        yield return new WaitForSeconds(1);

        dog.transform.position = duck.transform.position;

        yield return new WaitForSeconds(1);

        elapsedTime = 0f;
        lerpDuration = 2f;
        startAlpha = 1;

        while (elapsedTime < lerpDuration)
        {
            float t = elapsedTime / lerpDuration;

            panel.color = new Color(0, 0, 0, Mathf.Lerp(startAlpha, 0, t));

            elapsedTime += Time.deltaTime;

            yield return null;
        }
        panel.color = new Color(0, 0, 0, 0);
        text.text = " ";
        yield return new WaitForSeconds(1);

        audioSource.PlayOneShot(voicelines[1]);
        text.text = "I can see you!";

        yield return new WaitForSeconds(2);

        audioSource.PlayOneShot(voicelines[2]);

        text.text = "Let me hide for real then";

        elapsedTime = 0f;
        lerpDuration = 2f;
        startAlpha = 0;

        while (elapsedTime < lerpDuration)
        {
            float t = elapsedTime / lerpDuration;

            panel.color = new Color(0, 0, 0, Mathf.Lerp(startAlpha, 1, t));

            elapsedTime += Time.deltaTime;

            yield return null;
        }
        panel.color = new Color(0, 0, 0, 1);
        text.text = " ";

        yield return new WaitForSeconds(1);

        dog.transform.position = castle.transform.position;
        dog.SetActive(false);

        yield return new WaitForSeconds(1);

        elapsedTime = 0f;
        lerpDuration = 2f;
        startAlpha = 1;

        while (elapsedTime < lerpDuration)
        {
            float t = elapsedTime / lerpDuration;

            panel.color = new Color(0, 0, 0, Mathf.Lerp(startAlpha, 0, t));

            elapsedTime += Time.deltaTime;

            yield return null;
        }
        panel.color = new Color(0, 0, 0, 0);
        text.text = " ";

        yield return new WaitForSeconds(1);

        audioSource.PlayOneShot(voicelines[3]);

        text.text = "Where did you go?";

        yield return new WaitForSeconds(2);

        elapsedTime = 0f;
        float camStart = cam.orthographicSize;

        while (elapsedTime < 1)
        {
            float t = elapsedTime / lerpDuration;

            cam.orthographicSize = Mathf.Lerp(camStart, 15, t);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
        cam.orthographicSize = 15;

        audioSource.PlayOneShot(voicelines[4]);

        text.text = "I have to find him...";

        girl.GetComponent<PlayerMovement>().canMove = true;

        yield return new WaitForSeconds(2);
        text.text = " ";

    }

    private void Update()
    {
        if (!interactable.canInteract && !done)
        {
            done = true;
            audioSource.PlayOneShot(scream);
            girl.GetComponent<PlayerMovement>().canMove = false;
            girl.GetComponent<PlayerMovement>().GetScared();
            hyena.SetActive(true);

            StartCoroutine(EndTutorial());
            //Invoke(nameof(SwitchScene), 3);
        }
    }

    private IEnumerator EndTutorial()
    {
        float elapsedTime = 0f;
        float lerpDuration = 2f;
        Vector2 start = dog.transform.position;

        while (elapsedTime < lerpDuration)
        {
            float t = elapsedTime / lerpDuration;

            dog.transform.position = Vector2.Lerp(start, dogWaypoint.transform.position, t);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
        dog.transform.position = dogWaypoint.transform.position;
        text.text = " ";

        elapsedTime = 0f;
        lerpDuration = 2f;
        start = girl.transform.position;

        while (elapsedTime < lerpDuration)
        {
            float t = elapsedTime / lerpDuration;

            girl.transform.position = Vector2.Lerp(start, girlWaypoint.transform.position, t);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
        girl.transform.position = girlWaypoint.transform.position;
        text.text = " ";

        elapsedTime = 0f;
        lerpDuration = 2f;
        start = hyena.transform.position;

        while (elapsedTime < lerpDuration)
        {
            float t = elapsedTime / lerpDuration;

            hyena.transform.position = Vector2.Lerp(start, hyenaWaypoint1.transform.position, t);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
        hyena.transform.position = hyenaWaypoint1.transform.position;
        text.text = " ";

        audioSource.PlayOneShot(whistle);
        audioSource.PlayOneShot(hyenaSound);
        yield return new WaitForSeconds(0.25f);

        elapsedTime = 0f;
        lerpDuration = 2f;
        start = hyena.transform.position;

        while (elapsedTime < lerpDuration)
        {
            float t = elapsedTime / lerpDuration;

            hyena.transform.position = Vector2.Lerp(start, hyenaWaypoint2.transform.position, t);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
        hyena.transform.position = hyenaWaypoint2.transform.position;
    }

    private void SwitchScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
