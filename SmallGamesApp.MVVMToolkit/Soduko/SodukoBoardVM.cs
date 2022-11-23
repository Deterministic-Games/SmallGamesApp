using CommunityToolkit.Mvvm.ComponentModel;

namespace SmallGamesApp.MVVMToolkit;

public partial class SodukoBoardVM : BaseBoardVM<SodukoRegionVM>
{
    [ObservableProperty]
    private bool _isTakingNotes;

    public SodukoBoardVM()
    {
        for (int i = 0; i < 9; i++)
        {
            Squares.Add(new(i));
        }
        Initialize();
    }

    protected override void Initialize()
    {
        
    }
}
