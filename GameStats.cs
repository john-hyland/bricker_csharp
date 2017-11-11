using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bricker
{
    public class GameStats
    {
        //private
        private List<HighScore> _highScores;
        private int _currentScore;
        private int _lines;
        private int _level;

        /// <summary>
        /// Class constructor.
        /// </summary>
        public GameStats()
        {
            _highScores = new List<HighScore>();
            _currentScore = 0;
            _lines = 0;
            _lines = 1;
        }

        /// <summary>
        /// Increments current score.
        /// </summary>
        public void IncrementScore(int count)
        {
            _currentScore += count;
        }

        /// <summary>
        /// Returns true if score can be placed on board.
        /// </summary>
        public bool IsHighScore()
        {
            if (_highScores.Count < 10)
                return true;
            int lowest = Int32.MaxValue;
            foreach (HighScore score in _highScores)
                if (score.Score < lowest)
                    lowest = score.Score;
            return _currentScore > lowest;
        }


        public void IncrementLines(int count)
        {
            _lines += count;
            _level = (_level / 20) + 1;
        }






        /// <summary>
        /// Internal class to store a high score.
        /// </summary>
        private class HighScore
        {
            public string Initials { get; }
            public int Score { get; }
            public HighScore(string initials, int score)
            {
                Initials = initials;
                Score = score;
            }
        }


    }

}
