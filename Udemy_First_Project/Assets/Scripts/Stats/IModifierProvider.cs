using System.Collections.Generic;

namespace RPG.Stats
{
    public interface IModifierProvder
    {
        IEnumerable<float> GetAdditiveModifiers(Stat stat);
        IEnumerable<float> GetPercentageModifiers(Stat stat);
    }
}