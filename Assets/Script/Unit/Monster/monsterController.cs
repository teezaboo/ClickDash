using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterController : MonoBehaviour{
    public float delayJump = 0;
    public float timeJump = 0;
    public Animator animatorJump;
    public Animator animatorIdle; 
    private Coroutine _coroutineDelayJump;
    public bool isWalk = true;
    public bool isBigBoss = false;
    public bool isBoss = false;
    public string uniqueID;
    [SerializeField] private List<Pool> _expPool;
    public float MaxHp = 100f;
    public float AttackDamage = 30f;
    [SerializeField] private List<Pool> _pools;
   // public manageaudio manageaudioaaaa;
    public int typemon;
    Vector3 newPosition;
    private Camera mainCamera;
    public PlayerController playerScr;
    public float maxMoveSpeed; // ความเร็วการเคลื่อนที่ของมอนสเตอร์
    private float moveSpeed; // ความเร็วการเคลื่อนที่ของมอนสเตอร์
    public GameObject playerObj;
    public Transform player;
    public Animator animator; // คอมโพเนนต์ Animator สำหรับการควบคุมการเล่นอนิเมชั่น
    public float attackDelay = 1f;
    private float nextAttackTime;
    private float hp; // ตัวแปรเก็บค่า HP
    public List<SpriteRenderer> sprites;
    public List<Shader> shaders;
    public Animator animatorAttacked; // คอมโพเนนต์ Animator สำหรับการควบคุมการเล่นอนิเมชั่น
    public Animator bodyanimatorAttacked; // คอมโพเนนต์ Animator สำหรับการควบคุมการเล่นอนิเมชั่น
    private bool _isPlayerFilp = false;
    [SerializeField] private Pool _boombPool;
    [SerializeField] private Pool_TextDamage _pool_TextDamageBoom;
    [SerializeField] private Pool_TextDamage _pool_TextDamageLightning;
    [SerializeField] private Pool_TextDamage _pool_TextDamageFire;
    [SerializeField] private Pool_TextDamage _pool_TextDamage;
    [SerializeField] private Pool_TextDamage _pool_TextDamage_Critical;
    private Coroutine _coroutinePlayerFilp;
    private Coroutine _coroutineAniDelay;
    private int ani_Ro;
    [SerializeField] private RateDropItem _rateDropItem;
    [SerializeField] private List<GameObject> _ShockList;
    [SerializeField] private GameObject _fireEffect;
    [SerializeField] private GameObject _iceEffect;
    private bool isLightingOne = false;
    private float timeStockFire = 0;
    private Coroutine _coroutineFire;
    private float damageFireStock = 0;
    private float StockIce = 0;
    private Coroutine _coroutineIce;
    private bool isStopIce = false;
    [SerializeField] private List<GameObject> _flipList;
    private bool StopForce = false;
    [SerializeField] private DifficultyLevelManager _difficultyLevelManager;
    private float MaxHpStock;
    private float AttackDamageStock;
    private bool isStopWalking = false;
    public void Awake(){
        MaxHpStock = MaxHp;
        AttackDamageStock = AttackDamage;
        mainCamera = Camera.main; // เก็บอ้างถึง Main Camera
    }
    private void OnEnable(){
        if(animatorJump != null){
            isWalk = false;
            if(_coroutineDelayJump != null) StopCoroutine(_coroutineDelayJump);
            _coroutineDelayJump = StartCoroutine(delayWalk());
        }
        MaxHp = MaxHpStock + (_difficultyLevelManager.LevelMonster * (MaxHpStock/20));
        if(AttackDamageStock + (_difficultyLevelManager.LevelMonster * (AttackDamageStock/10)) > 200){
            AttackDamage = 200;
        }else{
            AttackDamage = AttackDamageStock + (_difficultyLevelManager.LevelMonster * (AttackDamageStock/10));
        }
        _isPlayerFilp = false;
        animator.Play("idle", -1, 0f);
        animator.Play("idle");
        if(animatorJump != null){
            animatorJump.Play("jump", -1, 0f);
            animatorJump.Play("jump");
            animatorIdle.Play("idlejump", -1, 0f);
            animatorIdle.Play("idlejump");
        }
        moveSpeed = maxMoveSpeed;
        _iceEffect.SetActive(false);
        isStopIce = false;
        damageFireStock = 0;
        StockIce = 0;
        if(_coroutineFire != null) {
            StopCoroutine(_coroutineFire); 
        };
        if(_coroutineIce != null) {
            StopCoroutine(_coroutineIce);
        };
        if(_coroutinePlayerFilp != null) StopCoroutine(_coroutinePlayerFilp);
        if(_coroutineAniDelay != null) StopCoroutine(_coroutineAniDelay);
        foreach(GameObject shock in _ShockList){
            shock.SetActive(false);
        }
        _fireEffect.SetActive(false);
        isLightingOne = false;
        uniqueID = "ID_" + System.Guid.NewGuid().ToString();
        ani_Ro = Random.Range(0, 2);
        hp = MaxHp;
        moveSpeed = maxMoveSpeed; // กำหนดความเร็วการเคลื่อนที่สูงสุด
        _isPlayerFilp = false;
        if(shaders.Count > 0){
            for(int i = 0; i < sprites.Count; i++){
                sprites[i].material.shader = shaders[i];
                sprites[i].color = Color.white;
            }
        }
    }
    void Update (){
        if(playerObj.activeSelf == false) return;
        if(playerScr != null){
            if(playerScr.UpgradeManager.IsStopGame == true) {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                StopForce = true;
                isStopIce = true;
                if(_coroutineFire != null) {
                    StopCoroutine(_coroutineFire);
                };
                if(_coroutineIce != null) {
                    StopCoroutine(_coroutineIce);
                };
                return;
            }else{
                if(StopForce == true){
                    StopForce = false;
                    StartCoroutine(Attacked(0.2f));
                }
                if(timeStockFire > 0){
                    _coroutineFire = StartCoroutine(FireAttack(damageFireStock, timeStockFire));
                    timeStockFire = 0;
                }
                if(StockIce > 0 && isStopIce == true){
                    isStopIce = false;
                    _coroutineIce = StartCoroutine(IceEffectDelay(StockIce));
                    StockIce = 0;
                }
                if(StockIce > 0){
                    StockIce--;
                }
            }
            if (player != null && _isPlayerFilp == false && isWalk == true)
            {
                Vector3 direction = player.position - transform.position;
                direction.Normalize();
                transform.position += direction * moveSpeed * Time.deltaTime;
                if(transform.position.x < player.position.x && transform.localScale.x > 0){
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                    _coroutinePlayerFilp = StartCoroutine(PlayerFilpDelay(0.025f));
                }else if(transform.position.x > player.position.x && transform.localScale.x < 0){
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                    _coroutinePlayerFilp = StartCoroutine(PlayerFilpDelay(0.025f));
                }
            }
        }
    }
    
    IEnumerator delayWalk(){
        isWalk = !isWalk;
        if(isWalk == true){
            yield return new WaitForSeconds(delayJump);
        }else{
            yield return new WaitForSeconds(timeJump);
        }
        _coroutineDelayJump = StartCoroutine(delayWalk());
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if(playerScr != null){
            // ตรวจสอบว่าชนกับมอนสเตอร์หรือไม่
            if(playerScr.UpgradeManager.IsStopGame == true) return;
            if (other.gameObject.CompareTag("Player")&& Time.time >= nextAttackTime)
            {
                nextAttackTime = Time.time + attackDelay;
                // เข้าถึงสคริปต์ MonsterScript บน GameObject มอนสเตอร์
                PlayerController playerScript = other.gameObject.GetComponent<PlayerController>();
                // ตรวจสอบว่า monsterScript ไม่เป็น null และลด HP
                if (playerScript != null)
                {
                    playerScript.TakeDamage(AttackDamage); // ลด HP 1 หน่วย
                }
            }
        }
    }    
    IEnumerator PlayerFilpDelay(float delay) {
        _isPlayerFilp = true;
        yield return new WaitForSeconds(delay);
        _isPlayerFilp = false;
    }
    IEnumerator AttackedAniDelay(float delay) {
        animator.Play("attacked", -1, 0f);
        animator.Play("attacked");
        _isPlayerFilp = true;
        yield return new WaitForSeconds(delay);
        _isPlayerFilp = false;
        animator.Play("idle", -1, 0f);
        animator.Play("idle");
    }
    // เมธอดสำหรับลด HP และตรวจสอบการตาย
    public void TakeDamage(float damage, bool isCritical = false, Vector3? knockbackPosition = null, float knockbackPower = 0f)
    {
        playerScr.RandomPoolAudio.RandomPool(transform.position);
        if(isCritical){
            _pool_TextDamage_Critical.GetPool(new Vector3(transform.position.x + Random.Range(-0.111539f, 0.111539f), transform.position.y ,transform.position.z), ((int)damage).ToString());
        }else{
            _pool_TextDamage.GetPool(new Vector3(transform.position.x + Random.Range(-0.111539f, 0.111539f), transform.position.y ,transform.position.z), ((int)damage).ToString());
        }
        if(hp != 1){
            animatorAttacked.Play("onAttacked");
            _pools[0].GetPool(transform.position, new Vector3(0,0,0), new Vector3(1,1,1));
        }
        hp -= damage;
        if(knockbackPosition != null){
            if(isBoss == true){
                KncokBack((Vector3)knockbackPosition, knockbackPower/2);
            }else{
                KncokBack((Vector3)knockbackPosition, knockbackPower);
            }
        }
        if (hp <= 0)
        {
            _expPool[0].GetPool(transform.position);
            if(isBigBoss == true){
           //     _rateDropItem.spawnBigBigGold(transform.position);
            }else if(isBoss == true){
        //        _rateDropItem.spawnBigGold(transform.position);
            }else{
                _rateDropItem.spawnItem(transform.position);
            }
            Die();
        }else{
            if(ani_Ro == 0){
                bodyanimatorAttacked.Play("attacked_base", -1, 0f);
                bodyanimatorAttacked.Play("attacked_base");
                ani_Ro = 1;
            }
            else{
                bodyanimatorAttacked.Play("attacked_base2", -1, 0f);
                bodyanimatorAttacked.Play("attacked_base2");
                ani_Ro = 0;
            }
            if(_coroutinePlayerFilp != null) StopCoroutine(_coroutinePlayerFilp);
            if(_coroutineAniDelay != null) StopCoroutine(_coroutineAniDelay);
            _coroutineAniDelay = StartCoroutine(AttackedAniDelay(0.2f));
            StartCoroutine(FlashRed());
        }
    }
    
    public void BoomDamage(float damage, bool isCritical = false, Vector3? knockbackPosition = null, float knockbackPower = 0f)
    {
        playerScr._resourceManager.AddScore(5);
        _pool_TextDamageBoom.GetPool(new Vector3(transform.position.x + Random.Range(-0.111539f, 0.111539f), transform.position.y ,transform.position.z), ((int)damage).ToString());
        
        if(hp != 1){
            animatorAttacked.Play("onAttacked");
            _pools[0].GetPool(transform.position, new Vector3(0,0,0), new Vector3(1,1,1));
        }
        hp -= damage;
        if(knockbackPosition != null){
            if(isBoss == true){
                KncokBack((Vector3)knockbackPosition, knockbackPower/2);
            }else{
                KncokBack((Vector3)knockbackPosition, knockbackPower);
            }
        }
        if (hp <= 0)
        {
            _expPool[0].GetPool(transform.position);
            _rateDropItem.spawnItem(transform.position);
            Die();
        }else{
            if(ani_Ro == 0){
                bodyanimatorAttacked.Play("attacked_base", -1, 0f);
                bodyanimatorAttacked.Play("attacked_base");
                ani_Ro = 1;
            }
            else{
                bodyanimatorAttacked.Play("attacked_base2", -1, 0f);
                bodyanimatorAttacked.Play("attacked_base2");
                ani_Ro = 0;
            }
            if(_coroutinePlayerFilp != null) StopCoroutine(_coroutinePlayerFilp);
            if(_coroutineAniDelay != null) StopCoroutine(_coroutineAniDelay);
            _coroutineAniDelay = StartCoroutine(AttackedAniDelay(0.2f));
            StartCoroutine(FlashRed());
        }
    }
    public void KncokBack(Vector3 knockbackPosition, float knockbackPower){
        transform.GetComponent<Rigidbody2D>().AddForce(((Vector3)knockbackPosition - transform.position).normalized * knockbackPower*-1, ForceMode2D.Impulse);
        if(gameObject.activeSelf == false) return;
        StartCoroutine(Attacked(0.2f));
    }
    public void LightningAttacked(float damage){
        if(gameObject.activeSelf == false) return;
        foreach(GameObject shock in _ShockList){
            shock.SetActive(false);
            shock.SetActive(true);
        }
            _pool_TextDamageLightning.GetPool(new Vector3(transform.position.x + Random.Range(-0.111539f, 0.111539f), transform.position.y ,transform.position.z), ((int)(damage)).ToString());
        
        if(hp != 1){
            animatorAttacked.Play("onAttacked");
            _pools[0].GetPool(transform.position, new Vector3(0,0,0), new Vector3(1,1,1));
        }
        hp -= damage;
        playerScr._resourceManager.AddScore(5);
        if (hp <= 0)
        {
            _expPool[0].GetPool(transform.position);
            _rateDropItem.spawnItem(transform.position);
            Die();
        }else{
            if(ani_Ro == 0){
                bodyanimatorAttacked.Play("attacked_base", -1, 0f);
                bodyanimatorAttacked.Play("attacked_base");
                ani_Ro = 1;
            }
            else{
                bodyanimatorAttacked.Play("attacked_base2", -1, 0f);
                bodyanimatorAttacked.Play("attacked_base2");
                ani_Ro = 0;
            }
            if(_coroutinePlayerFilp != null) StopCoroutine(_coroutinePlayerFilp);
            if(_coroutineAniDelay != null) StopCoroutine(_coroutineAniDelay);
            _coroutineAniDelay = StartCoroutine(AttackedAniDelay(0.2f));
            StartCoroutine(FlashRed());
        }
    }
    public void FireAttacked(float damage,float time){
        if(gameObject.activeSelf == false) return;
        _fireEffect.SetActive(false);
        _fireEffect.SetActive(true);
        if(_coroutineFire != null) {
            StopCoroutine(_coroutineFire);
        };
        _coroutineFire = StartCoroutine(FireAttack(damage, time));
    }
    public void IceEffect(float speedSlow){
        if(gameObject.activeSelf == false) return;
        _iceEffect.SetActive(false);
        _iceEffect.SetActive(true);
        _coroutineIce = StartCoroutine(IceEffectDelay(speedSlow));
    }
    IEnumerator IceEffectDelay(float speedSlow){
        StockIce = speedSlow;
        if(isBoss == true){
            moveSpeed = maxMoveSpeed * ((speedSlow/100)/2);
        }else{
            moveSpeed = maxMoveSpeed * (speedSlow/100);
        }
        yield return new WaitForSeconds(1f);
        _iceEffect.SetActive(false);
        moveSpeed = maxMoveSpeed;
    }
    IEnumerator FireAttack(float damage, float time) {
        if(playerScr != null){
            if(playerScr.UpgradeManager.IsStopGame == true) {
                damageFireStock = damage;
                timeStockFire = time;
                if(_coroutineFire != null) {
                    StopCoroutine(_coroutineFire);
                };
                yield break;
            }
        }
        foreach(SpriteRenderer sprite in sprites){
            shaders.Add(sprite.material.shader);
            sprite.material.shader = Shader.Find("GUI/Text Shader");
            sprite.color = new Color(0.9f, 0.9f, 0.9f, 1f);
        }

        _pool_TextDamageFire.GetPool(new Vector3(transform.position.x + Random.Range(-0.111539f, 0.111539f), transform.position.y ,transform.position.z), ((int)(damage)).ToString());
        
        if(hp != 1){
            animatorAttacked.Play("onAttacked");
        }
        hp -= damage;
        playerScr._resourceManager.AddScore(1);
        if (hp <= 0)
        {
            _expPool[0].GetPool(transform.position);
            _rateDropItem.spawnItem(transform.position);
            Die();
        }else{
            if(ani_Ro == 0){
                bodyanimatorAttacked.Play("attacked_base", -1, 0f);
                bodyanimatorAttacked.Play("attacked_base");
                ani_Ro = 1;
            }
            else{
                bodyanimatorAttacked.Play("attacked_base2", -1, 0f);
                bodyanimatorAttacked.Play("attacked_base2");
                ani_Ro = 0;
            }
            if(_coroutinePlayerFilp != null) StopCoroutine(_coroutinePlayerFilp);
            if(_coroutineAniDelay != null) StopCoroutine(_coroutineAniDelay);
            _pools[2].GetPool(transform.position, new Vector3(0,0,0), new Vector3(1,1,1));
        }
        yield return new WaitForSeconds(0.1f);
        for(int i = 0; i < sprites.Count; i++){
            sprites[i].material.shader = shaders[i];
            sprites[i].color = Color.white;
        }

        yield return new WaitForSeconds(0.15f);
        if(time >= 0.25f){
            _coroutineFire = StartCoroutine(FireAttack(damage, time-0.25f));
        }else{
            _fireEffect.SetActive(false);
        }
    }    
    IEnumerator FlashRed() {
        foreach(SpriteRenderer sprite in sprites){
            shaders.Add(sprite.material.shader);
            sprite.material.shader = Shader.Find("GUI/Text Shader");
            sprite.color = new Color(0.9f, 0.9f, 0.9f, 1f);
        }

        yield return new WaitForSeconds(0.1f);
        for(int i = 0; i < sprites.Count; i++){
            sprites[i].material.shader = shaders[i];
            sprites[i].color = Color.white;
        }
    }    
    IEnumerator Attacked(float delay)
    {
        yield return new WaitForSeconds(delay);
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    // เมธอดสำหรับทำลายตัวเอง
    private void Die()
    {
        playerScr._monsterDieAudio.RandomPool(transform.position);
        foreach(GameObject shock in _ShockList){
            shock.SetActive(false);
        }
        if(playerScr.UpgradeStats.UpgradeExplodingMonstersDeath > 0){
            _boombPool.GetPool(transform.position, new Vector3(0,0,0), new Vector3(1,1,1));
        }
        if(playerScr != null){
            _pools[1].GetPool(transform.position, new Vector3(0,0,0), new Vector3(1,1,1));
            gameObject.SetActive(false);
     //       GetComponent<BackToPool>().timeToBack();
        }
    }
    public float GetHp(){
        return hp;
    }

    IEnumerator WaitAndChangeBoolean()
    {if(playerScr != null){
        /*
        newPosition = transform.position;
            newPosition.z =2+10; // กำหนดค่า z ใหม่
            transform.position = newPosition;
        if (Random.Range(0, 100) <= playerScr.per_drop_her){
            GameObject hher = Instantiate(her, transform.position, Quaternion.identity);
            hher.SetActive(true);
            hher.transform.parent = mainCamera.transform;
        }else if (Random.Range(0, 100) <= playerScr.per_drop_sw){
            GameObject ssw = Instantiate(sw, transform.position, Quaternion.identity);
            ssw.SetActive(true);
            ssw.transform.parent = mainCamera.transform;
        }*/
        yield return new WaitForSeconds(0.5f); // รอ 1 วินาที
        Die();}
    }
}