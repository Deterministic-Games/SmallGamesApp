using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace SmallGamesApp.MVVMToolkit.TicTacToe;

public class Node
{
    public SquareState[] state;
    public int player;
    public int depth;
    public float value;
    public Flag flag;

    public Node(SquareState[] state, int player, int depth)
    {
        this.state = state;
        this.player = player;
        this.depth = depth;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null or not Node) return false;

        var other = (Node) obj;

        return state.SequenceEqual(other.state)
            && other.player == player
            && other.depth == depth;
    }

    public override int GetHashCode() => (depth + state[0].GetHashCode() + state[^1].GetHashCode()) * player;
}
