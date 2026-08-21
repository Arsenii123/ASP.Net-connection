
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Homework2.Models;

public class MovieController : Controller
{
    private readonly MovieContext _context;
    private readonly ApplicationContext _contextFile; // контекст бази даних, інжектується через DI
    private readonly IWebHostEnvironment _appEnvironment; // !!! середовище хостингу, потрібне для доступу до wwwroot

    public MovieController(MovieContext context, ApplicationContext contextFile, IWebHostEnvironment appEnvironment)
    {
        _context = context;
        _context = context; // зберігаємо контекст для подальшої роботи
        _appEnvironment = appEnvironment; // зберігаємо середовище для роботи з файлами
    }

    // GET: MOVIES
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Movies.ToListAsync());
    }

    // GET: MOVIES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movies
            .FirstOrDefaultAsync(m => m.Id == id);
        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // GET: MOVIES/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: MOVIES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Director,Genre,Poster,Description,Age")] Movie movie)
    {
        if (ModelState.IsValid)
        {
            _context.Add(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(movie);
    }

    // GET: MOVIES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movies.FindAsync(id);
        if (movie == null)
        {
            return NotFound();
        }
        return View(movie);
    }

    // POST: MOVIES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Name,Director,Genre,Poster,Description,Age")] Movie movie)
    {
        if (id != movie.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(movie);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(movie.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(movie);
    }

    // GET: MOVIES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movies
            .FirstOrDefaultAsync(m => m.Id == id);
        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // POST: MOVIES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie != null)
        {
            _context.Movies.Remove(movie);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MovieExists(int? id)
    {
        return _context.Movies.Any(e => e.Id == id);
    }
    [HttpPost] // POST-запит
    public async Task<IActionResult> AddFile(List<IFormFile> uploadedFiles)
    {
        if (uploadedFiles == null || !uploadedFiles.Any() || uploadedFiles.All(f => f.Length == 0))
        {
            TempData["Error"] = "Будь ласка, оберіть хоча б один файл!";
            return RedirectToAction("Index"); // вью індекса зверху іфами перехоплює успішність або неуспішність завантаження файлів
        }

        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
        Directory.CreateDirectory(uploadsFolder); // створюємо папку, якщо немає

        foreach (var file in uploadedFiles)
        {
            if (file.Length > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(uploadsFolder, Guid.NewGuid() + "_" + fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // зберігаємо в базу
                var fileModel = new FileModel
                {
                    Name = fileName,
                    Path = "/img/" + Path.GetFileName(filePath),
                    UploadDate = DateTime.Now
                };

                _contextFile.Files.Add(fileModel); // додаємо файли в контекст (Files - це властивість в ApplicationContext)
            }
        }

        await _context.SaveChangesAsync(); // це обов’язковий рядок, без якого ніякі дані в базу НЕ запишуться, хоча ми начебто тільки-но додали їх через _context.Files.Add(fileModel).
                                           // _context.Files.Add(fileModel) — просто каже Entity Framework: oсь новий об'єкт, я хочу його додати в базу пізніше.
                                           // але він поки що лежить тільки в пам'яті (в так званому Change Tracker).
                                           // await _context.SaveChangesAsync(); — це команда: а от тепер дійсно виконай всі накопичені зміни і відправ їх у базу даних» (згенеруй і виконай SQL-команди INSERT)

        TempData["Success"] = "Файли успішно завантажено!";
        return RedirectToAction("Index");
    }
}
