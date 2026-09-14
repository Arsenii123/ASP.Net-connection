
using BuisnessLogicPlayer.Services;
using BuisnessLogicPlayer.Services.Interfaces;
using BusinessLogic.Services;
using BusinessLogic.Services.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



/// <summary>
/// Контролер для управління фільмами (CRUD-операції) та завантаження постерів.
/// </summary>
public class MovieController : Controller
{
    private readonly MovieContext _context;
    private ICreate fieldService;
    private IEdit myService2;

    private IDelete myService3;

    private IDetails myService4;

    private readonly IWebHostEnvironment _appEnvironment;

    /// <summary>
    /// Ініціалізує новий екземпляр <see cref="MovieController"/>.
    /// </summary>
    /// <param name="context">Контекст бази даних для роботи з фільмами.</param>
    /// <param name="appEnvironment">Середовище хостингу для доступу до файлової системи (wwwroot).</param>
    public MovieController(MovieContext context,ICreate service, IEdit service2, IDelete service3, IDetails service4, IWebHostEnvironment appEnvironment)
    {
        _context = context;
        fieldService = service;
        myService2 = service2;
        myService3 = service3;
        myService4= service4;
        _appEnvironment= appEnvironment;
    }

    /// <summary>
    /// Відображає список усіх фільмів з підвантаженими постерами.
    /// </summary>
    /// <returns>Представлення зі списком фільмів.</returns>
    // GET: MOVIES
    public async Task<IActionResult> Index()
    {
        return View(await _context.Movies
            .Include(m => m.Poster)
            .ToListAsync());
    }

    /// <summary>
    /// Відображає деталі конкретного фільму за ідентифікатором.
    /// </summary>
    /// <param name="id">Ідентифікатор фільму.</param>
    /// <returns>Представлення з деталями фільму або NotFound, якщо фільм не знайдено.</returns>
    // GET: MOVIES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var movie = await myService4.Details(id);
        if (movie == null) return NotFound();
        return View(movie);
    }

    /// <summary>
    /// Відображає форму створення нового фільму.
    /// </summary>
    /// <returns>Представлення форми створення.</returns>
    // GET: MOVIES/Create
    public IActionResult Create()
    {
        return View();
    }

    /// <summary>
    /// Створює новий фільм та зберігає завантажений постер.
    /// </summary>
    /// <param name="movie">Дані фільму (Name, Director, Genre, Description, Age).</param>
    /// <param name="posterFile">Файл постера (обов'язковий).</param>
    /// <returns>Перенаправлення на Index при успіху або форму з помилками валідації.</returns>
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
                var uploadsFolder = Path.Combine(_appEnvironment.WebRootPath, "img");
                Directory.CreateDirectory(uploadsFolder);

                var uniqueName = Guid.NewGuid() + "_" + Path.GetFileName(posterFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await posterFile.CopyToAsync(stream);
                }

                movie.Poster = new FileModel
                {
                    Name = posterFile.FileName,
                    Path = "/img/" + uniqueName,
                    UploadDate = DateTime.Now
                };
            }

            await fieldService.Create(movie);   // вызываем сервис
            return RedirectToAction(nameof(Index));
        }
        return View(movie);
    }

    /// <summary>
    /// Відображає форму редагування існуючого фільму.
    /// </summary>
    /// <param name="id">Ідентифікатор фільму.</param>
    /// <returns>Представлення форми редагування або NotFound.</returns>
    // GET: MOVIES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await myService4.Details(id.Value);// или через репозиторий/сервис
        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    /// <summary>
    /// Оновлює дані фільму. За бажанням замінює постер новим файлом.
    /// </summary>
    /// <param name="id">Ідентифікатор фільму.</param>
    /// <param name="movie">Оновлені дані фільму.</param>
    /// <param name="posterFile">Новий файл постера (необов'язковий).</param>
    /// <returns>Перенаправлення на Index при успіху або форму з помилками.</returns>
    // POST: MOVIES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Movie movie, IFormFile? posterFile)
    {
        if (id != movie.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            // Обработка нового постера (если загрузили)
            if (posterFile != null && posterFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_appEnvironment.WebRootPath, "img");
                Directory.CreateDirectory(uploadsFolder);

                var uniqueName = Guid.NewGuid() + "_" + Path.GetFileName(posterFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await posterFile.CopyToAsync(stream);
                }

                movie.Poster = new FileModel
                {
                    Name = posterFile.FileName,
                    Path = "/img/" + uniqueName,
                    UploadDate = DateTime.Now
                };
            }

            await myService2.Edit(movie);
            return RedirectToAction(nameof(Index));
        }

        return View(movie);
    }

    /// <summary>
    /// Відображає сторінку підтвердження видалення фільму.
    /// </summary>
    /// <param name="id">Ідентифікатор фільму.</param>
    /// <returns>Представлення підтвердження або NotFound.</returns>
    // GET: MOVIES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var movie = await myService4.Details(id);

        if (movie == null) return NotFound();
        return View(movie);
    }

    /// <summary>
    /// Видаляє фільм з бази даних після підтвердження.
    /// </summary>
    /// <param name="id">Ідентифікатор фільму.</param>
    /// <returns>Перенаправлення на Index.</returns>
    // POST: MOVIES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int?  id)
    {
        await myService3.Delete(id);
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Перевіряє, чи існує фільм з вказаним ідентифікатором.
    /// </summary>
    /// <param name="id">Ідентифікатор фільму.</param>
    /// <returns><c>true</c>, якщо фільм існує; інакше <c>false</c>.</returns>
    private bool MovieExists(int? id)
    {
        return _context.Movies.Any(e => e.Id == id);
    }
}
