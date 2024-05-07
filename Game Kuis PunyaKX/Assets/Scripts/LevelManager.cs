using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private PlayerProgress _playerProgress = null;
   
    [SerializeField]
    private LevelPackKuis _soalSoal = null;

    private int _indexSoal = -1;

    [SerializeField]
    private UI_Pertanyaan _pertanyaan = null;

    [SerializeField]
    private UI_PoinJawaban[] _pilihanJawaban = new UI_PoinJawaban[0];

    public void NextLevel()
    {
        //soal index selanjutnya
        _indexSoal++;

        //Jika index melampaui soal terakhir, ulang dari awal
        if (_indexSoal >= _soalSoal.BanyakLevel)
        {
            _indexSoal = 0;
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
        if (!_playerProgress.MuatProgress())
            {
                 _playerProgress.SimpanProgress();
            }
        
       
        NextLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
