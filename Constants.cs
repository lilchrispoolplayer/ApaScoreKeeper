using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApaScoreKeeper
{
    public class Constants
    {
        public const string GAME_TYPE = "GameType.txt";
        public const string PLAYER_1 = "Player 1";
        public const string PLAYER_1_NAME = "Player1Name.txt";
        public const string PLAYER_1_SKILL_LEVEL = "Player1SkillLevel.txt";
        public const string PLAYER_1_SCORE = "Player1Score.txt";
        public const string PLAYER_2 = "Player 2";
        public const string PLAYER_2_NAME = "Player2Name.txt";
        public const string PLAYER_2_SKILL_LEVEL = "Player2SkillLevel.txt";
        public const string PLAYER_2_SCORE = "Player2Score.txt";
        public const string RACE = "Race.txt";
        public const string INNINGS = "Innings.txt";
        public const string DEAD_BALLS = "DeadBalls.txt";
        public const string EIGHT_BALL = "8-Ball";
        public const string NINE_BALL = "9-Ball";

        public static int[] EIGHT_BALL_SKILL_LEVELS = { 2, 3, 4, 5, 6, 7 };
        public static int[] NINE_BALL_SKILL_LEVELS = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        public static int[] NINE_BALL_GAMES = { 14, 19, 25, 31, 38, 46, 55, 65, 75 };
        public static string[,] EIGHT_BALL_GAMES =
        {
            {"2/2", "2/3", "2/4", "2/5", "2/6", "2/7" },
            {"3/2", "2/2", "2/3", "2/4", "2/5", "2/6" },
            {"4/2", "3/2", "3/3", "3/4", "3/5", "2/5" },
            {"5/2", "4/2", "4/3", "4/4", "4/5", "3/5" },
            {"6/2", "5/2", "5/3", "5/4", "5/5", "4/5" },
            {"7/2", "6/2", "5/2", "5/3", "5/4", "5/5" }
        };
    }
}
