using System;

namespace Models
{
    public class GameModel
    {
        public int Score { get; private set; }

        private float _timer;
        private readonly float _interval;

        public GameModel(float secondPoint)
        {
            _interval = secondPoint;
        }
        
        public void Update(float deltaTime, Action<int> callback)
        {
            _timer += deltaTime;
            if (_timer >= _interval)
            {
                _timer -= _interval;
                Score += 1;
                callback?.Invoke(Score);
            }
        }

        public void ResetScore(Action<int> callback)
        {
            Score = 0;
            callback?.Invoke(Score);
        }
    }
}