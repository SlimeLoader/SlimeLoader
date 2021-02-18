using System;

namespace SlimeLoader.Loader.Entrypoint {
	public class InvalidModEntrypointException : Exception {
		public InvalidModEntrypointException(string modId) : base($"Mod \"{modId}\" has an invalid entrypoint") {
		}
	}
}
