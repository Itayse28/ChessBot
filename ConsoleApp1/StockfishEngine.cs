using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace ConsoleApp1
{
    public class StockfishEngine
    {
        private Process stockfish;
        private StreamWriter input;
        private StreamReader output;

        public void StartEngine(string pathToExe)
        {
            stockfish = new Process();
            stockfish.StartInfo.FileName = pathToExe;
            stockfish.StartInfo.UseShellExecute = false;
            stockfish.StartInfo.RedirectStandardInput = true;
            stockfish.StartInfo.RedirectStandardOutput = true;
            stockfish.StartInfo.CreateNoWindow = true;
            stockfish.Start();

            input = stockfish.StandardInput;
            output = stockfish.StandardOutput;

            SendCommand("uci");
            WaitFor("uciok");
        }

        public void StopEngine()
        {
            SendCommand("quit");
            stockfish.Close();
        }

        private void SendCommand(string command)
        {
            input.WriteLine(command);
            input.Flush();
        }

        private void WaitFor(string expected)
        {
            string line;
            while ((line = output.ReadLine()) != null)
            {
                if (line.Contains(expected)) break;
            }
        }

        public string GetBestMove(string fen, int depth = 15)
        {
            SendCommand($"position fen {fen}");
            SendCommand($"go depth {depth}");

            string line;
            while ((line = output.ReadLine()) != null)
            {
                if (line.StartsWith("bestmove"))
                {
                    return line.Split(' ')[1];
                }
            }
            return null;
        }
    }
}
