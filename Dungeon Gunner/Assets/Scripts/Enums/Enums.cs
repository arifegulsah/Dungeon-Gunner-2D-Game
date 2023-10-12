public enum Orientation
{
    north,
    east,
    south,
    west,
    none
}

public enum AimDirection 
{
    Up,
    UpRight,
    UpLeft,
    Right,
    Left,
    Down
}

public enum GameState
{
    gameStarted,
    playingLevel,
    engagingEnemies,
    bossState,
    engagingBoss,
    levelCompleted,
    gameWon,
    gameLost,
    gamePaused,
    dungeonOverviewMap,
    restartGame
}