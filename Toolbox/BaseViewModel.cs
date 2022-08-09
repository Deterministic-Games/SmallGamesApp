using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Toolbox;

public class BaseViewModel : INotifyPropertyChanged
{
    #region Propety Changed logic
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? name = null) => 
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    #endregion
}