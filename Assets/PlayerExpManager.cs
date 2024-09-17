using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerExpManager : MonoBehaviour
{
    public int levelPlayer;
    public float expPlayer;
    public GameObject LevelUPObj;
    public TextMeshProUGUI textMyLeveL;
    public TextMeshProUGUI textLeveL;
    public TextMeshProUGUI textGold;
    public bool blockCheckExp = false;
    public HyplayDataPlayer hyplayDataPlayer;
    public ShopManager ShopManager;
    public Image filledImage;
    public bool isActive =false;
    void Start()
    {
    }
    public void SetFill(){
        textLeveL.text = "Level " + levelPlayer.ToString();
        if(expPlayer >= (levelPlayer) * 40)
        {
            filledImage.fillAmount = 1;
            return;
        }else
            filledImage.fillAmount = expPlayer / ((levelPlayer) * 40);
    }

    // Update is called once per frame
    void Update()
    {
        if(blockCheckExp == true || isActive == false)
        {
            return;
        }
        if(expPlayer >= (levelPlayer) * 40)
        {
         //   expPlayer = expPlayer - ((levelPlayer) * 40);
            textMyLeveL.text = (levelPlayer).ToString() + " => " + (levelPlayer + 1).ToString();
            LevelUPObj.SetActive(true);
            levelPlayer++;
            textGold.text = ((levelPlayer) * 50).ToString();
            ShopManager.playerGold += (levelPlayer) * 50;
            AddGold((levelPlayer+1) * 50);
            blockCheckExp = true;
            SetFill();
        }else
        {
            SetFill();
        }
    }
    public void CoroutineLevelUP()
    {
        blockCheckExp = false;
    }
    public void AddGold(int gold)
    {
    //    hyplayDataPlayer.IncrementGold(gold, 1, expPlayer);
    }
}
