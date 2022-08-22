namespace SmallGamesApp.Core.Minesweeper;

public class MinesweeperSquareModel
{
    #region Properties

    public bool HasMine { get; set; }
    public bool IsFlagged { get; set; }
    public bool IsOpened { get; set; }
    public byte NeighbourMineCount { get; set; }

    #endregion

    public static MinesweeperSquareModel CreateSquare() => new();
}