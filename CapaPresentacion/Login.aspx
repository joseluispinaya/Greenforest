<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CapaPresentacion.Login" %>

<!DOCTYPE html>

<html lang="ES">

<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Login</title>

    <!-- Custom fonts for this template-->
    <link href="vendor/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css">
    <link
        href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i"
        rel="stylesheet">

    <!-- Custom styles for this template-->
    <link href="css/sb-admin-2.css" rel="stylesheet">
    <link href="vendor/toastr/toastr.min.css" rel="stylesheet">
    <link href="vendor/sweetalert/sweetalert.css" rel="stylesheet">
</head>
    <!-- body class="bg-gradient-info"-->
<body class="bg-gradient-success">

    <div class="container">

        <!-- Outer Row -->
        <div class="row justify-content-center">

            <div class="col-xl-10 col-lg-12 col-md-9">

                <div class="card o-hidden border-0 shadow-lg my-5">
                    <div class="card-body p-0">
                        <!-- Nested Row within Card Body -->
                        <div class="row">
                            <div class="col-lg-6 d-none d-lg-block bg-login-image"></div>
                            <div class="col-lg-6">
                                <div class="p-5">
                                    <div class="text-center">
                                        <h1 class="h4 text-gray-900 mb-4">Bienvenido</h1>
                                    </div>
                                    <form class="user">
                                        <div class="form-group">
                                            <input type="email" class="form-control form-control-user" id="txtcorreo" placeholder="Correo">
                                        </div>
                                        <div class="form-group">
                                            <input type="password" class="form-control form-control-user" id="txtpassword" placeholder="Contraseña">
                                        </div>
                                        <%--<div class="form-group">
                                            <div class="custom-control custom-checkbox small">
                                                <input type="checkbox" class="custom-control-input" id="chkMantenerSesion">
                                                <label class="custom-control-label" for="chkMantenerSesion">Mantener Sesión</label>
                                            </div>
                                        </div>--%>

                                        <button type="button" class="btn btn-success btn-user btn-block" id="ingrsarLo">INGRESAR</button>
										<%--<a class="btn btn-primary btn-user btn-block" href="#">Ingresar</a>--%>
                                        <!-- <button type="submit" class="btn btn-primary btn-user btn-block">
                                            Ingresar 
                                        </button> -->
                                    </form>
                                    <hr>
                                    <div class="text-center">
                                        <a class="small" href="#">¿Olvidó su contraseña?</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>

        </div>

    </div>

    <script src="vendor/jquery/jquery.min.js"></script>
    <script src="vendor/toastr/toastr.min.js"></script>
    <script src="vendor/sweetalert/sweetalert.js"></script>
    <script src="vendor/loadingoverlay/loadingoverlay.min.js"></script>
    <script src="jsfro/Login.js" type="text/javascript"></script>
</body>
</html>
