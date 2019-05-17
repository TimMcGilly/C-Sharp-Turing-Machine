using System;
using System.IO;

namespace TuringMachine1
{
    static class Program
    {
        static void Main(string[] args)
        {
            Tape tape = new Tape("1100001".ToCharArray(), 0);
            State[] states = State.ReadStatesCSV(@"../../../../BitInverseState.csv");

            foreach (var state in states)
            {
                Console.WriteLine(state);
            }
            Console.WriteLine();
            Console.WriteLine(tape);
            Console.WriteLine();
            Console.WriteLine(RunTuringMachine(tape,states));

        }

        static string RunTuringMachine(Tape tape, State[] states)
        {
            int stateNum = 1;
            State matchingState;

            while(stateNum != 0)
            {
                matchingState = State.GetNextState(states, stateNum, tape.Read());
                tape.Write(matchingState.Write);
                tape.Move(matchingState.Movement);

                stateNum = matchingState.NextState;

                Console.WriteLine(tape);
            }

            return tape.ReadWholeTape();
        }
    }
}
