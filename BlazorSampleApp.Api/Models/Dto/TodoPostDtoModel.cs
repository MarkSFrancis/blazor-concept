using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorSampleApp.Api.Models.Dto
{
    public class TodoPostDtoModel
    {
        public string Id { get; set; }

        [Required]
        public string Text { get; set; }

        public DateTime? CompletedOn { get; set; }

        public DateTime? CreatedOn { get; set; }
    }
}