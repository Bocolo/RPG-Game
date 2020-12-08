using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI.DamageText
{
    public class DamageTextSpawner : MonoBehaviour
    {

        //float spawn;
        [SerializeField] DamageText damageText;

        private void Start()
        {
            SpawnText(11);
        }
        public void SpawnText(float damageAmount)
        {
            DamageText instance = Instantiate<DamageText>(damageText, transform);
        }
    }
}
