using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn_GroundTouching : MonoBehaviour
{
    [SerializeField] private List<GameObject> _groundTouching_Effect;
    public void spawn_GroundTouching_Effect()
    {
        Instantiate(_groundTouching_Effect[0], transform.position, Quaternion.identity);
    }
}