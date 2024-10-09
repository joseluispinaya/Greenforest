<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMa.Master" AutoEventWireup="true" CodeBehind="PanelPerfil.aspx.cs" Inherits="CapaPresentacion.PanelPerfil" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/strense.css" rel="stylesheet">
    <style>
        .box {
            position: relative;
            width: 100%; /* Ajusta el ancho al contenedor */
            height: 50px;
        }

            .box h2 {
                position: absolute;
                margin-bottom: 10px;
                top: -30px;
                font-size: 1em;
                color: #fff;
                font-weight: 500;
            }

            .box input {
                position: absolute;
                /*width: 100%;*/
                inset: 2px;
                z-index: 10;
                font-size: 1em;
                border: none;
                outline: none;
                padding: 10px 15px;
                background: #333;
                color: #fff;
            }

            .box .password-strength {
                position: absolute;
                inset: 0;
                background: #1115;
            }

                .box .password-strength:nth-child(3) {
                    filter: blur(5px);
                }

                .box .password-strength:nth-child(4) {
                    filter: blur(10px);
                }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="row">
    <div class="col-sm-8">

        <div class="card shadow mb-4">
            <div class="card-header py-3 bg-second-primary">
                <h6 class="m-0 font-weight-bold text-white"><i class="fas fa-user"></i> Mis Datos</h6>
            </div>
            <div class="card-body" id="loadPe">
                <input type="hidden" value="0" id="txtIdUsuarioP">
                <div class="row">
                    <div class="col-sm-4">
                        <div class="form-row">
                            <div class="form-group col-sm-12">
                                <img id="imgFotop" src="Imagenes/sinimagen.png"
                                    class="rounded mx-auto d-block" style="width: 140px; height: 140px;">
                            </div>
                        </div>
                        
                        <hr>
                        <div class="row">
                            <div class="col-sm-12">
                                <button type="button" class="btn btn-success btn-sm btn-block"
                                    id="btnGuardarCambiosp">Guardar Cambios</button>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-8">
                        <div class="form-row">
                            <div class="form-group col-sm-6">
                                <label for="txtNombre">Nombre</label>
                                <input type="text" class="form-control form-control-sm" id="txtNombre">
                            </div>
                            <div class="form-group col-sm-6">
                                <label for="txtapellido">Apellidos</label>
                                <input type="text" class="form-control form-control-sm" id="txtapellido">
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="form-group col-sm-6">
                                <label for="txtCorreo">Correo</label>
                                <input type="text" class="form-control form-control-sm" id="txtCorreo">
                            </div>
                            <div class="form-group col-sm-6">
                                <label for="txtRol">Rol</label>
                                <input type="text" class="form-control form-control-sm" disabled id="txtRol">
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="form-group col-sm-12">
                                <label for="txtFotop">Seleccionar Foto</label>
                                <input class="form-control-file" type="file" id="txtFotop" />
                            </div>
                        </div>
                    </div>
                </div>

                
            </div>
        </div>

    </div>

        <div class="col-sm-4">
            <div class="card shadow mb-4">
                <div class="card-header py-3 bg-second-primary">
                    <h6 class="m-0 font-weight-bold text-white"><i class="fas fa-key"></i> Cambiar
                Contraseña</h6>
                </div>
                <div class="card-body" style="background-color: #333;">
                    <div class="form-row">
                        <div class="form-group col-sm-12">
                            <div class="containerz">
                                <h2>Nueva Contraseña <span id="texto">-</span></h2>
                                <div class="inputBox">
                                    <input type="password" id="txtClaveNueva" class="input-validar" placeholder="Clave Nueva" name="Clave Nueva">
                                    <div class="shown"></div>
                                </div>

                                <div class="strengthMeter"></div>
                            </div>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-group col-sm-12">
                            <div class="containerzz">
                                <h2>Confirmar Contraseña</h2>
                                <div class="inputBoxz">
                                    <input type="password" id="txtConfirmarClave" class="input-validar" placeholder="Confirmar Clave" name="Confirmar Clave">
                                    <div class="showz" id="showConfirmarClave"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-group col-sm-12">
                            <div class="containerzz">
                                <h2>Contraseña Actual</h2>
                                <div class="inputBoxz">
                                    <input type="password" id="txtClaveActual" class="input-validar" placeholder="Clave Actual" name="Clave Actual">
                                    <div class="showz" id="showClaveActual"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <button type="button" class="btn btn-success btn-sm btn-block"
                                id="btnCambiarClave">
                                Guardar
                        Cambios</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
<%--    <div class="col-sm-4">
        <div class="card shadow mb-4">
            <div class="card-header py-3 bg-second-primary">
                <h6 class="m-0 font-weight-bold text-white"><i class="fas fa-key"></i> Cambiar Contraseña</h6>
            </div>

            <div class="card-body" id="loadis" style="background-color: #333;">
                <br />
                <div class="form-row">
                    <div class="form-group col-sm-12">
                        <div class="box">
                            <h2>Nueva Contraseña <span id="texto">-</span></h2>
                            <input type="password" id="txtClaveNueva" class="input-validar" placeholder="Clave Nueva" name="Clave Nueva">
                            <div class="password-strength"></div>
                            <div class="password-strength"></div>
                            <div class="password-strength"></div>
                        </div>
                    </div>
                </div>

                <div class="form-row">
                    <div class="form-group col-sm-12">
                        <label for="txtConfirmarClave" style="color: #fff;">Confirmar Contraseña</label>
                        <input type="password" class="form-control form-control-sm input-validar"
                            id="txtConfirmarClave" name="Confirmar Clave" style="background-color: #333; color: #fff;">
                    </div>
                </div>
                <div class="form-row">
                    <div class="form-group col-sm-12">
                        <label for="txtClaveActual" style="color: #fff;">Contraseña Actual</label>
                        <input type="password" class="form-control form-control-sm input-validar"
                            id="txtClaveActual" name="Clave Actual" style="background-color: #333; color: #fff;">
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <button type="button" class="btn btn-success btn-sm btn-block" id="btnCambiarClave">Guardar
                            Cambios</button>
                    </div>
                </div>
            </div>
          
        </div>
    </div>--%>
</div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script src="jsfro/PanelPerfil.js" type="text/javascript"></script>
</asp:Content>
