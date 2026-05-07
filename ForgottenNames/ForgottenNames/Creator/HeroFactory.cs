using ForgottenNames.Data;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;

namespace ForgottenNames.Creator
{
    public static class HeroFactory
    {
        public static void CreateHeroes()
        {
            List<HeroDefinition> heroes = HeroRoster.GetAllHeroes();

            foreach (HeroDefinition hero in heroes)
            {
                var newHero = CreateHero(hero);

            }
        }
    
        private static Hero CreateHero(HeroDefinition hero)
        {
            var template = MBObjectManager.Instance.GetObject<CharacterObject>(hero.TemplateId);

            var newHero = HeroCreator.CreateSpecialHero(template, null, null, null, hero.Age);

           
            newHero.SetName(new TextObject(hero.FullName), new TextObject(hero.FirstName));
            newHero.IsFemale = hero.IsFemale;

            var culture = MBObjectManager.Instance.GetObject<CultureObject>(hero.CultureId);
            newHero.Culture = culture;

            var settlement = MBObjectManager.Instance.GetObject<Settlement>(hero.BornSettlement);
            newHero.BornSettlement = settlement;

            // TODO: convert to StaticBodyProperties object
            newHero.StaticBodyProperties = hero.StaticBodyProperties;
            
        }
    }
}
