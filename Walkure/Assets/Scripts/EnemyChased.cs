using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChased : MonoBehaviour
{
    [SerializeField,Header("移動速度")]
    private float speed = 0.5f;

    private GameObject player;

    private Vector3 vector;


    void Start()
    {
        player = GameObject.Find("Player");

        vector = Vector3.zero;

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnWillRenderObject()
    {
        if (Camera.current.name == "MainCamera")
        {
            Chase();
        }
    }

    private void Chase()
    {
        vector = (player.transform.position - transform.position).normalized;

        transform.position += vector * speed * 0.01f;
    }
}
