using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualSoundCues : MonoBehaviour
{
    public static VisualSoundCues Instance { get; private set; }

    [SerializeField] GameObject visualSound;
    [SerializeField] RectTransform canvasRect;

    [SerializeField] float visualRadius = 14.8f;

    private void Start()
    {
        Instance = this;
      // visualRadius = GetComponent<CircleCollider2D>().radius;
    }

    public void MadeSound(Vector3 origin)
    {
        if (Vector2.Distance(origin, transform.position) > visualRadius)
        {
            Vector2 dir = (transform.position - origin).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);

            rotation.eulerAngles = new(0, 0, angle);
            RectTransform image = Instantiate(visualSound, canvasRect).GetComponent<RectTransform>();
            image.anchoredPosition = GetCanvasPosition(origin);
            image.rotation = rotation;

            Destroy(image.gameObject, 2f);
        }
    }

    private Vector2 GetCanvasPosition(Vector2 _origin)
    {
        Vector2 origin = _origin;

        Vector2 centerPos = transform.position;
        float distance = Vector2.Distance(origin, centerPos);

        if (distance > visualRadius)
        {
            Vector2 fromOrigin = origin - centerPos;
            fromOrigin *= visualRadius / distance;
            origin = centerPos + fromOrigin;
        }

        Vector2 objectPos = Camera.main.WorldToViewportPoint(origin);
        Vector2 screenPos = new(
            (objectPos.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f),
            (objectPos.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f));

        return screenPos;
    }
}
