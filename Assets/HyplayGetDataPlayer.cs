using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class HyplayGetDataPlayer : MonoBehaviour
{/*
    public HyplayDataPlayer hyplayDataPlayer;
    public string username;
    public string IdUser;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private TextMeshProUGUI textName2;
    private void Start()
    {
        GetUser();
    }

    public async void GetUser()
    {
        textName.text = "Username...";
        var res = await HyplayBridge.GetUserAsync();
        if (res.Success){
            textName.text = "Username : " + res.Data.Username;
            textName2.text = res.Data.Username;
            username = res.Data.Username;
            IdUser = res.Data.Id;
            hyplayDataPlayer.InitializeRewards();
        }
        else
            textName.text = res.Error;
    }*/
}
