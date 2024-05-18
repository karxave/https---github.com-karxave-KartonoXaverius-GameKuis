using UnityEngine;
using TMPro;

public class LevelMenuDataManager : MonoBehaviour
{
    [SerializeField]
    private InitialDataGamePlay _initialData = null;

    [SerializeField]
    private UI_LevelPackList _levelPackList = null;

    [SerializeField]
    private PlayerProgress _playerProgress = null;

    [SerializeField]
    private TextMeshProUGUI _numberCoin = null;

    [SerializeField]
    private LevelPackKuis[] _levelPacks = new LevelPackKuis[0];


    // Start is called before the first frame update
    void Start()
    {
        if (!_playerProgress.MuatProgress())
        {
            _playerProgress.SimpanProgress();
        }

        _levelPackList.LoadLevelPack(_levelPacks, _playerProgress.progressData);

        _numberCoin.text = $"{_playerProgress.progressData.koin}";

        AudioManager.instance.PlayBGM(0);
    }

    private void OnApplicationQuit()
    {
        _initialData.GameIsOver = false;
    }


}
