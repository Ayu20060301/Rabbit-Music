using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PictureAnimation : MonoBehaviour
{
    public static PictureAnimation instance = null;


    [SerializeField] Animator picAnim;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

    }

    private void OnEnable()
    {
        // シーンロード完了後に Animator を探す
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (picAnim == null)
        {
            GameObject charObj = GameObject.Find("立ち絵");
            if (charObj != null)
            {
                picAnim = charObj.GetComponent<Animator>();
            }

            if (picAnim == null)
            {
                Debug.LogWarning("Animator が見つかりません。");
            }
        }
    }

    //立ち絵のアニメーションの種類
    public enum PictureAnim
    {
        IDLE,
        SMILE, //スマイル
        SAD //悲しい

    }

    //リザルトシ－ンのランクごとの立ち絵の種類
    public enum ResultAnim
    {     
        RANK_S, //RankS時の立ち絵のアニメーション
        RANK_A,　//RankA時の立ち絵のアニメーション
        RANK_B, //RankB時の立ち絵のアニメーション
        RANK_C, //RankC時の立ち絵のアニメーション
        RANK_D //RankD時の立ち絵のアニメーション
    }


    //立ち絵のアニメーションの更新
    public void PlayerPictureAnimation(PictureAnim anim)
    {

        if (picAnim == null)
        {
            Debug.LogWarning("Animator が null です。再割り当てが必要です。");
            return;
        }

        switch (anim)
        {
            case PictureAnim.IDLE:
                picAnim.SetTrigger("IdleTrigger");
                break;
            case PictureAnim.SMILE:
                picAnim.SetTrigger("SmileTrigger");
                break;
            case PictureAnim.SAD:
                picAnim.SetTrigger("SadTrigger");
                break;
        }
    }

    //リザルトシーンのアニメーションの更新
    public void ResultSceneAnim(ResultAnim _anim)
    {
        if (picAnim == null)
        {
            Debug.LogWarning("Animator が null です。再割り当てが必要です。");
            return;
        }

        switch(_anim)
        {
            case ResultAnim.RANK_S:
                picAnim.SetTrigger("RankSTrigger");
                break;
            case ResultAnim.RANK_A:
                picAnim.SetTrigger("RankATrigger");
                break;
            case ResultAnim.RANK_B:
                picAnim.SetTrigger("RankBTrigger");
                break;
            case ResultAnim.RANK_C:
                picAnim.SetTrigger("RankCTrigger");
                break;
            case ResultAnim.RANK_D:
                picAnim.SetTrigger("RankDTrigger");
                break;
        }


    }
}
