using UnityEngine;

public class Light : MonoBehaviour
{

    [SerializeField] private float m_Speed = 3.0f;
    [SerializeField] private int m_num = 0;
    private Renderer m_Rend;
    private float m_Alfa = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!(m_Rend.material.color.a <= 0))
        {
            m_Rend.material.color = new Color(m_Rend.material.color.r, m_Rend.material.color.r, m_Rend.material.color.r, m_Alfa);
        }

        //キー番号が1
        if(m_num == 1)
        {
            //Sキーが押されたらColorChange関数が呼び出される
            if(Input.GetKeyDown(KeyCode.S))
            {
                ColorChange();
            }
        }
        //キー番号が2
        if (m_num == 2)
        {
            //Dキーが押されたらColorChange関数が呼び出される
            if (Input.GetKeyDown(KeyCode.D))
            {
                ColorChange();
            }
        }
        //キー番号が3
        if (m_num == 3)
        {
            //Fキーが押されたらColorChange関数が呼び出される
            if (Input.GetKeyDown(KeyCode.F))
            {
                ColorChange();
            }
        }
        //キー番号が4
        if (m_num == 4)
        {
            //Jキーが押されたらColorChange関数が呼び出される
            if (Input.GetKeyDown(KeyCode.J))
            {
                ColorChange();
            }
        }
        //キー番号が5
        if (m_num == 5)
        {
            //Kキーが押されたらColorChange関数が呼び出される
            if (Input.GetKeyDown(KeyCode.K))
            {
                ColorChange();
            }
        }
        //キー番号が6
        if (m_num == 6)
        {
            //Lキーが押されたらColorChange関数が呼び出される
            if (Input.GetKeyDown(KeyCode.L))
            {
                ColorChange();
            }
        }
        m_Alfa -= m_Speed * Time.deltaTime;
    }

    //ライト
    void ColorChange()
    {
        m_Alfa = 0.3f;
        m_Rend.material.color = new Color(m_Rend.material.color.r, m_Rend.material.color.g, m_Rend.material.color.b, m_Alfa);
    }
}

