﻿namespace TaskrowSharp.Models.TaskModels;

public class TasksByGroupResponse
{
    public bool Success { get; set; }

    public string Message { get; set; }

    public TaskListByGroupEntity Entity { get; set; }

    public string TargetURL { get; set; }
}
