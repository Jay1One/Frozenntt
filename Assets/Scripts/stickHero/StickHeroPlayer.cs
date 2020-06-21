using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickHeroPlayer : MonoBehaviour
{
    [SerializeField] private StickHeroController m_Controller;
	
    private bool isMoving;
    private float targetPosition;
    private bool isFalling;

    public void StartMovement(float targetPos, bool isFall)
    {
        isMoving = true;
        targetPosition = targetPos;
        isFalling = isFall;
    }
	
  
    private void Update()
    {
        if (isMoving)
        {
            //двигаемся, пока не достигнем нужной позиции (конец палки или центрe платформы)
            transform.Translate(Vector3.right * Time.deltaTime);
            if (transform.position.x < targetPosition) return;

            isMoving = false;
            if (!isFalling)
            {
                m_Controller.StopPlayerMovement();
            }
        }

        if (!isFalling) return;
		
        //падаем до определенной глубины
        transform.Translate( 2f* Time.deltaTime*Vector3.down );
        if (transform.position.y <= -1f)
        {
            isFalling = false;
            m_Controller.ShowScores();
        }
    }
}
