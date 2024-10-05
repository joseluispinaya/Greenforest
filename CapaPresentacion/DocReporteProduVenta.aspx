<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocReporteProduVenta.aspx.cs" Inherits="CapaPresentacion.DocReporteProduVenta" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta name="viewport" content="width=device-width" />
    <title>Venta Productos</title>
    <%--<link href="vendor/sweetalert/sweetalert.css" rel="stylesheet"/>
    <link href="css/frmReporteVen.css" rel="stylesheet" type="text/css"/>--%>

    <style>
        .contenedor {
            /*width: 900px !important;*/
            width: 100%;
            /*height: 842px !important;*/
            max-width: 900px;
            /*margin: auto;*/
            margin: 0 auto;
            padding: 10px;
            margin-bottom: 10px; /* Add some space between the two containers */
            box-sizing: border-box;
        }

        body {
            font-family: Arial, Helvetica, sans-serif
        }

        p.title {
            font-weight: bold;
        }

        p.title2 {
            font-weight: bold;
            color: #03A99F;
            font-size: 20px;
        }

        p.text {
            font-size: 15px;
            font-weight: 100;
            color: #858585;
        }

        p {
            margin: 0px
        }

        .tbth {
            text-align: left;
        }

        table.tbproductos {
            border-collapse: separate;
            border-spacing: 4px;
        }

            table.tbproductos thead tr th {
                background-color: #03A99F;
                padding: 10px;
                font-size: 15px;
                color: white;
            }

            table.tbproductos tbody tr td {
                padding: 5px;
            }

        .item {
            font-size: 15px;
            font-weight: 100;
            color: #757575;
        }

        .item-2 {
            font-size: 15px;
            font-weight: bold;
            color: #757575;
        }

        .item-3 {
            font-size: 15px;
            font-weight: bold;
            background-color: #03A99F;
            color: white;
            text-align: center;
        }

        .td-item {
            border-bottom: 2px solid #E8E8E8 !important;
        }
    </style>
</head>
<body>
    <div style="font-size: 11px; text-align: right;">
        <div style="text-align: center;">
            <button type="button" id="Imprimir" onclick="javascript:imprSelec('seleccion')" style="background-color: #4CAF50; color: white; padding: 10px 20px; border: none; border-radius: 5px; cursor: pointer;">
                IMPRIMIR
            </button>
        </div>
        <br />
    </div>
    <div class="contenedor" id="seleccion">

        <table style="width: 100%">
            <tr>
                <td>
                    <img src="ImagePro/reportIm.png" style="width: 120px; height: 120px" />
                </td>
                <td style="text-align: right">
                    <table style="margin-right: 0; margin-left: auto">
                        <tr>
                            <td>
                                <p class="title2">INFORME DE PRODUCTOS</p>
                            </td>
                        </tr>
                        <tr>
                            <td><span id="codi">000001232</span></td>
                        </tr>
                        <tr>
                            <td><span id="fechar">12/09/2024</span></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <table style="width: 100%">
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <p class="title">GREEN FOREST S.A.</p>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <p class="text">Direccion: Avenida Ejército Nacional</p>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <p class="text">Correo: info@greenforest.com.bo</p>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="text-align: right">
                    <table style="margin-right: 0; margin-left: auto">
                        <tr>
                            <td>
                                <p class="title">DETALLE REPORTE</p>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <p class="text" id="razonsocial">Razon social</p>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <p class="text" id="ruc">RUC</p>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <br />

        <table class="tbproductos" id="tbDetalles" style="width: 100%">
            <thead>
                <tr>
                    <th class="tbth">Producto</th>
                    <th class="tbth" style="width: 90px">Codigo</th>
                    <th class="tbth" style="width: 80px">Foto</th>
                    <th class="tbth" style="width: 100px">Precio</th>
                    <th class="tbth" style="width: 90px">Cantidad</th>
                    <th class="tbth" style="width: 100px">Total</th>
                </tr>
            </thead>
            <tbody>
            </tbody>
            <tfoot>
                <tr>
                    <td colspan="4" rowspan="2"></td>
                    <td class="td-item">
                        <br />
                        <p class="item-2">Cantidad</p>
                    </td>
                    <td class="item-3">
                        <p id="cantTotal">0</p>
                    </td>
                </tr>
                <tr>
                    <td class="td-item">
                        <br />
                        <p class="item-2">Total</p>
                    </td>
                    <td class="item-3">
                        <p id="costoTotal">0.00 /Bs.</p>
                    </td>
                </tr>
            </tfoot>
        </table>

    </div>

    <script src="vendor/jquery/jquery.min.js"></script>
    <%--<script src="vendor/sweetalert/sweetalert.js"></script>--%>
    <script src="jsfro/DocReporteProduVenta.js" type="text/javascript"></script>
</body>
</html>
