# Main idea
The game is an Ultrakill inspired rouguelike PvE shooter.
The playspace consist of several rooms in a giant spaceship. Each room is a separate level, where the player can deploy into. Rooms contain enemies which damage the room. If the rooms gets a critical damage - it detaches, and become no longer deployable. Also some rooms can produce the ammunitions of different types for different weapons, that a player has.
The goal of the player is to clear the levels as fast as possible, because the number of enemies on each level grows constatnly.

# Game loop
The game loop contains of three looping steps: 
- Deploy in a room
- Clear the room
- Upgrade and buy weapons
If there are no rooms left - the game is lost.

# Economy
Player receives credits after clearing a level based on the number of killed enemies + the basic fee for clearing a level. Player can spend the credits to unlock new weapon types.
# Content
## Weapons
- *Pistol* - Raycast shooting. Has infinite ammo.
- *Rifle* - Raycast shooting. Automatic shooting. Rifle ammo.
- *Minigun* - Raycast shooting. Ascending fire rate. Minigun ammo.
- *Railgun* - Raycast shooting. Railgun ammo. 
- *Rocket launcher* - Prefab shooting. Explosive repelling rockets .Rockets ammo.
## Enemies
- *Eyeball* - shoots damaging projectiles
- *Melee Crystal* - dealing damage with melee attacks
- *else under discussion...*
## Room modifiers
- *Rifle ammo factory*
- *Minigun ammo factory*
- *Railgun ammo factory*
- *Rocket launcher ammo factory*
