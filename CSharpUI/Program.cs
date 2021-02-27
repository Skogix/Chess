using System;

namespace CSharpUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var board = Core.Api.GetBoardAgent;
            board.Log("testar");
        }
    }
}