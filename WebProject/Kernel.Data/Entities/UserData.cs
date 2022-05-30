﻿namespace Kernel.Data.Entities;

public class UserData : BaseEntityData
{
    public int ChatId { get; set; }
    public string? Username { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}