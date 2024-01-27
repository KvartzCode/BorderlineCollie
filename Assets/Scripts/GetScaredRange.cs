using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GetScaredRange : MonoBehaviour
{
    public List<Hyena> hyenaList = new List<Hyena>();

    Hyena hyena;

    Light2D lightComponent;
    GameObject[] hyenas;


    public float rangeToScare;
    // Start is called before the first frame update
    void Start()
    {
        lightComponent = GetComponentInChildren<Light2D>();
        
    }

    // Update is called once per frame
    void Update()
    {

        lightComponent.pointLightOuterRadius = rangeToScare;
        foreach (Hyena hyenasGameObject in hyenaList) 
        {
            float distanceToHyena = Vector2.Distance(transform.position, hyenasGameObject.transform.position);
            
            hyena = hyenasGameObject.GetComponent<Hyena>();
            
            if(distanceToHyena < rangeToScare) 
            {
                hyena.GetScared();
            }
        }



    }

    public  void AddHyena(Hyena hyena)
    {
        hyenaList.Add(hyena);
    }

    public void RemoveHyena(Hyena hyena)
    {
        hyenaList.Remove(hyena);
    }
}


