using UnityEngine;
using UnityEngine.UI;

public class GManager : MonoBehaviour
{
    public static GManager instance = null;

    public int songID; //曲のID
    public float noteSpeed; //ノーツスピード
    public float timingOffset; //オフセット

    public float ratioScore; //スコアの比率
    public float maxScore; //スコアの最大値

    public bool start; //始まるかのフラグ
    public float startTime; //スタート時間

    public int combo; //コンボ数を記録
    public int score; //スコアを記録

    public int perfect; //perfectを記録
    public int great; //greatを記録
    public int good; //goodを記録
    public int miss; //missを記録

    public int selectIndex; //選択

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

            // 起動時に保存データを読み込む
            noteSpeed = PlayerPrefs.GetFloat("NoteSpeed", 3.0f);
            timingOffset = PlayerPrefs.GetFloat("Offset", 0.0f);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
