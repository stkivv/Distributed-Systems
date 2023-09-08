namespace Public.DTO.v1;
/// <summary>
/// Month. Has a number and name.
/// </summary>
public class Month
{
    public Guid Id { get; set; }

    public int MonthNr { get; set; }
    
    public string MonthName { get; set; } = default!;
    
}