using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _hpSlider;
    [SerializeField] private Slider _ease_hpSlider;
    [SerializeField] private float _maxHp = 100f;
    [SerializeField] private float _hp;
    [SerializeField] private float _lerpSpeed = 0.05f;
    public void SetStart(float maxHp)
    {
        _maxHp = maxHp;
        _hpSlider.maxValue = _maxHp;
        _ease_hpSlider.maxValue = _maxHp;
        _hp = _maxHp;
    }

    // Update is called once per frame
   // Update is called once per frame
    void Update()
    {
        if(_hpSlider.value != _hp){
            _hpSlider.value = _hp;
        }
        if(_hpSlider.value != _ease_hpSlider.value){
            _ease_hpSlider.value = Mathf.Lerp(_ease_hpSlider.value, _hp, _lerpSpeed);
        }
    }
    public void takeDamage(float damage){
        _hp -= damage;  
        if(_hp <= 0){
            _hp = 0;
        }
        /*
        if(_hp <= 0){
            gameObject.SetActive(false);
        }
        */
    }
    public void Update_maxHp(float Add_maxHp){
        _maxHp += Add_maxHp;
        _hpSlider.maxValue = _maxHp;
        _ease_hpSlider.maxValue = _maxHp;
        _ease_hpSlider.value = _maxHp;
        _hp += Add_maxHp;
        _hpSlider.value = _hp;
    }
}