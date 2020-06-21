using System;
using UnityEngine;


public class PlayerMovementWhileLoading : CharacterMovement
{
  
    public override void Move(Vector2 direction)
    {
        gameObject.transform.Translate(direction);
    }

    public override void Stop(float time)
    {
        throw new System.NotImplementedException();
    }

    public override void Jump(float force)
    {
        throw new System.NotImplementedException();
    }

    private void Update()
    {
        //надо бы высчитывать исходя из длинны полосы прогрузки и времени загрузки
        Move(Vector2.right * (Time.deltaTime * 80f));
    }
}
