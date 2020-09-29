using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBullet : MonoBehaviour
{

    [SerializeField, Header("弾速")]
    private float speed = 1.0f;

    private Vector3 velocity;

    [SerializeField, Header("ダメージ")]
    private float attackDamage = 1;
    

    // Start is called before the first frame update
    void Start()
    {
        //velocity = new Vector3(0, 0, 0.03f);
    }

    public void Initialize(Vector3 directionVector)
    {

        velocity = directionVector.normalized * speed * 0.01f;
        
    }


    private void FixedUpdate()
    {
        transform.position += velocity;

        //画面外に出たら
        //if (!objectRenderer.isVisible)
        //{
        //    Destroy(gameObject);
        //}

    }

    //画面外に出た瞬間呼び出される
    //Sceneビューも判定に入るので注意
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    //private void OnWillRenderObject()
    //{
    //    if (Camera.current.name != "SceneCamera" && Camera.current.name != "Preview Camera")
    //    {
    //        //Destroy(gameObject);
    //        Debug.Log(true);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        var obj = other.gameObject;

        if (obj.tag == "Enemy")
        {
            var enemy = obj.GetComponent<Enemy>();

            enemy.AttackedSpecialBullet(attackDamage);

            enemy.DamageEffect(attackDamage);
            
        }
    }
}
