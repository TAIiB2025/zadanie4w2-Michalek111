using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebAPI.models;

[Route("api/uslugi")]
[ApiController]
public class ListaController : Controller
{
    private static List<Usluga> uslugi = new List<Usluga>
    {
        new Usluga { Id = 1, Nazwa = "Malowanie ścian", Wykonawca = "Jan Kowalski", Rodzaj = "Budowlana", Rok = 2023 },
        new Usluga { Id = 2, Nazwa = "Naprawa laptopa", Wykonawca = "TechFix Serwis", Rodzaj = "Elektroniczna", Rok = 2024 },
        new Usluga { Id = 3, Nazwa = "Projekt ogrodu", Wykonawca = "Zielony Zakątek", Rodzaj = "Projektowa", Rok = 2022 },
        new Usluga { Id = 4, Nazwa = "Tłumaczenie dokumentów", Wykonawca = "Anna Nowak", Rodzaj = "Językowa", Rok = 2021 },
        new Usluga { Id = 5, Nazwa = "Kurs programowania", Wykonawca = "CodeAcademy", Rodzaj = "Edukacyjna", Rok = 2025 }
    };

    [HttpGet]
    public IEnumerable<Usluga> Get([FromQuery] string? search)
    {

        Console.WriteLine($"Szukana fraza: {search}");

        if (!string.IsNullOrEmpty(search))
        {
            return uslugi.Where(u => u.Nazwa.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        return uslugi;
    }


    [HttpGet("{id}")]
    public ActionResult<Usluga> GetById(int id)
    {
        var usluga = uslugi.FirstOrDefault(u => u.Id == id);
        return usluga != null ? Ok(usluga) : NotFound();
    }

    [HttpPost]
    public ActionResult<Usluga> Post([FromBody] Usluga usluga)
    {
        if (!ModelState.IsValid)
        {

            return BadRequest(ModelState);
        }

        usluga.Id = uslugi.Max(u => u.Id) + 1;
        uslugi.Add(usluga);
        return CreatedAtAction(nameof(GetById), new { id = usluga.Id }, usluga);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Usluga updated)
    {
        if (!ModelState.IsValid)
        {

            return BadRequest(ModelState);
        }
        var usluga = uslugi.FirstOrDefault(u => u.Id == id);
        if (usluga == null) return NotFound();

        usluga.Nazwa = updated.Nazwa;
        usluga.Wykonawca = updated.Wykonawca;
        usluga.Rodzaj = updated.Rodzaj;
        usluga.Rok = updated.Rok;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var usluga = uslugi.FirstOrDefault(u => u.Id == id);
        if (usluga == null) return NotFound();

        uslugi.Remove(usluga);
        return NoContent();
    }
}