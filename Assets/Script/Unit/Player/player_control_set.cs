using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_control_set : MonoBehaviour
{
    [SerializeField] private int _hat_Wearing = 0;
    [SerializeField] private int _bodySet_Wearing = 0;
    [SerializeField] private int _sword_Wearing = 0;
    [SerializeField] private int _veil_Wearing = 0;
    [SerializeField] private Transform _hat;
    [SerializeField] private Transform _head;
    [SerializeField] private Transform _body;
    [SerializeField] private Transform _sword;
    [SerializeField] private Transform _hand;
    [SerializeField] private Transform _veil;
    [SerializeField] private Transform _leg;
    [SerializeField] private Animator animator;
    
    public int Get_Hat_Wearing(){
        return _hat_Wearing;
    }
    public int Get_BodySet_Wearing(){
        return _bodySet_Wearing;
    }
    public int Get_Sword_Wearing(){
        return _sword_Wearing;
    }
    public int Get_Veil_Wearing(){
        return _veil_Wearing;
    }

    void Start()
    {
        ClearPlayerPrefs();
        if(PlayerPrefs.HasKey("CurArmorGrade")){
            _bodySet_Wearing +=  (PlayerPrefs.GetInt("CurArmorGrade"));
        }
        if(PlayerPrefs.HasKey("CurHatGrade")){
            _hat_Wearing += (PlayerPrefs.GetInt("CurHatGrade"));
        }
        if(PlayerPrefs.HasKey("CurSWGrade")){
            _sword_Wearing += (PlayerPrefs.GetInt("CurSWGrade"));
        }
        for(int i = 0; i < _hat.childCount; i++)
        {
            if (i == _hat_Wearing + 2)
            {
                _hat.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                _hat.GetChild(i).gameObject.SetActive(false);
            }
        }
        for(int i = 0; i < _body.childCount; i++)
        {
            if (i == _bodySet_Wearing)
            {
                _body.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                _body.GetChild(i).gameObject.SetActive(false);
            }
        }
        for(int i = 0; i < _sword.childCount; i++)
        {
            if (i == _sword_Wearing)
            {
                _sword.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                _sword.GetChild(i).gameObject.SetActive(false);
            }
        }
        for(int i = 0; i < _veil.childCount; i++)
        {
            if (i == _veil_Wearing)
            {
                _veil.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                _veil.GetChild(i).gameObject.SetActive(false);
            }
        }
        for(int i = 0; i < _hand.childCount; i++)
        {
            if (i == 0)
            {
                _hand.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                _hand.GetChild(i).gameObject.SetActive(false);
            }
        }
        for(int i = 0; i < _leg.childCount; i++)
        {
            if (i == _bodySet_Wearing)
            {
                _leg.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                _leg.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteKey("CurArmorGrade");
        PlayerPrefs.DeleteKey("CurHatGrade");
        PlayerPrefs.DeleteKey("CurSWGrade");
        PlayerPrefs.Save();
    }
    public void PlayAnimationPlayer(string animationName)
    {
        if(animationName == "attack"){
            animator.Play("attack", -1, 0f);
            animator.Play("attack");
            _body.GetChild(_bodySet_Wearing).GetComponent<Animator>().Play("attack", -1, 0f);
            _body.GetChild(_bodySet_Wearing).GetComponent<Animator>().Play("attack");
            _body.GetChild(_bodySet_Wearing).GetComponent<Animator>().SetFloat("speed_ani", 1f + ((GetComponent<PlayerController>().UpgradeStats.UpgradeDashCooldown/100f) * 1f));
            _sword.GetChild(_sword_Wearing).GetComponent<Animator>().Play("attack", -1, 0f);
            _sword.GetChild(_sword_Wearing).GetComponent<Animator>().Play("attack");
            _sword.GetChild(_sword_Wearing).GetComponent<Animator>().SetFloat("speed_ani", 1f + ((GetComponent<PlayerController>().UpgradeStats.UpgradeDashCooldown/100f) * 1f));
            _hand.GetChild(0).GetComponent<Animator>().Play("attack", -1, 0f);
            _hand.GetChild(0).GetComponent<Animator>().Play("attack");
            _hand.GetChild(0).GetComponent<Animator>().SetFloat("speed_ani", 1f + ((GetComponent<PlayerController>().UpgradeStats.UpgradeDashCooldown/100f) * 1f));
        }
    }
}
