using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DailyQuest : MonoBehaviour
{/*
    public HyplayDataPlayer hyplayDataPlayer;
    public GameObject GetRewardObj;
    public TextMeshProUGUI textGold;
    public List<GameObject> ConrectObj;
    public List<Animator> animator;
    public Image filledImage;
    public int DailyQuestTime = 0;
    public int DailyQuestTimeMaxMin = 6;
    public bool rewarded01 = false;
    public bool rewarded02 = false;
    public bool rewarded03 = false;
    public void SetAni(){
        if(rewarded01) ConrectObj[0].SetActive(true);
        else ConrectObj[0].SetActive(false);
        if(rewarded02) ConrectObj[1].SetActive(true);
        else ConrectObj[1].SetActive(false);
        if(rewarded03) ConrectObj[2].SetActive(true);
        else ConrectObj[2].SetActive(false);
    }
    public void SetRewarded(bool rewarded01, bool rewarded02, bool rewarded03){
        this.rewarded01 = rewarded01;
        this.rewarded02 = rewarded02;
        this.rewarded03 = rewarded03;
    }
    public void ButtonGetReward(int index){
        if(index == 0){
            if(filledImage.fillAmount >= 0.333f && rewarded01 == false){
                GetRewardObj.SetActive(true);
                textGold.text = "100";
                rewarded01 = true;
                hyplayDataPlayer.SetTimeReward(100, rewarded01, rewarded02, rewarded03);
                SetAni();
            }
        }else if(index == 1){
            if(filledImage.fillAmount >= 0.666f && rewarded02 == false){
                GetRewardObj.SetActive(true);
                textGold.text = "200";
                rewarded02 = true;
                hyplayDataPlayer.SetTimeReward(200, rewarded01, rewarded02, rewarded03);
                SetAni();
            }
        }else if(index == 2){
            if(filledImage.fillAmount >= 0.999f && rewarded03 == false){
                GetRewardObj.SetActive(true);
                textGold.text = "500";
                rewarded03 = true;
                hyplayDataPlayer.SetTimeReward(500, rewarded01, rewarded02, rewarded03);
                SetAni();
            }
        }
    }
    public void Update()
    {
        SetAni();
        if(DailyQuestTime >= (DailyQuestTimeMaxMin*60))
        {
            if(rewarded01 == false){
                animator[0].Play("reward");
            }else{
                animator[0].Play("realidle");
            }
            if(rewarded02 == false){
                animator[1].Play("reward");
            }else{
                animator[1].Play("realidle");
            }
            if(rewarded03 == false){
                animator[2].Play("reward");
            }else{
                animator[2].Play("realidle");
            }
            filledImage.fillAmount = 1;
        }
        else
        {
            if(filledImage.fillAmount >= 0.666f){
                if(rewarded01 == false){
                    animator[0].Play("reward");
                }else{
                    animator[0].Play("realidle");
                }
                if(rewarded02 == false){
                    animator[1].Play("reward");
                }else{
                    animator[1].Play("realidle");
                }
            }else if(filledImage.fillAmount >= 0.333f){
                if(rewarded01 == false){
                    animator[0].Play("reward");
                }else{
                    animator[0].Play("realidle");
                }
            }
            filledImage.fillAmount = ((float)DailyQuestTime) / (((float)DailyQuestTimeMaxMin)*60);
        }
    }

    public void debug(){
        DailyQuestTime = 6;
    }*/
}
