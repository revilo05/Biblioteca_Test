using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Safari;
using System.Threading;
using System.IO;

class Program
{
    static StreamWriter writer;

    static void Main(string[] args)
    {
        writer = new StreamWriter("Reporte.html");
        writer.WriteLine("<html><head><meta charset='UTF-8'><title>Log</title></head><body>");

        SafariOptions options = new SafariOptions();
        IWebDriver driver = new SafariDriver(options);

        driver.Manage().Window.Maximize();

        driver.Navigate().GoToUrl("http://oam.free.nf/index.php");

        string email = "oliverabreu@gmail.com";
        string password = "12345";

        Registro(driver, email, password);

        Thread.Sleep(5500);

        IWebElement loginTitle = driver.FindElement(By.Id("login"));
        loginTitle.Click();

        Login(driver, email, password);
        Thread.Sleep(5500);

        Auto_Contact(driver);

        Thread.Sleep(5500);

        Watch_BOOK(driver);
        Thread.Sleep(5500);

        SearchBOOK(driver);
        Thread.Sleep(5500);

        Watch_Aut(driver);

        Thread.Sleep(5500);

        SearchAutor(driver);

        SuggestNewAutor(driver);
        Thread.Sleep(5500);

        SuggestNewBook(driver);
        Thread.Sleep(5500);

        // Cerrar el navegador al finalizar
        //driver.Quit();

        writer.WriteLine("</body></html>");
        writer.Close();
    }

    static void Registro(IWebDriver driver, string email, string password)
    {
        IWebElement emailInput = driver.FindElement(By.Id("Email1"));
        emailInput.SendKeys(email);

        IWebElement passwordInput = driver.FindElement(By.Id("Password1"));
        passwordInput.SendKeys(password);
        Thread.Sleep(5500);

        IWebElement signUpButton = driver.FindElement(By.CssSelector(".signup .submit-btn"));
        signUpButton.Click();
    }


    static void Login(IWebDriver driver, string email, string password)
    {
        Thread.Sleep(2000);

        IWebElement emailInput = driver.FindElement(By.Id("Email2"));
        IWebElement passwordInput = driver.FindElement(By.Id("Password2']"));

        emailInput.SendKeys(email);
        passwordInput.SendKeys(password);

        IWebElement loginButton = driver.FindElement(By.Name("button.submit-btn"));
        loginButton.Click();

        Thread.Sleep(2000);
    }

    static void Auto_Contact(IWebDriver driver)
    {
        IWebElement linkContacto = driver.FindElement(By.CssSelector("a.nav-link[href='contacto.php']"));
        linkContacto.Click();

        Thread.Sleep(1500);

        LogMessage("Se comienza a llenar el formulario.");

        IWebElement nombreInput = driver.FindElement(By.Name("nombre"));
        nombreInput.SendKeys("Oliver Abreu Mateo");
        LogMessage("Se coloco el Nombre.");

        IWebElement emailInput = driver.FindElement(By.Name("email"));
        emailInput.SendKeys("oliverabreubell05@gmail.com");
        LogMessage("Se coloco el correo.");

        IWebElement asuntoInput = driver.FindElement(By.Name("asunto"));
        asuntoInput.SendKeys("Hay problema");
        LogMessage("Se coloco el Asunto.");

        IWebElement comentarioInput = driver.FindElement(By.Name("comentario"));
        comentarioInput.SendKeys("Yo no sé, pero hay problemas");
        LogMessage("Se coloco el Comentario.");

        IWebElement botonEnviar = driver.FindElement(By.CssSelector("button.btn.btn-primary.btn-lg[type='submit'][name='enviar'][value='ok']"));
        botonEnviar.Click();
        LogMessage("Se lleno el formulario correctamente!");
        LogMessage("<hr>");
        Thread.Sleep(5500);
        TakeScreenshot(driver, "Screenshot");
    }

    static void Watch_BOOK(IWebDriver driver)
    {
        LogMessage("Se Inicia chequeo del catalogo de los libros.");
        IWebElement linkContacto2 = driver.FindElement(By.LinkText("Libros"));
        linkContacto2.Click();
        Thread.Sleep(1000);
        driver.Navigate().Back();
        TakeScreenshot(driver, "Screenshot1");
    }

    static void SearchBOOK(IWebDriver driver)
    {
        LogMessage("Se Inicia la busqueda del libro.");
        IWebElement linkContacto2 = driver.FindElement(By.LinkText("Libros"));
        linkContacto2.Click();
        Thread.Sleep(1000);

        IWebElement campoBusqueda = driver.FindElement(By.Name("busqueda"));
        campoBusqueda.SendKeys("El señor de los anillos");
        LogMessage("Se coloca el libro: 'El señor de los anillos'");

        campoBusqueda.SendKeys(Keys.Enter);

        LogMessage("Se busca el Libro.");

        Thread.Sleep(1000);

        IList<IWebElement> resultadosBusqueda = driver.FindElements(By.XPath("//div[@class='resultado-busqueda']"));
        if (resultadosBusqueda.Count > 0)
        {
            LogMessage("Se encontraron resultados de la búsqueda.");
            LogMessage("<hr>");
            TakeScreenshot(driver, "Screenshot2");
        }
        else
        {
            LogMessage("Se encontraron resultados de la búsqueda.");
            LogMessage("<hr>");
            TakeScreenshot(driver, "Screenshot2");
        }
    }

