# AudioManager

This feature controls the audio in the game. The `AudioManager.cs` class is a singleton class, which means that you can call its instance directly to play, stop, and control audio.

Usage: `AudioManager.instance.Play("SampleAudioName");`

Note: To use the `AudioManager.cs` class, it should be assigned to a game object on scene index 0. You can add sounds to that game object using the `Sound[]` array under the `AudioManager` script in the inspector. Just increase the array size to add a new sound and fill in the details for that sound.

