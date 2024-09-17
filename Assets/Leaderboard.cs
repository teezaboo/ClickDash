using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Dan.Main;

public class Leaderboard : MonoBehaviour
{
    public GameObject serverUnavailable;
    public string nameTest;
    public int scoreTest;
    public List<TextMeshProUGUI> names;
    public List<TextMeshProUGUI> scores;
    private string publicKey = "56a714ded4ad9d6a972cac10c0d56bd7bf632d3326e9d0451f5c4c47cf30960b";
    private void Start()
    {
        if(names.Count > 0){
            for (int i = 0; i < names.Count; i++){
                names[i].text = "Loading...";
                scores[i].text = "Loading...";
            }
        }
        LoadEntries();
    }
    public void test(){
        UploadEntry(scoreTest, nameTest);
    }
    public void LoadEntries(){
        try{
            Leaderboards.ClickDashScore.GetEntries(entries =>
            {
                foreach (var t in scores)
                    t.text = "";
                var length = Mathf.Min(scores.Count, entries.Length);
                if(length == 0){
                    if(serverUnavailable != null){
                        serverUnavailable.SetActive(true);
                    }
                    return;
                }
                int index = 0;
                for (int i = 0; i < length; i++){
                    index++;
                    names[i].text = entries[i].Username;
                    scores[i].text = entries[i].Score.ToString();
                }
                for (int i = index; i < names.Count; i++){
                    names[i].text = "-";
                    scores[i].text = "-";
                }
            });
        }catch{
            if(serverUnavailable != null){
                serverUnavailable.SetActive(true);
            }
            Debug.Log("The server is unavailable.");
        }
    }
        public void UploadEntry(int score, string namePlayer)
        {
            Leaderboards.ClickDashScore.UploadNewEntry(namePlayer, score, isSuccessful =>
            {
                if (isSuccessful){
                    if(names.Count > 0){
                        LoadEntries();
                    }
                }
            });
        }
}