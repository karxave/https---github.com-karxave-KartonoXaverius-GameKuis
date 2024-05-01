using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class UI_Pertanyaan : MonoBehaviour
{   
    [SerializeField]
    private TextMeshProUGUI tempatTeks = null;
    [SerializeField]
    private Image tempatGambar = null;

    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(tempatTeks.text);        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
