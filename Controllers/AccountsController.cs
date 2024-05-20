using Account.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly TestDevContext _context;

    public AccountsController(TestDevContext context)
    {
        _context = context;
    }

    [HttpGet("top-level-accounts")]
    public async Task<IActionResult> GetTopLevelAccounts()
    {
        var result = await _context.Accounts
            .Where(a => a.AccParent == null)
            .Select(a => new
            {
                TopLevelAccount = a.AccNumber,
                TotalBalance = _context.Accounts
                                .Where(child => child.AccParent == a.AccNumber)
                                .Sum(child => child.Balance)
            })
            .ToListAsync();

        return Ok(result);
    }

    [HttpGet("account-details/{id}")]
    public async Task<IActionResult> GetAccountDetails(int id)
    {
        var accountDetails = await _context.Accounts
            .Where(a => a.AccNumber == id.ToString())
            .Select(a => new
            {
                a.AccNumber,
                a.Balance
            })
            .ToListAsync();

        return Ok(accountDetails);
    }
}
