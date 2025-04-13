This is the directory for team lead 5 sources

Box:

Description: This prefab represents a basic box bullet in Unity. It includes essential components for movement, sprites, and collision detection.

Components:

1. EnemyCauseDamage Script
Handles collision behavior between player and box, damages player upon contact.

2. EnemyStats Script
Handles health and base attack damage of box.

3. EnemyBullet Script
Handles movement of bullet, which it travels in a straight line towards the known location of the player upon creation of prefab box object.

4. Sprite Renderer
Displays the Box's sprite.
Ensure that the sorting layer and order in layer are set appropriately to display correctly in your game's environment.


Keep the box prefab in your assets. Instantiate instances of the box bullets at the tip of your gun or cannon in order.
Requirements: Unity 2022.3.42f1 or later



Ground Enemy:
This is the tracking ground enemy that can be dragged and dropped into any scene. The enemy contains scripts called
"walkable" and "ground movement" which allow it to follow the main character and speed up towards the player, overshooting
if the player jumps over the enemy. "enemy stats" and "enemy cause damage" mean that the player can shoot the enemy
with a gun and the enemy damages the player on contact. There's also an animation on it. The walking thanggg.
