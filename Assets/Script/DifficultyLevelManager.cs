using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DifficultyLevelManager : MonoBehaviour
{
    [SerializeField] private UpgradeManager UpgradeManager;
    [SerializeField] private SpawnManager _spawnManager;
    [SerializeField] private GameObject _transformUiDifficulty;
    [SerializeField] private float _speedUI = 0.01f;
    int Sec = 0;
    int minSec = 0;
    private int _difficultyLevel = 1;
    public float LevelMonster;
    private void Update()
    {
        if(UpgradeManager.IsStopGame == true) return;
        minSec++;
        if(minSec >= 900){
            Sec += 15;
            minSec = 0;
            if(Sec >= 60){
                Sec = 0;    
                _difficultyLevel += 2;
                _spawnManager.SetDifficultyLevel(_difficultyLevel);
                DifficultyMonster(_difficultyLevel);
            }else{
                _difficultyLevel++;
                _spawnManager.SetDifficultyLevel(_difficultyLevel);
                DifficultyMonster(_difficultyLevel);
            }
        }
        _transformUiDifficulty.GetComponent<RectTransform>().anchoredPosition = new Vector2(_transformUiDifficulty.GetComponent<RectTransform>().anchoredPosition.x + _speedUI * -1, _transformUiDifficulty.GetComponent<RectTransform>().anchoredPosition.y);
    }
    public void DifficultyMonster(int level){
        /*
        float Inlevelmonster = 1f;
        if(level == 0){
            Inlevelmonster = 1f;
        }else{
            for (int i = 0; i < level; i++)
            {
                Inlevelmonster *= 1.05f;
            }
        }
*/
        LevelMonster = level - 1;
    }
}

