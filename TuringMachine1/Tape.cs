using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuringMachine1
{
    enum Move { Left, Right, Stay }

    class Tape
    {
        private LinkedList<char> tape;
        public LinkedListNode<char> CurrentRead;

        public Tape(Char[] tape, int startingPosition)
        {
            this.tape = new LinkedList<char>(tape);
            CurrentRead = this.tape.First;
            for (int i = 0; i < startingPosition; i++)
            {
                CurrentRead = CurrentRead.Next;
            }
        }

        public Tape(Char[] tape) : this(tape, 0) { }

        public Char Read()
        {
            return CurrentRead.Value;
        }

        public void Write(Char character)
        {
            CurrentRead.Value = character;
        }

        public void Move(Move move)
        {
            switch (move)
            {
                case TuringMachine1.Move.Left:
                    if (CurrentRead.Previous is null)
                    {
                        tape.AddBefore(CurrentRead, ' ');
                    }
                    CurrentRead = CurrentRead.Previous;
                    break;
                case TuringMachine1.Move.Right:
                    if (CurrentRead.Next is null)
                    {
                        tape.AddAfter(CurrentRead, ' ');
                    }
                    CurrentRead = CurrentRead.Next;
                    break;
            }
        }

        public string ReadWholeTape()
        {
            return new String(tape.ToArray<char>());
        }

        public override string ToString()
        {
            return string.Format("[Tape: {0}; Current Read: {1}]", ReadWholeTape(), CurrentRead.Value);
        }

    }
}
