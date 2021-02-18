using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using Newtonsoft.Json;
using log4net;
using SlimeLoader.Loader.Entrypoint;

namespace SlimeLoader.Loader {
	// TODO: convert pseudocode
	public static class ModLoader {
		public static readonly ILog log = LogManager.GetLogger(typeof(ModLoader));

		public static List<Mod> Mods { get; internal set; }

		public static void Init() {
			LoadMods("../../Mods");
			// load game assembly
			try {
				var gameAsm = Assembly.LoadFrom("Assembly-CSharp.slimeloader.dll");
			} catch (DllNotFoundException) {
				log.Fatal("Could not find Game DLL (Assembly-CSharp.slimeloader.dll)!");
				log.Info("Help: is the SlimeLoader.dll file and Assembly-CSharp.slimeloader.dll in the \"<game directory>/Managed\" directory?");
				Environment.Exit(-1);
			}
			// invoke "GameLoad" mod entrypoints
		}

		internal static void LoadMods(string modsPath) {
			string[] dir = Directory.GetFiles(modsPath);

			// iterate through mod dlls in mods dir
			foreach (string modFile in dir) {
				var modAsm = Assembly.LoadFrom(modFile);
				var mod = new Mod(ReadModInfo(modFile).Result, modAsm);
				Mods.Add(mod);
				Console.WriteLine($"Loading mod assembly {modFile}");
				// invoke "Initialize" mod entrypoint
				try {
					var type = modAsm.GetType(mod.info.entrypoint);
					if (!type.IsSubclassOf(typeof(ModEntrypoint))) {
						throw new InvalidModEntrypointException(mod.Id);
					}
					type.GetMethod("Initialize").Invoke(null, new object[] { });
				} catch (TypeLoadException) {
					throw new InvalidModEntrypointException(mod.Id);
				}
			}

			// handle mod dependencies

			// find and load mixins into an array (don't patch them yet)

			// generate current audit log based on mixins loaded

			// check if mixin audit log matches current audit log

			// if not,

			//  patch mixins
		}

		// asynchronously read and parse "mod.slimeloader.json" in assembly "<modFile>"
		private static async Task<ModInfo> ReadModInfo(string modFile) {
			var modAsm = Assembly.LoadFrom(modFile);
			var modConfigFile = modAsm.GetFile("mod.slimeloader.json");
			byte[] modConfigData = new byte[modConfigFile.Length];

			await modConfigFile.ReadAsync(modConfigData, 0, modConfigData.Length);

			string modConfigJson = Encoding.Default.GetString(modConfigData);
			return JsonConvert.DeserializeObject<ModInfo>(modConfigJson);
		}
	}
}
