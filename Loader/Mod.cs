using System;
using System.IO;
using Semver;
using log4net;
using System.Reflection;
using SlimeLoader.Loader.Entrypoint;

namespace SlimeLoader.Loader {
	public class Mod {
		public string Id { get => info.id; }
		public SemVersion Version { get; internal set; }

		internal ModInfo info;
		internal Assembly ModAsm { get; private set; }
		internal ModEntrypoint Entrypoint { get; private set; }

		public Mod(ModInfo info, Assembly modAsm) {
			this.info = info;
			// FIXME: don't initialize the target entrypoint until it's proven to be valid
			try {
				Entrypoint = (ModEntrypoint) modAsm.GetType(info.entrypoint).TypeInitializer.Invoke(null);
			} catch (TypeLoadException) { }

			// parse semver version
			// if invalid semver, print warning
			// else, set "Version" property to "version"
			bool validSemver = SemVersion.TryParse(info.version, out SemVersion version, true);
			
			if (!validSemver) {
				ModLoader.log.Warn($"Mod \"{Id}\" ({info.version}) does not use semver. Version comparison support limited!");
			} else {
				Version = version;
			}
		}
	}
}
