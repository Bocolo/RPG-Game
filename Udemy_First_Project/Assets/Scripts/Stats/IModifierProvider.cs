using System.Collections.Generic;

namespace RPG.Stats
{
    public interface IModifierProvder
    {
        IEnumerable<float> GetAdditiveModifier(Stat stat);
    }
}