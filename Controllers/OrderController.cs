using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _7_Elephant.Models;

namespace _7_Elephant.Controllers;

[Authorize]
public class OrderController : Controller
{
    private readonly ApplicationDbContext _context;

    public OrderController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var userEmail = User.Identity?.Name ?? string.Empty;

        var orders = await _context.Orders
            .Where(o => o.UserEmail == userEmail)
            .ToListAsync();

        return View(orders);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var order = await OwnOrder(id.Value);

        if (order == null)
        {
            return NotFound();
        }

        return View(order);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Order order)
    {
        order.UserEmail = User.Identity?.Name;

        if (!ModelState.IsValid)
        {
            return View(order);
        }

        _context.Add(order);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var order = await OwnOrder(id.Value);

        if (order == null)
        {
            return NotFound();
        }

        return View(order);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Order order)
    {
        if (id != order.OrderId)
        {
            return NotFound();
        }

        var existingOrder = await OwnOrder(id);

        if (existingOrder == null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(order);
        }

        existingOrder.ProductId = order.ProductId;
        existingOrder.Quantity = order.Quantity;
        existingOrder.Total = order.Total;
        existingOrder.Address = order.Address;

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var order = await OwnOrder(id.Value);

        if (order == null)
        {
            return NotFound();
        }

        return View(order);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var order = await OwnOrder(id);

        if (order != null)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddOrder(
        int ProductId,
        int Quantity,
        string Address)
    {
        // Log received values
        Console.WriteLine(
            $"ProductId: {ProductId}, Quantity: {Quantity}, Address: {Address}");

        System.Diagnostics.Debug.WriteLine(
            $"ProductId: {ProductId}, Quantity: {Quantity}, Address: {Address}");

        if (ProductId <= 0 ||
            Quantity <= 0 ||
            string.IsNullOrWhiteSpace(Address))
        {
            ModelState.AddModelError(
                string.Empty,
                "Invalid product, quantity, or address.");

            return View(
                "Index",
                await _context.Orders
                    .Where(o => o.UserEmail == (User.Identity!.Name ?? string.Empty))
                    .ToListAsync());
        }

        var product = await _context.Phones
            .SingleOrDefaultAsync(m => m.ProductId == ProductId);

        if (product == null || product.Price == null)
        {
            ModelState.AddModelError(
                string.Empty,
                "Product not found or price missing.");

            return View(
                "Index",
                await _context.Orders
                    .Where(o => o.UserEmail == (User.Identity!.Name ?? string.Empty))
                    .ToListAsync());
        }

        var myOrder = new Order
        {
            UserEmail = User.Identity?.Name ?? string.Empty,
            ProductId = ProductId,
            Quantity = Quantity,
            Total = product.Price.HasValue
                ? product.Price.Value * Quantity
                : 0,
            Address = Address
        };

        try
        {
            _context.Orders.Add(myOrder);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            ModelState.AddModelError(
                string.Empty,
                $"Database error: {ex.Message}");

            return View(
                "Index",
                await _context.Orders
                    .Where(o => o.UserEmail == (User.Identity!.Name ?? string.Empty))
                    .ToListAsync());
        }

        return RedirectToAction(nameof(Index));
    }

    private Task<Order?> OwnOrder(int id)
    {
        var userEmail = User.Identity?.Name ?? string.Empty;

        return _context.Orders
            .FirstOrDefaultAsync(o =>
                o.OrderId == id && o.UserEmail == userEmail);
    }
}
