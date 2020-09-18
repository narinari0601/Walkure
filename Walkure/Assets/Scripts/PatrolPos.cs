﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPos : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0, 0, 0.5f);
        Gizmos.DrawSphere(transform.position, 0.1f);
    }
}
