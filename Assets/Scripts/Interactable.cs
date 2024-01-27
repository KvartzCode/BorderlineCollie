using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] Material defaultMat;
    [SerializeField] Material outlineMat;
    private SpriteRenderer sr;
    private bool canInteract = true;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SetOutline(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SetOutline(false);
        }
    }

    private void SetOutline(bool outlined)
    {
        if (outlined)
            sr.material = outlineMat;
        else
            sr.material = defaultMat;
    }

    public void Interact()
    {
        if (!canInteract) { return; }

        SetOutline(false);
        canInteract = false;
        sr.sortingOrder = 5;
    }
}
