using UnityEngine;

public class EnemyMovementScript : Movement
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        verticalSpeed = 0;
        gravity = this.GetComponent<Rigidbody>().mass;
    }

    // Update is called once per frame
    void Update()
    {
        if (characterController.isGrounded)
        {
            verticalSpeed = 0;
        }
        verticalSpeed -= gravity * Time.deltaTime;
        
        characterController.Move(new Vector3(0,verticalSpeed, 0)* Time.deltaTime);
    }
}
