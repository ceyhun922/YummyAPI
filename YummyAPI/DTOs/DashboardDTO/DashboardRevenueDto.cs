namespace YummyAPI.DTOs.DashboardDTO
{
    public class DashboardRevenueDto
{
    public List<string> Labels { get; set; } = new();    
    public List<double> Revenue { get; set; } = new();   
    public List<int> Reservations { get; set; } = new();  

    public double WeeklyEarnings { get; set; }
    public double MonthlyEarnings { get; set; }
    public double YearlyEarnings { get; set; }

    public int TotalCustomers { get; set; }
    public double TotalIncome { get; set; }
    public int ProjectCompleted { get; set; }   
    public double TotalExpense { get; set; }   
    public int NewCustomers { get; set; }       
}

}