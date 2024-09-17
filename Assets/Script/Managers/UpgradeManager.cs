using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UpgradeManager : MonoBehaviour
{
    public bool IsStopGame = false;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private UpgradeStats _upgradeStats;
    [SerializeField] private float _commonRate;
    [SerializeField] private float _rareRate;
    [SerializeField] private float _epicRate;
    [SerializeField] private float _legendaryRate;
    [SerializeField] private List<GameObject> _upgradeCloseUI;
    [SerializeField] private List<GameObject> _upgradeUI;
    [SerializeField] private List<GameObject> _dataUI;
    [SerializeField] private List<GameObject> _gradeUI;
    [SerializeField] private UpgradeDataSO _upgradeDataSO;
    [SerializeField] private UpgradeDataSO _upgrade2DataSO;
    private List<UpgradeDetail> _upgradeData = new List<UpgradeDetail>();
    private List<UpgradeDetail> refCommon = new List<UpgradeDetail>();
    private List<UpgradeDetail> refRare = new List<UpgradeDetail>();
    private List<UpgradeDetail> refEpic = new List<UpgradeDetail>();
    private List<UpgradeDetail> refLegendary = new List<UpgradeDetail>();
    [SerializeField] private LevelUpManager _levelUpManager;
    public List<int> _idRefSlot = new List<int> { 0, 0, 0 };
    public List<int> _LevelUpRefSlot = new List<int> { 0, 0, 0 };
    bool isAdded = false;
    [SerializeField] private RateDropItem _rateDropItem;
    [SerializeField] private GameObject _player;
    void Start()
    {
        UpgradeDataSO _monsterRaceSO2 = Instantiate(_upgradeDataSO);
        _upgradeData = new List<UpgradeDetail>(_monsterRaceSO2.Upgrade);
        for(int i = 0; i < _upgradeData.Count; i++){
            if(_upgradeData[i].Common != 0){
                refCommon.Add(_upgradeData[i]);
            }
            if(_upgradeData[i].Rare != 0){
                refRare.Add(_upgradeData[i]);
            }
            if(_upgradeData[i].Epic != 0){
                refEpic.Add(_upgradeData[i]);
            }
            if(_upgradeData[i].Legendary != 0){
                refLegendary.Add(_upgradeData[i]);
            }
        }
    }
    public void PlayerDieUpgrade(){
        foreach(GameObject ui in _upgradeUI){
            ui.SetActive(false);
        }
        foreach(GameObject ui in _upgradeCloseUI){
            ui.SetActive(false);
        }
        IsStopGame = true;
    }
    public void Upgrade(){
        foreach(GameObject ui in _upgradeUI){
            ui.SetActive(true);
        }
        foreach(GameObject ui in _upgradeCloseUI){
            ui.SetActive(false);
        }
        IsStopGame = true;
        for(int slotNumber = 0; slotNumber < _idRefSlot.Count; slotNumber++){
            while(_idRefSlot[slotNumber] == 0){
                float randomRate = Random.Range(0.00f, 100.00f);
                Debug.Log("slotNumber "+ slotNumber + " ran = " + randomRate);
                if(randomRate >= 0 && randomRate < _commonRate){
                    bool isNotMore = true;
                    for(int indexInrefCommon = 0; indexInrefCommon < refCommon.Count; indexInrefCommon++){
                        if(refCommon[indexInrefCommon].Level + refCommon[indexInrefCommon].Common <= refCommon[indexInrefCommon].DetailText.Count - 1){
                            isNotMore = false;
                        }
                    }
                    if(isNotMore == false){
                        for(int child = 0; child < _gradeUI[slotNumber].transform.childCount; child++){
                            _gradeUI[slotNumber].transform.GetChild(child).gameObject.SetActive(false);
                        }
                        _gradeUI[slotNumber].transform.GetChild(0).gameObject.SetActive(true);
                            int randomIndex = Random.Range(0, refCommon.Count);
                            if(refCommon[randomIndex].Common != 0 && refCommon[randomIndex].Level + refCommon[randomIndex].Common <= refCommon[randomIndex].DetailText.Count - 1){
                                bool isNotSame = true;
                                if(refCommon[randomIndex].Id != -1){
                                    for(int index = 0; index < _idRefSlot.Count; index++){
                                        if(_idRefSlot[index] == refCommon[randomIndex].Id){
                                            isNotSame = false;
                                        }
                                    }
                                }
                                if(isNotSame == true){
                                    _dataUI[slotNumber].transform.GetChild(0).GetComponent<Image>().sprite = refCommon[randomIndex]._imgProflie;
                                    _dataUI[slotNumber].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = refCommon[randomIndex].TopicText;
                                    _dataUI[slotNumber].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = refCommon[randomIndex].DetailText[refCommon[randomIndex].Level + refCommon[randomIndex].Common];
                                    _idRefSlot[slotNumber] = refCommon[randomIndex].Id;
                                    _LevelUpRefSlot[slotNumber] = refCommon[randomIndex].Common;
                                }
                            }
                    }
                }
                else if(randomRate >= _commonRate && randomRate < _commonRate + _rareRate){
                    bool isNotMore = true;
                    for(int indexInrefRare = 0; indexInrefRare < refRare.Count; indexInrefRare++){
                        if(refRare[indexInrefRare].Level + refRare[indexInrefRare].Rare <= refRare[indexInrefRare].DetailText.Count - 1){
                            isNotMore = false;
                        }
                    }
                    if(isNotMore == false){
                        for(int child = 0; child < _gradeUI[slotNumber].transform.childCount; child++){
                            _gradeUI[slotNumber].transform.GetChild(child).gameObject.SetActive(false);
                        }
                        _gradeUI[slotNumber].transform.GetChild(1).gameObject.SetActive(true);
                            int randomIndex = Random.Range(0, refRare.Count);
                            if(refRare[randomIndex].Rare != 0 && refRare[randomIndex].Level + refRare[randomIndex].Rare <= refRare[randomIndex].DetailText.Count - 1){
                                bool isNotSame = true;
                                if(refRare[randomIndex].Id != -1){
                                    for(int index = 0; index < _idRefSlot.Count; index++){
                                        if(_idRefSlot[index] == refRare[randomIndex].Id){
                                            isNotSame = false;
                                        }
                                    }
                                }
                                if(isNotSame == true){
                                    _dataUI[slotNumber].transform.GetChild(0).GetComponent<Image>().sprite = refRare[randomIndex]._imgProflie;
                                    _dataUI[slotNumber].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = refRare[randomIndex].TopicText;
                                    _dataUI[slotNumber].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = refRare[randomIndex].DetailText[refRare[randomIndex].Level + refRare[randomIndex].Rare];
                                    _idRefSlot[slotNumber] = refRare[randomIndex].Id;
                                    _LevelUpRefSlot[slotNumber] = refRare[randomIndex].Rare;
                                }
                            }
                    }
                }
                else if(randomRate >= _commonRate + _rareRate && randomRate < _commonRate + _rareRate + _epicRate){
                    bool isNotMore = true;
                    for(int indexInrefEpic = 0; indexInrefEpic < refEpic.Count; indexInrefEpic++){
                        if(refEpic[indexInrefEpic].Level + refEpic[indexInrefEpic].Epic <= refEpic[indexInrefEpic].DetailText.Count - 1){
                            isNotMore = false;
                        }
                    }
                    if(isNotMore == false){
                        for(int child = 0; child < _gradeUI[slotNumber].transform.childCount; child++){
                            _gradeUI[slotNumber].transform.GetChild(child).gameObject.SetActive(false);
                        }
                        _gradeUI[slotNumber].transform.GetChild(2).gameObject.SetActive(true);
                            int randomIndex = Random.Range(0, refEpic.Count);
                            if(refEpic[randomIndex].Epic != 0 && refEpic[randomIndex].Level + refEpic[randomIndex].Epic <= refEpic[randomIndex].DetailText.Count - 1){
                                bool isNotSame = true;
                                if(refEpic[randomIndex].Id != -1){
                                    for(int index = 0; index < _idRefSlot.Count; index++){
                                        if(_idRefSlot[index] == refEpic[randomIndex].Id){
                                            isNotSame = false;
                                        }
                                    }
                                }
                                if(isNotSame == true){
                                    _dataUI[slotNumber].transform.GetChild(0).GetComponent<Image>().sprite = refEpic[randomIndex]._imgProflie;
                                    _dataUI[slotNumber].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = refEpic[randomIndex].TopicText;
                                    _dataUI[slotNumber].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = refEpic[randomIndex].DetailText[refEpic[randomIndex].Level + refEpic[randomIndex].Epic];
                                    _idRefSlot[slotNumber] = refEpic[randomIndex].Id;
                                    _LevelUpRefSlot[slotNumber] = refEpic[randomIndex].Epic;
                                }
                            }
                    }
                }
                else if(randomRate >= _commonRate + _rareRate + _epicRate){
                    bool isNotMore = true;
                    for(int indexInrefLegendary = 0; indexInrefLegendary < refLegendary.Count; indexInrefLegendary++){
                        if(refLegendary[indexInrefLegendary].Level + refLegendary[indexInrefLegendary].Legendary <= refLegendary[indexInrefLegendary].DetailText.Count - 1){
                            isNotMore = false;
                        }
                    }
                    if(isNotMore == false){
                        for(int child = 0; child < _gradeUI[slotNumber].transform.childCount; child++){
                            _gradeUI[slotNumber].transform.GetChild(child).gameObject.SetActive(false);
                        }
                        _gradeUI[slotNumber].transform.GetChild(3).gameObject.SetActive(true);
                            int randomIndex = Random.Range(0, refLegendary.Count);
                            if(refLegendary[randomIndex].Legendary != 0 && refLegendary[randomIndex].Level + refLegendary[randomIndex].Legendary <= refLegendary[randomIndex].DetailText.Count - 1){
                                bool isNotSame = true;
                                if(refLegendary[randomIndex].Id != -1){
                                    for(int index = 0; index < _idRefSlot.Count; index++){
                                        if(_idRefSlot[index] == refLegendary[randomIndex].Id){
                                            isNotSame = false;
                                        }
                                    }
                                }
                                if(isNotSame == true){
                                    _dataUI[slotNumber].transform.GetChild(0).GetComponent<Image>().sprite = refLegendary[randomIndex]._imgProflie;
                                    _dataUI[slotNumber].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = refLegendary[randomIndex].TopicText;
                                    _dataUI[slotNumber].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = refLegendary[randomIndex].DetailText[refLegendary[randomIndex].Level + refLegendary[randomIndex].Legendary];
                                    _idRefSlot[slotNumber] = refLegendary[randomIndex].Id;
                                    _LevelUpRefSlot[slotNumber] = refLegendary[randomIndex].Legendary;
                                }
                            }
                    }
                }
            }
        }
    }
    public void CloseUpgrade(){
        foreach(GameObject ui in _upgradeUI){
            ui.SetActive(false);
        }
        _levelUpManager.checkLevelUp();
    }

    public void RunGame() {
        IsStopGame = false;
    }

    public void UpdateSet(int slotNumber){
        int index = _idRefSlot[slotNumber];
        
        if(index == 1){
            for(int j = 0; j < _upgradeData.Count; j++){
                if(_upgradeData[j].Id == index){
                    for(int i = 0; i < _LevelUpRefSlot[slotNumber]; i++){
                        _upgradeData[j].Level++;
                        Debug.Log("upgradeData[index - 1].Level " + _upgradeData[j].Level);
                        _upgradeStats.UpgradeDashCount += _upgradeData[j].ValueAdd[_upgradeData[j].Level].value[0];
                    }
                    break;
                }
            }
        }
        else if(index == 2){
            for(int j = 0; j < _upgradeData.Count; j++){
                if(_upgradeData[j].Id == index){
                    for(int i = 0; i < _LevelUpRefSlot[slotNumber]; i++){
                        _upgradeData[j].Level++;
                        Debug.Log("upgradeData[index - 1].Level " + _upgradeData[j].Level);
                        _upgradeStats.UpgradeWiderDash += _upgradeData[j].ValueAdd[_upgradeData[j].Level].value[0];
                    }
                    break;
                }
            }
        }
        else if(index == 3){
            for(int j = 0; j < _upgradeData.Count; j++){
                if(_upgradeData[j].Id == index){
                    for(int i = 0; i < _LevelUpRefSlot[slotNumber]; i++){
                        _upgradeData[j].Level++;
                        Debug.Log("upgradeData[index - 1].Level " + _upgradeData[j].Level);
                        _upgradeStats.UpgradeIndomitable += _upgradeData[j].ValueAdd[_upgradeData[j].Level].value[0];
                    }
                    break;
                }
            }
        }
        else if(index == 4){
            for(int j = 0; j < _upgradeData.Count; j++){
                if(_upgradeData[j].Id == index){
                    for(int i = 0; i < _LevelUpRefSlot[slotNumber]; i++){
                        _upgradeData[j].Level++;
                        Debug.Log("upgradeData[index - 1].Level " + _upgradeData[j].Level);
                        _upgradeStats.UpgradeAttackDamage += _upgradeData[j].ValueAdd[_upgradeData[j].Level].value[0];
                    }
                    break;
                }
            }
        }
        else if(index == 5){
            for(int j = 0; j < _upgradeData.Count; j++){
                if(_upgradeData[j].Id == index){
                    for(int i = 0; i < _LevelUpRefSlot[slotNumber]; i++){
                        _upgradeData[j].Level++;
                        Debug.Log("upgradeData[index - 1].Level " + _upgradeData[j].Level);
                        _upgradeStats.UpgradeLethalAttack += _upgradeData[j].ValueAdd[_upgradeData[j].Level].value[0];
                    }
                    break;
                }
            }
        }
        else if(index == 6){
            for(int j = 0; j < _upgradeData.Count; j++){
                if(_upgradeData[j].Id == index){
                    for(int i = 0; i < _LevelUpRefSlot[slotNumber]; i++){
                        _upgradeData[j].Level++;
                        Debug.Log("upgradeData[index - 1].Level " + _upgradeData[j].Level);
                        _upgradeStats.UpgradeDashCooldown += _upgradeData[j].ValueAdd[_upgradeData[j].Level].value[0];
                    }
                    break;
                }
            }
        }
        else if(index == 7){
            for(int j = 0; j < _upgradeData.Count; j++){
                if(_upgradeData[j].Id == index){
                    for(int i = 0; i < _LevelUpRefSlot[slotNumber]; i++){
                        _upgradeData[j].Level++;
                        Debug.Log("upgradeData[index - 1].Level " + _upgradeData[j].Level);
                        _upgradeStats.UpgradeExplodingMonstersDeath += _upgradeData[j].ValueAdd[_upgradeData[j].Level].value[0];
                    }
                    break;
                }
            }
        }
        else if(index == 8){
            for(int j = 0; j < _upgradeData.Count; j++){
                if(_upgradeData[j].Id == index){
                    for(int i = 0; i < _LevelUpRefSlot[slotNumber]; i++){
                        _upgradeData[j].Level++;
                        Debug.Log("upgradeData[index - 1].Level " + _upgradeData[j].Level);
                        _upgradeStats.UpgradeAttackDamagePer += _upgradeData[j].ValueAdd[_upgradeData[j].Level].value[0];
                    }
                    break;
                }
            }
        }
        else if(index == 9){
            for(int j = 0; j < _upgradeData.Count; j++){
                if(_upgradeData[j].Id == index){
                    for(int i = 0; i < _LevelUpRefSlot[slotNumber]; i++){
                        _upgradeData[j].Level++;
                        Debug.Log("upgradeData[index - 1].Level " + _upgradeData[j].Level);
                        _upgradeStats.UpgradeCriticalDamage += _upgradeData[j].ValueAdd[_upgradeData[j].Level].value[0];
                    }
                    break;
                }
            }
        }
        else if(index == 10){
            for(int j = 0; j < _upgradeData.Count; j++){
                if(_upgradeData[j].Id == index){
                    for(int i = 0; i < _LevelUpRefSlot[slotNumber]; i++){
                        _upgradeData[j].Level++;
                        Debug.Log("upgradeData[index - 1].Level " + _upgradeData[j].Level);
                        _upgradeStats.UpgradeChanceCriticalAttack += _upgradeData[j].ValueAdd[_upgradeData[j].Level].value[0];
                    }
                    break;
                }
            }
        }
        else if(index == 11){
            for(int j = 0; j < _upgradeData.Count; j++){
                if(_upgradeData[j].Id == index){
                    for(int i = 0; i < _LevelUpRefSlot[slotNumber]; i++){
                        _upgradeData[j].Level++;
                        Debug.Log("upgradeData[index - 1].Level " + _upgradeData[j].Level);
                        _upgradeStats.UpgradeBlockShield = _upgradeData[j].ValueAdd[_upgradeData[j].Level].value[0];
                    }
                    break;
                }
            }
        }
        else if(index == 12){
            for(int j = 0; j < _upgradeData.Count; j++){
                if(_upgradeData[j].Id == index){
                    for(int i = 0; i < _LevelUpRefSlot[slotNumber]; i++){
                        _upgradeData[j].Level++;
                        Debug.Log("upgradeData[index - 1].Level " + _upgradeData[j].Level);
                        _playerController.UpdateMaxHp(_upgradeData[j].ValueAdd[_upgradeData[j].Level].value[0]);
                    }
                    break;
                }
            }
        }
        else if(index == 13){
            for(int j = 0; j < _upgradeData.Count; j++){
                if(_upgradeData[j].Id == index){
                    for(int i = 0; i < _LevelUpRefSlot[slotNumber]; i++){
                        _upgradeData[j].Level++;
                        Debug.Log("upgradeData[index - 1].Level " + _upgradeData[j].Level);
                        _upgradeStats.UpgradeEvasion += _upgradeData[j].ValueAdd[_upgradeData[j].Level].value[0];
                    }
                    break;
                }
            }
        }
        else if(index == 14){
            for(int j = 0; j < _upgradeData.Count; j++){
                if(_upgradeData[j].Id == index){
                    for(int i = 0; i < _LevelUpRefSlot[slotNumber]; i++){
                        _upgradeData[j].Level++;
                        Debug.Log("upgradeData[index - 1].Level " + _upgradeData[j].Level);
                        _upgradeStats.UpgradeSkillDuration += _upgradeData[j].ValueAdd[_upgradeData[j].Level].value[0];
                    }
                    break;
                }
            }
        }
        else if(index == 15){
            for(int j = 0; j < _upgradeData.Count; j++){
                if(_upgradeData[j].Id == index){
                    for(int i = 0; i < _LevelUpRefSlot[slotNumber]; i++){
                        _upgradeData[j].Level++;
                        Debug.Log("upgradeData[index - 1].Level " + _upgradeData[j].Level);
                        _upgradeStats.UpgradeFlexibility += _upgradeData[j].ValueAdd[_upgradeData[j].Level].value[0];
                    }
                    break;
                }
            }
        }
        else if(index == 16){
            for(int j = 0; j < _upgradeData.Count; j++){
                if(_upgradeData[j].Id == index){
                    for(int i = 0; i < _LevelUpRefSlot[slotNumber]; i++){
                        _upgradeData[j].Level++;
                        Debug.Log("upgradeData[index - 1].Level " + _upgradeData[j].Level);
                        _upgradeStats.UpgradeDropRateDash += _upgradeData[j].ValueAdd[_upgradeData[j].Level].value[0];
                    }
                    break;
                }
            }
        }
        else if(index == 17){
            for(int j = 0; j < _upgradeData.Count; j++){
                if(_upgradeData[j].Id == index){
                    for(int i = 0; i < _LevelUpRefSlot[slotNumber]; i++){
                        _upgradeData[j].Level++;
                        Debug.Log("upgradeData[index - 1].Level " + _upgradeData[j].Level);
                        _upgradeStats.UpgradeDropRateFood += _upgradeData[j].ValueAdd[_upgradeData[j].Level].value[0];
                    }
                    break;
                }
            }
        }
        else if(index == 18){
            for(int j = 0; j < _upgradeData.Count; j++){
                if(_upgradeData[j].Id == index){
                    for(int i = 0; i < _LevelUpRefSlot[slotNumber]; i++){
                        _upgradeData[j].Level++;
                        Debug.Log("upgradeData[index - 1].Level " + _upgradeData[j].Level);
                        _upgradeStats.UpgradeDropRateTNT += _upgradeData[j].ValueAdd[_upgradeData[j].Level].value[0];
                    }
                    break;
                }
            }
        }
        else if(index == 19){
            for(int j = 0; j < _upgradeData.Count; j++){
                if(_upgradeData[j].Id == index){
                    for(int i = 0; i < _LevelUpRefSlot[slotNumber]; i++){
                        _upgradeData[j].Level++;
                        Debug.Log("upgradeData[index - 1].Level " + _upgradeData[j].Level);
                        _upgradeStats.UpgradeDropRateGold += _upgradeData[j].ValueAdd[_upgradeData[j].Level].value[0];
                    }
                    break;
                }
            }
        }
        else if(index == 20){
            for(int j = 0; j < _upgradeData.Count; j++){
                if(_upgradeData[j].Id == index){
                    for(int i = 0; i < _LevelUpRefSlot[slotNumber]; i++){
                        _upgradeData[j].Level++;
                        Debug.Log("upgradeData[index - 1].Level " + _upgradeData[j].Level);
                        _upgradeStats.UpgradeFireSword += _upgradeData[j].ValueAdd[_upgradeData[j].Level].value[0];
                    }
                    break;
                }
            }
        }
        else if(index == 21){
            for(int j = 0; j < _upgradeData.Count; j++){
                if(_upgradeData[j].Id == index){
                    for(int i = 0; i < _LevelUpRefSlot[slotNumber]; i++){
                        _upgradeData[j].Level++;
                        Debug.Log("upgradeData[index - 1].Level " + _upgradeData[j].Level);
                        _upgradeStats.UpgradeIceSword += _upgradeData[j].ValueAdd[_upgradeData[j].Level].value[0];
                    }
                    break;
                }
            }
        }
        else if(index == 22){
            for(int j = 0; j < _upgradeData.Count; j++){
                if(_upgradeData[j].Id == index){
                    for(int i = 0; i < _LevelUpRefSlot[slotNumber]; i++){
                        _upgradeData[j].Level++;
                        Debug.Log("upgradeData[index - 1].Level " + _upgradeData[j].Level);
                        _upgradeStats.UpgradeLightningSword += _upgradeData[j].ValueAdd[_upgradeData[j].Level].value[0];
                        _upgradeStats.UpgradeLightningSwordSpread += _upgradeData[j].ValueAdd[_upgradeData[j].Level].value[1];
                    }
                    break;
                }
            }
        }
        else if(index == 23){
            for(int j = 0; j < _upgradeData.Count; j++){
                if(_upgradeData[j].Id == index){
                    for(int i = 0; i < _LevelUpRefSlot[slotNumber]; i++){
                        _upgradeData[j].Level++;
                        Debug.Log("upgradeData[index - 1].Level " + _upgradeData[j].Level);
                        _upgradeStats.UpgradeVampireSword += _upgradeData[j].ValueAdd[_upgradeData[j].Level].value[0];
                    }
                    break;
                }
            }
        }
        else if(index == 24){
            for(int j = 0; j < _upgradeData.Count; j++){
                if(_upgradeData[j].Id == index){
                    for(int i = 0; i < _LevelUpRefSlot[slotNumber]; i++){
                        _upgradeData[j].Level++;
                        Debug.Log("upgradeData[index - 1].Level " + _upgradeData[j].Level);
                        _upgradeStats.UpgradeDarkSword += _upgradeData[j].ValueAdd[_upgradeData[j].Level].value[0];
                        _upgradeStats.UpgradeDarkSwordWide += _upgradeData[j].ValueAdd[_upgradeData[j].Level].value[1];
                    }
                    break;
                }
            }
        }
        else if(index == -1){
            for(int j = 0; j < _upgradeData.Count; j++){
                if(_upgradeData[j].Id == -1){
                    _upgradeData[j].Level = -1;
                    Debug.Log("upgradeData[index - 1].Level " + _upgradeData[j].Level);
                    _rateDropItem.GetGoldPlayer(_player.transform.position, _upgradeData[j].ValueAdd[_LevelUpRefSlot[slotNumber] - 1].value[0]);
                    break;
                }
            }
        }

        
        for(int j = 0; j < _upgradeData.Count; j++){
            if(_upgradeData[j].Id == index){
                if(_upgradeData[j].Level >= _upgradeData[j].DetailText.Count - 1){
                    _upgradeData.RemoveAt(j);
                    Debug.Log("Remove " + (j));
                }
                break;
            }
        }
        if(_upgradeData.Count <= 2 && isAdded == false){
            isAdded = true;
            UpgradeDataSO _monsterRaceSO2 = Instantiate(_upgrade2DataSO);
            _upgradeData.Add(_monsterRaceSO2.Upgrade[0]);
        }
        refCommon = new List<UpgradeDetail>();
        refRare = new List<UpgradeDetail>();
        refEpic = new List<UpgradeDetail>();
        refLegendary = new List<UpgradeDetail>();
        for(int i = 0; i < _upgradeData.Count; i++){
            if(_upgradeData[i].Common != 0){
                refCommon.Add(_upgradeData[i]);
            }
            if(_upgradeData[i].Rare != 0){
                refRare.Add(_upgradeData[i]);
            }
            if(_upgradeData[i].Epic != 0){
                refEpic.Add(_upgradeData[i]);
            }
            if(_upgradeData[i].Legendary != 0){
                refLegendary.Add(_upgradeData[i]);
            }
        }
        _idRefSlot = new List<int> { 0, 0, 0 };
        _LevelUpRefSlot = new List<int> { 0, 0, 0 };
    }
}
