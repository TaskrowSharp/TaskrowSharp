using System;
using System.Text.Json.Serialization;
using TaskrowSharp.JsonConverters;

namespace TaskrowSharp.Models.TaskModels;

public class JobPipelineStep
{
    public int JobPipelineID { get; set; }
    public int JobPipelineStepID { get; set; }
    public int Order { get; set; }
    public string Title { get; set; }
    public string PluralTitle { get; set; }
    //public object? ColorID { get; set; }
    public int? JobPipelineStepClassID { get; set; }

    [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
    public DateTime? LastModificationDate { get; set; }

    public int? LastModificationUserID { get; set; }
}
