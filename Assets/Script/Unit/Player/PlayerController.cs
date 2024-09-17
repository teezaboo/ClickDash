using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public HyplayDataPlayer _hyplayDataPlayer;
    [SerializeField] private Leaderboard _leaderboard;
    public RandomPoolAudio _monsterDieAudio;
    [SerializeField] private Pool _poolAudioHeal;
    [SerializeField] private Pool _poolAudioAttacked;
    public RandomPoolAudio RandomPoolAudio;
    public UpgradeManager UpgradeManager;
    public UpgradeStats UpgradeStats;
    [SerializeField] private Pool_TextDamage _poolTextEvaluate;
    [SerializeField] private Pool _poolHeal;
    [SerializeField] private Pool _poolVampireHeal;
    public ResourceManager _resourceManager;
    public float _maxHp = 100;
    public float _hp;
    public float AttackDamage = 1;
    public float CriticalRate = 10;
    public float CriticalDamage = 200;
    [SerializeField] private List<Transform> _allSprite;
    private bool _immotal = false;
    public bool IsDie = false;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private PowerBar _powerBar;
    [SerializeField] private List<GameObject> _PowerEffectList;
    private float _timeBlockAttack = 0;
    public GameObject _objInfinityDash;
    public TextMeshProUGUI _textSecInfinityDash;
    public TextMeshProUGUI _textMInfinityDash;
    public int Min = 0;
    public int Sec = 0;
    public bool _isBlock = false;
    public GameObject _blockObj;
    public GameObject _blockEvaluate;
    private float timeIsBlock;
    private bool _isBlock2 = true;
    [SerializeField] private GameObject _ripObj;
    [SerializeField] private GameObject partiBlock;
    [SerializeField] private GameOverManager _gameOverManager;
    public void Start()
    {
        _hp = _maxHp;
        _healthBar.SetStart(_maxHp);
    }
    public float GetPlayerAttackDamage()
    {
        return AttackDamage;
    }
    public void Update()
    {
        if(UpgradeManager.IsStopGame == true) return;
        if(_timeBlockAttack > 0){
            _timeBlockAttack -= 1f/60f;
            if(_timeBlockAttack <= 0){
                GetComponent<Player_Attack>().isBlockAttack = false;
                _timeBlockAttack = 0;
                GetComponent<Player_Attack>().PowerSkill(UpgradeStats.UpgradeSkillDuration + 5f);
            }else{
                GetComponent<Player_Attack>().isBlockAttack = true;
            }
        }
        if(UpgradeStats.UpgradeBlockShield > 0 && _isBlock2 == true && timeIsBlock <= 0){
            Block();
        }
        if(timeIsBlock > 0){
            timeIsBlock -= 1f/60f;
        }
    }
    public void TakeDamage(float damage)
    {
        if(GetComponent<Player_Attack>().isPowerSkill == true) return;
        if(UpgradeManager.IsStopGame == true) return;
        if(_immotal == true) return;
        if(Random.Range(0, 100) < UpgradeStats.UpgradeEvasion + 5f){
            _blockEvaluate.SetActive(true);
            _poolTextEvaluate.GetPool(new Vector3(transform.position.x + Random.Range(-0.111539f, 0.111539f), transform.position.y ,transform.position.z), "Dodge!");
            _immotal = true;
            StartCoroutine(FlashBlue());
            return;
        }
        if(_isBlock == true){
            timeIsBlock = UpgradeStats.UpgradeBlockShield;
            partiBlock.SetActive(true);
            _isBlock = false;
            _isBlock2 = true;
            _blockObj.SetActive(false);
            _immotal = true;
            StartCoroutine(FlashBlock());
            return;
        }
        _resourceManager.AddSumScore();
        _poolAudioAttacked.GetPool(transform.position, new Vector3(0,0,0), new Vector3(1,1,1));
        if(_immotal != true){
            _immotal = true;
            float myDamage = (damage - (damage * UpgradeStats.UpgradeFlexibility/100)) - UpgradeStats.UpgradeIndomitable;
            if(myDamage < 0){
                myDamage = 0;
            }
            _hp -= myDamage;
            _healthBar.takeDamage(myDamage);
            if (_hp <= 0)
            {
                Die();
            }else{
                StartCoroutine(FlashRed());
            }
        }
    }
    public void heal()
    {
        _hp += 1;
    }

    public void Die()
    {
        StartCoroutine(FlashRed());
        StartCoroutine(diee());
        // ทำลาย GameObject หรือทำอะไรตามที่คุณต้องการเมื่อตาย
    }
    IEnumerator FlashRed() {
        foreach(Transform mySprite in _allSprite){
            SpriteRenderer[] sprites = mySprite.GetComponentsInChildren<SpriteRenderer>();
            foreach(SpriteRenderer sprite in sprites){
                sprite.color = Color.red;
            }
        }

        yield return new WaitForSeconds(0.2f);

        foreach(Transform mySprite in _allSprite){
            SpriteRenderer[] sprites = mySprite.GetComponentsInChildren<SpriteRenderer>();
            foreach(SpriteRenderer sprite in sprites){
                sprite.color = new Color(1f, 1f, 1f, 0.75f);
            }
        }

        yield return new WaitForSeconds(1f);
        _immotal = false;
        foreach(Transform mySprite in _allSprite){
            SpriteRenderer[] sprites = mySprite.GetComponentsInChildren<SpriteRenderer>();
            foreach(SpriteRenderer sprite in sprites){
                sprite.color = Color.white;
            }
        }
    }
    IEnumerator FlashBlue() {
        foreach(Transform mySprite in _allSprite){
            SpriteRenderer[] sprites = mySprite.GetComponentsInChildren<SpriteRenderer>();
            foreach(SpriteRenderer sprite in sprites){
                sprite.color = Color.blue;
            }
        }

        yield return new WaitForSeconds(0.2f);

        foreach(Transform mySprite in _allSprite){
            SpriteRenderer[] sprites = mySprite.GetComponentsInChildren<SpriteRenderer>();
            foreach(SpriteRenderer sprite in sprites){
                sprite.color = new Color(1f, 1f, 1f, 0.75f);
            }
        }

        yield return new WaitForSeconds(1f);
        _immotal = false;
        foreach(Transform mySprite in _allSprite){
            SpriteRenderer[] sprites = mySprite.GetComponentsInChildren<SpriteRenderer>();
            foreach(SpriteRenderer sprite in sprites){
                sprite.color = Color.white;
            }
        }
    }
    IEnumerator FlashBlock() {
        foreach(Transform mySprite in _allSprite){
            SpriteRenderer[] sprites = mySprite.GetComponentsInChildren<SpriteRenderer>();
            foreach(SpriteRenderer sprite in sprites){
                sprite.color = Color.yellow;
            }
        }

        yield return new WaitForSeconds(0.2f);

        foreach(Transform mySprite in _allSprite){
            SpriteRenderer[] sprites = mySprite.GetComponentsInChildren<SpriteRenderer>();
            foreach(SpriteRenderer sprite in sprites){
                sprite.color = new Color(1f, 1f, 1f, 0.75f);
            }
        }

        yield return new WaitForSeconds(1f);
        _immotal = false;
        foreach(Transform mySprite in _allSprite){
            SpriteRenderer[] sprites = mySprite.GetComponentsInChildren<SpriteRenderer>();
            foreach(SpriteRenderer sprite in sprites){
                sprite.color = Color.white;
            }
        }
    }
    public void AddPower(){
        _powerBar.AddPower(2);
    }
    public void Power(){
        _timeBlockAttack = 1f;
        // หาตำแหน่งปัจจุบันของเรา
        Vector2 center = transform.position;

        // ค้นหา Collider ทั้งหมดที่อยู่ภายใน Circle ที่กำหนด
        Collider2D[] colliders = Physics2D.OverlapCircleAll(center, 10000000f);

        // ตรวจสอบ Collider ที่ค้นพบ
        foreach (Collider2D col in colliders)
        {
            // ตรวจสอบ Tag ของ GameObject ที่ค้นพบ
            if (col.CompareTag("monster"))
            {
                // เข้าถึงสคริปต์ MonsterController บน GameObject มอนสเตอร์
                monsterController monsterScript = col.gameObject.GetComponent<monsterController>();

                // ตรวจสอบว่า monsterScript ไม่เป็น null และทำดาเมจ
                if (monsterScript != null)
                {
                    monsterScript.KncokBack(transform.position, 12f);
                }
            }
        }
        foreach(GameObject powerEffect in _PowerEffectList){
            powerEffect.SetActive(false);
            powerEffect.SetActive(true);
        }
        _textSecInfinityDash.text = "00";
        _textMInfinityDash.text = (UpgradeStats.UpgradeSkillDuration + 5f).ToString();
        _objInfinityDash.SetActive(false);
        _objInfinityDash.SetActive(true);
    }
    public void ClosePower(){
        foreach(GameObject powerEffect in _PowerEffectList){
            powerEffect.SetActive(false);
        }
    }
    public void Heal(){
        if(_hp < _maxHp){
            _hp += _maxHp/4;
            _poolAudioHeal.GetPool(transform.position, new Vector3(0,0,0), new Vector3(1,1,1));
            _poolHeal.GetPool(transform.position, new Vector3(0,0,0), new Vector3(1,1,1));
            if(_hp > _maxHp){
                _hp = _maxHp;
            }
            _healthBar.takeDamage(-_maxHp/4);
        }else{
            _poolHeal.GetPool(transform.position, new Vector3(0,0,0), new Vector3(1,1,1));
        }
    }
    public void VampireHeal(float value){
        if(_hp < _maxHp){
            _hp += value;
            _poolVampireHeal.GetPool(transform.position, new Vector3(0,0,0), new Vector3(1,1,1));
            if(_hp > _maxHp){
                _hp = _maxHp;
            }
            _healthBar.takeDamage(-value);
        }
    }
    public void UpdateMaxHp(float value){
        _maxHp += value;
        _hp += value;
        _healthBar.Update_maxHp(value);
    }
    public void Block(){
        _isBlock = true;
        _blockObj.SetActive(false);
        _blockObj.SetActive(true);
        _isBlock2 = false;
    }
    IEnumerator diee()
    {
        _resourceManager.AddSumScore();
        UpgradeManager.PlayerDieUpgrade();
        IsDie = true;
        _immotal = true;
        yield return new WaitForSeconds(0.2f);
    //    _hyplayDataPlayer.IncrementGol2(_resourceManager.playerID, (int)_resourceManager._goldCount, (int)(_resourceManager._dieCount/3), (int)_resourceManager._sumScore);
   //     _hyplayDataPlayer.SetTimeQ((_resourceManager.hr * 60 * 60) + (_resourceManager.min * 60) + _resourceManager.sec,_resourceManager.playerID);
        if(_resourceManager._sumScore > PlayerPrefs.GetInt("ScorePlayer")){
            PlayerPrefs.SetInt("ScorePlayer", _resourceManager._sumScore);
            _leaderboard.UploadEntry(_resourceManager._sumScore, PlayerPrefs.GetString("NamePlayer"));
        }
        transform.gameObject.SetActive(false);
        UpgradeManager.IsStopGame = true;
        _ripObj.SetActive(true);
        _ripObj.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        _gameOverManager.GameOverStart();
    }
}