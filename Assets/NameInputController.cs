using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using TMPro;
public class NameInputController : MonoBehaviour
{
    public TextMeshProUGUI name;
    public Animator _AniName;
    public Animator _AniName2;
    public Animator _AniName3;
    public GameObject _EnterName1;
    public GameObject _EnterName2;
    public GameObject _EnterName3;
    public InputField nameInputField;
    public GameObject errorText;
    
    public GameObject conButton;

    void Start()
    {
        // เช็คทุกครั้งที่ผู้ใช้กรอกข้อมูลลงใน InputField
        nameInputField.onValueChanged.AddListener(CheckNameLength);
    }

    void CheckNameLength(string input)
    {
        // ถ้าชื่อยาวเกิน 10 ตัวอักษร
        if (input.Length > 10)
        {
            // ตัดให้เหลือ 10 ตัวอักษร
            nameInputField.text = input.Substring(0, 10);
            errorText.SetActive(true);
        }
        
        if (input.Length < 3)
        {
            conButton.SetActive(false);
        }else{
            conButton.SetActive(true);
        }
    }

    public void SubmitName(){
        StartCoroutine(Close());
    }   
    public void reData(){
        PlayerPrefs.DeleteKey("ScorePlayer");
        PlayerPrefs.DeleteKey("NamePlayer");
    }   
    IEnumerator Close()
    {
        name.text = nameInputField.text;
        PlayerPrefs.SetInt("ScorePlayer", 0);
        PlayerPrefs.SetString("NamePlayer", nameInputField.text);
        yield return new WaitForSeconds(1f);
        _AniName.Play("panelClose");
        _AniName2.Play("closeLeveLUP");
        _AniName3.Play("exitAbi");
        yield return new WaitForSeconds(1.5f);
        _EnterName1.SetActive(false);
        _EnterName2.SetActive(false);
        _EnterName3.SetActive(false);
    }
}
