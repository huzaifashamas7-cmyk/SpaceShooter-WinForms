# Space Shooter (C# WinForms)

A simple 2D space shooter game made using C# and Windows Forms. The player controls
a spaceship, dodges enemy lasers and falling rocks, and tries to destroy the enemy
ship before running out of health.

## How it works

- The player ship is controlled using the arrow keys (Up, Down, Left, Right).
- Pressing Space fires a laser bullet upward from the player's ship.
- The enemy ship moves left and right automatically and fires lasers downward
  at random intervals.
- Rocks fall from the top of the screen as an extra obstacle.
- The player has a health bar (ProgressBar) that decreases by 20 whenever hit
  by enemy fire.
- If the enemy is destroyed or the player's health reaches 0, a "Game End"
  screen appears with the option to restart or exit.

## Controls

- Arrow keys – move the ship
- Spacebar – shoot

## Built with

- C# (.NET Framework, WinForms)
- EZInput library (for keyboard input handling)

## Project structure

- `GameForm1.cs` – main game loop, player/enemy movement, collision detection
- `GameForm3.cs` – game over / restart screen
- `Program.cs` – application entry point
- `space shooter assets/` – sprites and images used in the game

## Notes

This was built as a practice/coursework project to apply Object-Oriented
Programming concepts (classes, collections, event handling) in a real game.
