using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet("dashboard-summary")]
        public async Task<IActionResult> DashboardSummary()
        {
            var now = DateOnly.FromDateTime(DateTime.UtcNow);

            var TotalOrg = await _context.Organizations.CountAsync();
            var TotalChef = await _context.Chefs.CountAsync();
            var TotalGal = await _context.Galleries.CountAsync();
            var TotalMsg = await _context.Contacts.CountAsync();
            var MonthOrg = await _context.Organizations.CountAsync(x => x.CreateDate.Month == now.Month &&
    x.CreateDate.Year == now.Year);
            var WeekMsg = await _context.Contacts.CountAsync(x => x.CreateDate >= now.AddDays(-7));
            var TrashMsg = await _context.Contacts.Where(x => x.messageBox == Entities.MessageBoxType.Trash).CountAsync();

            return Ok(new
            {
                TotalOrg,
                TotalChef,
                TotalGal,
                TotalMsg,
                MonthOrg,
                WeekMsg,
                TrashMsg
            });
        }

        [HttpGet("dashboard-widget")]
        public async Task<IActionResult> DashboardWidget()
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            var TodayOrg = await _context.Organizations.CountAsync(x => x.CreateDate == today);
            var UncomingOrg = await _context.Organizations.CountAsync(x => x.CreateDate >= today && x.CreateDate <= today.AddDays(7));
            var UnreadMsg = await _context.Contacts.Where(x => x.IsRead == false).CountAsync();
            var PendingRez = await _context.Rezervations.Where(x => x.RezervationStatus == Entities.RezervationStatus.Pending).CountAsync();
            return Ok(new
            {
                TodayOrg,
                UncomingOrg,
                UnreadMsg,
                PendingRez
            });
        }
    }
}