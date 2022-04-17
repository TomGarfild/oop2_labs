﻿using System;
using System.Threading;

namespace Server.Models
{
    public class Round
    {
        public enum OptionChoice
        {
            Undefined = 0,
            Rock = 1,
            Paper = 2,
            Scissor = 3
        }

        public enum Result
        {
            Undefined = 0,
            Lose = 1,
            Draw = 2,
            Win = 3
        }

        private OptionChoice _user1Choice = OptionChoice.Undefined;
        private OptionChoice _user2Choice = OptionChoice.Undefined;
        public CancellationTokenSource Source { get; private set; } = new CancellationTokenSource();

        public Result GetResult()
        {
            if (_user1Choice == _user2Choice)
                return Result.Draw;
            else if (_user1Choice == OptionChoice.Rock && _user2Choice == OptionChoice.Scissor)
            {
                return Result.Win;
            }
            else if (_user1Choice == OptionChoice.Paper && _user2Choice == OptionChoice.Rock)
            {
                return Result.Win;
            }
            else if (_user1Choice == OptionChoice.Scissor && _user2Choice == OptionChoice.Paper)
            {
                return Result.Win;
            }

            return Result.Lose;
        }

        public bool IsDone()
        {
            if (_user1Choice != OptionChoice.Undefined && _user2Choice != OptionChoice.Undefined)
            {
                return true;
            }

            return false;
        }
        public void SetChoice1(string choice)
        {
            _user1Choice = ParseChoice(choice);
            if (IsDone())
            {
                Source.Cancel();
            }
        }
        public void SetChoice2(string choice)
        {
            _user2Choice = ParseChoice(choice);
            if (IsDone())
            {
                Source.Cancel();
            }
        }
        public static OptionChoice ParseChoice(string choice)
        {
            switch (choice)
            {
                case "Rock":
                    return OptionChoice.Rock;
                case "Paper":
                    return OptionChoice.Paper;
                case "Scissors":
                    return OptionChoice.Scissor;
                default:
                    return OptionChoice.Undefined;
            }
        }

        public void SetRandomChoice()
        {
            _user2Choice =(OptionChoice) new Random().Next(3) + 1;
        }

        public void Cancel()
        {
            Source.Cancel();
        }
    }
}
