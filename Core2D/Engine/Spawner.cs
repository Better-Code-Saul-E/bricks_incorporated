using Microsoft.Xna.Framework;

namespace Core2D.Engine;

// remove?
// possibly inherit from IUpdatable, and have spanwer control its own sprite
// intended to only spawn
// abstract into manual spawner, and automated spawner, both inherit from THIS
public abstract class Spawner<T> where T : class
{
    private float timer;

    public float SpawnInterval { get; set; }
    public bool Enabled { get; set; } = true;



    protected Spawner(float spawnInterval)
    {
        SpawnInterval = spawnInterval;
    }



    public T time(GameTime gameTime)
    {
        if (!Enabled || SpawnInterval <= 0f) return null;

        timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (timer < SpawnInterval) return null;

        timer -= SpawnInterval;
        return Spawn();
    }

    protected abstract T Spawn();
}