using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeSound : MonoBehaviour
{
    void Start()
    {
        InvokeRepeating(nameof(Thing), 1, 5);
    }

    private void Thing()
    {
        VisualSoundCues.Instance.MadeSound(transform.position);
        GetComponent<AudioSource>().Play();
    }
}
