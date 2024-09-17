using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Slider _powerSlider;
    [SerializeField] private float _maxPower = 100f;
    [SerializeField] private float _power = 0;
    public void Awake()
    {
        _powerSlider.maxValue = _maxPower;
        _powerSlider.value = _power;
    }
    public void SetStart(float maxPower)
    {
        _maxPower = maxPower;
        _powerSlider.maxValue = _maxPower;
        _power = _maxPower;
    }

    public void AddPower(float power){
        _power += power;  
        if(_power >= _maxPower){
            _playerController.Power();
            _power = 0;
        }
        _powerSlider.value = _power;
    }
}