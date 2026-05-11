using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using ForgottenNames;
using TaleWorlds.Core;

namespace ForgottenNames.Data
{
    public static class HeroRoster
    {
        public static List<HeroDefinition> GetAllHeroes()
        {
            return new List<HeroDefinition>
            {
                BjarneTheStill()
            };
        }

        private static HeroDefinition BjarneTheStill()
        {
            return new HeroDefinition
            {
                HeroId = "fn_bjarne_the_still",
                TemplateId = "spc_wanderer_nord_0n",
                FirstName = "Bjarne",
                FullName = "Bjarne the Still",
                IsFemale = false,
                Age = 30,
                CultureId = "nord",
                BornSettlementId = "town_N2",
                BodyPropertiesXml = "<BodyProperties version=4 age=30 weight=0.1713 build=0.3241  key=000208029AB453C06A45278753C516B489D76A3B6974290462B7919A598964A302F53F240F356A53083A9B6F6D35A844000000000000001D0000000051E83044  />",
                Weight = 0.5f,
                Build = 0.5f,
                TraitCalculating = 1,
                TraitGenerosity = -1,
                TraitHonor = 1,
                TraitMercy = 1,
                TraitValor = 2,
                Skills = new Dictionary<string, int>
                {
                    { "OneHanded", 185 },
                    { "TwoHanded", 150 },
                    { "Polearm", 140 },
                    { "Bow", 45 },
                    { "Crossbow", 25 },
                    { "Throwing", 110 },
                    { "Riding", 90 },
                    { "Athletics", 185 },
                    { "Crafting", 25 },
                    { "Scouting", 90 },
                    { "Tactics", 160 },
                    { "Roguery", 70 },
                    { "Charm", 45 },
                    { "Leadership", 140 },
                    { "Trade", 30 },
                    { "Steward", 15 },
                    { "Medicine", 35 },
                    { "Engineering", 10 },
                    { "Mariner",  150 },
                    { "Boatswain", 90 },
                    { "Shipmaster", 60 }
                },
                PreferredUpgradeFormation = "Infantry",
                Level = 20,
                // BattleEquipment = // deferred -- using default for testing
                // CivillianEquipment = // deferred -- using default for testing
                Gold = 500,
                EncyclopediaEntry = @"Bjarne, called the Still, is a Nord of the Kjolding people, born to a family of fighting men in
the coastal fjords of Nordvyg. His father served as huscarl to a petty chieftain before dying at sea on a raiding voyage — Bjarne was 
twelve years old. He was raised thereafter by his mother, a woman remembered by those who knew her as practical beyond sentiment.

He came to the huscarl's oath young, selected by a Kjolding Jarl whose name is not recorded
in any southern chronicle.Those who served alongside him describe a fighter of uncommon 
steadiness — not the loudest man in a shield wall, but the one others found themselves 
moving toward when the line began to break. He bore a wound taken in a raid that left a scar
across his face and kept him bedridden with fever in a village some distance from his Jarl's 
hall. On the night he lay there, the hall burned. The Jarl, his household, his huscarls — all
of them gone before morning. The cause, as far as Bjarne has ever learned, remains uncertain.
A rival clan, perhaps. An old debt settled in fire.

He did not return to Kjolding lands after. Those who have encountered him in the ports of Nordvyg 
and along the Sturgian coast describe a man who works hard speaks little, and fights when there is 
cause to fight with a focus that unsettles men expecting rage. He takes contracts without asking too 
many questions and moves on without leaving much behind. What he is waiting for, if anything, he has 
not said. He was found most recently in Hvalvik, crewing a merchant knarr out of the northern fjords.",
                ShortDescription = "A Nord huscarl without a hall. Finds work on the water. Does not speak of what he lost.",
                HiddenInEncyclopedia = false,
                SpawnLocations = new List<string>() { "town_N1" },
                ExcludedFactions = new List<string>(),
                CharmOverride = 0,
                VoiceType = "curt",
            };
        }
    }
}