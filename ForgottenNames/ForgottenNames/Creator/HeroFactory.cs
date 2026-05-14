using System;
using System.Collections.Generic;

using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;
using TaleWorlds.CampaignSystem;
using NavalDLC.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.CharacterDevelopment;

using ForgottenNames.Data;

namespace ForgottenNames.Creator
{
    public static class HeroFactory
    {

        private static readonly Dictionary<string, SkillObject> SkillMap = new Dictionary<string, SkillObject>
        {
            { "OneHanded", DefaultSkills.OneHanded },
            { "TwoHanded", DefaultSkills.TwoHanded },
            { "Polearm", DefaultSkills.Polearm },
            { "Bow", DefaultSkills.Bow },
            { "Crossbow", DefaultSkills.Crossbow },
            { "Throwing", DefaultSkills.Throwing },
            { "Riding", DefaultSkills.Riding },
            { "Athletics", DefaultSkills.Athletics },
            { "Crafting", DefaultSkills.Crafting },
            { "Scouting", DefaultSkills.Scouting },
            { "Tactics", DefaultSkills.Tactics },
            { "Roguery", DefaultSkills.Roguery },
            { "Charm", DefaultSkills.Charm },
            { "Leadership", DefaultSkills.Leadership },
            { "Trade", DefaultSkills.Trade },
            { "Steward", DefaultSkills.Steward },
            { "Medicine", DefaultSkills.Medicine },
            { "Engineering", DefaultSkills.Engineering },
            { "Mariner", NavalSkills.Mariner },
            { "Boatswain", NavalSkills.Boatswain },
            { "Shipmaster", NavalSkills.Shipmaster }
        };


        public static List<Hero> CreateHeroes()
        {
            HeroRegistry.Clear();
            var heroes = new List<Hero>();
            List<HeroDefinition> heroDefs = HeroRoster.GetAllHeroes();

            foreach (HeroDefinition def in heroDefs)
            {
                var existing = Hero.FindFirst(h => h.Name.ToString() == def.FullName);
                Hero hero = existing ?? CreateHero(def);
                HeroRegistry.Register(def.HeroId, hero);
                heroes.Add(hero);
            }

            return heroes;
        }
    
        private static Hero CreateHero(HeroDefinition def)
        {
            var template = MBObjectManager.Instance.GetObject<CharacterObject>(def.TemplateId);

            var hero = HeroCreator.CreateSpecialHero(template, null, null, null, def.Age);

            hero.SetNewOccupation(Occupation.Wanderer); // Tavern companion
            hero.SetName(new TextObject(def.FullName), new TextObject(def.FirstName));
            hero.IsFemale = def.IsFemale;

            var culture = MBObjectManager.Instance.GetObject<CultureObject>(def.CultureId);
            hero.Culture = culture;

            var bornSettlement = MBObjectManager.Instance.GetObject<Settlement>(def.BornSettlementId);
            hero.BornSettlement = bornSettlement;

            BodyProperties bodyProperties;
            if (BodyProperties.FromString(def.BodyPropertiesXml, out bodyProperties))
            {
                hero.StaticBodyProperties = bodyProperties.StaticProperties;
            }

            hero.Weight = def.Weight;
            hero.Build = def.Build;

            hero.SetTraitLevel(DefaultTraits.Calculating, def.TraitCalculating);
            hero.SetTraitLevel(DefaultTraits.Generosity, def.TraitGenerosity);
            hero.SetTraitLevel(DefaultTraits.Honor, def.TraitHonor);
            hero.SetTraitLevel(DefaultTraits.Mercy, def.TraitMercy);
            hero.SetTraitLevel(DefaultTraits.Valor, def.TraitValor);


            foreach (var skill in def.Skills)
            {
                if (SkillMap.TryGetValue(skill.Key, out var skillObject ))
                {
                    hero.SetSkillValue(skillObject, skill.Value);           
                }
                else
                {
                    InformationManager.DisplayMessage(
                        new InformationMessage("[Forgotten Names] Unknown skill: " + skill.Key));
                }
            }

            if (Enum.TryParse(def.PreferredUpgradeFormation, out FormationClass upgradeFormation))
            {
                hero.PreferredUpgradeFormation = upgradeFormation;
            }

            hero.Level = def.Level;

            // BattleEquipment = // deferred -- using default for testing
            // CivillianEquipment = // deferred -- using default for testing

            hero.Gold = def.Gold;

            hero.EncyclopediaText = new TextObject(def.EncyclopediaEntry);

            // Shortdescription -- not sure how this works

            hero.HiddenInEncyclopedia = def.HiddenInEncyclopedia;

            if (def.SpawnLocations.Count > 0)
            {
                var spawnSettlement = MBObjectManager.Instance.GetObject<Settlement>(def.SpawnLocations[0]);
                hero.StayingInSettlement = spawnSettlement;
            }

            hero.ChangeState(Hero.CharacterStates.Active);

            // ExcludedFactions -- custom behavior
            // CharmOverride -- custom behavior
            // VoiceType -- carried from template, needs investigation to override

            return hero;
        }
    }
}
