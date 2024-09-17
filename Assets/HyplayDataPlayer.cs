using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class HyplayDataPlayer : MonoBehaviour
{/*
    public Leaderboard _leaderboard;
    public GameObject objLoading;
    public GameObject objFail;
    public HyplayGetDataPlayer hyplayGetDataPlayer;
    public bool isGamePlay = false;
    [SerializeField] private TextMeshProUGUI goldText;
    public int goldPlayer = 0;
    [SerializeField] private TextMeshProUGUI levelText;
    public int playerLevel = 0;
   // [SerializeField] private HyplaySettings settings; 
    [SerializeField] private TextMeshProUGUI textTest;
    [SerializeField] private TextMeshProUGUI textMyScore;
    public ShopManager shopManager;
    public PlayerExpManager playerExpManager;
    public LoginManager LoginManager;
    public DailyQuest DailyQuest;

    private int loop01 = 0;
    private int loop02 = 0;
    private int loop03 = 0;
    private int loop04 = 0;
    private int loop05 = 0;
    private int loop06 = 0;
    private int loop07 = 0;
    private int loop08 = 0;
    private int loop09 = 0;
    private int loop10 = 0;

    void Start()
    {
    }

[Serializable]
public class Rewards
{
    public int Level;
    public float exp;
    public DateTime LastLoggedIn;
    public int HighScore;
    public int gold;
    public int CurSWGrade = 0;
    public int CurHatGrade = 0;
    public int CurArmorGrade = 0;
    public int loginDay = 0;
    public bool firstLogin = false;
    public int DailyQuestTimeSec = 0;
    public bool rewarded01 = false;
    public bool rewarded02 = false;
    public bool rewarded03 = false;
}

    public async void InitializeRewards()
    {
    try
    {

        while (!HyplayBridge.IsLoggedIn) await Task.Yield();
        goldText.text = "Loading...";
        levelText.text = "Loading...";
        Debug.Log(settings.Token);


        var rewardsRes6 = await HyplayBridge.GetState<Rewards>(hyplayGetDataPlayer.IdUser);
        if(!rewardsRes6.Success){
            Debug.LogError(rewardsRes6.Error);
            loop01++;
            if(loop01 < 100){
                InitializeRewards();
            }
            return;
            //objFail.SetActive(true);
        }
        if(rewardsRes6.Data.ProtectedState.firstLogin != true){
            var initialRewards = new Rewards
            {
                Level = 1,
                gold = 0,
                exp = 0,
                LastLoggedIn = DateTime.Now,
                HighScore = 0,
                CurArmorGrade = 0,
                CurHatGrade = 0,
                CurSWGrade = 0,
                loginDay = 0,
                firstLogin = true,
                DailyQuestTimeSec = 0,
                rewarded01 = false,
                rewarded02 = false,
                rewarded03 = false,
            };
            var data = HyplayAppState<Rewards>.WithProtectedState(hyplayGetDataPlayer.IdUser, true, initialRewards);

            var setRes = await HyplayBridge.SetState(data);

            if(setRes != ""){
                Debug.LogError(setRes);
                //objFail.SetActive(true);
                loop01++;
                if(loop01 < 100){
                    InitializeRewards();
                }
                return;
            }else{
                var rewardsRes5 = await HyplayBridge.GetState<Rewards>(hyplayGetDataPlayer.IdUser);
                if(!rewardsRes5.Success){
                    Debug.LogError(rewardsRes5.Error);
                    //objFail.SetActive(true);
                    loop01++;
                    if(loop01 < 100){
                        InitializeRewards();
                    }
                    return;
                }else{
                    objLoading.SetActive(false);
                    textMyScore.text = rewardsRes5.Data.ProtectedState.HighScore.ToString();
                    goldText.text = (rewardsRes5.Data.ProtectedState.gold).ToString();
                    shopManager.playerGold = rewardsRes5.Data.ProtectedState.gold;
                    playerLevel = rewardsRes5.Data.ProtectedState.Level;
                    playerExpManager.levelPlayer = rewardsRes5.Data.ProtectedState.Level;
                    playerExpManager.expPlayer = rewardsRes5.Data.ProtectedState.exp;
                    shopManager.CurSWGrade = rewardsRes5.Data.ProtectedState.CurSWGrade;
                    shopManager.CurHatGrade = rewardsRes5.Data.ProtectedState.CurHatGrade;
                    shopManager.CurArmorGrade = rewardsRes5.Data.ProtectedState.CurArmorGrade;
                    DailyQuest.DailyQuestTime = rewardsRes5.Data.ProtectedState.DailyQuestTimeSec;
                    playerExpManager.levelPlayer = rewardsRes5.Data.ProtectedState.Level;
                    DailyQuest.SetRewarded(
                        rewardsRes5.Data.ProtectedState.rewarded01,
                        rewardsRes5.Data.ProtectedState.rewarded02,
                        rewardsRes5.Data.ProtectedState.rewarded03
                    );
                    playerExpManager.isActive = true;
                    LoginManager.login = rewardsRes5.Data.ProtectedState.loginDay;
                    PlayerPrefs.SetInt("CurArmorGrade", rewardsRes5.Data.ProtectedState.CurArmorGrade);
                    PlayerPrefs.SetInt("CurHatGrade", rewardsRes5.Data.ProtectedState.CurHatGrade);
                    PlayerPrefs.SetInt("CurSWGrade", rewardsRes5.Data.ProtectedState.CurSWGrade);
                    PlayerPrefs.SetInt("addGold", 0);
                    PlayerPrefs.SetFloat("addExp", 0);
                    LoginManager.CheckLogin(0);
                    playerExpManager.SetFill();
                }
            }



        }else{
        var rewardsRes9 = await HyplayBridge.GetState<Rewards>(hyplayGetDataPlayer.IdUser);
        if(!rewardsRes9.Success){
            Debug.LogError(rewardsRes6.Error);
            loop01++;
            if(loop01 < 100){
                InitializeRewards();
            }
            return;
            //objFail.SetActive(true);
        }
        int addgold = 0;
        if(PlayerPrefs.HasKey("mygold")){
            addgold = PlayerPrefs.GetInt("mygold");
        PlayerPrefs.SetInt("mygold", 0);
        }
        int addexp = 0;
        if(PlayerPrefs.HasKey("exp2")){
            addexp = PlayerPrefs.GetInt("exp2");
            PlayerPrefs.SetInt("exp2", 0);
        }
        int myyscore = 0;
        if(PlayerPrefs.HasKey("ADDscore")){
            myyscore = PlayerPrefs.GetInt("ADDscore");
            PlayerPrefs.SetInt("ADDscore", 0);
        }
        if(rewardsRes9.Data.ProtectedState.HighScore > myyscore){
            myyscore = rewardsRes9.Data.ProtectedState.HighScore;
        }
        var data = HyplayAppState<Rewards>.WithProtectedState(hyplayGetDataPlayer.IdUser, true,
        new Rewards
        {
            Level = rewardsRes9.Data.ProtectedState.Level,
            gold = rewardsRes9.Data.ProtectedState.gold + addgold,
            exp = rewardsRes9.Data.ProtectedState.exp + addexp,
            LastLoggedIn = rewardsRes9.Data.ProtectedState.LastLoggedIn,
            HighScore = myyscore,
            CurArmorGrade = rewardsRes9.Data.ProtectedState.CurArmorGrade,
            CurHatGrade = rewardsRes9.Data.ProtectedState.CurHatGrade,
            CurSWGrade = rewardsRes9.Data.ProtectedState.CurSWGrade,
            loginDay = rewardsRes9.Data.ProtectedState.loginDay,
            firstLogin = rewardsRes9.Data.ProtectedState.firstLogin,
            DailyQuestTimeSec = rewardsRes9.Data.ProtectedState.DailyQuestTimeSec,
            rewarded01 = rewardsRes9.Data.ProtectedState.rewarded01,
            rewarded02 = rewardsRes9.Data.ProtectedState.rewarded02,
            rewarded03 = rewardsRes9.Data.ProtectedState.rewarded03,
        });

        var setRes = await HyplayBridge.SetState(data);
            if(setRes != ""){
                Debug.LogError(setRes);
                //objFail.SetActive(true);
                loop01++;
                if(loop01 < 100){
                    InitializeRewards();
                }
                return;
            }else{
                var rewardsRes5 = await HyplayBridge.GetState<Rewards>(hyplayGetDataPlayer.IdUser);
                if(!rewardsRes5.Success){
                    Debug.LogError(rewardsRes5.Error);
                    //objFail.SetActive(true);
                    loop01++;
                    if(loop01 < 100){
                        InitializeRewards();
                    }
                    return;
                }else{
                    objLoading.SetActive(false);
                    loop01 = 0;
                    textMyScore.text = rewardsRes5.Data.ProtectedState.HighScore.ToString();
                    goldText.text = (rewardsRes5.Data.ProtectedState.gold).ToString();
                    shopManager.playerGold = rewardsRes5.Data.ProtectedState.gold;
                    playerLevel = rewardsRes5.Data.ProtectedState.Level;
                    playerExpManager.levelPlayer = rewardsRes5.Data.ProtectedState.Level;
                    playerExpManager.expPlayer = rewardsRes5.Data.ProtectedState.exp;
                    playerExpManager.isActive = true;
                    shopManager.CurSWGrade = rewardsRes5.Data.ProtectedState.CurSWGrade;
                    shopManager.CurHatGrade = rewardsRes5.Data.ProtectedState.CurHatGrade;
                    shopManager.CurArmorGrade = rewardsRes5.Data.ProtectedState.CurArmorGrade;
                    playerExpManager.levelPlayer = rewardsRes5.Data.ProtectedState.Level;
                    LoginManager.login = rewardsRes5.Data.ProtectedState.loginDay;
                    DailyQuest.DailyQuestTime = rewardsRes5.Data.ProtectedState.DailyQuestTimeSec;
                    DailyQuest.SetRewarded(
                        rewardsRes5.Data.ProtectedState.rewarded01,
                        rewardsRes5.Data.ProtectedState.rewarded02,
                        rewardsRes5.Data.ProtectedState.rewarded03
                    );
                    
                    PlayerPrefs.SetInt("CurArmorGrade", rewardsRes5.Data.ProtectedState.CurArmorGrade);
                    PlayerPrefs.SetInt("CurHatGrade", rewardsRes5.Data.ProtectedState.CurHatGrade);
                    PlayerPrefs.SetInt("CurSWGrade", rewardsRes5.Data.ProtectedState.CurSWGrade);
                    PlayerPrefs.SetInt("addGold", 0);
                    PlayerPrefs.SetFloat("addExp", 0);
                    playerExpManager.SetFill();
                    if (HasPassedOneDay(rewardsRes5.Data.ProtectedState.LastLoggedIn))
                    {
                        if(rewardsRes5.Data.ProtectedState.loginDay < 7)
                            LoginManager.CheckLogin(rewardsRes5.Data.ProtectedState.loginDay);
                        var rewardsRes = await HyplayBridge.GetState<Rewards>(hyplayGetDataPlayer.IdUser);
                        var data4 = HyplayAppState<Rewards>.WithProtectedState(hyplayGetDataPlayer.IdUser, true,
                        new Rewards
                        {
                            Level = rewardsRes.Data.ProtectedState.Level,
                            gold = rewardsRes.Data.ProtectedState.gold,
                            exp = rewardsRes.Data.ProtectedState.exp,
                            LastLoggedIn = DateTime.Now,
                            HighScore = rewardsRes.Data.ProtectedState.HighScore,
                            CurArmorGrade = rewardsRes.Data.ProtectedState.CurArmorGrade,
                            CurHatGrade = rewardsRes.Data.ProtectedState.CurHatGrade,
                            CurSWGrade = rewardsRes.Data.ProtectedState.CurSWGrade,
                            loginDay = rewardsRes.Data.ProtectedState.loginDay,
                            firstLogin = rewardsRes.Data.ProtectedState.firstLogin,
                            DailyQuestTimeSec = 0,
                            rewarded01 = false,
                            rewarded02 = false,
                            rewarded03 = false,
                        });
                        var setRes4 = await HyplayBridge.SetState(data4);
                        if(setRes4 != ""){
                            Debug.LogError(setRes);
                            //objFail.SetActive(true);
                            loop01++;
                            if(loop01 < 100){
                                InitializeRewards();
                            }
                            return;
                        }
                //      var rewardsRes5 = await HyplayBridge.GetState<Rewards>(hyplayGetDataPlayer.IdUser);
                    }
                    _leaderboard.UploadEntry(rewardsRes5.Data.ProtectedState.HighScore, hyplayGetDataPlayer.username);
                    LoginManager.CheckLoginReward2();
                }
            }






        }
    }
    catch (Exception ex)
    {
        Debug.LogError($"Error in IncrementShop: {ex.Message}");
    }
    }
    public void DebugReset(){
        DebugResetData();
    }
    public void DebugDay(){
        DebugDayy();
    }
    
    public async void DebugDayy(){
        try
        {
            var rewardsRes5 = await HyplayBridge.GetState<Rewards>(hyplayGetDataPlayer.IdUser);
            rewardsRes5.Data.ProtectedState.LastLoggedIn = DateTime.Now.AddDays(-1);
            if (HasPassedOneDay(rewardsRes5.Data.ProtectedState.LastLoggedIn))
            {
                if(rewardsRes5.Data.ProtectedState.loginDay < 7)
                    LoginManager.CheckLogin(rewardsRes5.Data.ProtectedState.loginDay);
                var rewardsRes = await HyplayBridge.GetState<Rewards>(hyplayGetDataPlayer.IdUser);
                var data = HyplayAppState<Rewards>.WithProtectedState(hyplayGetDataPlayer.IdUser, true,
                new Rewards
                {
                    Level = rewardsRes.Data.ProtectedState.Level,
                    gold = rewardsRes.Data.ProtectedState.gold,
                    exp = rewardsRes.Data.ProtectedState.exp,
                    LastLoggedIn = DateTime.Now,
                    HighScore = rewardsRes.Data.ProtectedState.HighScore,
                    CurArmorGrade = rewardsRes.Data.ProtectedState.CurArmorGrade,
                    CurHatGrade = rewardsRes.Data.ProtectedState.CurHatGrade,
                    CurSWGrade = rewardsRes.Data.ProtectedState.CurSWGrade,
                    loginDay = rewardsRes.Data.ProtectedState.loginDay + 1,
                    firstLogin = rewardsRes.Data.ProtectedState.firstLogin,
                    DailyQuestTimeSec = rewardsRes.Data.ProtectedState.DailyQuestTimeSec,
                    rewarded01 = DailyQuest.rewarded01,
                    rewarded02 = DailyQuest.rewarded02,
                    rewarded03 = DailyQuest.rewarded03,
                });
                var setRes = await HyplayBridge.SetState(data);
            }
        }
            catch (Exception ex)
            {
                Debug.LogError($"Error in IncrementShop: {ex.Message}");
            }
    }
    public async void DebugResetData()
    {
    try
    {
            var initialRewards = new Rewards
            {
                Level = 1,
                gold = 0,
                exp = 0,
                LastLoggedIn = DateTime.Now,
                HighScore = 0,
                CurArmorGrade = 0,
                CurHatGrade = 0,
                CurSWGrade = 0,
                loginDay = 0,
                firstLogin = false,
                DailyQuestTimeSec = 0,
                rewarded01 = false,
                rewarded02 = false,
                rewarded03 = false,
            };

            var data = HyplayAppState<Rewards>.WithProtectedState(hyplayGetDataPlayer.IdUser, true, initialRewards);

            var setRes = await HyplayBridge.SetState(data);
            if(setRes != ""){
                Debug.LogError(setRes);
                //objFail.SetActive(true);
            }else{
                var rewardsRes5 = await HyplayBridge.GetState<Rewards>(hyplayGetDataPlayer.IdUser);
                if(!rewardsRes5.Success){
                    Debug.LogError(rewardsRes5.Error);
                    //objFail.SetActive(true);
                }else{
                    textMyScore.text = rewardsRes5.Data.ProtectedState.HighScore.ToString();
                        goldText.text = (rewardsRes5.Data.ProtectedState.gold).ToString();
                        shopManager.playerGold = rewardsRes5.Data.ProtectedState.gold;
                    playerLevel = rewardsRes5.Data.ProtectedState.Level;
                    playerExpManager.levelPlayer = rewardsRes5.Data.ProtectedState.Level;

                    playerExpManager.expPlayer = rewardsRes5.Data.ProtectedState.exp;

                    shopManager.CurSWGrade = rewardsRes5.Data.ProtectedState.CurSWGrade;
                    shopManager.CurHatGrade = rewardsRes5.Data.ProtectedState.CurHatGrade;
                    shopManager.CurArmorGrade = rewardsRes5.Data.ProtectedState.CurArmorGrade;
                    playerExpManager.levelPlayer = rewardsRes5.Data.ProtectedState.Level;
                    LoginManager.login = rewardsRes5.Data.ProtectedState.loginDay;
                    PlayerPrefs.SetInt("addGold", 0);
                    PlayerPrefs.SetFloat("addExp", 0);
                    playerExpManager.SetFill();
                }
            }
    }
    catch (Exception ex)
    {
        Debug.LogError($"Error in IncrementShop: {ex.Message}");
    }
    }
public async void IncrementShop(int curSW, int curHat, int curArmor)
{
    try
    {
        var rewardsRes = await HyplayBridge.GetState<Rewards>(hyplayGetDataPlayer.IdUser);
        var data = HyplayAppState<Rewards>.WithProtectedState(hyplayGetDataPlayer.IdUser, true,
        new Rewards
        {
            Level = rewardsRes.Data.ProtectedState.Level,
            gold = shopManager.playerGold,
            exp = rewardsRes.Data.ProtectedState.exp,
            LastLoggedIn = DateTime.Now,
            HighScore = rewardsRes.Data.ProtectedState.HighScore,
            CurArmorGrade = shopManager.CurArmorGrade,
            CurHatGrade = shopManager.CurHatGrade,
            CurSWGrade = shopManager.CurSWGrade,
            loginDay = rewardsRes.Data.ProtectedState.loginDay,
            firstLogin = rewardsRes.Data.ProtectedState.firstLogin,
            DailyQuestTimeSec = rewardsRes.Data.ProtectedState.DailyQuestTimeSec,
                    rewarded01 = DailyQuest.rewarded01,
                    rewarded02 = DailyQuest.rewarded02,
                    rewarded03 = DailyQuest.rewarded03,
        });

        var setRes = await HyplayBridge.SetState(data);
            if(setRes != ""){
                Debug.LogError(setRes);
                //objFail.SetActive(true);
            }else{
                PlayerPrefs.SetInt("CurSWGrade", rewardsRes.Data.ProtectedState.CurSWGrade + curSW);
                PlayerPrefs.SetInt("CurHatGrade", rewardsRes.Data.ProtectedState.CurHatGrade + curHat);
                PlayerPrefs.SetInt("CurArmorGrade", rewardsRes.Data.ProtectedState.CurArmorGrade + curArmor);
                shopManager.CurSWGrade = rewardsRes.Data.ProtectedState.CurSWGrade + curSW;
                shopManager.CurHatGrade = rewardsRes.Data.ProtectedState.CurHatGrade + curHat;
                shopManager.CurArmorGrade = rewardsRes.Data.ProtectedState.CurArmorGrade + curArmor;
            }
        
            
    }
    catch (Exception ex)
    {
        Debug.LogError($"Error in IncrementShop: {ex.Message}");
    }
}
    public async void SetLoginDay(int indexLogin, int mygold)
    {
        try
        {
            var rewardsRes = await HyplayBridge.GetState<Rewards>(hyplayGetDataPlayer.IdUser);
            var data = HyplayAppState<Rewards>.WithProtectedState(hyplayGetDataPlayer.IdUser, true,
            new Rewards
            {
                Level = rewardsRes.Data.ProtectedState.Level,
                gold = rewardsRes.Data.ProtectedState.gold + mygold,
                exp = rewardsRes.Data.ProtectedState.exp,
                LastLoggedIn = DateTime.Now,
                HighScore = rewardsRes.Data.ProtectedState.HighScore,
                CurArmorGrade = rewardsRes.Data.ProtectedState.CurArmorGrade,
                CurHatGrade = rewardsRes.Data.ProtectedState.CurHatGrade,
                CurSWGrade = rewardsRes.Data.ProtectedState.CurSWGrade,
                loginDay = indexLogin + 1,
                firstLogin = rewardsRes.Data.ProtectedState.firstLogin,
                DailyQuestTimeSec = rewardsRes.Data.ProtectedState.DailyQuestTimeSec,
                    rewarded01 = DailyQuest.rewarded01,
                    rewarded02 = DailyQuest.rewarded02,
                    rewarded03 = DailyQuest.rewarded03,
            });

            var setRes = await HyplayBridge.SetState(data);
            if(setRes != ""){
                Debug.LogError(setRes);
                //objFail.SetActive(true);
            }else{
                var rewardsRes5 = await HyplayBridge.GetState<Rewards>(hyplayGetDataPlayer.IdUser);
                if(!rewardsRes5.Success){
                    Debug.LogError(rewardsRes5.Error);
                    //objFail.SetActive(true);
                }else{
                    playerExpManager.SetFill();
                    goldText.text = rewardsRes5.Data.ProtectedState.gold.ToString();
                    LoginManager.openColider();
                    LoginManager.CheckLoginReward2();
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error in IncrementShop: {ex.Message}");
        }
    }

    public void DebugGold(int gold = 0){
        AddGoldDay(gold);
    }
    public async void AddGoldDay(int mygold)
    {
        try
        {
            var rewardsRes = await HyplayBridge.GetState<Rewards>(hyplayGetDataPlayer.IdUser);

            var data = HyplayAppState<Rewards>.WithProtectedState(hyplayGetDataPlayer.IdUser, true,
            new Rewards
            {
                Level = rewardsRes.Data.ProtectedState.Level,
                gold = shopManager.playerGold,
                exp = rewardsRes.Data.ProtectedState.exp,
                LastLoggedIn = DateTime.Now,
                HighScore = rewardsRes.Data.ProtectedState.HighScore,
                CurSWGrade =  rewardsRes.Data.ProtectedState.CurSWGrade,
                CurArmorGrade = rewardsRes.Data.ProtectedState.CurArmorGrade,
                CurHatGrade = rewardsRes.Data.ProtectedState.CurHatGrade,
                loginDay = rewardsRes.Data.ProtectedState.loginDay,
                firstLogin = rewardsRes.Data.ProtectedState.firstLogin,
                DailyQuestTimeSec = rewardsRes.Data.ProtectedState.DailyQuestTimeSec,
                    rewarded01 = DailyQuest.rewarded01,
                    rewarded02 = DailyQuest.rewarded02,
                    rewarded03 = DailyQuest.rewarded03,
            });
            var setRes = await HyplayBridge.SetState(data);
            if(setRes != ""){
                 AddGoldDay(mygold);
                Debug.LogError(setRes);
                //objFail.SetActive(true);
            }else{
                var rewardsRes5 = await HyplayBridge.GetState<Rewards>(hyplayGetDataPlayer.IdUser);
                if(!rewardsRes5.Success){
                    AddGoldDay(mygold);
                    Debug.LogError(rewardsRes5.Error);
                    //objFail.SetActive(true);
                }else{
            shopManager.playerGold += mygold;
            goldText.text = shopManager.playerGold.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error in IncrementShop: {ex.Message}");
        }
    }    
    public async void GoldShop(int mygold, int curSW, int curHat, int curArmor)
    {
        try
        {
            shopManager.playerGold -= mygold;
            shopManager.CurSWGrade += curSW;
            shopManager.CurHatGrade += curHat;
            shopManager.CurArmorGrade += curArmor;
            shopManager.textPlayerMoney.text = shopManager.playerGold.ToString();
            shopManager.SetShopUpgrade();
            var rewardsRes = await HyplayBridge.GetState<Rewards>(hyplayGetDataPlayer.IdUser);

            var data = HyplayAppState<Rewards>.WithProtectedState(hyplayGetDataPlayer.IdUser, true,
            new Rewards
            {
                Level = rewardsRes.Data.ProtectedState.Level,
                gold = shopManager.playerGold,
                exp = rewardsRes.Data.ProtectedState.exp,
                LastLoggedIn = DateTime.Now,
                HighScore = rewardsRes.Data.ProtectedState.HighScore,
                CurSWGrade = shopManager.CurSWGrade,
                CurArmorGrade = shopManager.CurArmorGrade,
                CurHatGrade = shopManager.CurHatGrade,
                loginDay = rewardsRes.Data.ProtectedState.loginDay,
                firstLogin = rewardsRes.Data.ProtectedState.firstLogin,
                DailyQuestTimeSec = rewardsRes.Data.ProtectedState.DailyQuestTimeSec,
                    rewarded01 = DailyQuest.rewarded01,
                    rewarded02 = DailyQuest.rewarded02,
                    rewarded03 = DailyQuest.rewarded03,
            });
            var setRes = await HyplayBridge.SetState(data);
            if(setRes != ""){            
                shopManager.playerGold += mygold;
                shopManager.CurSWGrade -= curSW;
                shopManager.CurHatGrade -= curHat;
                shopManager.CurArmorGrade -= curArmor;
                shopManager.textPlayerMoney.text = shopManager.playerGold.ToString();
                Debug.LogError(setRes);
                GoldShop(mygold, curSW, curHat, curArmor);
                //objFail.SetActive(true);
            }else{
                var rewardsRes5 = await HyplayBridge.GetState<Rewards>(hyplayGetDataPlayer.IdUser);
                if(!rewardsRes5.Success){
                    shopManager.playerGold += mygold;
                    shopManager.CurSWGrade -= curSW;
                    shopManager.CurHatGrade -= curHat;
                    shopManager.CurArmorGrade -= curArmor;
                    Debug.LogError(rewardsRes5.Error);
                    GoldShop(mygold, curSW, curHat, curArmor);
                    //objFail.SetActive(true);
                }else{
                    shopManager.textPlayerMoney.text = shopManager.playerGold.ToString();
                    shopManager.playerGold = rewardsRes5.Data.ProtectedState.gold;
                    PlayerPrefs.SetInt("CurSWGrade",  PlayerPrefs.GetInt("CurSWGrade") + curSW);
                    PlayerPrefs.SetInt("CurHatGrade",  PlayerPrefs.GetInt("CurHatGrade") + curHat);
                    PlayerPrefs.SetInt("CurArmorGrade",  PlayerPrefs.GetInt("CurArmorGrade") + curArmor);
                    shopManager.SetShopUpgrade();
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error in IncrementShop: {ex.Message}");
        }
    }
    public async void IncrementGold(int mygold = 0, int levelup = 0, float exp2 = 0)
    {
        try
        {
            var rewardsRes = await HyplayBridge.GetState<Rewards>(hyplayGetDataPlayer.IdUser);

            if(exp2 == 0){
                var data = HyplayAppState<Rewards>.WithProtectedState(hyplayGetDataPlayer.IdUser, true,
                new Rewards
                {
                    Level = rewardsRes.Data.ProtectedState.Level + levelup,
                    gold = rewardsRes.Data.ProtectedState.gold + mygold,
                    exp = rewardsRes.Data.ProtectedState.exp,
                    LastLoggedIn = DateTime.Now,
                    HighScore = rewardsRes.Data.ProtectedState.HighScore,
                    CurSWGrade = rewardsRes.Data.ProtectedState.CurSWGrade,
                    CurArmorGrade = rewardsRes.Data.ProtectedState.CurArmorGrade,
                    CurHatGrade = rewardsRes.Data.ProtectedState.CurHatGrade,
                    loginDay = rewardsRes.Data.ProtectedState.loginDay,
                    firstLogin = rewardsRes.Data.ProtectedState.firstLogin,
                    DailyQuestTimeSec = rewardsRes.Data.ProtectedState.DailyQuestTimeSec,
                    rewarded01 = DailyQuest.rewarded01,
                    rewarded02 = DailyQuest.rewarded02,
                    rewarded03 = DailyQuest.rewarded03,
                });
                var setRes = await HyplayBridge.SetState(data);
                if(setRes != ""){
                    IncrementGold(mygold, levelup, exp2);
                    Debug.LogError(setRes);
                    //objFail.SetActive(true);
                    return;
                }else{
                    goldText.text = (rewardsRes.Data.ProtectedState.gold + mygold).ToString();
                    shopManager.playerGold = rewardsRes.Data.ProtectedState.gold + mygold;
                }
            }else{
                var data = HyplayAppState<Rewards>.WithProtectedState(hyplayGetDataPlayer.IdUser, true,
                new Rewards
                {
                    Level = rewardsRes.Data.ProtectedState.Level + levelup,
                    gold = rewardsRes.Data.ProtectedState.gold + mygold,
                    exp = exp2,
                    LastLoggedIn = DateTime.Now,
                    HighScore = rewardsRes.Data.ProtectedState.HighScore,
                    CurSWGrade = rewardsRes.Data.ProtectedState.CurSWGrade,
                    CurArmorGrade = rewardsRes.Data.ProtectedState.CurArmorGrade,
                    CurHatGrade = rewardsRes.Data.ProtectedState.CurHatGrade,
                    loginDay = rewardsRes.Data.ProtectedState.loginDay,
                    firstLogin = rewardsRes.Data.ProtectedState.firstLogin,
                    DailyQuestTimeSec = rewardsRes.Data.ProtectedState.DailyQuestTimeSec,
                    rewarded01 = rewardsRes.Data.ProtectedState.rewarded01,
                    rewarded02 = rewardsRes.Data.ProtectedState.rewarded02,
                    rewarded03 = rewardsRes.Data.ProtectedState.rewarded03,
                });
                var setRes = await HyplayBridge.SetState(data);
                if(setRes != ""){
                    IncrementGold(mygold, levelup, exp2);
                    Debug.LogError(setRes);
                    //objFail.SetActive(true);
                    return;
                }else{
                    goldText.text = (rewardsRes.Data.ProtectedState.gold + mygold).ToString();
                    shopManager.playerGold = rewardsRes.Data.ProtectedState.gold + mygold;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error in IncrementShop: {ex.Message}");
        }
    }
    public async void IncrementGol2(string username, int mygold, int exp2, int score)
    {
        
        PlayerPrefs.SetInt("mygold", mygold);
        PlayerPrefs.SetInt("exp2", exp2);
        PlayerPrefs.SetInt("ADDscore", score);
    }
    public async void SetTimeQ(int Sec, string username)
    {
        try
        {
            var rewardsRes = await HyplayBridge.GetState<Rewards>(username);
                var data = HyplayAppState<Rewards>.WithProtectedState(username, true,
                new Rewards
                {
                    Level = rewardsRes.Data.ProtectedState.Level,
                    gold = rewardsRes.Data.ProtectedState.gold,
                    exp = rewardsRes.Data.ProtectedState.exp,
                    LastLoggedIn = DateTime.Now,
                    HighScore = rewardsRes.Data.ProtectedState.HighScore,
                    CurArmorGrade = rewardsRes.Data.ProtectedState.CurArmorGrade,
                    CurHatGrade = rewardsRes.Data.ProtectedState.CurHatGrade,
                    CurSWGrade = rewardsRes.Data.ProtectedState.CurSWGrade,
                    loginDay = rewardsRes.Data.ProtectedState.loginDay,
                    firstLogin = rewardsRes.Data.ProtectedState.firstLogin,
                    DailyQuestTimeSec = rewardsRes.Data.ProtectedState.DailyQuestTimeSec + Sec,
                    rewarded01 = DailyQuest.rewarded01,
                    rewarded02 = DailyQuest.rewarded02,
                    rewarded03 = DailyQuest.rewarded03,
                });
                var setRes = await HyplayBridge.SetState(data);
                if(setRes != ""){
                    SetTimeQ(Sec, username);
                    Debug.LogError(setRes);
                    //objFail.SetActive(true);
                }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error in IncrementShop: {ex.Message}");
        }
    }
    public async void SetTimeReward(int mygold, bool reward01, bool reward02, bool reward03)
    {
        try
        {
            shopManager.playerGold += mygold;
            shopManager.textPlayerMoney.text = shopManager.playerGold.ToString();
            var rewardsRes = await HyplayBridge.GetState<Rewards>(hyplayGetDataPlayer.IdUser);
                var data = HyplayAppState<Rewards>.WithProtectedState(hyplayGetDataPlayer.IdUser, true,
                new Rewards
                {
                    Level = rewardsRes.Data.ProtectedState.Level,
                    gold = shopManager.playerGold,
                    exp = rewardsRes.Data.ProtectedState.exp,
                    LastLoggedIn = DateTime.Now,
                    HighScore = rewardsRes.Data.ProtectedState.HighScore,
                    CurArmorGrade = rewardsRes.Data.ProtectedState.CurArmorGrade,
                    CurHatGrade = rewardsRes.Data.ProtectedState.CurHatGrade,
                    CurSWGrade = rewardsRes.Data.ProtectedState.CurSWGrade,
                    loginDay = rewardsRes.Data.ProtectedState.loginDay,
                    firstLogin = rewardsRes.Data.ProtectedState.firstLogin,
                    DailyQuestTimeSec = rewardsRes.Data.ProtectedState.DailyQuestTimeSec,
                    rewarded01 = reward01,
                    rewarded02 = reward02,
                    rewarded03 = reward03,
                });
                var setRes = await HyplayBridge.SetState(data);
                if(setRes != ""){
                    shopManager.playerGold -= mygold;
                    Debug.LogError(setRes);
                    //objFail.SetActive(true);
                    SetTimeReward(mygold, reward01, reward02, reward03);
                }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error in IncrementShop: {ex.Message}");
        }
    }   
    bool HasPassedOneDay(DateTime lastLoggedIn)
    {
        // หากไม่มีข้อมูลล็อกอินก่อนหน้านี้ หรือ ไม่ใช่วันเดียวกัน
        return lastLoggedIn.Date != DateTime.Now.Date;
    }*/
}   