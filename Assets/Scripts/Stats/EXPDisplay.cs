using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RPG.Stats
{
    public class EXPDisplay : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI EXPText;

        private Experience playerEXP;

        private void Awake()
        {
            //find object with player tag and get its Experience component
            playerEXP = GameObject.FindWithTag("Player").GetComponent<Experience>();
        }

        private void Update()
        {
            //dynamic text generated from player's experience total
            EXPText.text = $"EXP: {playerEXP.GetPoints():F0}";
        }
    }
}

