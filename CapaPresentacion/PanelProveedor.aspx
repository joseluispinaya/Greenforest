<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMa.Master" AutoEventWireup="true" CodeBehind="PanelProveedor.aspx.cs" Inherits="CapaPresentacion.PanelProveedor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

<div class="row">
    <div class="col-sm-5">
        <div class="row">
            <div class="col-sm-12">
                <div class="card shadow mb-4" id="loaddd">
                    <div class="card-header py-3 bg-second-primary">
                        <h6 class="m-0 font-weight-bold text-white"><i class="fas fa-fw fa-street-view"></i> Compra</h6>
                    </div>
                    <div class="card-body">
                        <input type="hidden" value="0" id="txtIdProveedor">
                        <div class="form-row">
                            <div class="form-group col-sm-6">
                                <label for="txtCantidad">Cantidad Kls</label>
                                <input type="text" class="form-control form-control-sm input-validar"
                                    id="txtCantidad" name="Cantidad">
                            </div>
                            <div class="form-group col-sm-6">
                                <label for="txtPrecio">Total Pagado</label>
                                <input type="text" class="form-control form-control-sm input-validar"
                                    id="txtPrecio" name="Precio">
                            </div>
                        </div>

                        <div class="form-row">
                            <div class="form-group col-sm-6">
                                <div class="input-group input-group-sm mb-0">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text" id="inputGroupSubTotal">Fecha</span>
                                    </div>
                                    <input type="text" class="form-control" aria-label="Small" aria-describedby="inputGroupSubTotal" readonly id="txtFechaIni">
                                </div>
                            </div>
                            <div class="form-group col-sm-6 text-center">
                                <button type="button" id="btnGuardarProv" class="btn btn-success btn-sm"><i class="fas fa-cart-plus"></i> Guardar Cambio</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-7">
        <div class="row">
            <div class="col-sm-12">
                <div class="card shadow mb-4">
                    <div class="card-header py-3 bg-second-primary">
                        <h6 class="m-0 font-weight-bold text-white"><i class="fas fa-fw fa-clipboard-list"></i> Lista Compras</h6>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <table class="table table-bordered" id="tbProveedor" cellspacing="0" style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th>Id</th>
                                            <th>Cantidad</th>
                                            <th>Precio</th>
                                            <th>Fecha</th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script src="jsfro/PanelProveedor.js" type="text/javascript"></script>
</asp:Content>
