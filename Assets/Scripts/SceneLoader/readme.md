SceneLoader Feature:

The SceneLoader feature is a script that can be used to load and transition between scenes in a Unity game. It also allows you to control the speed of the transition.

Usage:

Add the SceneLoader script to a GameObject in the first scene of your game. Note that the script only needs to be added once, and it will automatically move between scenes due to the use of DontDestroyOnLoad.
Call the LoadScene or ReloadScene functions by using its instance (singleton) from any other script to move to a new scene or reload the current scene.
Example 1: SceneLoader.instance.LoadScene(SceneLoader.WhichScene.GameScene);
Example 2: SceneLoader.instance.LoadScene(SceneLoader.WhichScene.GameScene, 1.5f);
Use the IsTransitioning function to check whether the scene is currently transitioning or not.


Public Variables:

transitionSpeed : Controls the speed of the transition. The default value is 0.8.
loadingImage : The Image component of the black screen that appears during the transition.


Public Functions:

LoadScene(WhichScene sceneName) : Loads the specified scene by its name. The sceneName parameter should be an enum value of the WhichScene enum.
LoadScene(WhichScene sceneName, float transitioningSpeed) : Same as the previous function, but allows you to specify the speed of the transition.
ReloadScene() : Reloads the current scene.
ReloadScene(float transitioningSpeed) : Same as the previous function, but allows you to specify the speed of the transition.
IsTransitioning() : Returns true if the scene is currently transitioning, false otherwise.


Private Variables:

instance : A static instance of the SceneLoader script. Ensures that there is only one instance of the script.
copyTransSpeed : A copy of the transitionSpeed variable. Used to reset the transition speed after a scene transition.
sceneName : The name of the current scene.
currentAction : An enum value that represents the current state of the transition. Can be none, show, or hide.


Private Functions:

Awake() : Initializes the instance variable and sets the DontDestroyOnLoad flag to ensure that the SceneLoader object persists across scenes.
Update() : Checks the current currentAction and calls either Show() or Hide().
Show() : Increases the alpha value of the black screen's color over time until it is fully visible, then loads the new scene and sets the currentAction to hide.
Hide() : Decreases the alpha value of the black screen's color over time until it is fully transparent, then sets the currentAction to none and disables the black screen.