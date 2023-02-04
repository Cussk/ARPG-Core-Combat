using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI playerHealthText;

        private Health playerHealth;

        private void Awake()
        {
            playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>();
        }

        private void Update()
        {   
            playerHealthText.text = $"Health: {playerHealth.GetPercentage():F0}%";
        }
    }
}
