using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class HyplayController : MonoBehaviour
{
    /*
    [SerializeField] private ScenceManagers _scenceManagers;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject _buttonLogin;
    [SerializeField] private bool isStartActive = false;
    void Start()
    {
        if(isStartActive == false) return;
        text.text = "Check Login...";
        if (HyplayBridge.IsLoggedIn){
            text.text = "Logged in!";
            StartCoroutine(GoMainMenu());
        }else{
            text.text = "Login";
            _buttonLogin.SetActive(true);
        }
    }
    public async void Login()
    {
        text.text = "Login";
        await HyplayBridge.LoginAsync();
        text.text = "Login";
        StartCoroutine(GoMainMenu());
    }
        public async void GetUser()
        {
            text.text = "Getting user...";
            var res = await HyplayBridge.GetUserAsync();
            if (res.Success)
                text.text = $"Successfully got user {res.Data.Username}";
            else
                text.text = $"Failed to get user: {res.Error}";
        }
    public async void DeleteSession()
    {
        await HyplayBridge.LogoutAsync();
        SceneManager.LoadScene("Loadding");
    }
    IEnumerator GoMainMenu() {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainMenu");
    }*/
}