# Breakout Project modifications

## Modification Description

I focused on turning the breakout implementation into separate screens and reusable component. 
I added a home, play, level complete, and gameover screens. 
I also added gameplay that increases difficulty when you win (clearing bricks), kind of like pacman it just moves onto the next level. Brick colors are based on health, and a hit flash when a brick takes damage.

I changed how the ball bounces off the paddle. The original adjusted the balls direction based on paddle movement, impact location, and some randomness. My version calculates the bounce from where the ball hits the paddle.

I also changed the controls. The original used the Up arrow to launch the ball and left/right arrows to move the paddle. My version uses Space to launch, and supports both A/D and the arrow keys for movement.

The changes keep the gameplay more consitent.

## Structural Changes

I moved the game flow out of `Game1` and into separate scenes: `Home`, `Play`, `LevelComplete`, and `GameOver`. `Game1` now updates and draws a `SceneManager`, while each scene manages the components it needs.

I added `Scene`, `SceneContext`, and `SceneManager` to my `Core2D` library. `SceneContext` gives scenes access to shared tools, like graphics, content, input, and scene management. This replaces the original pattern of looking up services through `Game.Services`.

I have been refacotring reusable graphics and input types in `Core2D` Along with `Sprite`, `ShapeSprite`, `TextureSprite`, `TextElement`, `Transform2D`, `Trajectory2D`, `InputManager`, and collision utilities. The original used `DrawableSprite`, `DrawableGameComponent`, and `InputHandler`.

I replaced the original `PaddleController` with input handling through `InputManager` in `Paddle`. This lets the paddle not need a reference to the ball. The `Play` scene keeps the ball positioned on the paddle before launch, and the ball handles its own paddle bounce.

For the bricks, I replaced the original `BlockManager` and `MonogameBlock` structure with multiple `Brick`'s that are managed by `ComponentManager<Brick>`. The brick layout is currently built inside `Play`.


## State Changes

I replaced the original `BallState` values, `OnPaddleStart` and `Playing`, with the balls `IsLaunched` property and its `Launch` and `Reset` methods.

I kept the brick states `Normal`, `Hit`, and `Broken`. I health so a brick can take multiple hits before breaking. The `Hit` state also controls the short red flash and prevents the brick from taking damage again during the cool down.

I replaced the original static `Lives`, `Level`, and `Score` fields in `ScoreManager` with a `GameSession`. The session controls score, lives, level, and high score through methods such as `AddScore`, `LifeLost`, and `NextLevel`. 

## Maintainability

I improved maintainability by abstracting the game into scenes and passing refences to those scenes. I had also kept the code dyamic, calling a update on the scenemanager, calls update on the current scene, which calls update on entities its managing