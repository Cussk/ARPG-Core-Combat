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
            //get player game object
            playerBaseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
        }

        private void Update()
        {
            //dynamic text player current level
            levelText.text = $"Level: {playerBaseStats.GetLevel():F0}";
        }
    }
}

