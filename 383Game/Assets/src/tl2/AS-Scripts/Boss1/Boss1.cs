using UnityEngine;

public class Boss1 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public Transform player;

    public bool isFlipped = false;

    public void facePlayer(){

        Vector3 flipped = transform.localScale;
        flipped.z = -1f;

        if(transform.position.x > player.position.x && isFlipped) { 
            
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;

            
        }else if(transform.position.x < player.position.x && !isFlipped){

            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;

        }

    }
}
