using UnityEngine;
using TMPro;

public class UI_MenuConfirmMessage : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _tempatKoin = null;

    [SerializeField]
    private PlayerProgress _playerProgress = null;

    [SerializeField]
    private GameObject _pesanCukupKoin = null;

    [SerializeField]
    private GameObject _pesanTakCukupKoin = null;

    private UI_OpsiLevelPack _tombolLevelPack = null;
    private LevelPackKuis _levelPack = null;

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

    private void UI_OpsiLevelPack_EventIfClick(UI_OpsiLevelPack tombolLevelPack, LevelPackKuis levelPack, bool terkunci)
    {
        if (!terkunci) return;

        gameObject.SetActive(true);

        if (_playerProgress.progressData.koin < levelPack.Harga)
        {
            // jumlah koin tidak cukup
            _pesanCukupKoin.SetActive(false);
            _pesanTakCukupKoin.SetActive(true);
            return;
        }

        // jumlah koin cukup
        _pesanTakCukupKoin.SetActive(false);
        _pesanCukupKoin.SetActive(true);
        _tombolLevelPack = tombolLevelPack;
        _levelPack = levelPack;
        
    }

    public void OpenLevel()
    {
        _playerProgress.progressData.koin -= _levelPack.Harga;

        _playerProgress.progressData.progressLevel[_levelPack.name] = 1;

        _tempatKoin.text = $"{_playerProgress.progressData.koin}";

        _playerProgress.SimpanProgress();

        _tombolLevelPack.UnlockLevelPack();

        _pesanCukupKoin.SetActive(false);
    }

}
