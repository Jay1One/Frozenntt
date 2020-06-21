using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class HopPlayer : MonoBehaviour
{
    [SerializeField] private AnimationCurve m_JumpCurve;
    [SerializeField] private float m_JumpHeight = 1f;
    [SerializeField] private float m_JumpDistance = 2f;
    //[SerializeField] private GameObject m_Text;
    [SerializeField] private float m_BallSpeed = 1f;
    [SerializeField] private HopInput m_Input;
    [SerializeField] private HopTrack m_Track;
    //private int score = -1;
    //private int lives = 3;
    //public bool start = false;
    private float iteration; //цикл прыжка
    private float startZ;// точка начала прыжка

    private void Start()
    {
        
    }

    private void Update()
    {
        //if (start)
        //{
            
            Vector3 pos = transform.position;
            //смещение
            pos.x = Mathf.Lerp(pos.x, m_Input.Strafe, Time.deltaTime * 5f);
            // прыжок
            pos.y = m_JumpCurve.Evaluate(iteration) * m_JumpHeight;
            
            //движение вперёд
             pos.z = startZ + iteration * m_JumpDistance;
             transform.position = pos;
            //
            // //увеличиваем счётчик прыжка
            iteration += Time.deltaTime * m_BallSpeed;

            if (iteration < 1f)
            {
                return;

            }

            iteration = 0f;
            startZ += m_JumpDistance;
        //transform.Translate(Vector3.forward * Time.deltaTime * 2f);
        //if (transform.position.z >= score * 3)
        //{
        //    lives--;
        //    score++;
        //    //print(m_Text.GetComponent<Text>().text);
        //    m_Text.GetComponent<Text>().text = "Score:" + (score-1);
        //    print("жизни"+lives);
        //}

        //if (lives == 0)
        //{
        //    print("you lose");
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //}

        //if (score == 11)
        //{
        //    print("you win");
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //}
        //}
        if (m_Track.IsBallOnPlatform(transform.position))
        {
            return;
        }
        print("lose");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    other.gameObject.GetComponent<MeshRenderer>().material.color=Color.blue;
    //    score++;
    //    m_Text.GetComponent<Text>().text = "Score:" + (score-1);
    //    //print(score);
    //}



    //public void StartGame()
    //{
    //    start = true;
    //}
    public void ChangeJumpHeight(float jumpHeight)
    {
        m_JumpHeight = jumpHeight;
    }

    public void ChangeBallSpeed(float ballSpeed)
    {
        m_BallSpeed = ballSpeed;
    }

    public void ResetParametrs()
    {
        m_JumpHeight = 1f;
        m_BallSpeed = 1f;
    }
}
