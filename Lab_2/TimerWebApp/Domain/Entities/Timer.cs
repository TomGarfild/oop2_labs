using System;

namespace Domain.Entities
{
    public record Timer
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

        public string Name { get; init; }
        public DateTime Time { get; init; }
        public string Sound { get; init; }
    }
}