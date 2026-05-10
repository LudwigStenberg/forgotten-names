using System.Collections.Generic;
using TaleWorlds.CampaignSystem;

namespace ForgottenNames.Data
{
    public static class HeroRegistry
    {
        private static readonly Dictionary<string, Hero> _heroesById = new Dictionary<string, Hero>();

        public static void Register(string heroId, Hero hero) => _heroesById[heroId] = hero;
        public static Hero Get(string heroId) => _heroesById.TryGetValue(heroId, out var hero) ? hero : null;
        public static void Clear() => _heroesById.Clear();
    }
}
