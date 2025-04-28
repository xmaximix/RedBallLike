

https://github.com/user-attachments/assets/00e09601-3ef7-4de7-a0db-016c64f66ec5

RedBallLike is a compact 2D platformer prototype for Android, inspired by Red Ball 4. It launches from a single Boot scene, uses VContainer for dependency injection and R3 for state management between menu and gameplay, and loads levels via Addressables. Player movement is handled by simple, testable C# classes and driven each physics frame by a FixedTickRunner. Input relies on Unity’s new Input System with on-screen buttons. On death, the player immediately respawns. On level completion, "win" is logged and returns to the menu after a brief delay. UI is set up in the editor for simplicity, but can be instantiated at runtime.
