# Undo-Redo feature in Unity
A simple Undo-Redo feature that I made myself for Unity3D Engine.<br>
Made in version 2019.2.2f1.

You can create/destroy the cubes, and drag them around.<br>
Then Undo/Redo the actions that you did on the cubes.

**Controls:**
- `Delete` -> Deletes the currently selected object.
- `Left-Click` -> Click *and hold* on an object to select it and drag around; Let go to unselect the object.
- `Z` -> Undo the last performed action
- `Y` -> Redo the last undone action

Use the button on the bottom left to create a new cube.<Br>
Controls are adjustable inside `PlayerController.cs`.
