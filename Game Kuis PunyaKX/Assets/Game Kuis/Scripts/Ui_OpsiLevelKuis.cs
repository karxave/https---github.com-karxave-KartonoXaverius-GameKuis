using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Ui_OpsiLevelKuis : MonoBehaviour
{
    public static event System.Action<int> EventIfClick;

    [SerializeField]
    private Button _tombolLevel = null;

    [SerializeField]
    private TextMeshProUGUI _levelName = null;

    [SerializeField]
    private LevelSoalKuis _levelKuis = null;

    public bool InteraksiTombol
    {
        get => _tombolLevel.interactable;
        set => _tombolLevel.interactable = value;
    }

    private void Start()
    {
        if (_levelKuis != null)
        {
            SetLevelKuis(_levelKuis, _levelKuis.levelPackIndex);
        }

        // subscribe event
        _tombolLevel.onClick.AddListener(IfClick);


    }

    private void OnDestroy()
    {
        //unsubscribe event
        _tombolLevel.onClick.RemoveListener(IfClick);
    }

    public void SetLevelKuis(LevelSoalKuis levelKuis, int index)
    {
        _levelName.text = levelKuis.name;
        _levelKuis = levelKuis;

        _levelKuis.levelPackIndex = index;
    }

    private void IfClick()
    {
        EventIfClick?.Invoke(_levelKuis.levelPackIndex);
    }
}
