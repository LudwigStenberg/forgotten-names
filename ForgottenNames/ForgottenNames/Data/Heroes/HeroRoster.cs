using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using ForgottenNames;
using TaleWorlds.Core;

namespace ForgottenNames.Data
{
    public static class HeroRoster
    {
        public static HeroDefinition BjarneTheStill()
        {
            return new HeroDefinition
            {
                HeroId = "fn_bjarne_the_still",
                FirstName = "Bjarne",
                FullName = "Bjarne the Still",
                Age = 30,
                CultureId = 
                BodyProperties =
                Weight = 0.5,
                Build = 0.5,
                TraitCalculating = 1,
                TraitGenerosity = -1,
                TraitHonor = 1,
                TraitMercy = 1,
                TraitValor = 2,
                // Skills = 
                PreferredUpgradeFormation = "Infantry",
                // BattleEquipment =
                // CivillianEquipment =
                EncyclopediaEntry = "entry here or from XML?",
                ShortDescription = "-..-",
                // SpawnLocations
                ExcludedFactions = new List<string>(),
                CharmOverride = 0,
                VoiceType = "What are the options?",
            }
        }
    }
}