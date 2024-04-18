<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Añadir Autores</title>
    <link rel="stylesheet" href="Style/Sty3.css">
        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous">
    <link rel="stylesheet" href="style.css">
    <link rel="icon" href="icono.png">
</head>

<nav class="navbar navbar-expand-md navbar-dark fixed-top bg-dark" id="navbar">
    <div class="container-fluid">
        <a class="navbar-brand" href="/Inicio.php">BOOKERS</a>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarCollapse"
            aria-controls="navbarCollapse" aria-expanded="false" aria-label="Toggle navigation" id="navbarToggler">
            <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse justify-content-end" id="navbarCollapse">
            <ul class="navbar-nav mb-2 mb-md-0">
                <li class="nav-item">
                    <a class="nav-link active" aria-current="page" href="libros.php">Libros</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="autores.php">Autores</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="contacto.php">Contacto</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="Add_B.php">Agregar Libro</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="Add_A.php">Agregar Autor</a>
                </li>
            </ul>
            </form>
        </div>
    </div>
</nav>

<body>
    <div class="form-structor">
        <div class="signup">
            <h2 class="form-title" id="signup"><span>Añadir</span>Autor</h2>
            <div class="form-holder">
                <form action="<?php echo htmlspecialchars($_SERVER["PHP_SELF"]); ?>" method="POST">
                    <input type="text" class="input" name="nombre" placeholder="Nombre" required>
                    <input type="text" class="input" name="apellido" placeholder="Apellido" required>
                    <input type="tel" class="input" name="telefono" placeholder="Teléfono">
                    <input type="text" class="input" name="direccion" placeholder="Dirección">
                    <input type="text" class="input" name="ciudad" placeholder="Ciudad">
                    <input type="text" class="input" name="estado" placeholder="Estado">
                    <input type="text" class="input" name="pais" placeholder="País">
                    <input type="url" class="input" name="url" placeholder="URL de la imagen del Autor">
                    <button type="submit" class="submit-btn">Guardar</button>
                </form>
            </div>
        </div>
    </div>

<?php

$conexion = new mysqli("sql212.infinityfree.com", "if0_36307240", "sMEBzmPZyj2PM", "if0_36307240_bib");

if ($conexion->connect_error) {
    die("Error de conexión: " . $conexion->connect_error);
}

$nombre = $apellido = $telefono = $direccion = $ciudad = $estado = $pais = $url = "";

if($_SERVER["REQUEST_METHOD"] == "POST"){

    $nombre = htmlspecialchars(trim($_POST["nombre"]));
    $apellido = htmlspecialchars(trim($_POST["apellido"]));
    $telefono = htmlspecialchars(trim($_POST["telefono"]));
    $direccion = htmlspecialchars(trim($_POST["direccion"]));
    $ciudad = htmlspecialchars(trim($_POST["ciudad"]));
    $estado = htmlspecialchars(trim($_POST["estado"]));
    $pais = htmlspecialchars(trim($_POST["pais"]));
    $url = htmlspecialchars(trim($_POST["url"]));

    $sql = "INSERT INTO autores (nombre, apellido, telefono, direccion, ciudad, estado, pais, url) VALUES (?, ?, ?, ?, ?, ?, ?, ?)";

    if($stmt = $conexion->prepare($sql)){

        $stmt->bind_param("ssssssss", $nombre, $apellido, $telefono, $direccion, $ciudad, $estado, $pais, $url);

        if($stmt->execute()){
            echo "<p>Autor agregado exitosamente.</p>";
        } else{
            echo "<p>Ocurrió un error al agregar el autor.</p>";
        }
    }

    $stmt->close();
}

$conexion->close();
?>
</body>
</html>
