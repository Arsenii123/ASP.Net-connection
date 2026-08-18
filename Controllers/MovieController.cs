using Homework2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;



namespace Homework2.Controllers
{
    public class MovieController:Controller
    {
       MovieContext db; // контролер виходить у базу даних через контекст даних StudentContext
        public MovieController(MovieContext context) // контекст даних отримується через механізм впровадження залежностей (Dependency Injection)
        { // прямого згадування або створення об'єкта StudentContext тут немає, все робиться автоматично завдяки налаштуванням в Program.cs
            db = context;
        } // можна було б і це навіть не писати, є спеціальний атрибут [FromServices], але так наочніше

        // звісно, ще крутіше було б використати репозиторій, але це вже інша історія
        // public StudentController(IRepository r) щось типу такого буде (але згодом)
        // при такому підході, контролер не залежить від конкретної реалізації контексту даних,
        // база даних може бути замінена на будь-яку іншу, що реалізує інтерфейс IRepository, в тому числі на мок, це зручно для тестування

        public async Task<IActionResult> Index() // public async Task<IActionResult> Index([FromServices] StudentContext db), тоді не потрібен конструктор
        {
            IEnumerable<Movie> movies = await Task.Run(() => db.Movies); // отримання списку студентів з бази даних через контекст даних
            return View(movies); // повернення представлення Index (Views/Student/Index.cshtml)
        } // вью отримає список студентів як модель (на читання)
          // Метод для отображения страницы с формой

        // методи контролера бажано робити асинхронними, щоб не блокувати потік обробки запитів
        // це особливо важливо при роботі з базою даних, де операції можуть бути тривалими
        // якщо метод не асинхронний, то він блокує потік, поки виконується операція з базою даних
        // в результаті, сервер може не встигати обробляти інші запити, що призводить до погіршення продуктивності
        // а користувачів буде не 10, а 1000+, і всі вони чекатимуть відповіді від сервера
        // спочатку запити ставляться в чергу, але й черга не безмежна, і врешті-решт сервер почне відмовляти в обслуговуванні нових запитів!
    }
}
