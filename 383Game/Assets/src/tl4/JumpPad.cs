using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
This script is attached to the JumpPad GameObject.
When the player collides with the JumpPad, it applies an upward force to the player, making them jump.
*/

public class JumpPad : MonoBehaviour
{
    [SerializeField] private float jumpForce = 20f; // The force applied to the player when they jump on the pad

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // Apply an upward force to the player
            AudioManager.playSound(SoundType.JUMP);
        }
    }
}
