using System;
using System.IO;
using Semver;
using log4net;

namespace SlimeLoader.Loader {
	public class Mod {
		public FileStream File { get; internal set; }
		public string Id { get => info.id; }
		public SemVersion Version { get; internal set; }

		private ModInfo info;

		public Mod(ModInfo info) {
			this.info = info;

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
