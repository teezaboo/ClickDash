using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscManager : MonoBehaviour
{
    [SerializeField] private GameObject _panelPause;
    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey && e.keyCode == KeyCode.Escape)
        {
            _panelPause.SetActive(!_panelPause.activeSelf);
        }
    }
}
