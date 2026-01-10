using Microsoft.AspNetCore.Mvc;
using YummyAPI.Context;
using YummyAPI.DTOs.DashboardDTO;

namespace YummyAPI.Controllers
{
    [ApiController]
    [Route("api[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly ApiContext _context;

        public DashboardController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet("revenue")]
        public IActionResult GetRevenue()
        {
            var now = DateTime.Now;

            var months = Enumerable.Range(0, 6)
                .Select(i => new DateTime(now.Year, now.Month, 1).AddMonths(-5 + i))
                .ToList();

            var labels = months.Select(m => m.ToString("MMM")).ToList();

            var approved = _context.Rezervations
                .Where(x => x.RezervationStatus == Entities.RezervationStatus.Approved);

            var reservationsByMonth = months.Select(m =>
                approved.Count(r => r.RezervationDate.Year == m.Year && r.RezervationDate.Month == m.Month)
            ).ToList();

            var avgPrice = _context.Organizations.Any()
                ? _context.Organizations.Average(o => o.OrganizationPrice)
                : 0;

            var revenueByMonth = reservationsByMonth.Select(c => c * avgPrice).ToList();

            var thisMonthCount = approved.Count(r =>
                r.RezervationDate.Year == now.Year && r.RezervationDate.Month == now.Month);

            var thisYearCount = approved.Count(r => r.RezervationDate.Year == now.Year);

            var last7 = DateOnly.FromDateTime(now.AddDays(-7));
            var weeklyCount = approved.Count(r => r.RezervationDate >= last7);

            var totalCustomers = approved.Count();
            var totalIncome = totalCustomers * avgPrice;

            var dto = new DashboardRevenueDto
            {
                Labels = labels,
                Revenue = revenueByMonth,
                Reservations = reservationsByMonth,

                WeeklyEarnings = avgPrice * weeklyCount,
                MonthlyEarnings = avgPrice * thisMonthCount,
                YearlyEarnings = avgPrice * thisYearCount,

                TotalCustomers = totalCustomers,
                TotalIncome = totalIncome,
                ProjectCompleted = _context.Organizations.Count(),
                TotalExpense = 0,
                NewCustomers = thisMonthCount
            };

            return Ok(dto);
        }

    }
    }