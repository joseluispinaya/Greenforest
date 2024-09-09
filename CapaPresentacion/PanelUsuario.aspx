<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMa.Master" AutoEventWireup="true" CodeBehind="PanelUsuario.aspx.cs" Inherits="CapaPresentacion.PanelUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="d-sm-flex align-items-center justify-content-between mb-4">
        <h1 class="h3 mb-0 text-gray-800">Panel Usuarios</h1>
    </div>
    <div class="card shadow mb-4">
        <div class="card-header py-3 bg-second-primary">
            <h6 class="m-0 font-weight-bold text-white">Lista de Usuarios</h6>
        </div>
        <div class="card-body">
            <div class="row">
                <div class="col-sm-3">
                    <button type="button" id="btnNuevoUsu" class="btn btn-success" ><i class="fas fa-user-plus"></i> Nuevo Usuario</button>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-sm-12">
                    <table class="table table-bordered" id="tbUsuario" cellspacing="0" style="width: 100%">
                        <thead>
                            <tr>
                                <th>Id</th>
                                <th>Foto</th>
                                <th>Nombre</th>
                                <th>Apellidos</th>
                                <th>Correo</th>
                                <th>Cargo</th>
                                <th>Estado</th>
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

    <div class="modal fade" id="modalData" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h6>Detalle Usuario</h6>
                    <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">
                    <input type="hidden" value="0" id="txtIdUsuario">
                    <div class="row">
                        <div class="col-sm-8">
                            <div class="form-row">
                                <div class="form-group col-sm-6">
                                    <label for="txtNombre">Nombres</label>
                                    <input type="text" class="form-control form-control-sm input-validar" id="txtNombre" name="Nombre">
                                </div>
                                <div class="form-group col-sm-6">
                                    <label for="txtapellido">Apellidos</label>
                                    <input type="text" class="form-control form-control-sm input-validar" id="txtapellido" name="Apellidos">
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="form-group col-sm-6">
                                    <label for="txtCorreo">Correo</label>
                                    <input type="email" class="form-control form-control-sm input-validar" id="txtCorreo" name="Correo">
                                </div>
                                <div class="form-group col-sm-6">
                                    <label for="txtClave">Contraseña</label>
                                    <input type="text" class="form-control form-control-sm input-validar" id="txtClave" name="Contraseña">
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="form-group col-sm-6">
                                    <label for="cboEstado">Estado</label>
                                    <select class="form-control form-control-sm" id="cboEstado">
                                        <option value="1">Activo</option>
                                        <option value="0">No Activo</option>
                                    </select>
                                </div>
                                <div class="form-group col-sm-6">
                                    <label for="cboRol">Rol</label>
                                    <select class="form-control form-control-sm" id="cboRol">
                                    </select>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="form-group col-sm-12">
                                    <label for="txtFoto">Foto</label>
                                    <input class="form-control-file" type="file" id="txtFoto" />
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4 text-center">
                            <%--<img id="imgUsuario" style="max-width:200px;" src="https://images.unsplash.com/photo-1519648023493-d82b5f8d7b8a?w=300" class="rounded mx-auto d-block" alt="Foto usuario">--%>
                            <img id="imgUsuarioM" src="Imagenes/sinimagen.png" alt="Foto usuario" style="height: 150px; max-width: 150px; border-radius: 50%;">
                        </div>

                    </div>
                </div>
                <div class="modal-footer">
                    <button id="btnGuardarCambios" class="btn btn-primary btn-sm" type="button">Guardar</button>
                    <button class="btn btn-danger btn-sm" type="button" data-dismiss="modal">Cancel</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script src="jsfro/PanelUsuario.js" type="text/javascript"></script>
</asp:Content>
