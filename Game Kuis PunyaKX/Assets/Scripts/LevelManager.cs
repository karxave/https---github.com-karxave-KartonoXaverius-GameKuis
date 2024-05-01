using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public struct DataSoal
    {
        public string pertanyaan;
        public Sprite petunjukJawaban;

        public string[] pilihanJawaban;
        public bool[] adalahBenar;
    }

    [SerializeField]
    private DataSoal[] _soalSoal = new DataSoal[0];

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
        if (_indexSoal >= _soalSoal.Length)
        {
            _indexSoal = 0;
        }

        // Ambil data pertanyaan dari array
        DataSoal soal = _soalSoal[_indexSoal];

        //Set informasi soal
        _pertanyaan.SetPertanyaan(soal.pertanyaan, soal.petunjukJawaban);

        for (int i = 0; i < _pilihanJawaban.Length; i++)
        {
            UI_PoinJawaban poin = _pilihanJawaban[i];

            poin.SetJawaban(soal.pilihanJawaban[i], soal.adalahBenar[i]);

        }

    }

    // Start is called before the first frame update
    void Start()
    {
        NextLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
