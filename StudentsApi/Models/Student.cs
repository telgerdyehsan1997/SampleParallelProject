using System.Text.Json.Serialization;

namespace StudentsApi.Models
{
    public class Student
    {
        [JsonPropertyName("FirstName")]
        public string? FirstName { get; set; }
        [JsonPropertyName("LastName")]
        public string? LastName { get; set; }

        [JsonPropertyName("Age")]
        public int Age { get; set; }
    }
}
