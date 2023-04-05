using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallGamesApp.MVVMToolkit;

public class ConnectFourAI
{
    public static async Task<int> GetBestMoveAsync(ConnectFourBoardVM state, int depth)
    {
        return await Task.Run(() => GetBestMove(state, depth));
    }

    public static int GetBestMove(ConnectFourBoardVM state, int depth)
    {
        int bestMove = -1;
        int bestScore = int.MinValue;

        for (int col = 0; col < ConnectFourBoardVM.BoardColumns; col++)
        {
            ConnectFourBoardVM newState = state.Copy();
            if (newState.TryPlaceTokenOnColumn(col))
            {
                int score = Minimax(newState, depth, int.MinValue, int.MaxValue, false);
                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = col;
                }
            }
        }

        return bestMove;
    }


    private static int EvaluateGameState(ConnectFourBoardVM state)
    {
        int score = 0;

        int[] weights = { 0, 1, 10, 100, 1000 };

        for (int row = 0; row < ConnectFourBoardVM.BoardRows; row++)
        {
            for (int col = 0; col < ConnectFourBoardVM.BoardColumns; col++)
            {
                var flatIndex = state.FlattenIndex(row, col);
                if (state.Squares[flatIndex].State != SquareState.Empty)
                {
                    var player = (int)state.Squares[flatIndex].State;

                    int[] connectedCounts = new int[4];

                    for (int direction = 0; direction < 4; direction++)
                    {
                        int rowDir = direction == 1 ? 1 : (direction == 2 || direction == 3 ? 1 : 0);
                        int colDir = direction == 0 ? 1 : (direction == 2 ? -1 : (direction == 3 ? 1 : 0));

                        for (int step = 1; step <= 3; step++)
                        {
                            int newRow = row + rowDir * step;
                            int newCol = col + colDir * step;

                            if (newRow >= 0 && newRow < ConnectFourBoardVM.BoardRows && newCol >= 0 && newCol < ConnectFourBoardVM.BoardColumns)
                            {
                                if (state.Squares[flatIndex].State == state.Squares[state.FlattenIndex(row, col)].State)
                                {
                                    connectedCounts[direction]++;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }

                    foreach (int count in connectedCounts)
                    {
                        score += player * weights[count];
                    }
                }
            }
        }
        return score;
    }

    private static int Minimax(ConnectFourBoardVM state, int depth, int alpha, int beta, bool isMaximizing)
    {
        if (depth == 0 || state.Winner != SquareState.Empty)
        {
            return EvaluateGameState(state);
        }

        if (isMaximizing)
        {
            var maxEval = int.MinValue;

            for (int col = 0; col < ConnectFourBoardVM.BoardColumns; col++)
            {
                var newState = state.Copy();

                if (newState.TryPlaceTokenOnColumn(col))
                {
                    var eval = Minimax(newState, depth - 1, alpha, beta, false);

                    maxEval = Math.Max(maxEval, eval);
                    alpha = Math.Max(alpha, eval);

                    if (beta <= alpha)
                    {
                        break;
                    }
                }
            }
            return maxEval;
        }
        else
        {
            var minEval = int.MaxValue;

            for (int col = 0; col < ConnectFourBoardVM.BoardColumns; col++)
            {
                var newState = state.Copy();

                if (newState.TryPlaceTokenOnColumn(col))
                {
                    var eval = Minimax(newState, depth - 1, alpha, beta, true);

                    minEval = Math.Min(minEval, eval);
                    alpha = Math.Min(beta, eval);

                    if (beta <= alpha)
                    {
                        break;
                    }
                }
            }
            return minEval;
        }
    }
}

