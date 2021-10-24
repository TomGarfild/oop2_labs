using System;

namespace Domain.Dtos
{
    public record EditTimerDto(string Name, int TimeInSeconds, string Sound);
}