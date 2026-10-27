using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _7_Elephant.Models;

namespace _7_Elephant.Controllers;

public class PhoneController : Controller
{
    private readonly ApplicationDbContext _context;

    public PhoneController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Phones.ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var phone = await _context.Phones
            .FirstOrDefaultAsync(m => m.ProductId == id);

        if (phone == null)
        {
            return NotFound();
        }

        return View(phone);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Phone phone)
    {
        if (!ModelState.IsValid)
        {
            return View(phone);
        }

        _context.Add(phone);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var phone = await _context.Phones.FindAsync(id);

        if (phone == null)
        {
            return NotFound();
        }

        return View(phone);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Phone phone)
    {
        if (id != phone.ProductId)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(phone);
        }

        try
        {
            _context.Update(phone);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PhoneExists(phone.ProductId))
            {
                return NotFound();
            }

            throw;
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var phone = await _context.Phones
            .FirstOrDefaultAsync(m => m.ProductId == id);

        if (phone == null)
        {
            return NotFound();
        }

        return View(phone);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var phone = await _context.Phones.FindAsync(id);

        if (phone != null)
        {
            _context.Phones.Remove(phone);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Order()
    {
        return View(await _context.Phones.ToListAsync());
    }

    public async Task<IActionResult> Place_Order(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var phone = await _context.Phones
            .FirstOrDefaultAsync(m => m.ProductId == id);

        if (phone == null)
        {
            return NotFound();
        }

        return View(phone);
    }

    private bool PhoneExists(int id)
    {
        return _context.Phones.Any(e => e.ProductId == id);
    }
}
