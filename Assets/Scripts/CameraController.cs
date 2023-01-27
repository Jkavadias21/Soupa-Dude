using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform soupPlayer;

    void Update()
    {
        transform.position = new Vector3(soupPlayer.position.x,
            soupPlayer.position.y + 2, soupPlayer.position.z - 0.5f);
    }
}
