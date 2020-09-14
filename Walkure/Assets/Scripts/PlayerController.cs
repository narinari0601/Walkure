using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody rb;

    [SerializeField, Header("移動速度")]
    private float speed = 1.0f;

    private Vector3 velocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        velocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        //キーボード入力
        float key_x = Input.GetAxisRaw("Horizontal");
        float key_z = Input.GetAxisRaw("Vertical");

        velocity.Set(key_x, 0, key_z);
        velocity = velocity.normalized * speed * Time.deltaTime;
        rb.MovePosition(transform.position + velocity);
    }
}
