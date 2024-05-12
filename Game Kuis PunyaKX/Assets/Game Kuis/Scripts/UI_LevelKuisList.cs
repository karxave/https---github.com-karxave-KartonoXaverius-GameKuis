using UnityEngine;

public class UI_LevelKuisList : MonoBehaviour
{
    [SerializeField]
    private InitialDataGamePlay _initialData = null;

    [SerializeField]
    private Ui_OpsiLevelKuis _tombolLevel = null;

    [SerializeField]
    private RectTransform _content = null;

    [SerializeField]
    private LevelPackKuis _levelPack = null;

    [SerializeField]
    private GameSceneManager _gameSceneManager = null;

    [SerializeField]
    private string _gamePlayScene = null;

    private void Start()
    {
        // subscribe event
        Ui_OpsiLevelKuis.EventIfClick += Ui_OpsiLevelKuis_EventIfClick;
        
    }

    private void OnDestroy()
    {
        // unsubscribe event
        Ui_OpsiLevelKuis.EventIfClick -= Ui_OpsiLevelKuis_EventIfClick;

    }

    private void Ui_OpsiLevelKuis_EventIfClick(int index)
    {
        _initialData.levelIndex = index;

        _gameSceneManager.OpenScene(_gamePlayScene);

    }

    public void UnloadLevelPack(LevelPackKuis levelPack)
    {

        HapusIsiKonten();

        _levelPack = levelPack;

        for (int i = 0; i < levelPack.BanyakLevel; i++)
        {
            // buat salinan objek dari prefab tombol level pack
            var t = Instantiate(_tombolLevel);

            t.SetLevelKuis(levelPack.AmbilLevelKe(i), i);

            // masukkan objek tombol sebagai anak dari objek "content"
            t.transform.SetParent(_content);
            t.transform.localScale = Vector3.one;
        }
    }



    private void HapusIsiKonten()
    {
        var cc = _content.childCount;

        for (int i = 0; i < cc; i++)
        {
            Destroy(_content.GetChild(i).gameObject);
        }
    }
}
