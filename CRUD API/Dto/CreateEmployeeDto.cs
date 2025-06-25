namespace CRUD_API.Dto;

public class CreateEmployeeDto
{
    public string FullName { get; set; }
    public string Position { get; set; }
    public decimal Salary { get; set; }
    public DateTime HireDate { get; set; }
}
public class UpdateEmployeeDto : CreateEmployeeDto { }