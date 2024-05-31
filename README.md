# Space Shooter Game

Welcome to the Space Shooter Game, a Unity-based arcade shooter where you control a spaceship and fight against waves of enemies. The game utilizes Unity's event system for handling various in-game actions and events.

## Table of Contents

- [Controls](#controls)
- [Gameplay](#gameplay)
- [Events](#events)

## Features

- Classic arcade-style gameplay
- Power-ups
- Score tracking
- Event-driven architecture
- Responsive controls
- Engaging sound effects and graphics

- Controls
Move: Use the arrow keys or AD to navigate your spaceship.
Shoot: Press the spacebar to fire your weapon.
Pause: Press the 'escape' key to pause the game.

- Gameplay
Objective: Destroy as many enemies as possible to achieve a high score.
Enemies: If you hit the enemies in front of you, you will gain 100 points; if you cannot, you will lose 200 points; if they touch you, you will lose your life.
Power-ups: Collect ammo-box bullet capacity increases.

- Events
The game uses Unity's event system to handle interactions and game logic. Key events include:

PlayerDamage: Triggered when the player's spaceship takes damage.
EnemyDestroyed: Triggered when an enemy is destroyed.
PowerUpCollected: Triggered when the player collects a ammo-box.
GameOver: Triggered when the player's spaceship is destroyed.
