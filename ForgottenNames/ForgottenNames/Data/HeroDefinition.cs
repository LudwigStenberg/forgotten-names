namespace ForgottenNames
{
    /// <summary>
    /// Defines all the data needed to create a custom hero.
    /// One instance per hero — Bjarne, the next hero, etc.
    /// This is a pure data container — no game logic.
    /// </summary>
    public class HeroDefinition
    {
        // ── Identity ──
        // From: spspecialcharacters.xml --> id, name, age, culture, occupation
        // From: heroes.xml --> text (encyclopedia)
        public string HeroId { get; set; }            // e.g. "fn_bjarne_the_still"
        public string FirstName { get; set; }          // e.g. "Bjarne"
        public string FullName { get; set; }           // e.g. "Bjarne the Still"
        public bool IsFemale { get; set; }             // from: is_female attribute
        public int Age { get; set; }                   // from: age attribute
        public string CultureId { get; set; }          // from: culture="Culture.nord"

        // ── Appearance ──
        // From: spspecialcharacters.xml --> face_key_template (but we use specific codes)
        public string BodyProperties { get; set; }     // the full BodyProperties XML string
        public float Weight { get; set; }              // body weight
        public float Build { get; set; }               // body build

        // ── Personality Traits ──
        // From: spspecialcharacters.xml --> Traits block
        // Range: -2 to 2 for each
        public int TraitCalculating { get; set; }
        public int TraitGenerosity { get; set; }
        public int TraitHonor { get; set; }
        public int TraitMercy { get; set; }
        public int TraitValor { get; set; }

        // ── Skills ──
        // From: spspecialcharacters.xml --> skill_template (but we define directly)
        // Dictionary of skill name --> value
        public Dictionary<string, int> Skills { get; set; }

        // ── Equipment ──
        // From: spspecialcharacters.xml --> Equipments block
        // Item IDs for battle and civilian loadouts
        public List<string> BattleEquipment { get; set; }   // e.g. ["Item.nordic_axe", "Item.round_shield"]
        public List<string> CivilianEquipment { get; set; }  // tavern outfit

        // ── Lore ──
        // From: heroes.xml --> text attribute
        public string EncyclopediaEntry { get; set; }    // full encyclopedia text
        public string ShortDescription { get; set; }     // one-liner for lists

        // ── Recruitment / Spawn ──
        // Not in vanilla XML — our custom system
        public List<string> SpawnLocations { get; set; }   // town IDs for Location-Pool
        public List<string> ExcludedFactions { get; set; }
        public int CharmOverride { get; set; }               // 0 = no charm gate

        // ── Dialogue ──
        // Not in vanilla XML in this form — our custom phased system
        public string VoiceType { get; set; } // from: voice attribute ("curt", "softspoken", "earnest")
        // Actual dialogue lines handled by a separate dialogue class

        public HeroDefinition()
        {
            Skills = new Dictionary<string, int>();
            BattleEquipment = new List<string>();
            CivilianEquipment = new List<string>();
            SpawnLocations = new List<string>();
            ExcludedFactions = new List<string>();
        }
    }
}
