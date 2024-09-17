using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    [SerializeField] private List<Pool> _breakEffectPool;
    [SerializeField] private float _sizeup = 1;
    [SerializeField] private List<Pool> _slashPrefab;
    [SerializeField] private List<Pool> _slashEffectPrefab;
    [SerializeField] private int _effectFrequency;
    
    public void SlashAttack(Vector3 middlePosition, Vector3 currentPosition, Vector3 startPosition)
    {
        if(GetComponent<PlayerController>().UpgradeStats.UpgradeDarkSwordWide > 0){
            _breakEffectPool[Random.Range(0, _breakEffectPool.Count)].GetPool(currentPosition, Quaternion.identity.eulerAngles, new Vector3(1 * GetComponent<PlayerController>().UpgradeStats.UpgradeDarkSwordWide/1f, 1 * GetComponent<PlayerController>().UpgradeStats.UpgradeDarkSwordWide/1f, 1 * GetComponent<PlayerController>().UpgradeStats.UpgradeDarkSwordWide/1f));
        }
        // หาความยาวของ slashPrefab โดยใช้ Vector3.Distance
        float totalDistance = Vector3.Distance(startPosition, currentPosition) * 0.28f;
        int _partialfrequency = Mathf.FloorToInt(totalDistance* _effectFrequency);
        
        // หมุนภาพติดตาให้ชี้ไปที่ตำแหน่งที่คลิก
        Vector3 direction = (currentPosition - startPosition).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        // สร้าง slashPrefab ในตำแหน่งกลาง
        if (GetComponent<PlayerController>().UpgradeStats.UpgradeWiderDash > 0){
            _slashPrefab[GetComponent<player_control_set>().Get_Sword_Wearing()].GetPool(
                middlePosition, 
                Quaternion.Euler(0, 0, angle + 90).eulerAngles, 
                new Vector3(0.005f * _sizeup * GetComponent<PlayerController>().UpgradeStats.UpgradeWiderDash, totalDistance, 1),
                GetComponent<PlayerController>().GetPlayerAttackDamage() + GetComponent<PlayerController>().UpgradeStats.UpgradeAttackDamage, 
                GetComponent<PlayerController>().CriticalRate, 
                GetComponent<PlayerController>().CriticalDamage
            );
        }else{
            _slashPrefab[GetComponent<player_control_set>().Get_Sword_Wearing()].GetPool(
                middlePosition, 
                Quaternion.Euler(0, 0, angle + 90).eulerAngles, 
                new Vector3(0.005f * _sizeup, totalDistance, 1),
                GetComponent<PlayerController>().GetPlayerAttackDamage() + GetComponent<PlayerController>().UpgradeStats.UpgradeAttackDamage, 
                GetComponent<PlayerController>().CriticalRate, 
                GetComponent<PlayerController>().CriticalDamage
            );
        }
        
        // คำนวณระยะห่างระหว่างแต่ละตำแหน่งของ partial
        if(GetComponent<player_control_set>().Get_Sword_Wearing() == 0) return;
        
        float partialDistance = totalDistance / (_partialfrequency - 1);
        for (int i = 0; i < _partialfrequency; i++)
        {
            // คำนวณตำแหน่งของแต่ละ partial
            Vector3 partialPosition = Vector3.Lerp(startPosition, currentPosition, (float)i / (_partialfrequency - 1));
            
            // สปอน partial ในตำแหน่งที่คำนวณ
            _slashEffectPrefab[GetComponent<player_control_set>().Get_Sword_Wearing()].GetPool(
                partialPosition, 
                Quaternion.identity.eulerAngles, 
                new Vector3(1, 1, 1)
            );
        }
    }
}
