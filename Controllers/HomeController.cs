using Microsoft.AspNetCore.Mvc;



    namespace Homework2.Controllers
    {
        /// <summary>
        /// Стандартний контролер головної сторінки додатку.
        /// </summary>
        public class HomeController : Controller
        {
            /// <summary>
            /// Відображає головну сторінку.
            /// </summary>
            /// <returns>Представлення Index.</returns>
            public IActionResult Index()
            {
                return View();
            }

            /// <summary>
            /// Відображає сторінку політики конфіденційності.
            /// </summary>
            /// <returns>Представлення Privacy.</returns>
            public IActionResult Privacy()
            {
                return View();
            }
        }
    }

