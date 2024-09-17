using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private SpawnManager _spawnManager;
    [SerializeField] private Animator _animatorGold;
    [SerializeField] private TextMeshProUGUI _goldText;
    public float _goldCount = 0;
    [SerializeField] private Animator _animatorCountDie;
    [SerializeField] private TextMeshProUGUI _dieCountText;
    public float _dieCount = 0;
    [SerializeField] private UpgradeManager UpgradeManager;
    [SerializeField] private TextMeshProUGUI _addScoreText;
    [SerializeField] private TextMeshProUGUI _addScoreText2;
    [SerializeField] private TextMeshProUGUI _sumScoreText;
    [SerializeField] private Image _xScoreBar;
    [SerializeField] private GameObject _xScoreObj;
    [SerializeField] private TextMeshProUGUI _textXScore;
    [SerializeField] private TextMeshProUGUI _textXScore2;
    public int _sumScore = 0;
    public int Score = 0;
    [SerializeField] private int xScore = 0;
    [SerializeField] private Animator animatorAddScore;
    [SerializeField] private Animator animatorXScore;
    [SerializeField] private GameObject _HrObj;
    [SerializeField] private GameObject _TimeObj;
    [SerializeField] private TextMeshProUGUI _textHr;
    [SerializeField] private TextMeshProUGUI _textMin;
    [SerializeField] private TextMeshProUGUI _textSec;
    [SerializeField] private TextMeshProUGUI _textMinSec;
    [SerializeField] private GameObject animatorBloodXScore;
    public int hr = 0;
    public int min = 0;
    public int sec = 0;
    public int minSec = 0;
    private float stackTimeBigBossSpawn;
    public string playerIDName;
    public string playerID;

    void Awake(){
        _sumScoreText.text = _sumScore.ToString();
        _xScoreObj.SetActive(false);
        _dieCountText.text = "0";
        _goldText.text = "0";
        getIDNamePlayer();
    }
    public async void getIDNamePlayer(){/*
        var res = await HyplayBridge.GetUserAsync();
        if (res.Success)
        {
            playerIDName = res.Data.Username;
            playerID = res.Data.Id;
            return;
        }
        else{
            Debug.Log(res.Error);
            return;
        }*/
    }
    public void AddDieCount(){
        _animatorCountDie.Play("addScore", -1, 0);
        _animatorCountDie.Play("addScore");
        _dieCount++;
        _dieCountText.text = _dieCount.ToString();
    }
    public void AddGold(float gold){
        _animatorGold.Play("addScore", -1, 0);
        _animatorGold.Play("addScore");
        _goldCount += gold;
        _goldText.text = _goldCount.ToString();
    }
    void Update()
    {
        if(UpgradeManager.IsStopGame == true) return;
        if(_xScoreObj.activeSelf){
            _xScoreBar.fillAmount -= 0.006f;
            if(_xScoreBar.fillAmount <= 0){
                AddSumScore();
                xScore = 0;
                Score = 0;
                _xScoreObj.SetActive(false);
            }
        }
        if(minSec == 30 && min > 8){
            _spawnManager.SpawnMonsterMiniBoss();
        }
        minSec++;
        if(minSec < 10)
        {
            _textMinSec.text = "." + "0" + minSec.ToString();
        }
        else
        {
            _textMinSec.text = "." + minSec.ToString();
        }
        if(minSec == 60)
        {
            minSec = 0;
            sec++;
            if(sec < 10)
            {
                _textSec.text = "0" + sec.ToString();
            }
            else
            {
                _textSec.text = sec.ToString();
                }
            if(minSec < 10)
            {
                _textMinSec.text = "." + "0" + minSec.ToString();
            }
            else
            {
                _textMinSec.text = "." + minSec.ToString();
            }
        }
        if(sec == 60)
        {
            sec = 0;
            min++;
            spawnBoss(min);
            if(min < 10)
            {
                _textMin.text = "0" + min.ToString();
            }
            else
            {
                _textMin.text = min.ToString();
            }
            if(sec < 10)
            {
                _textSec.text = "0" + sec.ToString();
            }
            else
            {
                _textSec.text = sec.ToString();
            }
        }
        if(min == 60)
        {
            min = 0;
            hr++;
            _HrObj.SetActive(true);
            _TimeObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(26.7f, -42f);
            if(hr < 10)
            {
                _textHr.text = "0" + hr.ToString();
            }
            else
            {
                _textHr.text = hr.ToString();
            }
            if(min < 10)
            {
                _textMin.text = "0" + min.ToString();
            }
            else
            {
                _textMin.text = min.ToString();
            }
        }
    }
    public void AddSumScore()
    {
        if(Score == 0) return;
        _sumScore += Score;
        xScore = 0;
        Score = 0;
        _sumScoreText.text = _sumScore.ToString();
        animatorAddScore.Play("addScore", -1, 0);
        animatorAddScore.Play("addScore");
        _xScoreObj.SetActive(false);
    }

    public void AddScore(int addScore = 0)
    {
        if(addScore == 0) return;
        Score += addScore * xScore;
        _addScoreText.text = "+" + Score.ToString() + " Score";
        _addScoreText2.text = "+" + Score.ToString() + " Score";
    }
    public void AddXScore()
    {
        _xScoreBar.fillAmount = 1f;
        xScore++;
        _textXScore.text = "X" + xScore.ToString();
        _textXScore2.text = "X" + xScore.ToString();
        _xScoreObj.SetActive(true);
        Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        animatorBloodXScore.transform.rotation = randomRotation;
        animatorBloodXScore.GetComponent<Animator>().Play("bloodXScore", -1, 0);
        animatorBloodXScore.GetComponent<Animator>().Play("bloodXScore");
        animatorXScore.Play("xScore_shake", -1, 0);
        animatorXScore.Play("xScore_shake");
        // Create a random rotation for the game object
    }
    public void spawnBoss(int min)
    {
        if(min == 1){
            _spawnManager.SpawnMonsterMiniBoss();
        }else if(min == 2){
            _spawnManager.SpawnMonsterBoss(0);
        }else if(min == 3){
            _spawnManager.SpawnMonsterMiniBoss();
        }else if(min == 4){
            _spawnManager.SpawnMonsterBoss(1);
        }else if(min == 5){
            _spawnManager.SpawnMonsterMiniBoss();
        }else if(min == 6){
            _spawnManager.SpawnMonsterBoss(2);
        }else if(min == 7){
            _spawnManager.SpawnMonsterBoss(3);
        }else if(min == 8){
            _spawnManager.SpawnMonsterBigBoss();
            stackTimeBigBossSpawn = min;
        }else{
            if(min - stackTimeBigBossSpawn == 4){
                _spawnManager.SpawnMonsterBigBoss();
                stackTimeBigBossSpawn = min;
            }else{
                _spawnManager.SpawnMonsterBoss(Random.Range(0, 4));
            }
        }
    }
}
