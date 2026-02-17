using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultScene : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI perfectText; //perfectTextの参照
    [SerializeField] TextMeshProUGUI greatText;　//greatTextの参照
    [SerializeField] TextMeshProUGUI goodText; //goodTextの参照
    [SerializeField] TextMeshProUGUI missText; //missTextの参照
    [SerializeField] TextMeshProUGUI scoreText; //scoreTextの参照
    [SerializeField] TextMeshProUGUI rankText; //rankTextの参照

    private void Start()
    {
        rankText.text = string.Empty; //何も書かれてない

        //950000点以上はランクS
        if (GManager.instance.score > 950000)
        {
            rankText.text = "S";
            rankText.color = Color.blue;
            PictureAnimation.instance.ResultSceneAnim(PictureAnimation.ResultAnim.RANK_S);
        }
        //950000点以下かつ850000点以上はランクA
        else if (GManager.instance.score <= 950000 && GManager.instance.score > 850000)
        {
            rankText.text = "A";
            rankText.color = Color.yellow;
            PictureAnimation.instance.ResultSceneAnim(PictureAnimation.ResultAnim.RANK_A);
        }
        //850000点以下かつ800000点以上はランクB
        else if (GManager.instance.score <= 850000 && GManager.instance.score > 800000)
        {
            rankText.text = "B";
            rankText.color = Color.orange;
            PictureAnimation.instance.ResultSceneAnim(PictureAnimation.ResultAnim.RANK_B);
        }
        //800000点以下かつ750000点以上はランクC
        else if (GManager.instance.score <= 800000 && GManager.instance.score > 750000)
        {
            rankText.text = "C";
            rankText.color = Color.green;
            PictureAnimation.instance.ResultSceneAnim(PictureAnimation.ResultAnim.RANK_C);
        }
        //それ以外はランクD
        else
        {
            rankText.text = "D";
            PictureAnimation.instance.ResultSceneAnim(PictureAnimation.ResultAnim.RANK_D);
        }

    }

    void Update()
    {
        scoreText.text = GManager.instance.score.ToString();
        perfectText.text = GManager.instance.perfect.ToString();
        greatText.text = GManager.instance.great.ToString();
        goodText.text = GManager.instance.good.ToString();
        missText.text = GManager.instance.miss.ToString();
    }

    //ゲームシーンに遷移するボタンを押したときの処理
    public void OnGameSceneClick()
    {
        SceneManager.LoadScene("GameScene"); 
        SceneManager.LoadScene("BackGroundScene", LoadSceneMode.Additive);

        GManager.instance.score = 0; //スコア数を0
        GManager.instance.perfect = 0; //perfect数を0
        GManager.instance.great = 0; //great数を0
        GManager.instance.good = 0; //good数を0
        GManager.instance.miss = 0; //miss数を0
        GManager.instance.combo = 0; //combo数を0

    }

    //セレクトシーンに遷移するボタンを押したときの処理
    public void OnSelectSceneClick()
    {
        SceneManager.LoadScene("SelectScene");
        SceneManager.LoadScene("BackGroundScene", LoadSceneMode.Additive);

        GManager.instance.score = 0; //スコア数を0
        GManager.instance.perfect = 0; //perfect数を0
        GManager.instance.great = 0; //great数を0
        GManager.instance.good = 0; //good数を0
        GManager.instance.miss = 0; //miss数を0
        GManager.instance.combo = 0; //combo数を0
    }
}
