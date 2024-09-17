using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpManager : MonoBehaviour
{
    [SerializeField] private  UpgradeManager _upgradeManager;
    [SerializeField] private  Slider _expSlider;
    [SerializeField] private TextMeshProUGUI _textLevel;
    [SerializeField] private int _level = 1;
    [SerializeField] private int _exp = 0;
    [SerializeField] private int _growth = 100; // 100% growth as default
    [SerializeField] private int _maxExpLevelUp;
    [SerializeField] private Animator animatorExpBar;

    void Start()
    {
        _maxExpLevelUp = CalculateMaxExp(_level);
        _expSlider.maxValue = _maxExpLevelUp;
    }

    public void AddExp(int exp)
    {
        _exp += exp * _growth / 100; // Applying growth multiplier
        _expSlider.value = _exp;

        if (_exp >= _maxExpLevelUp)
        {
            animatorExpBar.Play("levelup", -1, 0);
            animatorExpBar.Play("levelup");
            LevelUp();
        }else{
            animatorExpBar.Play("idleEXP", -1, 0);
            animatorExpBar.Play("idleEXP");
        }
    }

    private void LevelUp()
    {
        _upgradeManager.Upgrade();
    }

    public void checkLevelUp(){
        _level++;
        _textLevel.text = "LV "+_level.ToString();
        _exp -= _maxExpLevelUp;
        _expSlider.value = 0;


        if (_level >= 40)
        {
            _maxExpLevelUp = CalculateMaxExp(_level) + 2400;
        }
        else if (_level >= 20)
        {
            _maxExpLevelUp = CalculateMaxExp(_level) + 600;
        }
        else
        {
            _maxExpLevelUp = CalculateMaxExp(_level);
        }
        _expSlider.maxValue = _maxExpLevelUp;
        if(_exp >= _maxExpLevelUp)
        {
            animatorExpBar.Play("levelup", -1, 0);
            animatorExpBar.Play("levelup");
            LevelUp();
            _expSlider.value = _maxExpLevelUp;
        }else{
            animatorExpBar.Play("idleEXP", -1, 0);
            animatorExpBar.Play("idleEXP");
            _expSlider.value = _exp;
        }
    }

    private int CalculateMaxExp(int level)
    {
        if (level < 20)
        {
            return 5 + 10 * (level - 1);
        }
        else if (level < 40)
        {
            return 5 + 10 * 19 + 13 * (level - 20);
        }
        else if (level >= 40)
        {
            return 5 + 10 * 19 + 13 * (level - 20);
        }
        else
        {
            return 5 + 10 * 19 + 13 * 20 + 16 * (level - 40);
        }
    }
}
