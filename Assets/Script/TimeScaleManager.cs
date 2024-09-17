using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleManager : MonoBehaviour
{
    [SerializeField] private float _timeSpeed = 1f;
    void Update()
    {
        Time.timeScale =_timeSpeed;
    }
}
