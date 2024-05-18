using System;
using UnityEngine;

public class UI_LevelPackList : MonoBehaviour
{
    [SerializeField]
    private Animator _animator = null;

    [SerializeField]
    private InitialDataGamePlay _initialData = null;

    [SerializeField]
    private UI_LevelKuisList _levelList = null;

    [SerializeField]
    private UI_OpsiLevelPack _tombolLevelPack = null;

    [SerializeField]
    private RectTransform _content = null;

   //[SerializeField]
   // private LevelPackKuis[] _levelPacks = new LevelPackKuis[0];

    private void Start()
    {
      //  LoadLevelPack();

        if (_initialData.GameIsOver)
        {
            UI_OpsiLevelPack_EventIfClick(null, _initialData.levelPack, false);
        }

        //subscribe events
        UI_OpsiLevelPack.EventIfClick += UI_OpsiLevelPack_EventIfClick;
    }

    private void OnDestroy()
    {
        //unsubscribe events
        UI_OpsiLevelPack.EventIfClick -= UI_OpsiLevelPack_EventIfClick;
    }

    private void UI_OpsiLevelPack_EventIfClick(UI_OpsiLevelPack tombolLevelPack,  LevelPackKuis levelPack, bool terkunci)
    {
        if (terkunci) return;

        // open Menu Levels
        //_levelList.gameObject.SetActive(true);
        _levelList.UnloadLevelPack(levelPack);

        //close Menu Level Packs
        //gameObject.SetActive(false);

        _initialData.levelPack = levelPack;

        _animator.SetTrigger("KeLevels");
    }

    public void LoadLevelPack(LevelPackKuis[] levelPacks, PlayerProgress.MainData playerData)
    {
       foreach ( var lp in levelPacks)
        {
            // buat salinan objek dari prefab tombol level pack
            var t = Instantiate(_tombolLevelPack);

            t.SetLevelPack(lp);

            // masukkan objek tombol sebagai anak dari objek "content"
            t.transform.SetParent(_content);
            t.transform.localScale = Vector3.one;

            if (!playerData.progressLevel.ContainsKey(lp.name))
            {
                t.LockLevelPack();
            }

        }
    }
}
