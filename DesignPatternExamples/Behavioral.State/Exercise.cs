using System;

namespace Coding.Exercise
{
    public class CombinationLock
    {
        enum State
        {
            LOCKED,
            OPEN,
            ERROR
        }

        int[] combination;
        private int current;

        public CombinationLock(int[] combination)
        {
            // todo
            this.combination = combination;
            Status = State.LOCKED.ToString();
            current = 0;
        }

        // you need to be changing this on user input
        public string Status;

        public void EnterDigit(int digit)
        {
            // todo
            if (combination[current] == digit)
            {
                if (current == 0)
                    Status = digit.ToString();
                else
                    Status += digit;

                if (++current == combination.Length)
                {
                    Status = State.OPEN.ToString();
                }
            }
            else
            {
                current = 0;
                Status = State.ERROR.ToString();
            }
        }
    }
    
}