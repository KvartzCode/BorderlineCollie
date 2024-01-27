using System.Collections.Generic;
using UnityEngine;

public enum HyenaState
{
    Inactive,
    Retreating, // Running away from player
    Lurking, // Hding outside player view until attack is triggered
    Approaching, // Can be scared away during this state
    Attacking
}

public class Hyena : MonoBehaviour
{
    public HyenaState state = HyenaState.Inactive;

    private GameObject player;
    private Rigidbody2D rb2d;

    private int confidence = 1;
    private float actionTimer = 0;
    private float actionTimerMax = 1;
    private int timesScared = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (actionTimer <= 0 && state != HyenaState.Inactive)
        {
            actionTimer = actionTimerMax;
            PerformAction();
        }

        actionTimer -= Time.deltaTime;
    }

    private void PerformAction()
    {
        Vector2 direction = player.transform.position - transform.position;
        switch (state)
        {
            case HyenaState.Retreating:
                rb2d.velocity = direction.normalized * -4;
                if (direction.sqrMagnitude > 200)
                {
                    rb2d.velocity = Vector2.zero;
                    state = HyenaState.Lurking;
                    actionTimerMax = 8;
                    actionTimer = 0;
                }
                break;

            case HyenaState.Lurking:
                Vector3 randomPosition = Random.insideUnitCircle.normalized * 12;

                transform.position = player.transform.position + randomPosition;

                if (Random.Range(0, 11) + confidence >= 11)
                {
                    state = HyenaState.Approaching;
                    actionTimerMax = 0.25f;
                    actionTimer = 0;
                }
                break;

            case HyenaState.Approaching:
                rb2d.velocity = direction.normalized;
                if(direction.sqrMagnitude <= 6)
                {
                    state = HyenaState.Attacking;
                    actionTimerMax = 0.1f;
                    actionTimer = 0;
                }
                break;

            case HyenaState.Attacking:
                rb2d.velocity = direction.normalized * 3;
                break;
        }
    }

    public void Activate()
    {
        state = HyenaState.Retreating;
        actionTimerMax = 0.5f;
        var foundHyenas = FindObjectsOfType<Hyena>();
        foreach (var hyena in foundHyenas)
        {
            if (hyena == this) continue;
            hyena.OtherHyenaActivated();
        }
    }

    private void OtherHyenaActivated()
    {
        confidence++;
    }

    public void GetScared()
    {
        if (timesScared == 3) return;

        timesScared++;
        state = HyenaState.Retreating;
        actionTimerMax = 0.5f;
    }

    // Temp, will be replaced by an actual way to scare the hyenas.
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            GetScared();
    }
}
