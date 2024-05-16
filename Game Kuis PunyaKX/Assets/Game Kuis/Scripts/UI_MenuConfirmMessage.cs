using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MenuConfirmMessage : MonoBehaviour
{
    [SerializeField]
    private PlayerProgress _playerProgress = null;

    [SerializeField]
    private GameObject _pesanCukupKoin = null;

    [SerializeField]
    private GameObject _pesanTakCukupKoin = null;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }

        UI_OpsiLevelPack.EventIfClick += UI_OpsiLevelPack_EventIfClick;
    }

    private void OnDestroy()
    {
        UI_OpsiLevelPack.EventIfClick -= UI_OpsiLevelPack_EventIfClick;
    }

    private void UI_OpsiLevelPack_EventIfClick(LevelPackKuis levelPack, bool terkunci)
    {
        if (!terkunci) return;

        gameObject.SetActive(true);

        if (_playerProgress.progressData.koin < levelPack.Harga)
        {
            _pesanCukupKoin.SetActive(false);
            _pesanTakCukupKoin.SetActive(true);
        }

        _pesanTakCukupKoin.SetActive(false);
        _pesanCukupKoin.SetActive(true);

    }
}
