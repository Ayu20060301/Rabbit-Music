using UnityEngine;
using UnityEngine.UI;

public class Notes : MonoBehaviour
{

    private float m_Speed = 6.0f; //ノーツスピード
    bool start;
   

    private void Start()
    {
        m_Speed = GManager.instance.noteSpeed;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            start = true;
           
        }
        if (start)
        {
            this.transform.position -= this.transform.forward * Time.deltaTime * m_Speed;
        }
    }
}
