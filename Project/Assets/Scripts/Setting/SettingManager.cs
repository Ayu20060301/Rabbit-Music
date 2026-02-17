using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SettingManager : MonoBehaviour
{
    //Sliderの参照
    [SerializeField] private Slider m_NoteSpeedSlider;
    [SerializeField] private Slider m_OffsetSlider;

    //テキストの参照
    [SerializeField] private TextMeshProUGUI m_NoteSpeedText; 
    [SerializeField] private TextMeshProUGUI m_OffSetText; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //初期値の読み込み
        float speed = PlayerPrefs.GetFloat("NoteSpeed", 16.0f);
        float offset = PlayerPrefs.GetFloat("Offset", 0.0f);

        m_NoteSpeedSlider.value = speed;
        m_OffsetSlider.value = offset;

        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();
    }

    //テキストの更新
    void UpdateText()
    {
        m_NoteSpeedText.text =$"{ m_NoteSpeedSlider.value :F1}";
        m_OffSetText.text = $"{m_OffsetSlider.value :F1}ms";
    }

    //セーブ
    public void SaveSettings()
    {

        PlayerPrefs.SetFloat("NoteSpeed", m_NoteSpeedSlider.value);
        PlayerPrefs.SetFloat("Offset", m_OffsetSlider.value);
        PlayerPrefs.Save();

        // GManager に反映
        GManager.instance.noteSpeed = m_NoteSpeedSlider.value;
        GManager.instance.timingOffset = m_OffsetSlider.value;


        Debug.Log("設定を保存しました speed={GManager.instance.noteSpeed}, offset={GManager.instance.timingOffset}");
    }
}
