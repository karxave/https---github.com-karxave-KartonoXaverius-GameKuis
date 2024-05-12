using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_OpsiLevelPack : MonoBehaviour
{
    public static event System.Action<LevelPackKuis> EventIfClick;

    [SerializeField]
    private Button _tombol = null;

    [SerializeField]
    private TextMeshProUGUI _packName = null;

    [SerializeField]
    private LevelPackKuis _levelPack = null;

    private void Start()
    {
        if (_levelPack != null) SetLevelPack(_levelPack);

            // subscribe events
            _tombol.onClick.AddListener(ifClicked);
        
    }

    private void OnDestroy()
    {
        //unsubscribe events
        _tombol.onClick.RemoveListener(ifClicked);
    }

    public void SetLevelPack(LevelPackKuis levelPack)
    {
        _packName.text = levelPack.name;
        _levelPack = levelPack;
        
    }

    private void ifClicked() 
    {
        EventIfClick?.Invoke(_levelPack);
    }

}
