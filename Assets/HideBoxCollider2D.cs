using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideBoxCollider2D : MonoBehaviour
{
    private void OnEnable(){
        GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine(DelayHide(0.3f));
    }
    IEnumerator DelayHide(float delay)
    {
        yield return new WaitForSeconds(delay);
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
