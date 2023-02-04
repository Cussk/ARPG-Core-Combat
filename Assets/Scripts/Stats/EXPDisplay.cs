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
            playerEXP = GameObject.FindWithTag("Player").GetComponent<Experience>();
        }

        private void Update()
        {
            EXPText.text = $"EXP: {playerEXP.GetPoints():F0}";
        }
    }
}

