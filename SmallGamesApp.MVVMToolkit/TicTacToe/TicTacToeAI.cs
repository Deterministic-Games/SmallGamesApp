using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SmallGamesApp.MVVMToolkit.TicTacToe;

public class TicTacToeAI
{
    private HashSet<Node> _transpositionTable = new();

    private float Negamax(Node node, int depth, float alpha, float beta, int player)
    {
        float alphaOrig = alpha;

        if (_transpositionTable.TryGetValue(node, out var entry) && entry.depth >= depth)
        {
            switch (entry.flag)
            {
                case Flag.LOWERBOUND:
                    alpha = Math.Max(alpha, entry.value);
                    break;
                case Flag.UPPERBOUND:
                    beta = Math.Min(beta, entry.value);
                    break;
                case Flag.EXACT:
                    return entry.value;
                default:
                    break;
            }
            if (alpha >= beta)
            {
                return entry.value;
            }
        }
        if (depth == 0)
        {
            return player * Heuristic(node);
        }
        var children = GenerateMoves(node);

        float value = float.NegativeInfinity;

        foreach (var child in children)
        {
            value = MathF.Max(value, -Negamax(child, depth - 1, -beta, -alpha, -player));
            alpha = MathF.Max(alpha, value);

            if (alpha >= beta)
            {
                break;
            }
        }
        node.value = value;

        if (value <= alphaOrig)
        {
            node.flag = Flag.UPPERBOUND;
        }
        else if (value >= beta)
        {
            node.flag = Flag.LOWERBOUND;
        }
        else
        {
            node.flag = Flag.EXACT;
        }
        node.depth = depth;
        _ = _transpositionTable.Add(node);

        return value;
    }

    private List<Node> GenerateMoves(Node node)
    {
        var children = new List<Node>();

        var state = node.state;
        var player = -node.player;
        var newState = new SquareState[state.Length];
        state.CopyTo(newState, 0);

        for (int i = 0; i < state.Length; i++)
        {
            if (state[i] == SquareState.Empty)
            {
                continue;
            }
            var square = player > 0 ? SquareState.Player1 : SquareState.Player2;
            newState[i] = square;
            children.Add(new(newState, player, node.depth - 1));
        }
        return children;
    }

    private float Heuristic(Node node) => node.state.Where(square => square == SquareState.Empty).Count();
}
