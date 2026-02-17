using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI blinkerText; //点滅させるテキストの参照

    float speed = 1.0f; //点滅スピード

    private float m_Time;

    private void Start()
    {
        SceneManager.LoadScene("BackGroundScene", LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        //スペースキーを押したらセレクトシーンに遷移
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("SelectScene");
            SceneManager.LoadScene("BackGroundScene", LoadSceneMode.Additive);
        }
        
        //ESCキーを押したらゲームを閉じる
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit(); 
        }

        blinkerText.color = GetAlphaColor(blinkerText.color);
    }
    Color GetAlphaColor(Color color)
    {
        m_Time += Time.deltaTime * 5.0f * speed;
        color.a = Mathf.Sin(m_Time) * 0.5f + 0.5f;

            return color;
    }

}
