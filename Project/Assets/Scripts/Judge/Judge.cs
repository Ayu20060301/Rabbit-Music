using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Judge : MonoBehaviour
{
    //変数の宣言
    [SerializeField] private GameObject[] m_MessageObj; //判定を入れるオブジェクト
    [SerializeField] NotesManager notesManager; //スクリプトを入れる変数
    [SerializeField] GameObject[] finishText; //曲が終了する際に表示するテキスト

    [SerializeField] TextMeshProUGUI scoreText; //scoreTextの参照

    [SerializeField] TextMeshProUGUI perfectText; //perfectTextの参照
    [SerializeField] TextMeshProUGUI greatText; //greatTextの参照
    [SerializeField] TextMeshProUGUI goodText; //goodTextの参照
    [SerializeField] TextMeshProUGUI missText; //missTextの参照
    [SerializeField] TextMeshProUGUI comboText; //comboTextの参照
    [SerializeField] GameObject startText; //テキスト「スペースキーをおしたらスタート」を格納する変数
  

    float endTime = 0.0f; //終了時間を取得する変数


     void Start()
    {

        //スコアなどを初期化
        GManager.instance.score = 0;
        GManager.instance.perfect = 0;
        GManager.instance.great = 0;
        GManager.instance.good = 0;
        GManager.instance.miss = 0;
        GManager.instance.combo = 0;
        GManager.instance.ratioScore = 0f;

        if (notesManager.NotesTime.Count > 0)
        {
            endTime = notesManager.NotesTime[notesManager.NotesTime.Count - 1];
        }
        else
        {
            Debug.LogWarning("NotesTime is empty at Start.");
            endTime = 0f; // もしくは適当な初期値
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GManager.instance.start)
        {

            startText.SetActive(false); //始まったらテキストを非表示にする

            PictureAnimation.instance.PlayerPictureAnimation(PictureAnimation.PictureAnim.IDLE);

            // キー入力判定（レーン番号を対応付け）
            CheckKey(KeyCode.S, 0);
            CheckKey(KeyCode.D, 1);
            CheckKey(KeyCode.F, 2);
            CheckKey(KeyCode.J, 3);
            CheckKey(KeyCode.K, 4);
            CheckKey(KeyCode.L, 5);

            if (notesManager.NotesTime.Count == 0)
            {

               

                //missしかなかったらテキスト「Failed」を表示する
                if (GManager.instance.perfect == 0 && GManager.instance.great == 0 && GManager.instance.good == 0 && GManager.instance.miss > 0)
                {
                    finishText[3].SetActive(true);
                }
                //ミスが一個でも出ていたらテキスト「Clear」を表示する
                else if (GManager.instance.miss > 0)
                {
                    finishText[0].SetActive(true);
                 
                }
                //MissもなくてかつGoodとGreatがなかったらテキスト「AllPerfect」を表示する
                else if (GManager.instance.good == 0 && GManager.instance.great == 0)
                {
                    finishText[2].SetActive(true);

                }
                
                //それ以外はテキスト「FullCombo」を表示
                else
                {
                    finishText[1].SetActive(true);
                }

                SoundManager.instance.SoundUpdate(SoundManager.Sound.END); //曲終了時に効果音を鳴らす

                Invoke("SceneChange", 6.0f); //6秒後にリザルトシーンに遷移する
            }

            //本来ノーツをたたくべき時間から入力がなかった場合
            if (notesManager.NotesTime.Count > 0 && Time.time > notesManager.NotesTime[0] + GManager.instance.startTime) 
            {
                message(3);
                Debug.Log("Miss");
                GManager.instance.miss++;
                GManager.instance.combo = 0; //コンボを0にする
                deleteData(0);
                SoundManager.instance.SoundUpdate(SoundManager.Sound.MISS);
                PictureAnimation.instance.PlayerPictureAnimation(PictureAnimation.PictureAnim.SAD);
                //ミス
            }
        }
    }


    // キー入力とレーン判定の共通化
    void CheckKey(KeyCode key, int lane)
    {
        if (!Input.GetKeyDown(key)) return;

        if (notesManager.LaneNum.Count > 0 && notesManager.LaneNum[0] == lane)
        {
            Judgement(GetABS(Time.time - (notesManager.NotesTime[0] + GManager.instance.startTime)), 0);
        }
        else if (notesManager.LaneNum.Count > 1 && notesManager.LaneNum[1] == lane)
        {
            Judgement(GetABS(Time.time - (notesManager.NotesTime[1] + GManager.instance.startTime)), 1);
        }
    }

    void Judgement(float timeLag,int numOffset)
    {

        PictureAnimation.instance.PlayerPictureAnimation(PictureAnimation.PictureAnim.IDLE);

        if (timeLag <= 0.10 + GManager.instance.timingOffset* 0.1f)//本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が0.1秒以下だったら
        {
            Debug.Log("Perfect");
            message(0);
            GManager.instance.ratioScore += 5.0f; //スコアの加算
            GManager.instance.perfect++; //perfectの加算
            GManager.instance.combo++; //comboの加算
            deleteData(numOffset);
            SoundManager.instance.SoundUpdate(SoundManager.Sound.HIT); //効果音鳴らす
            PictureAnimation.instance.PlayerPictureAnimation(PictureAnimation.PictureAnim.SMILE);

        }
        else
        {
            if (timeLag <= 0.15 + GManager.instance.timingOffset* 0.1f)//本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が0.15秒以下だったら
            {
                Debug.Log("Great");
                message(1);
                GManager.instance.ratioScore += 3.0f; //スコアの加算
                GManager.instance.great++; //greatの加算
                GManager.instance.combo++; //comboの加算
                deleteData(numOffset);
                SoundManager.instance.SoundUpdate(SoundManager.Sound.HIT); //効果音鳴らす
                PictureAnimation.instance.PlayerPictureAnimation(PictureAnimation.PictureAnim.SMILE);
            }
            else
            {
                if (timeLag <= 0.2 + GManager.instance.timingOffset* 0.1f)//本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が0.2秒以下だったら
                {
                    Debug.Log("Good");
                    message(2);
                    GManager.instance.ratioScore += 1.0f; //スコアの加算
                    GManager.instance.good++; //goodの加算
                    GManager.instance.combo++; //comboの加算
                    deleteData(numOffset);
                    SoundManager.instance.SoundUpdate(SoundManager.Sound.HIT); //効果音鳴らす
                    PictureAnimation.instance.PlayerPictureAnimation(PictureAnimation.PictureAnim.SMILE);
                }
            }
            

        }
    }

    float GetABS(float num)//引数の絶対値を返す関数
    {
        if (num >= 0)
        {
            return num;
        }
        else
        {
            return -num;
        }
    }
    void deleteData(int numOffset)//すでにたたいたノーツを削除する関数
    {

        // ノーツのオブジェクトも削除
        if (notesManager.NotesObj.Count > numOffset)
        {
            Destroy(notesManager.NotesObj[numOffset]);
            notesManager.NotesObj.RemoveAt(numOffset);
        }

        notesManager.NotesTime.RemoveAt(numOffset);
        notesManager.LaneNum.RemoveAt(numOffset);
        notesManager.NoteType.RemoveAt(numOffset);
        
        //スコア計算
        GManager.instance.score = (int)Math.Round(1000000 * Math.Floor(GManager.instance.ratioScore / GManager.instance.maxScore * 1000000) / 1000000);

        scoreText.text = GManager.instance.score.ToString();
        comboText.text = GManager.instance.combo.ToString();
        perfectText.text = GManager.instance.perfect.ToString();
        greatText.text = GManager.instance.great.ToString();
        goodText.text = GManager.instance.good.ToString();
        missText.text = GManager.instance.miss.ToString();
       
    }

    void message(int judge)//判定を表示する
    {
        Instantiate(m_MessageObj[judge], new Vector3(0.1f, 0.76f, 0.15f), Quaternion.Euler(45, 0, 0));
    }

    //リザルトシーンに遷移する関数
    void SceneChange()
    {
        SceneManager.LoadScene("ResultScene");
        SceneManager.LoadScene("BackGroundScene", LoadSceneMode.Additive);
    }
}
