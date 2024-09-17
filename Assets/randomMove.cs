using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomMove : MonoBehaviour
{
    [SerializeField] private float _range = 0.4f;
    private Vector3 _newPosition;
    private void OnEnable(){
        _newPosition = new Vector3(Random.Range(-_range, _range), Random.Range(-_range, _range), 0);
        _newPosition += transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, _newPosition) > 0.1f){
            transform.position = Vector3.MoveTowards(transform.position, _newPosition, Time.deltaTime);
        }
    }
}
