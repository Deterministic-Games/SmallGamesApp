using System.Windows;
using System.Windows.Controls;

namespace SmallGamesApp.Controls;
/// <summary>
/// Interaction logic for GameListItemControl.xaml
/// </summary>
public partial class GameListItemControl : UserControl
{
    public GameListItemControl()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        MessageBox.Show("Hi");
    }
}
