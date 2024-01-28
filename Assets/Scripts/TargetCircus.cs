using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetCircus : MonoBehaviour
{


    void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            VisualSoundCues.Instance.SetArrowPos(transform.position);
        }
    }

    public void WinningGame()
    {
        SceneManager.LoadScene("EndScene");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            WinningGame();
        }
    }

}
