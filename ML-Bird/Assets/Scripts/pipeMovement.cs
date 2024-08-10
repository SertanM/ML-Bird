using UnityEngine;

public class pipeMovement : MonoBehaviour
{
    
    void OnEnable()
    {
        GetComponent<Rigidbody2D>().linearVelocityX = -2f;
    }

    void Update(){
        if(this.transform.position.x < -12f){
            Destroy(this.gameObject);
        }
    }
}
