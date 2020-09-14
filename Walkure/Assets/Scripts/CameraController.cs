using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField, Header("メインカメラ")]
    private GameObject m_Camera = null;

    [SerializeField, Header("プレイヤー")]
    private GameObject player = null;

    [SerializeField, Header("プレイヤーとメインカメラの距離")]
    private Vector3 offset = Vector3.zero;

    private Vector3 mInitPos;

    // Start is called before the first frame update
    void Start()
    {
        mInitPos = player.transform.position + offset;

    }

    // Update is called once per frame
    void Update()
    {
        m_Camera.transform.position = new Vector3(mInitPos.x, mInitPos.y, player.transform.position.z + offset.z);
    }
}
