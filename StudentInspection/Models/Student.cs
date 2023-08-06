namespace StudentInspection.Models
{
    public class Student
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public int Age { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName} {Age}";
        }
    }

}