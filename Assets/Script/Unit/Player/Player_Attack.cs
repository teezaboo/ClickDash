using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Player_Attack : MonoBehaviour
{
    [SerializeField] private RandomPoolAudio AudioDash;
    private bool isDelayAttack = false;
    [SerializeField] private GameObject _hpBar;
    [SerializeField] private List<GameObject> _swCountObj;
    [SerializeField] private List<TextMeshProUGUI> _textSwCountObj;
    [SerializeField] private int sw_num_dash_max;
    private int sw_num_dash = 0;
    [SerializeField] private spawn_GroundTouching _spawn_GroundTouching;
    [SerializeField] private float speed_sw = 1;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private player_control_set _player_Control_Set;
    [SerializeField] private Slash _slash;
    public bool isPowerSkill = false;
    private Coroutine _waitCoroutine;
    private float timeStockSkill = 0;
    public bool isBlockAttack = false;
    private void Start(){
        sw_num_dash = sw_num_dash_max + (int)GetComponent<PlayerController>().UpgradeStats.UpgradeDashCount;
        _textSwCountObj[0].text = (sw_num_dash + 1).ToString();
        _textSwCountObj[1].text = (sw_num_dash + 1).ToString();
    }
    public void AddSW(){
        if(_waitCoroutine != null){
            StopCoroutine(_waitCoroutine);
            isDelayAttack = false;
            _player_Control_Set.PlayAnimationPlayer("idle");
        }
        sw_num_dash++;
            _textSwCountObj[0].text = (sw_num_dash + 1).ToString();
            _textSwCountObj[1].text = (sw_num_dash + 1).ToString();
    }
    public void Update(){
        if(isPowerSkill == true && GetComponent<PlayerController>().UpgradeManager.IsStopGame == false){
            if(timeStockSkill > 0){
                _textSwCountObj[0].text = "∞";
                _textSwCountObj[1].text = "∞";
                timeStockSkill -= 1f/60f;
            //    Debug.Log("timeStockSkill = " + timeStockSkill);
                GetComponent<PlayerController>().Sec++;
                if(GetComponent<PlayerController>().Sec >= 60){
                    GetComponent<PlayerController>().Min++;
                    GetComponent<PlayerController>().Sec = 0;
                    GetComponent<PlayerController>()._textSecInfinityDash.text = "00";
                    GetComponent<PlayerController>()._textMInfinityDash.text = "0";
                }else{
                    if(60 - GetComponent<PlayerController>().Sec < 10){
                        GetComponent<PlayerController>()._textSecInfinityDash.text = "0" + (60 - GetComponent<PlayerController>().Sec).ToString();
                    }else{
                        GetComponent<PlayerController>()._textSecInfinityDash.text = (60 - GetComponent<PlayerController>().Sec).ToString();
                    }
                    if(GetComponent<PlayerController>().UpgradeStats.UpgradeSkillDuration + 5f - 1f - GetComponent<PlayerController>().Min < 10){
                        GetComponent<PlayerController>()._textMInfinityDash.text = "0" + (GetComponent<PlayerController>().UpgradeStats.UpgradeSkillDuration + 5f - 1f - GetComponent<PlayerController>().Min).ToString();
                    }else{
                        GetComponent<PlayerController>()._textMInfinityDash.text = (GetComponent<PlayerController>().UpgradeStats.UpgradeSkillDuration + 5f - 1f - GetComponent<PlayerController>().Min).ToString();
                    }
                }
            }else{
                GetComponent<PlayerController>().Min = 0;
                GetComponent<PlayerController>().Sec = 0;
                
                GetComponent<PlayerController>()._objInfinityDash.SetActive(false);
                GetComponent<PlayerController>().ClosePower();
                timeStockSkill = 0;
                _textSwCountObj[0].text = (sw_num_dash + 1).ToString();
                _textSwCountObj[1].text = (sw_num_dash + 1).ToString();
                isPowerSkill = false;
                isDelayAttack = false;
            }
        }else{
            if(sw_num_dash > 0){
                _textSwCountObj[0].text = (sw_num_dash + 1).ToString();
                _textSwCountObj[1].text = (sw_num_dash + 1).ToString();
            }                
        }
    }
    public void PowerSkill(float time){
        timeStockSkill = time;
        isPowerSkill = true;
    }
    public void PlayerAttack()
    {
        if(isBlockAttack == true) return;
        GetComponent<PlayerController>().UpgradeManager.IsStopGame = false;
        if(_playerController.IsDie == true) return;
        if((isDelayAttack == true) && isPowerSkill == false) return;
        _spawn_GroundTouching.spawn_GroundTouching_Effect();
        Vector3 startPosition = transform.position;
        Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        clickPosition.z = 0;
        transform.position = clickPosition;
        if(startPosition.x > clickPosition.x && transform.localScale.x > 0){
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            _hpBar.transform.localScale = new Vector3(-_hpBar.transform.localScale.x, _hpBar.transform.localScale.y, _hpBar.transform.localScale.z);
            _swCountObj[0].gameObject.SetActive(true);
            _swCountObj[1].gameObject.SetActive(false);
        }else if(startPosition.x < clickPosition.x && transform.localScale.x < 0){
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            _hpBar.transform.localScale = new Vector3(-_hpBar.transform.localScale.x, _hpBar.transform.localScale.y, _hpBar.transform.localScale.z);
            _swCountObj[0].gameObject.SetActive(false);
            _swCountObj[1].gameObject.SetActive(true);
        }
        AudioDash.RandomPool(transform.position);
        if(sw_num_dash <= 0 && isPowerSkill == false){
            _textSwCountObj[0].text = "0";
            _textSwCountObj[1].text = "0";
            _player_Control_Set.PlayAnimationPlayer("attack");
            isDelayAttack = true;
            _waitCoroutine = StartCoroutine(WaitAndChangeBoolean());
        }else if(isPowerSkill == false){
            isDelayAttack = false;
            sw_num_dash--;
            _textSwCountObj[0].text = (sw_num_dash + 1).ToString();
            _textSwCountObj[1].text = (sw_num_dash + 1).ToString();
        }
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentPosition.z = 0;
        Vector3 middlePosition = (startPosition + currentPosition) / 2.0f;
        _slash.SlashAttack(middlePosition, currentPosition, startPosition);
    }
    IEnumerator WaitAndChangeBoolean()
    {
        yield return new WaitForSeconds(((1+0.1f)/(1+speed_sw)) - ((GetComponent<PlayerController>().UpgradeStats.UpgradeDashCooldown/100) * ((1+0.1f)/(1+speed_sw)))); // รอ 1 วินาที

        // หลังจากรอเสร็จ กำหนดค่าตัวแปรบูลลีนเป็น true
            sw_num_dash = sw_num_dash_max + (int)GetComponent<PlayerController>().UpgradeStats.UpgradeDashCount;
            _textSwCountObj[0].text = (sw_num_dash + 1).ToString();
            _textSwCountObj[1].text = (sw_num_dash + 1).ToString();
        isDelayAttack = false;

        // สามารถเรียกใช้โค้ดเพิ่มเติมที่นี่หลังจากเปลี่ยนค่าตัวแปรเสร็จสิ้น
    }
}
