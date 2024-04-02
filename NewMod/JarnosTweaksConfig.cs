using System; 
using PhoenixPoint.Modding;

namespace JarnosTweaks
{
	/// <summary>
	/// ModConfig is mod settings that players can change from within the game.
	/// Config is only editable from players in main menu.
	/// Only one config can exist per mod assembly.
	/// Config is serialized on disk as json.
	/// </summary>
	public class JarnosTweaksConfig : ModConfig
	{

		/*
        /// Only public fields are serialized.
        /// Supported types for in-game UI are:
        public int IntegerValue;
        public float FloatValue;
        public bool BoolValue;

        public enum CustomEnum
        {
            A, B, C
        }
        public CustomEnum CustomEnumValue;
        */

		[ConfigField(text: "Maximum stat edit",
			description: "Allows editing the maximum stat of the players soldiers (Default false)")]
		public bool maxstat = false;

		[ConfigField(text: "If the above is true, sets Maximum Strength",
			description: "Vanilla default is 35")]
		public int MaxStrength = 50;

		[ConfigField(text: "If the above is true, sets Maximum Willpower",
			description: "Vanilla default is 20")]
		public int MaxWill = 50;

		[ConfigField(text: "If the above is true, sets Maximum Speed",
			description: "Vanilla default is 20")]
		public int MaxSpeed = 50;

	}
}
