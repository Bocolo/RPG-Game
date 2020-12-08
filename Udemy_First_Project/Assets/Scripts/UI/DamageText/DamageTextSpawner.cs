using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI.DamageText
{
    public class DamageTextSpawner : MonoBehaviour
    {

      
        [SerializeField] DamageText damageText = null;

        
        public void SpawnText(float damageAmount)
        {
            DamageText instance = Instantiate<DamageText>(damageText, transform);
            instance.SetValue(damageAmount);
        }
    }
}
