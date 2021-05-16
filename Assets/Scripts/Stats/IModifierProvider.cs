using System.Collections.Generic;

namespace RPG.Stats
{
    public interface IModifierProvider
    {
        IEnumerable<float> GetAdditiveModifiers(Stat _stat);
        IEnumerable<float> GetPercentageModifiers(Stat _stat);
    }
}
