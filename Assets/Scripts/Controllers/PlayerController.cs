using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Fighter fighter;
        private Health health;

        private void Start()
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
        }
        private void Update()
        {
            if (health.IsDead()) return;

            if (InteractWithCombat()) return;

            if (InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] listRaycastHit = Physics.RaycastAll(GetMouseRay());

            foreach (RaycastHit raycastHit in listRaycastHit) 
            {
                CombatTarget combatTarget = raycastHit.transform.GetComponent<CombatTarget>();
                if (combatTarget == null) continue;

                if (!fighter.CanAttack(combatTarget.gameObject)) continue;

                if (Input.GetMouseButton(0))
                {
                    fighter.StartAttack(combatTarget.gameObject);
                }
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {  
            RaycastHit raycastHit;

            bool rayHasHit = Physics.Raycast(GetMouseRay(), out raycastHit);

            if (rayHasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(raycastHit.point, 1f);
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
