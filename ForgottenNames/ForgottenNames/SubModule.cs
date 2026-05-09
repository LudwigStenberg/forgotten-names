using ForgottenNames.Creator;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

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
                
            }
        }
    }
}
