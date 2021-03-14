using System;
using System.Collections.Generic;

namespace SlimeLoader.Loader {
	public class ModDependencies {
		public List<Mod> Requires { get; internal set; }
		public List<Mod> Suggests { get; internal set; }
		public List<Mod> Conflicts { get; internal set; }
		public List<Mod> Breaks { get; internal set; }
	}
}
