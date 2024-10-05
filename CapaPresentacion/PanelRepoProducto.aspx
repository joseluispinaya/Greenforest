﻿<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMa.Master" AutoEventWireup="true" CodeBehind="PanelRepoProducto.aspx.cs" Inherits="CapaPresentacion.PanelRepoProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="vendor/jquery-ui/jquery-ui.css" >
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

<div class="card shadow mb-4">
    <div class="card-header py-3 bg-second-primary">
        <h6 class="m-0 font-weight-bold text-white">Reporte Productos Venta</h6>
    </div>
    <div class="card-body">
        <div class="form-row align-items-end" id="omitirep">

            <div class="form-group col-sm-3">
                <label for="txtFechaInicio">Fecha Inicio</label>
                <input type="text" class="form-control form-control-sm" id="txtFechaInicio">
            </div>
            <div class="form-group col-sm-3">
                <label for="txtFechaFin">Fecha Fin</label>
                <input type="text" class="form-control form-control-sm" id="txtFechaFin">
            </div>
            <div class="form-group col-sm-3">
                <button type="button" class="btn btn-success btn-block btn-sm" id="btnBuscar"><i
                        class="fas fa-search"></i> Buscar</button>
            </div>
            <div class="form-group col-sm-3">
                <button class="btn btn-info btn-block btn-sm" type="button" id="btnImprimirP"><i
                        class="fas fa-print"></i> Reporte</button>
            </div>
        </div>

        <hr />
        <div class="row">
            <div class="col-sm-12">
                <table id="tbdatare" class="table table-sm table-striped" cellspacing="0" style="width:100%">
                    <thead>
                        <tr>
                            <th>Producto</th>
                            <th>Descripcion</th>
                            <th>Codigo</th>
                            <th>Imagen</th>
                            <th>Precio</th>
                            <th>Cantidad</th>
                            <th>Total Venta</th>
                        </tr>
                    </thead>
                    <tbody>

                    </tbody>
                </table>
            </div>
        </div>
    </div>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script src="vendor/jquery-ui/jquery-ui.js"></script>
    <script src="vendor/jquery-ui/idioma/datepicker-es.js"></script>
    <script src="jsfro/PanelRepoProducto.js" type="text/javascript"></script>
</asp:Content>
