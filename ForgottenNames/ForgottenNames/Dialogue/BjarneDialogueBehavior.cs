using ForgottenNames.Creator;
using ForgottenNames.Data;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.Party;

namespace ForgottenNames.Dialogue
{
    public class BjarneDialogueBehavior : CampaignBehaviorBase
    {
        private const string BjarneId = "fn_bjarne_the_still";

        // --- Token constants ---
        // Tokens describe conversational STATES, not line numbers.
        // Each one names "the moment in the conversation right after [something happened]".
        private const string TokGreet1Said = "fn_bjarne_greet_1_said";
        private const string TokMainOptions = "fn_bjarne_main_options";

        // "Who are you?" branch
        private const string TokWhoIntro1Said = "fn_bjarne_who_intro_1_said";
        private const string TokWhoIntro2Said = "fn_bjarne_who_intro_2_said";

        // Recruitment branch — Bjarne's three-line speech
        private const string TokRecruit1Said = "fn_bjarne_recruit_1_said";
        private const string TokRecruit2Said = "fn_bjarne_recruit_2_said";
        private const string TokRecruitChallenged = "fn_bjarne_recruit_challenged";  // player picks A/B/C here

        // Recruitment branch — three parallel reply tokens (one per player answer)
        private const string TokRecruitAnswerA = "fn_bjarne_recruit_answer_a";
        private const string TokRecruitAnswerB = "fn_bjarne_recruit_answer_b";
        private const string TokRecruitAnswerC = "fn_bjarne_recruit_answer_c";

        // Engine reserved
        private const string TokStart = "start";
        private const string TokClose = "close_window";


        public override void RegisterEvents()
        {
            CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, OnSessionLaunched);
        }

        public override void SyncData(IDataStore dataStore) { }

        private void OnSessionLaunched(CampaignGameStarter starter)
        {
            AddBjarneDialogue(starter);
        }


        private void AddBjarneDialogue(CampaignGameStarter starter)
        {
            // --- Greeting ------------------------------------------------
            // Two NPC lines back-to-back, ending at the main options hub.

            AddNpcLine(starter,
                id: "fn_bjarne_greet_1",
                input: TokStart,
                output: TokGreet1Said,
                text: "I saw you looking. Most people who look that long either want something or want trouble.",
                condition: IsTalkingToBjarneAsStranger,
                priority: 110);   // beats vanilla wanderer greetings at "start"

            AddNpcLine(starter,
                id: "fn_bjarne_greet_2",
                input: TokGreet1Said,
                output: TokMainOptions,
                text: "Say what you came to say.");


            // --- Main options menu ------------------------------------------------
            // Three player choices, all anchored at TokMainOptions (the hub).

            AddPlayerLine(starter,
                id: "fn_bjarne_player_who",
                input: TokMainOptions,
                output: TokWhoIntro1Said,
                text: "Tell me about yourself.");

            AddPlayerLine(starter,
                id: "fn_bjarne_player_recruit",
                input: TokMainOptions,
                output: TokRecruit1Said,
                text: "I could use someone like you. Ride with me.",
                condition: IsTalkingToBjarneAsStranger);

            AddPlayerLine(starter,
                id: "fn_bjarne_player_leave",
                input: TokMainOptions,
                output: TokClose,
                text: "Never mind.");


            // --- "Who are you?" branch ------------------------------------------------
            // Two NPC lines, then loops back to the menu hub.

            AddNpcLine(starter,
                id: "fn_bjarne_who_intro_1",
                input: TokWhoIntro1Said,
                output: TokWhoIntro2Said,
                text: "My name is Bjarne. I've worked ships up and down this coast for longer than I care to count. Before that I served a jarl whose hall doesn't stand anymore.");

            AddNpcLine(starter,
                id: "fn_bjarne_who_intro_2",
                input: TokWhoIntro2Said,
                output: TokMainOptions,   // loop back to menu
                text: "That's the short version. It's the one I give.");


            // --- Recruitment branch ------------------------------------------------
            // Three NPC lines in sequence, then player picks one of three answers.

            AddNpcLine(starter,
                id: "fn_bjarne_recruit_1",
                input: TokRecruit1Said,
                output: TokRecruit2Said,
                text: "I've had men ask me that before. Most of them I wouldn't follow into a tavern brawl, let alone a campaign.");

            AddNpcLine(starter,
                id: "fn_bjarne_recruit_2",
                input: TokRecruit2Said,
                output: TokRecruitChallenged,
                text: "You seem different. Hard to say why yet. I'll ask you one thing. Not about coin, not about where you're headed. I want to know what kind of man you are when things go wrong. Because things will go wrong.");


            // --- Recruitment — three parallel player answers -------------------

            AddPlayerLine(starter,
                id: "fn_bjarne_recruit_answer_a",
                input: TokRecruitChallenged,
                output: TokRecruitAnswerA,
                text: "I make hard calls. Not always the right ones. But I don't hide from them after.");

            AddPlayerLine(starter,
                id: "fn_bjarne_recruit_answer_b",
                input: TokRecruitChallenged,
                output: TokRecruitAnswerB,
                text: "I win. That's what matters when things go wrong.");

            AddPlayerLine(starter,
                id: "fn_bjarne_recruit_answer_c",
                input: TokRecruitChallenged,
                output: TokRecruitAnswerC,
                text: "Honestly? I improvise and hope for the best.");


            // --- Recruitment — Bjarne's three replies, all close the conversation ---
            // TODO: attach RecruitBjarne consequence to all three.

            AddNpcLine(starter,
                id: "fn_bjarne_recruit_reply_a",
                input: TokRecruitAnswerA,
                output: TokClose,
                text: "Good answer. That's the only kind worth giving. I'll ride with you. Don't make me regret the decision.",
                consequence: RecruitBjarne);
                

            AddNpcLine(starter,
                id: "fn_bjarne_recruit_reply_b",
                input: TokRecruitAnswerB,
                output: TokClose,
                text: "...I've heard that before. Usually from men who hadn't lost yet. We'll see if it holds. I'll ride with you.",
                consequence: RecruitBjarne);

            AddNpcLine(starter,
                id: "fn_bjarne_recruit_reply_c",
                input: TokRecruitAnswerC,
                output: TokClose,
                text: "...At least you're honest about it. Gods help me. Come on then.",
                consequence: RecruitBjarne);
        }


