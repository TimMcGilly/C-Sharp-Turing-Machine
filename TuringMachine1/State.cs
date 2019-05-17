using CsvHelper;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TuringMachine1
{
    class State
    {
        public int StateNum;
        public char Condition;
        public char Write;
        public Move Movement;
        public int NextState;

        public State(int stateNum, char condition, char write, Move movement, int NextState)
        {
            this.StateNum = stateNum;
            this.Condition = condition;
            this.Write = write;
            this.Movement = movement;
            this.NextState = NextState;
        }

        public override string ToString()
        {
            return string.Format("[StateNum: {0}; Condition: {1}; Write: {2}; Movement: {3}]", StateNum, Condition, Write, Movement);
        }

        static public State[] ReadStatesCSV(string filename)
        {
            List<State> states = new List<State>();
            

            using (TextReader reader = File.OpenText(filename))
            {
                CsvReader csv = new CsvReader(reader);
                csv.Configuration.Delimiter = ",";
                csv.Configuration.MissingFieldFound = null;
                csv.Configuration.PrepareHeaderForMatch = (string header, int index) => header.ToLower();

                while (csv.Read())
                {
                    State Record = csv.GetRecord<State>();
                    states.Add(Record);
                }
            }
            return states.ToArray();
        }

        static public State GetNextState(State[] states, int StateNum, char Read)
        {
            foreach (State state in states)
            {
                if (StateNum == state.StateNum && Read == state.Condition)
                {
                    return state;
                }
            }
            throw new MissingStateException(String.Format("Missing state number {0} to handle {1} condition.", StateNum, Read));
        }
    }

    public class MissingStateException : System.Exception
    {
        public MissingStateException() : base() { }
        public MissingStateException(string message) : base(message) { }
        public MissingStateException(string message, System.Exception inner) : base(message, inner) { }
    }
}
