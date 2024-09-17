using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animaton_Custom_Ui : MonoBehaviour
{
    public bool isFlip = false;
    public float positionX = -16.616f;
    public float Myspeed = 0.01f;
    private float speed;
    private Vector2 StartPosition;
    public void Awake()
    {
        StartPosition = GetComponent<RectTransform>().anchoredPosition;
    }
    void Update()
    {
        if(isFlip)
        {
            speed = -Myspeed;
            var rectTransform = GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x + speed, rectTransform.anchoredPosition.y);
            if(rectTransform.anchoredPosition.x <= positionX)
            {
                rectTransform.anchoredPosition = new Vector2(StartPosition.x, rectTransform.anchoredPosition.y);
            }
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x + speed, rectTransform.anchoredPosition.y);
            if(rectTransform.anchoredPosition.x <= positionX)
            {
                rectTransform.anchoredPosition = new Vector2(StartPosition.x, rectTransform.anchoredPosition.y);
            }
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x + speed, rectTransform.anchoredPosition.y);
            if(rectTransform.anchoredPosition.x <= positionX)
            {
                rectTransform.anchoredPosition = new Vector2(StartPosition.x, rectTransform.anchoredPosition.y);
            }
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x + speed, rectTransform.anchoredPosition.y);
            if(rectTransform.anchoredPosition.x <= positionX)
            {
                rectTransform.anchoredPosition = new Vector2(StartPosition.x, rectTransform.anchoredPosition.y);
            }
        }else{
            speed = Myspeed;
            var rectTransform = GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x + speed, rectTransform.anchoredPosition.y);
            if(rectTransform.anchoredPosition.x > positionX)
            {
                rectTransform.anchoredPosition = new Vector2(StartPosition.x, rectTransform.anchoredPosition.y);
            }
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x + speed, rectTransform.anchoredPosition.y);
            if(rectTransform.anchoredPosition.x > positionX)
            {
                rectTransform.anchoredPosition = new Vector2(StartPosition.x, rectTransform.anchoredPosition.y);
            }
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x + speed, rectTransform.anchoredPosition.y);
            if(rectTransform.anchoredPosition.x > positionX)
            {
                rectTransform.anchoredPosition = new Vector2(StartPosition.x, rectTransform.anchoredPosition.y);
            }
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x + speed, rectTransform.anchoredPosition.y);
            if(rectTransform.anchoredPosition.x > positionX)
            {
                rectTransform.anchoredPosition = new Vector2(StartPosition.x, rectTransform.anchoredPosition.y);
            }
        }
    }
}
