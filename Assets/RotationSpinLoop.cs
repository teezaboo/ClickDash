using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationSpinLoop : MonoBehaviour
{
    [SerializeField] private GameObject _bodyMonster;
    [SerializeField] private float _rotationSpeed = 0.5f;
    void Update()
    {
        Quaternion newRotation = transform.rotation;
        if(_bodyMonster.transform.localScale.x < 0)
        {
        newRotation *= Quaternion.Euler(0, 0, -_rotationSpeed);
        }else{
        newRotation *= Quaternion.Euler(0, 0, _rotationSpeed);
        }
        transform.rotation = newRotation;
    }
}
