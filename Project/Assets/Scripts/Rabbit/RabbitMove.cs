using UnityEngine;
using UnityEngine.AI;

public class RabbitMove : MonoBehaviour
{


    private NavMeshAgent m_NavMeshAgent; //NavMeahAgentコーポネントを入れる
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        nextGoal();
        //AnimationManager.instance.RabbitAnimation(AnimationManager.RabbitAnim.RUN);
      
    }

    void nextGoal()
    {
        var randomPos = new Vector3(Random.Range(-30, 20), 0, Random.Range(-30, 20));
        m_NavMeshAgent.destination = randomPos;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_NavMeshAgent.remainingDistance < 0.5f)
        {
            nextGoal();
        }
    }

    
}
