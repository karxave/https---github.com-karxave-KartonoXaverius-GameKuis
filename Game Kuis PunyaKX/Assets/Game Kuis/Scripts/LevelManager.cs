using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private InitialDataGamePlay _initialData = null;

    [SerializeField]
    private PlayerProgress _playerProgress = null;
   
    [SerializeField]
    private LevelPackKuis _soalSoal = null;

    private int _indexSoal = -1;

    [SerializeField]
    private UI_Pertanyaan _pertanyaan = null;

    [SerializeField]
    private UI_PoinJawaban[] _pilihanJawaban = new UI_PoinJawaban[0];

    [SerializeField]
    private GameSceneManager _gameSceneManager = null;

    [SerializeField]
    private string _nameSceneChooseMenu = string.Empty;

    [SerializeField]
    private SoundButtonClicked __pemanggilSuara = null;

    [SerializeField]
    private AudioClip _suaraYouWin = null;

    [SerializeField]
    private AudioClip _suaraGameOver = null;

    public void NextLevel()
    {
        //soal index selanjutnya
        _indexSoal++;

        //Jika index melampaui soal terakhir, ulang dari awal
        if (_indexSoal >= _soalSoal.BanyakLevel)
        {
            // _indexSoal = 0;
            _gameSceneManager.OpenScene(_nameSceneChooseMenu);
            return;
        }

        // Ambil data pertanyaan dari array
        LevelSoalKuis soal = _soalSoal.AmbilLevelKe(_indexSoal);

        //Set informasi soal
        _pertanyaan.SetPertanyaan($"Soal {_indexSoal + 1}", soal.pertanyaan, soal.petunjukJawaban);

        for (int i = 0; i < _pilihanJawaban.Length; i++)
        {
            UI_PoinJawaban poin = _pilihanJawaban[i];

            LevelSoalKuis.OpsiJawaban opsi = soal.opsiJawaban[i];

            poin.SetJawaban(opsi.jawabanTeks, opsi.adalahBenar);

        }

    }

    // Start is called before the first frame update
    void Start()
    {
      
        _soalSoal = _initialData.levelPack;

        _indexSoal = _initialData.levelIndex - 1;
       
        NextLevel();

        AudioManager.instance.PlayBGM(1);

        //subscribe events
        UI_PoinJawaban.EventJawabSoal += UI_PoinJawaban_EventJawabSoal;
    }

    private void OnDestroy()
    {
        //unsubscribe events
        UI_PoinJawaban.EventJawabSoal -= UI_PoinJawaban_EventJawabSoal;
    }

    
    private void OnApplicationQuit()
        {
            _initialData.GameIsOver = false;
        }

   private void UI_PoinJawaban_EventJawabSoal(string answer, bool answerIsCorrect) 
    {
        //  var nameLevelPack = _initialData.levelPack.name;

        __pemanggilSuara.PlaySoundClickButton(answerIsCorrect ? _suaraYouWin : _suaraGameOver);

       if (!answerIsCorrect) return;

        string namaLevelPack = _initialData.levelPack.name;
        int levelTerakhir = _playerProgress.progressData.progressLevel[namaLevelPack];


        if (_indexSoal + 2 > levelTerakhir)
        {
            _playerProgress.progressData.koin += 20;

            Debug.Log(_playerProgress.progressData.koin);

            _playerProgress.progressData.progressLevel[namaLevelPack] = _indexSoal + 2;

            _playerProgress.SimpanProgress();
        }
    }

   


}
