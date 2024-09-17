using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeStats : MonoBehaviour
{
    public void Awake(){
        if(PlayerPrefs.HasKey("CurArmorGrade"))
            UpgradeIndomitable += (PlayerPrefs.GetInt("CurArmorGrade")) * 5;
        if(PlayerPrefs.HasKey("CurHatGrade"))
            UpgradeIndomitable += (PlayerPrefs.GetInt("CurHatGrade")) * 5;
        if(PlayerPrefs.HasKey("CurSWGrade"))
            UpgradeAttackDamage += (PlayerPrefs.GetInt("CurSWGrade")) * 10;
    }
    public float GetGold = 0;
    public float UpgradeDashCount = 0;
    public float UpgradeWiderDash = 0;
    public float UpgradeIndomitable = 0;
    public float UpgradeAttackDamage = 0;
    public float UpgradeLethalAttack = 0;
    public float UpgradeDashCooldown = 0;
    public float UpgradeExplodingMonstersDeath = 0;
    public float UpgradeAttackDamagePer = 0;
    public float UpgradeCriticalDamage = 0;
    public float UpgradeChanceCriticalAttack = 0;
    public float UpgradeBlockShield = 0;
    public float UpgradeHealth = 0;
    public float UpgradeEvasion = 0;
    public float UpgradeSkillDuration = 0;
    public float UpgradeFlexibility = 0;
    public float UpgradeDropRateDash = 0;
    public float UpgradeDropRateFood = 0;
    public float UpgradeDropRateTNT = 0;
    public float UpgradeDropRateGold = 0;
    public float UpgradeFireSword = 0;
    public float UpgradeIceSword = 0;
    public float UpgradeLightningSword = 0;
    public float UpgradeLightningSwordSpread = 0;
    public float UpgradeVampireSword = 0;
    public float UpgradeDarkSword = 0;
    public float UpgradeDarkSwordWide = 0;
}