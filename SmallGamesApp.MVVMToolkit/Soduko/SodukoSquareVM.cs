using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace SmallGamesApp.MVVMToolkit;

public partial class SodukoSquareVM : ObservableObject
{
    [ObservableProperty]
    private SodukoNumber _number = SodukoNumber.None;

    public ObservableCollection<SodukoNumber> Notes { get; set; } = new() 
    { 
        SodukoNumber.One,
        SodukoNumber.Two,
        SodukoNumber.Three,
        SodukoNumber.Four,
        SodukoNumber.Five,
        SodukoNumber.Six,
        SodukoNumber.Seven,
        SodukoNumber.Eight,
        SodukoNumber.Nine
    };
}