        // --- Helpers — collapse 8-arg API calls into 4-5 args for the common case ---

        private void AddNpcLine(CampaignGameStarter starter, string id, string input, string output, string text,
                                ConversationSentence.OnConditionDelegate condition = null,
                                ConversationSentence.OnConsequenceDelegate consequence = null,
                                int priority = 100)
        {
            starter.AddDialogLine(id, input, output, text, condition, consequence, priority, null);
        }

        private void AddPlayerLine(CampaignGameStarter starter, string id, string input, string output, string text,
                                   ConversationSentence.OnConditionDelegate condition = null,
                                   ConversationSentence.OnConsequenceDelegate consequence = null,
                                   int priority = 100)
        {
            starter.AddPlayerLine(id, input, output, text, condition, consequence, priority, null);
        }


        // --- Conditions ---

        private bool IsTalkingToBjarne()
        {
            Hero bjarne = HeroRegistry.Get(BjarneId);
            return bjarne != null && Hero.OneToOneConversationHero == bjarne;
        }

        private bool IsTalkingToBjarneAsStranger()
        {
            Hero bjarne = HeroRegistry.Get(BjarneId);
            return bjarne != null
                && Hero.OneToOneConversationHero == bjarne
                && !bjarne.IsPlayerCompanion;
        }

        private void RecruitBjarne()
        {
            Hero bjarne = HeroRegistry.Get(BjarneId);
            if (bjarne == null) return;

            // 1. Mark Bjarne as a companion of the player's clan.
            // Must happen before AddHeroToPartyAction so the "joined" notification fires.
            AddCompanionAction.Apply(Clan.PlayerClan, bjarne);

            // 2. Move Bjarne to the player's party
            AddHeroToPartyAction.Apply(bjarne, MobileParty.MainParty);

            // 3. Deduct the hiring cost from the player's gold.
            int cost = Campaign.Current.Models.PartyWageModel
                    .GetTroopRecruitmentCost(bjarne.CharacterObject, Hero.MainHero, false)
                    .RoundedResultNumber;
            GiveGoldAction.ApplyBetweenCharacters(Hero.MainHero, null, cost, false);
        }
    }
}