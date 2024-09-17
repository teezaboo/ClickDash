using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingMenu : MonoBehaviour
{
    [SerializeField] private UpgradeManager _upgradeManager;

    private void OnEnable()
    {
        _upgradeManager.IsStopGame = true;
    }
    private void OnDisable()
    {
        _upgradeManager.IsStopGame = false;
    }
}
