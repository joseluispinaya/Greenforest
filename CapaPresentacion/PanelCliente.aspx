<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMa.Master" AutoEventWireup="true" CodeBehind="PanelCliente.aspx.cs" Inherits="CapaPresentacion.PanelCliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/progres.css" rel="stylesheet" />
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script src="jsfro/PanelCliente.js" type="text/javascript"></script>
</asp:Content>
