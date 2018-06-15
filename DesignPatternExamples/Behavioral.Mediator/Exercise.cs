using System;

namespace Coding.Exercise
{
    public class Participant
    {
        private readonly Mediator _mediator;
        public int Value { get; set; }

        public Participant(Mediator mediator)
        {
            _mediator = mediator;

            mediator.Alert += (sender, value) =>
            {
                if (sender != this)
                    Value += value;
            };
        }

        public void Say(int n)
        {
            // todo
            _mediator.Broadcast(this, n);
        }
    }

    public class Mediator
    {
        // todo
        public event EventHandler<int> Alert;

        public void Broadcast(object sender, int value)
        {
            Alert?.Invoke(sender, value);
        }
    }
}
