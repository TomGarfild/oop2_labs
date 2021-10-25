namespace Domain.ViewModels
{
    public class TimerVm
    {
        public TimerVm(
            int index,
            string name,
            int time,
            string sound)
        {
            Index = index;
            Name = name;
            Time = time;
            Sound = sound;
        }

        public int Index { get; set; }
        public string Name { get; set; }
        public int Time { get; set; }
        public string Sound { get; set; }
    }
}