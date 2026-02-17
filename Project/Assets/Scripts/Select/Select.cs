using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using System.Collections;

public class Select : MonoBehaviour
{
    [SerializeField] SongDataBase dataBase; //SongDataBaseを格納する変数
    [SerializeField] TextMeshProUGUI modeText; //「Easy、Normal、Expert、Master」
    
    [SerializeField] Image[] ModeImage;  //モードの画像残照
    [SerializeField] Image imageColor; //曲のイメージカラー参照

    [SerializeField] GameObject SettingPanel;// 設定パネル
    [SerializeField] SettingManager settingManager; //スクリプト「SettingManager」の参照


    [SerializeField] GameObject saveText;

    [SerializeField] GameObject startPanel;
    

    bool isSettingOpen = false;

    int selectIndex; //選択インデックス
    AudioSource audio;
    AudioClip clip;
    string songName;
    string clipName;
    

    Vector3 normalScele = Vector3.one; //通常サイズ
    Vector3 bigScale = new Vector3(0.5f,0.5f,0.5f); //ビックサイズ

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
        selectIndex = GManager.instance.selectIndex;
        audio = GetComponent<AudioSource>();
        songName = dataBase.songData[selectIndex].songName;
        clipName = "130 からすうさぎ";
        clip = (AudioClip)Resources.Load("Musics/" + clipName);
        audio.Stop();
        audio.PlayOneShot(clip);
        SongUpdateAll();
    }

    // Update is called once per frame
    void Update()
    {
        //設定画面を開いている場合のみの処理
        if (isSettingOpen)
        {
            //設定画面を閉じる
            if (isSettingOpen && Input.GetKeyDown(KeyCode.Escape))
            {
                saveText.SetActive(true);


                StartCoroutine(HideSaveTextAfterSeconds(2.0f));

                settingManager.SaveSettings();
                SettingPanel.SetActive(false);
                isSettingOpen = false;
            }
            return;
        }

        //右矢印キーを押した場合
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (selectIndex < dataBase.songData.Length - 1)
            {
                selectIndex++;
                SongUpdateAll();
            }
        }
        //左矢印キーを押した場合
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(selectIndex > 0)
            {
                selectIndex--;
                SongUpdateAll();
            }
        }
        //Enterキーを押したらGameSceneに遷移する
        if(Input.GetKeyDown(KeyCode.Return))
        {
            SongStart();
        }

        //Escapeキーを押したらTitleSceneに遷移する
        if(Input.GetKeyDown(KeyCode.Escape))
        {
          
            SceneManager.LoadScene("TitleScene");
        }
        //設定画面を開く
        if (Input.GetKeyDown(KeyCode.Tab))
        {
           
            SettingPanel.SetActive(true);
            isSettingOpen = true;
        }
    }

    private IEnumerator HideSaveTextAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        saveText.SetActive(false);
    }

    private void SongUpdateAll()
    {

        songName = dataBase.songData[selectIndex].songName;
        audio.Stop();
        audio.PlayOneShot(clip);
        for (int i = 0; i < ModeImage.Length; i++)
        {
           
            SongUpdate(i - selectIndex);
        }
    }

    private void SongUpdate(int id)
    {
        int index = selectIndex + id;

        // 範囲外なら処理しない
        if (index < 0 || index >= dataBase.songData.Length || index >= ModeImage.Length)
            return;

        //選択中
        if (id == 0)
        {
            //選択中のデータ
            imageColor.color = dataBase.songData[index].imageColor;
            modeText.text = dataBase.songData[index].modeText;
            ModeImage[index].sprite = dataBase.songData[index].levelImage;
            ModeImage[index].transform.localScale = normalScele;  // 大きく表示

        }
        //選択外
        else
        {
            ModeImage[index].sprite = dataBase.songData[index].levelImage;
            ModeImage[index].transform.localScale = bigScale; // 通常サイズに戻す
        }

    }


    public void SongStart()
    {
        GManager.instance.songID = selectIndex;
        SceneManager.LoadScene("GameScene");
        SceneManager.LoadScene("BackGroundScene",LoadSceneMode.Additive);
    }
}