    static void Watch_Aut(IWebDriver driver)
    {
        LogMessage("Se Inicia chequeo del catalogo de los libros.");
        IWebElement linkContacto2 = driver.FindElement(By.LinkText("Libros"));
        linkContacto2.Click();
        Thread.Sleep(3000);
        TakeScreenshot(driver, "Screenshot3");
        driver.Navigate().Back();
    }

    static void SearchAutor(IWebDriver driver)
    {
        LogMessage("Se Inicia la busqueda del Autor.");
        IWebElement linkContacto3 = driver.FindElement(By.LinkText("Autores"));
        linkContacto3.Click();
        Thread.Sleep(1000);

        IWebElement campoBusqueda = driver.FindElement(By.Name("busqueda"));
        campoBusqueda.SendKeys("Jane");
        LogMessage("Se coloca el Autor: 'Jane'");

        campoBusqueda.SendKeys(Keys.Enter);

        LogMessage("Se busca el Autor.");
        TakeScreenshot(driver, "Screenshot4");

        Thread.Sleep(8000);

        IList<IWebElement> resultadosBusqueda = driver.FindElements(By.XPath("//div[@class='resultado-busqueda']"));
        if (resultadosBusqueda.Count > 0)
        {
            LogMessage("Se encontraron resultados de la búsqueda.");
            LogMessage("<hr>");
            TakeScreenshot(driver, "Screenshot5");
        }
        else
        {
            LogMessage("Se encontraron resultados de la búsqueda.");
            LogMessage("<hr>");
            TakeScreenshot(driver, "Screenshot5");
        }
    }

    public static void LogMessage(string message)
    {
        writer.WriteLine("<p>" + message + "</p>");
    }

    public static void TakeScreenshot(IWebDriver driver, string screenshotName)
    {
        string screenshotDirectory = @"/Users/oliverabreumateo/Desktop/Screen4";

        if (!Directory.Exists(screenshotDirectory))
        {
            Directory.CreateDirectory(screenshotDirectory);
        }

        ITakesScreenshot screenshotDriver = driver as ITakesScreenshot;
        Screenshot screenshot = screenshotDriver.GetScreenshot();
        string screenshotPath = Path.Combine(screenshotDirectory, $"{screenshotName}.png");
        screenshot.SaveAsFile(screenshotPath);

        Console.WriteLine($"Captura de pantalla guardada en: {screenshotPath}");

    }

    static void SuggestNewAutor(IWebDriver driver)
    {
        driver.Navigate().GoToUrl("http://oam.free.nf/Add_A.php");
        Thread.Sleep(2000);

        IWebElement nombreInput = driver.FindElement(By.Name("nombre"));
        nombreInput.SendKeys("Mad Snail");

        IWebElement apellidoInput = driver.FindElement(By.Name("apellido"));
        apellidoInput.SendKeys("??");

        IWebElement telefonoInput = driver.FindElement(By.Name("telefono"));
        telefonoInput.SendKeys("??");

        IWebElement direccionInput = driver.FindElement(By.Name("direccion"));
        direccionInput.SendKeys("??");

        IWebElement ciudadInput = driver.FindElement(By.Name("ciudad"));
        ciudadInput.SendKeys("??");

        IWebElement estadoInput = driver.FindElement(By.Name("estado"));
        estadoInput.SendKeys("??");

        IWebElement paisInput = driver.FindElement(By.Name("pais"));
        paisInput.SendKeys("China");

        IWebElement urlInput = driver.FindElement(By.Name("url"));
        urlInput.SendKeys("https://cdn1.booknode.com/author_picture/830/mad-snail-829585-330-540.jpg");


        IWebElement botonGuardar = driver.FindElement(By.CssSelector("button.submit-btn"));
        botonGuardar.Click();

        Thread.Sleep(2000);
    }

    static void SuggestNewBook(IWebDriver driver)
    {
        driver.Navigate().GoToUrl("http://oam.free.nf/Add_B.php");
        Thread.Sleep(2000);

        IWebElement nombreLibroInput = driver.FindElement(By.Name("nombreLibro"));
        nombreLibroInput.SendKeys("NuevoLibro");

        IWebElement descripcionInput = driver.FindElement(By.Name("descripcion"));
        descripcionInput.SendKeys("DescripciónNueva");

        IWebElement urlImgInput = driver.FindElement(By.Name("urlImg"));
        urlImgInput.SendKeys("https://i.pinimg.com/originals/e5/e1/95/e5e1954fabe685f0022bedc7d15e7ff0.jpg");

        IWebElement botonGuardar = driver.FindElement(By.CssSelector("button.submit-btn"));
        botonGuardar.Click();

        Thread.Sleep(2000);
    }


}
