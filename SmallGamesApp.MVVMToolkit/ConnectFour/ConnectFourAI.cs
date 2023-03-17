using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallGamesApp.MVVMToolkit;

public class ConnectFourAI
{
    private ConnectFourBoardVM _state;

    public ConnectFourAI(ConnectFourBoardVM state)
    {
        _state = state.Copy();
    }
}

