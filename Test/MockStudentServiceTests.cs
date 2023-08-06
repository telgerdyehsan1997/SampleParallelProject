namespace Test;
public class MockStudentServiceTests
{

    public MockStudentServiceTests()
    {

    }
    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(5)]
    [InlineData(null)]
    public async void Add_TwoIntegers_ReturnsCorrectSum(int? count)
    {
        IStudentService service = new MockStudentService();
        // Act
        var students = await service.LoadStudents(count, CancellationToken.None);

        // Assert
        if (count.HasValue)
            Assert.Equal(students.Count(), count);
        else
            Assert.Equal(students.Count(), 6);
    }
}