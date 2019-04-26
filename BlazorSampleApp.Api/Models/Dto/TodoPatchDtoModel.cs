using System;

namespace BlazorSampleApp.Api.Models.Dto
{
    public class TodoPatchDtoModel
    {
        public string Text { get; set; }

        public DateTime? CompletedOn { get; set; }
    }
}