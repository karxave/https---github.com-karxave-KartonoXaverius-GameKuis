using TMPro;
using UnityEngine;

public class UI_PesanLevel : MonoBehaviour
{
    [SerializeField]
    private Animator _animator = null;

    [SerializeField]
    private GameObject _opsiMenang = null;

    [SerializeField]
    private GameObject _opsiKalah = null;

    [SerializeField]
    private TextMeshProUGUI _tempatPesan = null;

    public string Pesan
    {
        get => _tempatPesan.text;
        set => _tempatPesan.text = value;
    }

    private void Awake()
    {
        // untuk menonaktifkan halaman pesan level
        if (gameObject.activeSelf)
            gameObject.SetActive(false);

        // subscribe events
        UI_Timer.EventTimeIsZero += UI_Timer_EventTimeIsZero;

        UI_PoinJawaban.EventJawabSoal += UI_PoinJawaban_EventJawabSoal;

    }
    
    private void OnDestroy()
    {
        // unsubscribe events
        UI_Timer.EventTimeIsZero -= UI_Timer_EventTimeIsZero;

        UI_PoinJawaban.EventJawabSoal -= UI_PoinJawaban_EventJawabSoal;
    }
    
    private void UI_Timer_EventTimeIsZero()
    {
        Pesan = "Waktu sudah habis!";
        gameObject.SetActive(true);

        _opsiMenang.SetActive(false);
        _opsiKalah.SetActive(true);

    }
    private void UI_PoinJawaban_EventJawabSoal(string answerTeks, bool answerCorrect)
    {
        Pesan = $"Jawaban Anda {answerCorrect} (Jawab : {answerTeks})";

        gameObject.SetActive(true);

        if (answerCorrect)
        {
            _opsiMenang.SetActive(true);
            _opsiKalah.SetActive(false);
        }
        else
        {
            _opsiMenang.SetActive(false);
            _opsiKalah.SetActive(true);
        }

        _animator.SetBool("Menang", answerCorrect);
    }

    

   
}
