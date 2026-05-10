using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.CampaignSystem;

using HarmonyLib;

using ForgottenNames.Creator;
using ForgottenNames.Dialogue;

namespace ForgottenNames
{
    public class SubModule : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();

            // Initialize Harmony for any patches we need
            var harmony = new Harmony("com.forgottennames.patch");
            harmony.PatchAll();
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            base.OnGameStart(game, gameStarterObject);

            if (game.GameType is Campaign)
            {
                CampaignGameStarter campaignStarter = (CampaignGameStarter)gameStarterObject;


                // Register custom behaviors here
                campaignStarter.AddBehavior(new HeroSpawnBehavior());
                campaignStarter.AddBehavior(new BjarneDialogueBehavior());

            }
        }
    }
}
