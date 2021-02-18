using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using Semver;
using Newtonsoft.Json;
using log4net;

namespace SlimeLoader.Loader {
	public static class ModLoader {
		public static readonly ILog log = LogManager.GetLogger(typeof(ModLoader));

		public static List<Mod> Mods { get; internal set; }

		public static void Init() {
			LoadMods(Path.Combine(new string[] { Environment.CurrentDirectory, "../Mods" }));
			// load game assembly
			
			// invoke "GameLoad" mod entrypoints
		}

		internal static void LoadMods(string modsPath) {
			string[] dir = Directory.GetFiles(modsPath);

			// iterate through mod dlls in mods dir
			foreach (string modFile in dir) {
				Mod mod = new Mod(ReadModInfo(modFile).Result);
				Mods.Add(mod);
				// invoke "PreLoad" mod entrypoint
			}
			
			// handle mod dependencies
			
			// find and load mixins into an array (don't patch them yet)
			
			// generate current audit log based on mixins loaded
			
			// check if mixin audit log matches current audit log
			
			// if not,
			
			//   patch mixins
			
			// invoke "Load" mod entrypoints
		}

		// asynchronously read and parse "mod.slimeloader.json" in assembly "<modFile>"
		private static async Task<ModInfo> ReadModInfo(string modFile) {
			Assembly modAsm = Assembly.LoadFrom(modFile);
			FileStream modConfigFile = modAsm.GetFile("mod.slimeloader.json");
			byte[] modConfigData = new byte[modConfigFile.Length];

			await modConfigFile.ReadAsync(modConfigData, 0, modConfigData.Length);

			string modConfigJson = Encoding.Default.GetString(modConfigData);
			return JsonConvert.DeserializeObject<ModInfo>(modConfigJson);
		}
	}
}
