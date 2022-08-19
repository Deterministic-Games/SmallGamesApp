using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Minesweeper.Models;

public class SquareModel
{
    #region Properties

    public bool HasMine { get; set; }
    public bool IsFlagged { get; set; }
    public bool IsOpened { get; set; }
    public byte NeighbourMineCount { get; set; }

    #endregion

    public static SquareModel CreateSquare() => new();
}