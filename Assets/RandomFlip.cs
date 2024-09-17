using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFlip : MonoBehaviour
{
    [SerializeField] private GameObject _otherFilp;
    private void OnEnable(){
        if(Random.Range(0,2) == 0){
            transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
            if(_otherFilp != null){
                RectTransform otherFlipRectTransform = _otherFilp.GetComponent<RectTransform>();
                if (otherFlipRectTransform != null)
                {
                    otherFlipRectTransform.localScale = new Vector3(-otherFlipRectTransform.localScale.x, otherFlipRectTransform.localScale.y, otherFlipRectTransform.localScale.z);
                }
            }
        }else{
            transform.localScale = new Vector3(transform.localScale.x,transform.localScale.y,transform.localScale.z);
        }
    }
}
