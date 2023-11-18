using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame 
    public GameObject player;
    public Vector3 offset;
    public float speed;
    public Transform Transform { get => transform; }
 

    // Update is called once per frame
    void LateUpdate()
    {

        Transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, Time.deltaTime * speed);
    }
}
