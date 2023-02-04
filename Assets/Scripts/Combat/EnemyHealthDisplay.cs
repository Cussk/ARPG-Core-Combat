using RPG.Attributes;
using TMPro;
using UnityEngine;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI enemyHealthText;

        private Fighter fighter;

        private void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }

        private void Update()
        {
            if (fighter.GetTarget() == null)
            {
                enemyHealthText.text = "Enemy: N/A";
                return;
            }

            Health health = fighter.GetTarget();
            enemyHealthText.text = $"Enemy: {health.GetPercentage():F0}%";

        }
    }
}