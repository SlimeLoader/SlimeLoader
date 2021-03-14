using System;
using Newtonsoft.Json;

namespace SlimeLoader.Loader {
	public class ModInfo {
#pragma warning disable RECS0122 // Initializing field with default value is redundant
		[JsonIgnore]
		public static readonly int currentVersion = 0;
#pragma warning restore RECS0122 // Initializing field with default value is redundant
		
		[JsonRequired]
		internal int format;
		[JsonRequired]
		internal string id;
		internal string name;
		[JsonRequired]
		internal string description;
		[JsonRequired]
		internal string version;
		[JsonRequired]
		internal string entrypoint;
		internal string[] mixinConfig = { };
		internal ModDependenciesDef dependencies;
	}
}
