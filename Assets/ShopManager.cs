using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public AudioClick clickAduio;
    public TextMeshProUGUI textPlayerMoney;
    public int playerGold = 0;
    public int CurSWGrade = 0;
    public int CurHatGrade = 0;
    public int CurArmorGrade = 0;
    public List<GameObject> gradeSW;
    public List<int> prizeSW;
    public TextMeshProUGUI textPrizeSW;
    public List<GameObject> gradeHat;
    public List<int> prizeHat;
    public TextMeshProUGUI textPrizeHat;
    public List<GameObject> gradeArmor;
    public List<int> prizeArmor;
    public TextMeshProUGUI textPrizeArmor;
    public HyplayDataPlayer _hyplayDataPlayer;
    public List<GameObject> buttonBuy;
    public void Awake(){
        SetShopUpgrade();
    }
    public void OnEnable(){
        SetShopUpgrade();
    }
    public void SetShopUpgrade(){
        for(int i = 0; i < gradeSW.Count; i++){
            if(i == CurSWGrade){
                gradeSW[i].SetActive(true);
            }else{
                gradeSW[i].SetActive(false);
            }
        }
        for(int i = 0; i < gradeHat.Count; i++){
            if(i == CurHatGrade){
                gradeHat[i].SetActive(true);
            }else{
                gradeHat[i].SetActive(false);
            }
        }
        for(int i = 0; i < gradeArmor.Count; i++){
            if(i == CurArmorGrade){
                gradeArmor[i].SetActive(true);
            }else{
                gradeArmor[i].SetActive(false);
            }
        }
        if(CurSWGrade == prizeSW.Count-1) buttonBuy[0].SetActive(false);
        else
        {
            buttonBuy[0].SetActive(true);
        }
        
        if(CurHatGrade == prizeHat.Count-1) buttonBuy[1].SetActive(false);
        else
        {
            buttonBuy[1].SetActive(true);
        }
        
        
        if(CurArmorGrade == prizeArmor.Count-1) buttonBuy[2].SetActive(false);
        else
        {
            buttonBuy[2].SetActive(true);
        }
        
        textPrizeSW.text = prizeSW[CurSWGrade].ToString();
        textPrizeHat.text = prizeHat[CurHatGrade].ToString();
        textPrizeArmor.text = prizeArmor[CurArmorGrade].ToString();
    }    
    public void ButtonBuyUpgrade(int Type){
        if(Type == 0){
            if(playerGold >= prizeSW[CurSWGrade]){
                clickAduio.PlayAudio();
           //     _hyplayDataPlayer.GoldShop((prizeSW[CurSWGrade]),1, 0, 0);
            }
        }else if(Type == 1){
            if(playerGold >= prizeHat[CurHatGrade]){
                clickAduio.PlayAudio();
        //        _hyplayDataPlayer.GoldShop((prizeHat[CurHatGrade]),0, 1, 0);
            }
        }else if(Type == 2){
            if(playerGold >= prizeArmor[CurArmorGrade]){
                clickAduio.PlayAudio();
        //        _hyplayDataPlayer.GoldShop((prizeArmor[CurArmorGrade]),0, 0, 1);
            }
        }
    }
}
