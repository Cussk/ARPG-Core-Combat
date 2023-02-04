using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RPG.Stats
{
    public class LevelDisplay : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI levelText;

        private BaseStats playerBaseStats;

        private void Awake()
        {
            playerBaseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
        }

        private void Update()
        {
            levelText.text = $"Level: {playerBaseStats.GetLevel():F0}";
        }
    }
}

