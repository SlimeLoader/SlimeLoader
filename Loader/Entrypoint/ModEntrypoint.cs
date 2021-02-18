using System;

namespace SlimeLoader.Loader.Entrypoint {
	public interface ModEntrypoint {
		void Initialize();
		void GameLoad();
	}
}
