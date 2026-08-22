
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Homework2.Models;

public class MovieController : Controller
{
    private readonly MovieContext _context;
    private readonly IWebHostEnvironment _appEnvironment;

    public MovieController(MovieContext context, IWebHostEnvironment appEnvironment)
    {
        _context = context;
        _appEnvironment = appEnvironment;
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
    public async Task<IActionResult> Create(Movie movie, IFormFile? posterFile)
    {
        if (ModelState.IsValid)
        {
            if (posterFile != null && posterFile.Length > 0)
            {
                // Папка wwwroot/img
                var uploadsFolder = Path.Combine(_appEnvironment.WebRootPath, "img");
                Directory.CreateDirectory(uploadsFolder);

                var uniqueName = Guid.NewGuid() + "_" + Path.GetFileName(posterFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await posterFile.CopyToAsync(stream);
                }

                var fileModel = new FileModel
                {
                    Name = posterFile.FileName,
                    Path = "/img/" + uniqueName,   // ← путь для браузера
                    UploadDate = DateTime.Now
                };

                movie.Poster = fileModel;
            }

            _context.Movies.Add(movie);
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


}
