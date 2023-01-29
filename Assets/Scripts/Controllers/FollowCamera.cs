using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform targetToFollow;

     private void LateUpdate()
    {
        transform.position = targetToFollow.position;
    }
}
