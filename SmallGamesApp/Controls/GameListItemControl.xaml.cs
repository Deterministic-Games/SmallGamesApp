using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace SmallGamesApp.Controls;
/// <summary>
/// Interaction logic for GameListItemControl.xaml
/// </summary>
public partial class GameListItemControl : UserControl
{
    public bool IsSelected { get; set; }
    public GameListItemControl()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        
    }
}
