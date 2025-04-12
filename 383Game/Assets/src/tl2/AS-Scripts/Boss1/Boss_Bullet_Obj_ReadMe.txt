ReadMe file for Boss_Bullet_Obj.prefab

This prefab is used for the boss projectiles for Boss1, containing components for movement, object spawning, aim, and collision detection.

Components:

1. Sprite Renderer
	Displays the bullet sprite

2. Rigidbody 2D
	Manages the physics of the bullet
	Gravity and mass set to near 0 values to allow the bullet to travel in a straight line

3. Capsule Collider 2D
	Provides collision boundaries for the bullet

4. Boss1Bullet.cs
	Script to control initial values of the bullet
	Also controls the bullet's aim towards the player
	Sets the speed of the bullet

Setup:

Make sure your attack script has a public GameObject to hold the bullet prefab. Use the Instantiate function to spawn the bullet when needed.

Requirements:

Unity Editor 6000.0.33f1