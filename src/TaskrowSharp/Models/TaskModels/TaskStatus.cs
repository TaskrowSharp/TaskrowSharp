﻿namespace TaskrowSharp.Models.TaskModels;

public class TaskStatus
{
    public bool Disabled { get; set; }
    //public object? Group { get; set; }
    public bool Selected { get; set; }
    public string Text { get; set; }
    public string Value { get; set; }
}
