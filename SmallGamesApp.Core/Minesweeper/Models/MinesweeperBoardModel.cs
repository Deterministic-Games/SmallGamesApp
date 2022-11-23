using System.Text;

namespace SmallGamesApp.Core;

internal sealed class MinesweeperBoardModel
{
    #region Members

    private List<MinesweeperSquareModel> _squares = new();

    #endregion

    #region Properties

    private MinesweeperBoardSize _size;
    public MinesweeperBoardSize Size 
    {
        get => _size;
        set
        {
            _size = value;
            Initialize();
        }
    }

    public int Width { get; set; }
    public int Height { get; set; }
    public int Mines { get; set; }

    public bool HasWon => _squares.All(sqr => (sqr.IsOpened && !sqr.HasMine) || (sqr.HasMine && !sqr.IsOpened));
    public bool HasLost => _squares.Any(sqr => sqr.IsOpened && sqr.HasMine);

    #endregion

    #region Constructor

    public MinesweeperBoardModel(MinesweeperBoardSize size)
    {
        Size = size;
    }

    public MinesweeperBoardModel() 
    {
        Size = MinesweeperBoardSize.Medium;
    }

    #endregion

    #region Initialization
    public void Initialize()
    {
        switch (Size)
        {
            case MinesweeperBoardSize.Small:
                Height = 9;
                Width = 9;
                Mines = 10;
                break;
            case MinesweeperBoardSize.Medium:
                Height = 16;
                Width = 16;
                Mines = 40;
                break;
            case MinesweeperBoardSize.Large:
                Height = 16;
                Width = 30;
                Mines = 99;
                break;
            default:
                break;
        }
        _squares.Clear();

        AddSquares();
        LayMines();
    }

    private void AddSquares()
    {
        for (int i = 0; i < Width * Height; i++)
            _squares.Add(MinesweeperSquareModel.CreateSquare());
    }

    private void LayMines()
    {
        int bombs = Mines;
        while (bombs > 0)
        {
            var index = Random.Shared.Next(0, _squares.Count);

            if (!_squares[index].HasMine)
            {
                _squares[index].HasMine = true;
                bombs--;
            }
        }
    }

    #endregion

    #region Public methods

    public void Restart() => Initialize();

    public List<MinesweeperSquareModel> GetSquares() => _squares;

    public override string ToString()
    {
        var sb = new StringBuilder();

        for (int row = 0; row < Height; row++)
        {
            for (int col = 0; col < Width; col++)
            {
                var square = GetSquare(row, col);

                var content = square.HasMine ? " " : square.NeighbourMineCount.ToString();
                _ = sb.Append($" {content} ");
            }
            _ = sb.Append('\n');
        }
        return sb.ToString();
    }

    #endregion

    #region Private methods

    private MinesweeperSquareModel GetSquare(int row, int col) => _squares[(row * Width) + col];

    #endregion
}
