<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMa.Master" AutoEventWireup="true" CodeBehind="PanelCliente.aspx.cs" Inherits="CapaPresentacion.PanelCliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/progres.css" rel="stylesheet" />
        <style>
        .switch {
            position: relative;
            display: inline-block;
            width: 200px;
            height: 42px;
        }

            .switch input {
                /*opacity: 0;*/
                width: 0;
                height: 0;
            }

            .switch label {
                position: absolute;
                cursor: pointer;
                top: 0;
                left: 0;
                right: 0;
                bottom: 0;
                background-color: #3292e0; /* NEGRO 1E1E1E */
                transition: .4s;
                border-radius: 25px;
            }

                .switch label::before {
                    position: absolute;
                    content: "";
                    height: 20px;
                    width: 60px;
                    left: 5px;
                    bottom: 4px;
                    background-color: #ff9800; /* AMARILLO */
                    transition: .4s;
                    border-radius: 20px;
                }

            .switch input:checked + label {
                background-color: #ff9800; /* AMARILLO */
            }

                .switch input:checked + label::before {
                    transform: translateX(130px);
                    background-color: #3292e0; /* negro */
                }

            .switch input:checked::before,
            .switch input:checked::after {
                color: #fff;
            }

            .switch input::before,
            .switch input::after {
                position: absolute;
                top: 50%;
                transform: translateY(-55%);
                font-weight: bolder;
                z-index: 2;
            }

            .switch input::before {
                content: "OFF";
                left: 20px;
                color: #fff;
            }

            .switch input::after {
                content: "ON";
                right: 20px;
                color: #3292e0;  /* si */
            }

            .switch input:checked::before {
                color: #1E1E1E;
            }

            .switch input:checked::after {
                color: #fff;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="row justify-content-center align-items-center mb-2">
        <button type="button" id="btnAdd" class="btn btn-danger btn-sm mr-3"><i class="fas fa-user-plus"></i> Agregar Nuevo</button>
        <button type="button" id="btnListar" class="btn btn-primary btn-sm mr-3"><i class="fas fa-tasks"></i> Lista Desencriptada</button>
        <button type="button" id="btnListarEncrip" class="btn btn-primary btn-sm mr-3"><i class="fas fa-tasks"></i> Lista Encriptada</button>
        <button type="button" id="btnDetallee" class="btn btn-success btn-sm"><i class="fas fa-tools"></i> Informacion</button>
    </div>

    <div class="card shadow mb-4" id="loaddd">
        <div class="card-header py-3 bg-second-primary">
            <h6 class="m-0 font-weight-bold text-white"><i class="fas fa-address-book"></i> DETALLE DE CLIENTES</h6>
        </div>
        <div class="card-body">
            <div class="row">
                <div class="col-sm-12">
                    <table class="table table-bordered" id="tbCliente" cellspacing="0" style="width: 100%">
                        <thead>
                            <tr>
                                <th>Id</th>
                                <th>RUC</th>
                                <th>Razon Social</th>
                                <th>Telefono</th>
                                <th>Correo</th>
                                <th>Fecha Registro</th>
                                <th>Acciones</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>

        </div>
    </div>


    <div class="modal fade" id="modalclien" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h6 id="myTitulop">Detalle Cliente</h6>
                    <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">
                    <input type="hidden" value="0" id="txtIdCliente">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-row">
                                <div class="form-group col-sm-6">
                                    <label for="txtRuc">RUC:</label>
                                    <input type="text" class="form-control form-control-sm input-validar" id="txtRuc" name="RUC">
                                </div>
                                <div class="form-group col-sm-6">
                                    <label for="txtRazon">RAZON SOCIAL:</label>
                                    <input type="text" class="form-control form-control-sm input-validar" id="txtRazon" name="Razon Social">
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="form-group col-sm-6">
                                    <label for="txtTelefono">CONTACTO:</label>
                                    <input type="text" class="form-control form-control-sm input-validar" id="txtTelefono" name="CONTACTO">
                                </div>
                                <div class="form-group col-sm-6">
                                    <label for="txtCorreo">CORREO:</label>
                                    <input type="text" class="form-control form-control-sm input-validar" id="txtCorreo" name="CORREO">
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="form-group col-sm-12">
                                    <label for="txtDireccion">DIRECCION</label>
                                    <input type="text" class="form-control form-control-sm input-validar" id="txtDireccion" name="DIRECCION">
                                </div>
                            </div>
                            <div class="row justify-content-center align-items-center mb-2">
                                <button type="button" id="btnGuardarCamClie" class="btn btn-primary btn-sm mr-3"><i class="fas fa-user-plus"></i> Guardar Cambios</button>
                                <button class="btn btn-danger btn-sm" type="button" data-dismiss="modal">Cancel</button>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalClaveS" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h6><i class="fas fa-key"></i> Seguridad de Acceso</h6>
                <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">×</span>
                </button>
            </div>
            <div class="modal-body">
                <div class="form-row align-items-end">
                                
                    <div class="form-group col-sm-6">
                        <label for="txtClaveActualC">Ingrese Clave</label>
                        <input type="password" class="form-control form-control-sm" id="txtClaveActualC" name="Clave Actual">
                    </div>
                    
                    <div class="form-group col-sm-3">
                       <button type="button" class="btn btn-primary btn-block btn-sm" id="btnValidarC"><i class="fas fa-key"></i> Validar</button>
                    </div>
                    <div class="form-group col-sm-3">
                        <button class="btn btn-danger btn-sm" type="button" data-dismiss="modal">Cancel</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

        <div class="modal fade" id="modalApio" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h6><i class="fas fa-key"></i> Seguridad de Acceso</h6>
                <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">×</span>
                </button>
            </div>
            <div class="modal-body">
                <div class="form-row align-items-end">
                                
                    <div class="form-group col-sm-6 text-center">
                        <label for="cboBusciente">Api Establecer</label><br />
                        <span class="switch">
                            <input type="checkbox" id="switcher">
                            <label for="switcher"></label>
                        </span>
                    </div>
                    
                    <div class="form-group col-sm-3">
                       <button type="button" class="btn btn-primary btn-block btn-sm" id="btnInterr"><i class="fas fa-key"></i> Validar</button>
                    </div>
                    <div class="form-group col-sm-3">
                        <button class="btn btn-danger btn-sm" type="button" data-dismiss="modal">Cancel</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script src="jsfro/PanelCliente.js" type="text/javascript"></script>
</asp:Content>
