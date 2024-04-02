using Base.Levels;
using Base.Core;
using Base.Defs;
using HarmonyLib;
using PhoenixPoint.Common.Core;
using PhoenixPoint.Common.Game;
using PhoenixPoint.Common.Entities;
using PhoenixPoint.Common.Entities.Characters;
using PhoenixPoint.Geoscape.Entities;
using PhoenixPoint.Modding;
using PhoenixPoint.Tactical.Entities.Abilities;
using PhoenixPoint.Tactical.Entities.Equipments;
using PhoenixPoint.Tactical.Entities.Weapons;
using System.Linq;
using UnityEngine;
using System;

namespace JarnosTweaks
{
	/// <summary>
	/// This is the main mod class. Only one can exist per assembly.
	/// If no ModMain is detected in assembly, then no other classes/callbacks will be called.
	/// </summary>
	public class JarnosTweaksMain : ModMain
	{

		public static readonly DefRepository Repo = GameUtl.GameComponent<DefRepository>();
		/// Config is accessible at any time, if any is declared.
		public new JarnosTweaksConfig Config => (JarnosTweaksConfig)base.Config;
		public override bool CanSafelyDisable => true;
		public JarnosTweaksMain Main { get; private set; }

		/// This property indicates if mod can be Safely Disabled from the game.
		/// Safely sisabled mods can be reenabled again. Unsafely disabled mods will need game restart ot take effect.
		/// Unsafely disabled mods usually cannot revert thier changes in OnModDisabled



        /// <summary>
        /// Callback for when mod is enabled. Called even on game starup.
        /// </summary>
        public override void OnModEnabled() 
			{

			/// All mod dependencies are accessible and always loaded.
			int c = Dependencies.Count();
			/// Mods have their own logger. Message through this logger will appear in game console and Unity log file.
			Logger.LogInfo($"Say anything crab people-related.");
			/// Metadata is whatever is written in meta.json
			string v = MetaData.Version.ToString();
			/// Game creates Harmony object for each mod. Accessible if needed.
			HarmonyLib.Harmony harmony = (HarmonyLib.Harmony)HarmonyInstance;
			/// Mod instance is mod's runtime representation in game.
			string id = Instance.ID;
			/// Game creates Game Object for each mod. 
			GameObject go = ModGO;
			/// PhoenixGame is accessible at any time.
			PhoenixGame game = GetGame();

			this.OnConfigChanged();

			/// Apply any general game modifications.
			Main = this;

			try
			{
				((HarmonyLib.Harmony)HarmonyInstance).PatchAll(GetType().Assembly);
			}
			catch (Exception e)
			{
				Logger.LogInfo($"{e.ToString()}");
			}

		}
		//here ends the new

		/// Apply any general game modifications.


		/// <summary>
		/// Callback for when mod is disabled. This will be called even if mod cannot be safely disabled.
		/// Guaranteed to have OnModEnabled before.
		/// </summary>
		public override void OnModDisabled() {

			try { ((HarmonyLib.Harmony)HarmonyInstance).UnpatchAll(((Harmony)base.HarmonyInstance).Id); }
			catch (Exception e)
			{
				Logger.LogInfo($"{e.ToString()}");
			}
			//
			Main = null;
		}
		/// Undo any game modifications if possible. Else "CanSafelyDisable" must be set to false.
		/// ModGO will be destroyed after OnModDisabled.
	

		/// <summary>
		/// Callback for when any property from mod's config is changed.
		/// </summary>
		public override void OnConfigChanged() {
			/// Config is accessible at any time.
			/// 

			//this is new
			if (Config.maxstat == true)
			{
				BaseStatSheetDef baseStatSheetDef1 = Repo.GetAllDefs<BaseStatSheetDef>().FirstOrDefault(a => a.name.Equals("HumanSoldier_BaseStatSheetDef"));
				baseStatSheetDef1.MaxStrength = Config.MaxStrength;
				baseStatSheetDef1.MaxSpeed = Config.MaxSpeed;
				baseStatSheetDef1.MaxWill = Config.MaxWill;
				//			baseStatSheetDef1.Stamina = Config.Stamina     //100;
				//			baseStatSheetDef1.TiredStatusStaminaBelow = 15;
				baseStatSheetDef1.ExhaustedStatusStaminaBelow = 0;
			}
		}

	
	

	/// <summary>
	/// In Phoenix Point there can be only one active level at a time. 
	/// Levels go through different states (loading, unloaded, start, etc.).
	/// General puprose level state change callback.
	/// </summary>
	/// <param name="level">Level being changed.</param>
	/// <param name="prevState">Old state of the level.</param>
	/// <param name="state">New state of the level.</param>
	public override void OnLevelStateChanged(Level level, Level.State prevState, Level.State state) {
			/// Alternative way to access current level at any time.
			Level l = GetLevel();
		}

		/// <summary>
		/// Useful callback for when level is loaded, ready, and starts.
		/// Usually game setup is executed.
		/// </summary>
		/// <param name="level">Level that starts.</param>
		public override void OnLevelStart(Level level) {
		}

		/// <summary>
		/// Useful callback for when level is ending, before unloading.
		/// Usually game cleanup is executed.
		/// </summary>
		/// <param name="level">Level that ends.</param>
		public override void OnLevelEnd(Level level) {
		}
	}
}
