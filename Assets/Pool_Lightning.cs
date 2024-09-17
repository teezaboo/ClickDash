using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool_Lightning : MonoBehaviour
{
    [SerializeField] private bool _isAutoBack = false;
    [SerializeField] private float _timeToBack = 2f;
    private int _refIndex = 1;
    private void Awake()
    {
        transform.GetChild(0).transform.SetParent(transform);
        if(transform.childCount == 1)
        {
            GameObject childInPool = Instantiate(transform.GetChild(transform.childCount - 1).gameObject);
            childInPool.transform.SetParent(transform);
            childInPool.transform.SetParent(transform);
            childInPool.SetActive(false);
            childInPool.name =  transform.GetChild(0).name.Substring(0, transform.GetChild(0).name.Length - 3) + "("  + (transform.childCount) + ")";
        }
    }    
    public void GetPool(GameObject startObj, float attackDamage, float diffusionAmount, List<string> allID, bool isFirst = false)
    {
        if(_refIndex >= transform.childCount)
        {
            _refIndex = 1;
        }
        if(transform.GetChild(_refIndex).gameObject.activeSelf == false)
        {
            GameObject childInPool = transform.GetChild(_refIndex).gameObject;
            childInPool.transform.position = startObj.transform.position;
            childInPool.SetActive(true);
            if(_isAutoBack == true)
            {
                childInPool.GetComponent<BackToPool>().SetPool(this.gameObject);
                childInPool.GetComponent<BackToPool>().timeToBack();
            }
            childInPool.GetComponent<LightningEffect>().Damage = attackDamage;
            childInPool.GetComponent<LightningEffect>().StartLightning(diffusionAmount, startObj, isFirst, allID);
        }else
        {
            bool isHave = false;
            for (int i = 0; i < transform.childCount; i++)
            {
                if(i == 0) continue;
                if(transform.GetChild(i).gameObject.activeSelf == false){
                    _refIndex = i;
                    isHave = true;
                    break;
                }
            }
            if(isHave == true){
                GameObject childInPool = transform.GetChild(_refIndex).gameObject;
                childInPool.transform.position = startObj.transform.position;
                childInPool.SetActive(true);
                if(childInPool.GetComponent<BackToPool>() != null){
                    childInPool.GetComponent<BackToPool>().SetPool(this.gameObject);
                }
                if(_isAutoBack == true)
                {
                    childInPool.GetComponent<BackToPool>().SetPool(this.gameObject);
                    childInPool.GetComponent<BackToPool>().timeToBack();
                }
                childInPool.GetComponent<LightningEffect>().Damage = attackDamage;
                childInPool.GetComponent<LightningEffect>().StartLightning(diffusionAmount, startObj, isFirst, allID);
            }else{
                GameObject childInPool = Instantiate(transform.GetChild(0).gameObject);
                childInPool.transform.position = startObj.transform.position;
                childInPool.SetActive(true);
                childInPool.transform.SetParent(transform);
                childInPool.name =  transform.GetChild(0).name.Substring(0, transform.GetChild(0).name.Length - 3) + "("  + (transform.childCount) + ")";
                if(_isAutoBack == true)
                {
                    childInPool.GetComponent<BackToPool>().SetPool(this.gameObject);
                    childInPool.GetComponent<BackToPool>().timeToBack();
                }
                childInPool.GetComponent<LightningEffect>().Damage = attackDamage;
                childInPool.GetComponent<LightningEffect>().StartLightning(diffusionAmount, startObj, isFirst, allID);
            }
        }
        _refIndex++;
    }
    public float GetTimeToBack()
    {
        return _timeToBack;
    }
}
