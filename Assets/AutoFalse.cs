using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoFalse : MonoBehaviour
{
    [SerializeField] private float _delay;
    Coroutine _coroutine;
    private void OnEnable()
    {
        if(_coroutine != null){
            StopCoroutine(_coroutine);
        }
        _coroutine = StartCoroutine(AutoFalseObject());
    }
    private void OnDisable()
    {
        if(_coroutine != null){
            StopCoroutine(_coroutine);
        }
    }
    IEnumerator AutoFalseObject()
    {
        yield return new WaitForSeconds(_delay);
        gameObject.SetActive(false);
    }
}
