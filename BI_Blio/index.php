<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>LOG</title>
    <link rel="stylesheet" href="Style/Sty.css">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous">
    <link rel="stylesheet" href="style.css">
    <link rel="icon" href="icono.png">
</head>
<body>


<div class="form-structor">
    <div class="signup">
        <h2 class="form-title" id="signup"><span>or</span>Sign up</h2>
        <div class="form-holder">
            <input type="text" class="input" placeholder="Name" />
            <input type="email" class="input" placeholder="Email" />
            <input type="password" class="input" placeholder="Password" />
        </div>
        <button class="submit-btn">Sign up</button>
    </div>
    <div class="login slide-up">
        <div class="center">
            <h2 class="form-title" id="login"><span>or</span>Log in</h2>
            <div class="form-holder">
                <form action="<?php echo htmlspecialchars($_SERVER["PHP_SELF"]); ?>" method="post">
                    <input type="email" class="input" placeholder="Email" name="email" required>
                    <input type="password" class="input" placeholder="Password" name="password" required>
                    <span class="help-block"><?php echo $email_err; ?></span>
                    <span class="help-block"><?php echo $password_err; ?></span>
                </div>
                <button type="submit" class="submit-btn">Log in</button>
                </form>
            </div>
        </div>
    </div>
</div>

<?php
$conexion = new mysqli("sql212.infinityfree.com", "if0_36307240", "sMEBzmPZyj2PM", "if0_36307240_bib");

if ($conexion->connect_error) {
    die("Error de conexión: " . $conexion->connect_error);
}

$email = $password = "";
$email_err = $password_err = "";

if($_SERVER["REQUEST_METHOD"] == "POST"){

    if(empty(trim($_POST["email"]))){
        $email_err = "Por favor, ingrese su correo electrónico.";
    } else{
        $email = trim($_POST["email"]);
    }
    
    if(empty(trim($_POST["password"]))){
        $password_err = "Por favor, ingrese su contraseña.";
    } else{
        $password = trim($_POST["password"]);
    }
    
    if(empty($email_err) && empty($password_err)){
        $sql = "SELECT id, email, password FROM usser WHERE email = ?";
        
        if($stmt = $conexion->prepare($sql)){
            $stmt->bind_param("s", $param_email);
            
            $param_email = $email;
            
            if($stmt->execute()){
                $stmt->store_result();
                
                if($stmt->num_rows == 1){                    
                    $stmt->bind_result($id, $email, $hashed_password);
                    if($stmt->fetch()){
                        if(password_verify($password, $hashed_password)){
                            session_start();
                            
                            $_SESSION["loggedin"] = true;
                            $_SESSION["id"] = $id;
                            $_SESSION["email"] = $email;                            
                            
                            header("location: welcome.php");
                        } else{
                            $password_err = "La contraseña que ha ingresado no es válida.";
                        }
                    }
                } else{
                    $email_err = "No se encontró ninguna cuenta registrada con ese correo electrónico.";
                }
            } else{
                echo "Oops! Algo salió mal. Por favor, inténtelo de nuevo más tarde.";
            }
        }
        $stmt->close();
    }
}
?>
<script>
        console.clear();

        const loginBtn = document.getElementById('login');
        const signupBtn = document.getElementById('signup');

        loginBtn.addEventListener('click', (e) => {
            let parent = e.target.parentNode.parentNode;
            Array.from(e.target.parentNode.parentNode.classList).find((element) => {
                if(element !== "slide-up") {
                    parent.classList.add('slide-up')
                }else{
                    signupBtn.parentNode.classList.add('slide-up')
                    parent.classList.remove('slide-up')
                }
            });
        });

        signupBtn.addEventListener('click', (e) => {
            let parent = e.target.parentNode;
            Array.from(e.target.parentNode.classList).find((element) => {
                if(element !== "slide-up") {
                    parent.classList.add('slide-up')
                }else{
                    loginBtn.parentNode.parentNode.classList.add('slide-up')
                    parent.classList.remove('slide-up')
                }
            });
        });
    </script>
</body>
</html>
