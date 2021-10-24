using System;

namespace Domain.Dtos
{
    public record TimerDto(string Name, DateTime Time, string Sound);
}