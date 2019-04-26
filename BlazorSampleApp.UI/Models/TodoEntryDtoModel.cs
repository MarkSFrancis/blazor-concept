using System;

namespace BlazorSampleApp.UI.Models
{
    public class TodoEntryDtoModel
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? CompletedOn { get; set; }
    }
}
