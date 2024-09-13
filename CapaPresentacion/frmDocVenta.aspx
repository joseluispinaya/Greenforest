<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmDocVenta.aspx.cs" Inherits="CapaPresentacion.frmDocVenta" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>ImprimirVenta</title>
    <%--<link href="css/sb-admin-2.css" rel="stylesheet"/>--%>
    <link href="vendor/sweetalert/sweetalert.css" rel="stylesheet"/>
    <link href="css/frmReporteVen.css" rel="stylesheet" type="text/css"/>
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
    <%--<div>
        
    </div>--%>
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
                            <p class="title2">NÚMERO VENTA</p>
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
                            <p class="title">CLIENTE</p>
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
                <th class="tbth" style="width: 130px">Cantidad</th>
                <th class="tbth" style="width: 130px">Precio</th>
                <th class="tbth" style="width: 130px">Total</th>
            </tr>
        </thead>
        <tbody>
        </tbody>
        <tfoot>
            <tr>
                <td colspan="2" rowspan="2"></td>
                <td class="td-item">
                    <p class="item-2">Cantidad Total</p>
                </td>
                <td class="item-3">
                    <p id="cantTotal">0</p>
                </td>
            </tr>
            <tr>
                <td class="td-item">
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
    <script src="vendor/sweetalert/sweetalert.js"></script>
    <script src="jsfro/frmDocVenta.js" type="text/javascript"></script>
</body>
</html>
