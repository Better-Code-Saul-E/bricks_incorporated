namespace brick_incorporated.GamePlay;


public class GameSession
{
    public int Score { get; private set; }
    public int Lives { get; private set; } = 3;
    public int Level { get; private set; }
    public int HighScore { get; private set; }
    public bool IsGameOver => Lives <= 0;



    public void AddScore(int points)
    {
        Score += points;

        if (Score > HighScore)
        {
            HighScore = Score;
        }
    }

    public void LoseLife() => Lives--;
    public void NextLevel() => Level++;
}