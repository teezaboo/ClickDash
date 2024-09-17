using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class auto_destory : MonoBehaviour
{
    [SerializeField] private float _timeToDestroy = 2f;
    private void Awake()
    {
        Destroy(gameObject, _timeToDestroy);
    }
}
