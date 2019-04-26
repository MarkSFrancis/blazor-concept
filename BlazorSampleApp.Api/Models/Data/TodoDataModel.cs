using System;

namespace BlazorSampleApp.Api.Models.Data
{
    public class TodoDataModel
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? CompletedOn { get; set; }
    }
}