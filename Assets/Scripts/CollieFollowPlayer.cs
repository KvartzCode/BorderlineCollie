using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollieFollowPlayer : MonoBehaviour
{
    public GameObject player;

    public bool followPlayer;

    public float closeEoughToStop;

    public float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        followPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(followPlayer)
        {
            FollowPLayer();
        }
        else
        {
        }
    }


    void FollowPLayer()
    {
        float distance = Vector2.Distance(gameObject.transform.position, player.transform.position);

        if (distance > closeEoughToStop)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, speed * Time.deltaTime);
        }



    }
}
