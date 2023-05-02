using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    //interface to access stat modifiers
    public interface IModifierProvider   
    {
        IEnumerable<float> GetAdditiveModifiers(Stat stat);
        IEnumerable<float> GetPercentageModifiers(Stat stat);
    }
}
