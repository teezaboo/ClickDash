using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private ScenceManagers _scenceManagers;
    [SerializeField] private ResourceManager _resourceManager;
    [SerializeField] private TextMeshProUGUI _ScoreText;
    [SerializeField] private TextMeshProUGUI _KillText;
    [SerializeField] private TextMeshProUGUI _GoldText;
    [SerializeField] private TextMeshProUGUI _ExpText;
    [SerializeField] private TextMeshProUGUI _timeSurvivledText;
    [SerializeField] private List<GameObject> gameOverPanel;
    [SerializeField] private Animator AnimatorGameOver;
    [SerializeField] private Animator AnimatorGameOver2;
    public void GameOverStart(){
        _ScoreText.text = _resourceManager._sumScore.ToString();
        _KillText.text = "x" + _resourceManager._dieCount.ToString();
        _GoldText.text = "x" + _resourceManager._goldCount.ToString();
        _ExpText.text = "x" + ((int)(_resourceManager._dieCount/3)).ToString();
        if(_resourceManager.hr == 0){
            if(_resourceManager.min < 10){
                if(_resourceManager.sec < 10){
                    _timeSurvivledText.text = "0" + _resourceManager.min.ToString() + ":0" + _resourceManager.sec.ToString();
                }else{
                    _timeSurvivledText.text = "0" + _resourceManager.min.ToString() + ":" + _resourceManager.sec.ToString();
                }
            }else{
                if(_resourceManager.sec < 10){
                    _timeSurvivledText.text = _resourceManager.min.ToString() + ":0" + _resourceManager.sec.ToString();
                }else{
                    _timeSurvivledText.text = _resourceManager.min.ToString() + ":" + _resourceManager.sec.ToString();
                }
            }
        }else{
            if(_resourceManager.min < 10){
                if(_resourceManager.sec < 10){
                    _timeSurvivledText.text = "0" + _resourceManager.hr.ToString() + ":" + "0" + _resourceManager.min.ToString() + ":0" + _resourceManager.sec.ToString();
                }else{
                    _timeSurvivledText.text = "0" + _resourceManager.hr.ToString() + ":" + "0" + _resourceManager.min.ToString() + ":" + _resourceManager.sec.ToString();
                }
            }else{
                if(_resourceManager.sec < 10){
                    _timeSurvivledText.text = "0" + _resourceManager.hr.ToString() + ":"  + _resourceManager.min.ToString() + ":0" + _resourceManager.sec.ToString();
                }else{
                    _timeSurvivledText.text = "0" + _resourceManager.hr.ToString() + ":" + _resourceManager.min.ToString() + ":" + _resourceManager.sec.ToString();
                }
            }
        }
        StartCoroutine(DelayStartUI());
    }
    IEnumerator DelayStartUI() {
        yield return new WaitForSeconds(1.2f);
        foreach (var item in gameOverPanel)
        {
            item.SetActive(true);
        }
    }
    public void GameOverEnd(){
        StartCoroutine(DelayEndUI());
    }
    IEnumerator DelayEndUI() {
        yield return new WaitForSeconds(1.2f);
        AnimatorGameOver.Play("closeLeveLUP");
        AnimatorGameOver2.Play("CloseAbility");
        _scenceManagers.StartAniGotoMenu();
    }
}
