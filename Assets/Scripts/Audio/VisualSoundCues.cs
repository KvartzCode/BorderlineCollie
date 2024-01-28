using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class VisualSoundCues : MonoBehaviour
{
    public static VisualSoundCues Instance { get; private set; }

    [SerializeField] GameObject visualSound;
    [SerializeField] RectTransform canvasRect;
    [SerializeField] RectTransform tentArrow;

    [SerializeField] float visualRadius = 14.8f;

    private void Start()
    {
        Instance = this;
    }

    public void SetArrowPos(Vector3 tentPosition)
    {
        if (Vector2.Distance(tentPosition, transform.position) > visualRadius)
        {
            tentArrow.gameObject.SetActive(true);

            Vector2 dir = (transform.position - tentPosition).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);

            rotation.eulerAngles = new(0, 0, angle + 90);
            tentArrow.anchoredPosition = GetCanvasPosition(tentPosition);
            tentArrow.rotation = rotation;
        }
        else
            tentArrow.gameObject.SetActive(false);
    }

    public void MadeSound(Vector3 origin)
    {
        Vector2 dir = (transform.position - origin).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);

        rotation.eulerAngles = new(0, 0, angle - 90);
        RectTransform image = Instantiate(visualSound, canvasRect).GetComponent<RectTransform>();
        image.anchoredPosition = GetCanvasPosition(origin);
        image.rotation = rotation;

        Destroy(image.gameObject, 3f);
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
