using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System; 
using System.Linq;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "ScriptableObjects/UpgradeDataSO")]
public class UpgradeDataSO : ScriptableObject
{
    public List<UpgradeDetail> Upgrade;
}
[Serializable]
public class ValueAdd {
    public List<float> value;
}
[Serializable]
public class UpgradeDetail {
    public int Id = 0;
    public int Level = -1;
    public Sprite _imgProflie;
    public int Common = 0;
    public int Rare = 0;
    public int Epic = 0;
    public int Legendary = 0;
    public string TopicText;
    public List<ValueAdd> ValueAdd;
    public List<string> DetailText;
}
