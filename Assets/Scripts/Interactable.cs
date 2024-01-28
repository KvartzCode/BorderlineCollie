using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private Material defaultMat;
    [SerializeField] Material outlineMat;
    private SpriteRenderer sr;
    public GameObject objectToSpawn = null;
    public bool canInteract = true;
    public AudioClip rustlingLeaf;

    private BuschParticleSystem bushParticles;

    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        defaultMat = sr.material;
        bushParticles = GetComponentInChildren<BuschParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!canInteract) { return; }
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

        audioSource.PlayOneShot(rustlingLeaf);
        SetOutline(false);
        bushParticles.particleSystemsEnabled = true; 
        canInteract = false;
        sr.sortingOrder = 5;

        if (objectToSpawn == null) return;
        if (objectToSpawn.GetComponent<Hyena>() != null) 
            objectToSpawn.GetComponent<Hyena>().Activate();
    }
}
