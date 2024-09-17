using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash_Attack : MonoBehaviour
{
    [SerializeField] private Pool _poolAudioPickSW;
    [SerializeField] private UpgradeStats _upgradeStats;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Player_Attack _player_Attack;
    [SerializeField] private ResourceManager _resourceManager;
    private Vector3 _newPosition;
    private float _damage = 1;
    private float _criticalRate = 10;
    private float _criticalDamage = 200;
    private bool _isActive;
    private bool _isAttacked = false;
    [SerializeField] private List<Pool> _pool_Attacked;
    private bool addRotationSwAttacked = false;
    private float _chanceLightning = 0;
    [SerializeField] private Pool_Lightning _poolLightning;
    void OnEnable()
    {
        Debug.Log("Slash_Attack");
        GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 1); // กำหนดค่า alpha เป็น 1
        _isActive = true;
        GetComponent<BoxCollider2D>().enabled = true;
        StartCoroutine(endAttack());
        _isAttacked = false;
    }
    void Update()
    {
        if(!_isActive) return;
        _newPosition = transform.position;
        _newPosition.z = 2-10; // กำหนดค่า z ใหม่
        transform.position = _newPosition;
        SpriteRenderer slashRenderer = transform.GetComponent<SpriteRenderer>();
        Color color = slashRenderer.color;
        color.a -= Time.deltaTime*2; // ลด alpha ตามเวลา
        slashRenderer.color = color;    

        // ถ้า alpha เป็นค่าน้อยกว่าหรือเท่ากับ 0 ให้ทำลาย GameObject และ reset currentSlash
        if (color.a <= 0)
        {
            gameObject.SetActive(false);
            _isActive = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("monster"))
        {
            if(_player_Attack.isPowerSkill == false){
                _playerController.AddPower();
            }
            if(_isAttacked == false){
                _isAttacked = true;
                _resourceManager.AddXScore();
                _resourceManager.AddScore(10);
            }
            monsterController monsterScript = other.gameObject.GetComponent<monsterController>();
            if (monsterScript != null)
            {
                if(_upgradeStats.UpgradeLightningSword > 0){
                    _chanceLightning = Random.Range(0, 100);
                    if(_chanceLightning <= _upgradeStats.UpgradeLightningSword){
                        _poolLightning.GetPool(other.gameObject, _damage/2, _upgradeStats.UpgradeLightningSwordSpread , new List<string>(), true);
                    }
                }
                if(_upgradeStats.UpgradeFireSword > 0){
                    monsterScript.FireAttacked(_damage/10, _upgradeStats.UpgradeFireSword);
                }
                if(_upgradeStats.UpgradeIceSword > 0){
                    monsterScript.IceEffect(_upgradeStats.UpgradeIceSword);
                }
                if(_upgradeStats.UpgradeLethalAttack > 0){
                    if(Random.Range(0, 100) <= _upgradeStats.UpgradeLethalAttack){
                        _pool_Attacked[2].GetPool(new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y + 0.1f, other.gameObject.transform.position.z), new Vector3(0, 0, Random.Range(0, 360)), new Vector3(1,1,1));
                        if(monsterScript.isBoss){
                            monsterScript.BoomDamage(_damage * 5f, true, transform.position, 1);
                        }else{
                            monsterScript.BoomDamage(monsterScript.GetHp(), true, transform.position, 1);
                        }
                        return;
                    }
                }
                if(Random.Range(0, 100) <= _criticalRate){
                    addRotationSwAttacked = !addRotationSwAttacked;
                    if(addRotationSwAttacked){
                        _pool_Attacked[1].GetPool(new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y + 0.1f, other.gameObject.transform.position.z), new Vector3(0, 0, Random.Range(0, 70)), new Vector3(1,1,1));
                    }else{
                        _pool_Attacked[1].GetPool(new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y + 0.1f, other.gameObject.transform.position.z), new Vector3(0, 0, Random.Range(110f, 180f)), new Vector3(1,1,1));
                    }
                    if(_upgradeStats.UpgradeVampireSword > 0){
                        _playerController.VampireHeal((_damage * _criticalDamage/100f) * (_upgradeStats.UpgradeVampireSword/100f));
                    }
                    monsterScript.TakeDamage(_damage * _criticalDamage/100f, true);
                }else{
                    addRotationSwAttacked = !addRotationSwAttacked;
                    if(addRotationSwAttacked){
                        _pool_Attacked[0].GetPool(new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y + 0.1f, other.gameObject.transform.position.z), new Vector3(0, 0, Random.Range(0, 70)), new Vector3(1,1,1));
                    }else{
                        _pool_Attacked[0].GetPool(new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y + 0.1f, other.gameObject.transform.position.z), new Vector3(0, 0, Random.Range(110f, 180f)), new Vector3(1,1,1));
                    }
                    if(_upgradeStats.UpgradeVampireSword > 0){
                        _playerController.VampireHeal((_damage) * (_upgradeStats.UpgradeVampireSword/100f));
                    }
                    monsterScript.TakeDamage(_damage, false);
                }
            }
        }
        if (other.gameObject.CompareTag("boomb"))
        {
            TNT_Controller TNT_Controller = other.gameObject.GetComponent<TNT_Controller>();
            TNT_Controller._boombEffectPool.GetPool(new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, other.gameObject.transform.position.z), new Vector3(0, 0, 0), new Vector3(1,1,1));
            other.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("SW") && _player_Attack.isPowerSkill == false)
        {
            _poolAudioPickSW.GetPool(transform.position, new Vector3(0,0,0), new Vector3(1,1,1));
            _player_Attack.AddSW();
            other.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Food") && _playerController._hp < _playerController._maxHp)
        {
            _playerController.Heal();
            other.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }
    IEnumerator endAttack()
    {
        yield return new WaitForSeconds(0.05f);
        GetComponent<BoxCollider2D>().enabled = false;
            if(_isAttacked == false && _player_Attack.isPowerSkill == false){
                _resourceManager.AddSumScore();
            }
    }
    public void Set_Damage(float damage, float criticalRate, float criticalDamage){
        _damage = damage + (damage * _upgradeStats.UpgradeAttackDamagePer);
        _criticalRate = criticalRate + _upgradeStats.UpgradeChanceCriticalAttack;
        _criticalDamage = criticalDamage + _upgradeStats.UpgradeCriticalDamage;
    }
    public float Get_Damage(){
        return _damage;
    }
}
