using FormulaAirline.API.Models;
using FormulaAirline.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FormulaAirline.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BookingController : ControllerBase
{
     private readonly ILogger<BookingController> _logger;
    private readonly IMessageProducer _messageProducer;

    // In-Memory db
    public static readonly List<Booking> _booking =new();
    public BookingController(ILogger<BookingController> logger,IMessageProducer messageProducer)
    {
        _logger = logger;
        _messageProducer=messageProducer;
    }
    

    [HttpPost]
    public IActionResult CreateBooking(Booking newBooking)
    {
        if(!ModelState.IsValid) return BadRequest();

        _booking.Add(newBooking);

        _messageProducer.SendingMessage<Booking>(newBooking);

        return Ok();
    }
}
