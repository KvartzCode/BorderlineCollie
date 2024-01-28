using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMusic : MonoBehaviour
{
    #region singleton
    public static BGMusic Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    #endregion

    [SerializeField] AudioSource defaultMusic;
    [SerializeField] AudioSource huntedMusic;

    private bool isHunted;
    [Range(0f, 0.8f)]
    public float distance;

    private void SwitchMusic()
    {
        isHunted = !isHunted;
        StartCoroutine(SwapFade());
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            distance += Time.deltaTime / 100;
        }

        huntedMusic.volume = distance;
        //defaultMusic.volume = 0.5f - huntedMusic.volume;
    }

    private IEnumerator SwapFade()
    {
        float timeToFade = 1f;
        float timer = 0;

        while (timer < timeToFade)
        {
            timer += Time.deltaTime;
            if (isHunted)
            {
                defaultMusic.volume = Mathf.Lerp(1, 0f, timer / timeToFade);
                huntedMusic.volume = Mathf.Lerp(0, 1f, timer / timeToFade);
            }
            else
            {
                huntedMusic.volume = Mathf.Lerp(1, 0f, timer / timeToFade);
                defaultMusic.volume = Mathf.Lerp(0, 1f, timer / timeToFade);
            }

            yield return null;
        }
    }
}