using System.Collections;
using System.Collections.Generic; // เพิ่มบรรทัดนี้
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Jint; // ใช้ Jint เพื่อรัน JavaScript ใน Unity (ต้องติดตั้ง Jint)
using UnityEngine.Networking; // ต้องเพิ่มบรรทัดนี้
using System.Linq; // เพิ่มบรรทัดนี้เพื่อใช้ LINQ


public class LeaderboardManager : MonoBehaviour
{
    public TextMeshProUGUI leaderboardText; // Text หรือ TextMeshPro สำหรับแสดงลีดเดอร์บอร์ด

    private Engine jsEngine;

    string url = "https://api1.teezaboo.com/submit-score"; // URL ของเซิร์ฟเวอร์ Node.js
    private void Start()
    {
        // สร้าง instance ของ Jint (JavaScript Engine)
        jsEngine = new Engine();


        // ตัวอย่างข้อมูลไดนามิก (สมมติว่ามาจาก API หรือแหล่งอื่นๆ)
        List<PlayerData> leaderboardData = new List<PlayerData>
        {
            new PlayerData { Username = "Player1", Score = 1000 },
            new PlayerData { Username = "Player2", Score = 950 },
            new PlayerData { Username = "Player3", Score = 900 }
        };
        // รันฟังก์ชันเพื่อดึงข้อมูลลีดเดอร์บอร์ดเมื่อเริ่มเกม
        StartCoroutine(GetLeaderboard());
    }

    private IEnumerator GetLeaderboard()
    {

        UnityWebRequest www = UnityWebRequest.Get("https://api1.teezaboo.com/get-leaderboard");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error retrieving leaderboard: " + www.error);
        }
        else
        {
            string leaderboardData = www.downloadHandler.text;
            // แสดงข้อมูลลีดเดอร์บอร์ดใน UI
            leaderboardText.text = leaderboardData;
        }
        /*
        // เพิ่มฟังก์ชัน getLeaderboard ลงใน Engine ของ Jint
        string leaderboardScript = @"
            function getLeaderboard(data) {
                return data;
            }
        ";

        // รันโค้ด JavaScript ใน Jint
        jsEngine.Execute(leaderboardScript);

        // ส่งข้อมูลไดนามิกจาก C# ไปให้ฟังก์ชัน getLeaderboard ใน JavaScript
        jsEngine.SetValue("leaderboardData", leaderboardData);

        // รันฟังก์ชัน getLeaderboard ใน JavaScript พร้อมข้อมูลที่ส่งไป
        jsEngine.Execute("var leaderboard = getLeaderboard(leaderboardData);");

        // แปลงผลลัพธ์จาก JavaScript เป็น object ที่สามารถใช้ใน C#
        var leaderboard = jsEngine.GetValue("leaderboard").ToObject();

        // แสดงข้อมูลลีดเดอร์บอร์ดใน UI
        DisplayLeaderboard(leaderboard);

        yield return null;
        */
    }

    public void DisplayLeaderboard(dynamic leaderboard)
    {
        leaderboardText.text = ""; // ล้างข้อมูลเก่าใน Text UI

        // สร้างรายการใหม่จากข้อมูลที่ได้รับ
        foreach (var entry in leaderboard)
        {
            string username = entry.Username;
            string score = entry.Score.ToString();

            // เพิ่มข้อมูล username และ score ลงใน Text UI
            leaderboardText.text += $"{username}: {score}\n";
        }
    }

    public IEnumerator SubmitScore(string username, int score)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("score", score);

        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error submitting score: " + www.error);
        }
        else
        {
            Debug.Log("Score submitted successfully!");
        }
    }

    // เรียกใช้ SubmitScore จากปุ่มหรือเหตุการณ์อื่น
    public void OnSubmitScoreButton(string name)
    {
        StartCoroutine(SubmitScore(name, 1000)); // ตัวอย่างการส่งข้อมูล
    }
}

public class PlayerData
{
    public string Username { get; set; }
    public int Score { get; set; }
}