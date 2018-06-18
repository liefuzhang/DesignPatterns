using System;

namespace Coding.Exercise
{
    public class Game
    {
        // todo
        // remember - no fields or properties!
        public event EventHandler RatNumberChanged;

        public void OnRatNumberChanged()
        {
            RatNumberChanged?.Invoke(this, EventArgs.Empty);
        }
        
        public int GetSubscriptionCount()
        {
            return RatNumberChanged?.GetInvocationList()?.Length ?? 0;
        }
    }

    public class Rat : IDisposable
    {
        private readonly Game _game;
        public int Attack = 1;

        public Rat(Game game)
        {
            _game = game;
            // todo
            game.RatNumberChanged += RatNumberChangedHandler;
            game.OnRatNumberChanged();
        }
        
        public void Dispose()
        {
            // todo
            _game.RatNumberChanged -= RatNumberChangedHandler;
            _game.OnRatNumberChanged();
        }

        public void RatNumberChangedHandler(object sender, EventArgs args)
        {
            Attack = _game.GetSubscriptionCount();
        }
    }
}

