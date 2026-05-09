using TaleWorlds.CampaignSystem;

namespace ForgottenNames.Creator
{
    public class HeroSpawnBehavior : CampaignBehaviorBase
    {
        public override void RegisterEvents()
        {
            CampaignEvents.OnNewGameCreatedEvent.AddNonSerializedListener(this, OnNewGameCreated);

            CampaignEvents.OnGameLoadedEvent.AddNonSerializedListener(this, OnGameLoaded);
        }

        public override void SyncData(IDataStore dataStore) { }


       private void OnNewGameCreated(CampaignGameStarter starter)
       {
            HeroFactory.CreateHeroes();
       }

       private void OnGameLoaded(CampaignGameStarter starter)
       {
            HeroFactory.CreateHeroes();
       }
    }
}
