using System;

namespace TimerWebApp.Shared
{
    public class Timer
    {
        public Timer(
            string name,
            DateTime time,
            string sound)
        {
            Name = name;
            Time = time;
            Sound = sound;
        }

        public string Name { get; private set; }
        public DateTime Time { get; private set; }
        public string Sound { get; private set; }
    }
}