using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] Transform target;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MoveToCursor();
        }

        UpdateAnimator();
    }

    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;

        bool rayHasHit = Physics.Raycast(ray, out raycastHit);

        if (rayHasHit)
        {
            GetComponent<NavMeshAgent>().destination = raycastHit.point;
        }

        
    }

    private void UpdateAnimator()
    {
        //take global velocity
        Vector3 navMeshAgentVelocity = GetComponent<NavMeshAgent>().velocity;
        //convert to local velocity
        Vector3 localVelocity = transform.InverseTransformDirection(navMeshAgentVelocity);
        float speed = localVelocity.z;

        GetComponent<Animator>().SetFloat("forwardSpeed", speed);
    }
}
