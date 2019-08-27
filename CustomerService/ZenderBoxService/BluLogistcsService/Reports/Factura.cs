using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for Factura
/// </summary>
public class Factura : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private System.Windows.Forms.BindingSource bindingSource1;
    private XRBarCode xrBarCode1;
    private XRPictureBox xrPictureBox1;
    private XRLabel xrLabel1;
    private XRLabel xrLabel2;
    private XRLabel xrLabel3;
    private XRLabel xrLabel4;
    private XRLabel xrLabel5;
    private XRLabel xrLabel9;
    private XRLabel xrLabel8;
    private XRLabel xrLabel7;
    private XRLabel xrLabel6;
    private XRLabel xrLabel10;
    private XRLabel xrLabel11;
    private XRLabel xrLabel16;
    private XRLabel xrLabel14;
    private XRLabel xrLabel13;
    private XRLabel xrLabel12;
    private XRLabel xrLabel17;
    private XRLabel xrLabel18;
    private XRLabel xrLabel19;
    private XRLabel xrLabel20;
    private XRLabel xrLabel22;
    private XRLabel xrLabel23;
    private XRLabel xrLabel24;
    private XRLabel xrLabel26;
    private XRLabel xrLabel27;
    private XRLabel xrLabel28;
    private XRLabel xrLabel29;
    private XRLabel xrLabel30;
    private XRLabel xrLabel35;
    private XRLabel xrLabel36;
    private XRLabel xrLabel37;
    private XRLabel xrLabel38;
    private XRLabel xrLabel39;
    private XRLabel xrLabel34;
    private XRLabel xrLabel32;
    private XRLabel xrLabel33;
    private XRLabel xrLabel43;
    private XRLabel xrLabel40;
    private XRLabel xrLabel41;
    private XRLabel xrLabel42;
    private XRLabel xrLabel44;
    private XRLabel xrLabel45;
    private XRLabel xrLabel46;
    private XRLabel xrLabel47;
    private XRLabel xrLabel48;
    private XRLabel xrLabel49;
    private XRLabel xrLabel50;
    private XRLabel xrLabel51;
    private XRLabel xrLabel52;
    private XRLabel xrLabel53;
    private XRLabel xrLabel54;
    private XRLabel xrLabel55;
    private XRLabel xrLabel56;
    private XRLabel xrLabel57;
    private XRLabel xrLabel58;
    private XRLabel xrLabel60;
    private XRLabel xrLabel61;
    private XRLabel xrLabel63;
    private XRLabel xrLabel64;
    private XRLabel xrLabel65;
    private XRLabel xrLabel66;
    private XRLabel xrLabel67;
    private XRLabel xrLabel68;
    private XRLabel xrLabel69;
    private XRLabel xrLabel71;
    private XRLabel xrLabel72;
    private XRLabel xrLabel73;
    private XRLabel xrLabel74;
    private XRLabel xrLabel76;
    private XRLabel xrLabel77;
    private XRLabel xrLabel78;
    private XRLabel xrLabel79;
    private XRLabel xrLabel80;
    private XRLabel xrLabel81;
    private XRLabel xrLabel82;
    private XRLabel xrLabel83;
    private XRLabel xrLabel84;
    private XRLabel xrLabel85;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public Factura()
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Factura));
            DevExpress.XtraPrinting.BarCode.Code128Generator code128Generator1 = new DevExpress.XtraPrinting.BarCode.Code128Generator();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrLabel47 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel48 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel49 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel50 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel51 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel52 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel53 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel54 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel55 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel56 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel57 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel58 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel60 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel61 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel63 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel64 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel65 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel66 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel67 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel68 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel69 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel71 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel72 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel73 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel74 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel76 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel77 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel78 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel79 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel80 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel81 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel82 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel83 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel84 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel85 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel43 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel40 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel41 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel42 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel35 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel36 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel37 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel38 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel39 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel34 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel32 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel33 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel30 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel29 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel23 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel24 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel26 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel27 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel28 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel22 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrBarCode1 = new DevExpress.XtraReports.UI.XRBarCode();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrLabel46 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel45 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel44 = new DevExpress.XtraReports.UI.XRLabel();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel47,
            this.xrLabel48,
            this.xrLabel49,
            this.xrLabel50,
            this.xrLabel51,
            this.xrLabel52,
            this.xrLabel53,
            this.xrLabel54,
            this.xrLabel55,
            this.xrLabel56,
            this.xrLabel57,
            this.xrLabel58,
            this.xrLabel60,
            this.xrLabel61,
            this.xrLabel63,
            this.xrLabel64,
            this.xrLabel65,
            this.xrLabel66,
            this.xrLabel67,
            this.xrLabel68,
            this.xrLabel69,
            this.xrLabel71,
            this.xrLabel72,
            this.xrLabel73,
            this.xrLabel74,
            this.xrLabel76,
            this.xrLabel77,
            this.xrLabel78,
            this.xrLabel79,
            this.xrLabel80,
            this.xrLabel81,
            this.xrLabel82,
            this.xrLabel83,
            this.xrLabel84,
            this.xrLabel85,
            this.xrLabel43,
            this.xrLabel40,
            this.xrLabel41,
            this.xrLabel42,
            this.xrLabel35,
            this.xrLabel36,
            this.xrLabel37,
            this.xrLabel38,
            this.xrLabel39,
            this.xrLabel34,
            this.xrLabel32,
            this.xrLabel33,
            this.xrLabel30,
            this.xrLabel29,
            this.xrLabel23,
            this.xrLabel24,
            this.xrLabel26,
            this.xrLabel27,
            this.xrLabel28,
            this.xrLabel17,
            this.xrLabel18,
            this.xrLabel19,
            this.xrLabel20,
            this.xrLabel22,
            this.xrLabel16,
            this.xrLabel14,
            this.xrLabel13,
            this.xrLabel12,
            this.xrLabel11,
            this.xrLabel10,
            this.xrLabel9,
            this.xrLabel8,
            this.xrLabel7,
            this.xrLabel6,
            this.xrLabel5,
            this.xrLabel4,
            this.xrLabel3});
            this.Detail.Dpi = 254F;
            this.Detail.HeightF = 1971.567F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel47
            // 
            this.xrLabel47.Dpi = 254F;
            this.xrLabel47.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel47.LocationFloat = new DevExpress.Utils.PointFloat(478.8958F, 52.81076F);
            this.xrLabel47.Name = "xrLabel47";
            this.xrLabel47.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel47.SizeF = new System.Drawing.SizeF(478.8958F, 43.53718F);
            this.xrLabel47.StylePriority.UseFont = false;
            this.xrLabel47.Text = "CIUDAD GENERA FACTURA:";
            // 
            // xrLabel48
            // 
            this.xrLabel48.Dpi = 254F;
            this.xrLabel48.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel48.LocationFloat = new DevExpress.Utils.PointFloat(478.8958F, 106.8658F);
            this.xrLabel48.Name = "xrLabel48";
            this.xrLabel48.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel48.SizeF = new System.Drawing.SizeF(98.5573F, 48.49812F);
            this.xrLabel48.StylePriority.UseFont = false;
            this.xrLabel48.Text = "TEL:";
            // 
            // xrLabel49
            // 
            this.xrLabel49.Dpi = 254F;
            this.xrLabel49.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel49.LocationFloat = new DevExpress.Utils.PointFloat(478.8958F, 197.3395F);
            this.xrLabel49.Name = "xrLabel49";
            this.xrLabel49.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel49.SizeF = new System.Drawing.SizeF(301.9557F, 40.2299F);
            this.xrLabel49.StylePriority.UseFont = false;
            this.xrLabel49.Text = "FACTURA VENTA";
            // 
            // xrLabel50
            // 
            this.xrLabel50.Dpi = 254F;
            this.xrLabel50.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel50.LocationFloat = new DevExpress.Utils.PointFloat(478.8958F, 237.5694F);
            this.xrLabel50.Name = "xrLabel50";
            this.xrLabel50.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel50.SizeF = new System.Drawing.SizeF(301.9557F, 37.70087F);
            this.xrLabel50.StylePriority.UseFont = false;
            this.xrLabel50.Text = "FORMA DE PAGO";
            // 
            // xrLabel51
            // 
            this.xrLabel51.Dpi = 254F;
            this.xrLabel51.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel51.LocationFloat = new DevExpress.Utils.PointFloat(478.8958F, 275.2702F);
            this.xrLabel51.Name = "xrLabel51";
            this.xrLabel51.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel51.SizeF = new System.Drawing.SizeF(301.9557F, 37.70081F);
            this.xrLabel51.StylePriority.UseFont = false;
            this.xrLabel51.Text = "FECHA FACTURA";
            // 
            // xrLabel52
            // 
            this.xrLabel52.Dpi = 254F;
            this.xrLabel52.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel52.LocationFloat = new DevExpress.Utils.PointFloat(478.8958F, 312.9711F);
            this.xrLabel52.Name = "xrLabel52";
            this.xrLabel52.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel52.SizeF = new System.Drawing.SizeF(301.9557F, 37.70081F);
            this.xrLabel52.StylePriority.UseFont = false;
            this.xrLabel52.Text = "FECHA REMESA";
            // 
            // xrLabel53
            // 
            this.xrLabel53.Dpi = 254F;
            this.xrLabel53.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel53.LocationFloat = new DevExpress.Utils.PointFloat(478.8958F, 350.6718F);
            this.xrLabel53.Name = "xrLabel53";
            this.xrLabel53.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel53.SizeF = new System.Drawing.SizeF(301.9557F, 36.92267F);
            this.xrLabel53.StylePriority.UseFont = false;
            this.xrLabel53.Text = "REMESA";
            // 
            // xrLabel54
            // 
            this.xrLabel54.Dpi = 254F;
            this.xrLabel54.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel54.LocationFloat = new DevExpress.Utils.PointFloat(478.8958F, 397.7109F);
            this.xrLabel54.Name = "xrLabel54";
            this.xrLabel54.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel54.SizeF = new System.Drawing.SizeF(450.9784F, 38.47903F);
            this.xrLabel54.StylePriority.UseFont = false;
            this.xrLabel54.Text = "FECHA ESTIMADA DE ENTREGA";
            // 
            // xrLabel55
            // 
            this.xrLabel55.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Model.origin.name")});
            this.xrLabel55.Dpi = 254F;
            this.xrLabel55.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel55.LocationFloat = new DevExpress.Utils.PointFloat(210.9635F, 498.9891F);
            this.xrLabel55.Name = "xrLabel55";
            this.xrLabel55.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel55.SizeF = new System.Drawing.SizeF(726.0365F, 38.47903F);
            this.xrLabel55.StylePriority.UseFont = false;
            this.xrLabel55.Text = "REMITENTE";
            // 
            // xrLabel56
            // 
            this.xrLabel56.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Model.origin.Identification")});
            this.xrLabel56.Dpi = 254F;
            this.xrLabel56.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel56.LocationFloat = new DevExpress.Utils.PointFloat(210.9635F, 537.4681F);
            this.xrLabel56.Name = "xrLabel56";
            this.xrLabel56.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel56.SizeF = new System.Drawing.SizeF(726.0365F, 36.14447F);
            this.xrLabel56.StylePriority.UseFont = false;
            this.xrLabel56.Text = "CC O NIT:";
            // 
            // xrLabel57
            // 
            this.xrLabel57.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Model.origin.address")});
            this.xrLabel57.Dpi = 254F;
            this.xrLabel57.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel57.LocationFloat = new DevExpress.Utils.PointFloat(210.9635F, 573.6125F);
            this.xrLabel57.Name = "xrLabel57";
            this.xrLabel57.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel57.SizeF = new System.Drawing.SizeF(726.0365F, 36.14447F);
            this.xrLabel57.StylePriority.UseFont = false;
            this.xrLabel57.Text = "DIRECCION";
            // 
            // xrLabel58
            // 
            this.xrLabel58.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Model.origin.Location.City")});
            this.xrLabel58.Dpi = 254F;
            this.xrLabel58.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel58.LocationFloat = new DevExpress.Utils.PointFloat(210.9635F, 609.757F);
            this.xrLabel58.Name = "xrLabel58";
            this.xrLabel58.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel58.SizeF = new System.Drawing.SizeF(726.0365F, 36.14447F);
            this.xrLabel58.StylePriority.UseFont = false;
            this.xrLabel58.Text = "ORIGEN";
            // 
            // xrLabel60
            // 
            this.xrLabel60.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Model.origin.Phone")});
            this.xrLabel60.Dpi = 254F;
            this.xrLabel60.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel60.LocationFloat = new DevExpress.Utils.PointFloat(318.256F, 645.9014F);
            this.xrLabel60.Name = "xrLabel60";
            this.xrLabel60.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel60.SizeF = new System.Drawing.SizeF(301.9557F, 36.14447F);
            this.xrLabel60.StylePriority.UseFont = false;
            this.xrLabel60.Text = "TELEFONO / CELULAR";
            // 
            // xrLabel61
            // 
            this.xrLabel61.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Model.receiver.phone")});
            this.xrLabel61.Dpi = 254F;
            this.xrLabel61.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel61.LocationFloat = new DevExpress.Utils.PointFloat(318.256F, 894.2505F);
            this.xrLabel61.Name = "xrLabel61";
            this.xrLabel61.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel61.SizeF = new System.Drawing.SizeF(637.0945F, 36.14447F);
            this.xrLabel61.StylePriority.UseFont = false;
            this.xrLabel61.Text = "TELEFONO / CELULAR";
            // 
            // xrLabel63
            // 
            this.xrLabel63.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Model.receiver.Location.City")});
            this.xrLabel63.Dpi = 254F;
            this.xrLabel63.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel63.LocationFloat = new DevExpress.Utils.PointFloat(227.5417F, 858.1061F);
            this.xrLabel63.Name = "xrLabel63";
            this.xrLabel63.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel63.SizeF = new System.Drawing.SizeF(727.8088F, 36.14441F);
            this.xrLabel63.StylePriority.UseFont = false;
            this.xrLabel63.Text = "DESTINO";
            // 
            // xrLabel64
            // 
            this.xrLabel64.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Model.receiver.Location.Address")});
            this.xrLabel64.Dpi = 254F;
            this.xrLabel64.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel64.LocationFloat = new DevExpress.Utils.PointFloat(227.5417F, 821.9616F);
            this.xrLabel64.Name = "xrLabel64";
            this.xrLabel64.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel64.SizeF = new System.Drawing.SizeF(727.8088F, 36.14447F);
            this.xrLabel64.StylePriority.UseFont = false;
            this.xrLabel64.Text = "DIRECCION";
            // 
            // xrLabel65
            // 
            this.xrLabel65.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Model.receiver.Identification")});
            this.xrLabel65.Dpi = 254F;
            this.xrLabel65.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel65.LocationFloat = new DevExpress.Utils.PointFloat(227.5417F, 785.8171F);
            this.xrLabel65.Name = "xrLabel65";
            this.xrLabel65.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel65.SizeF = new System.Drawing.SizeF(727.8088F, 36.14447F);
            this.xrLabel65.StylePriority.UseFont = false;
            this.xrLabel65.Text = "CC O NIT:";
            // 
            // xrLabel66
            // 
            this.xrLabel66.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Model.receiver.Name")});
            this.xrLabel66.Dpi = 254F;
            this.xrLabel66.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel66.LocationFloat = new DevExpress.Utils.PointFloat(227.5417F, 747.3381F);
            this.xrLabel66.Name = "xrLabel66";
            this.xrLabel66.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel66.SizeF = new System.Drawing.SizeF(727.8088F, 38.47906F);
            this.xrLabel66.StylePriority.UseFont = false;
            this.xrLabel66.Text = "DESTINATARIO";
            // 
            // xrLabel67
            // 
            this.xrLabel67.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Model.content.Description")});
            this.xrLabel67.Dpi = 254F;
            this.xrLabel67.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel67.LocationFloat = new DevExpress.Utils.PointFloat(478.8958F, 1214.433F);
            this.xrLabel67.Name = "xrLabel67";
            this.xrLabel67.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel67.SizeF = new System.Drawing.SizeF(301.9557F, 36.14447F);
            this.xrLabel67.StylePriority.UseFont = false;
            this.xrLabel67.Text = "DECLARA CONTENER";
            // 
            // xrLabel68
            // 
            this.xrLabel68.Dpi = 254F;
            this.xrLabel68.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel68.LocationFloat = new DevExpress.Utils.PointFloat(291.0417F, 1120.042F);
            this.xrLabel68.Name = "xrLabel68";
            this.xrLabel68.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel68.SizeF = new System.Drawing.SizeF(199.6243F, 36.14447F);
            this.xrLabel68.StylePriority.UseFont = false;
            this.xrLabel68.Text = "CONTACTO";
            // 
            // xrLabel69
            // 
            this.xrLabel69.Dpi = 254F;
            this.xrLabel69.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel69.LocationFloat = new DevExpress.Utils.PointFloat(291.0417F, 1083.898F);
            this.xrLabel69.Name = "xrLabel69";
            this.xrLabel69.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel69.SizeF = new System.Drawing.SizeF(301.9557F, 36.14441F);
            this.xrLabel69.StylePriority.UseFont = false;
            this.xrLabel69.Text = "TELEFONO/ CELULAR";
            // 
            // xrLabel71
            // 
            this.xrLabel71.Dpi = 254F;
            this.xrLabel71.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel71.LocationFloat = new DevExpress.Utils.PointFloat(291.0417F, 1037.171F);
            this.xrLabel71.Name = "xrLabel71";
            this.xrLabel71.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel71.SizeF = new System.Drawing.SizeF(199.6243F, 36.14447F);
            this.xrLabel71.StylePriority.UseFont = false;
            this.xrLabel71.Text = "DIRECCION";
            // 
            // xrLabel72
            // 
            this.xrLabel72.Dpi = 254F;
            this.xrLabel72.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel72.LocationFloat = new DevExpress.Utils.PointFloat(291.0417F, 998.6923F);
            this.xrLabel72.Name = "xrLabel72";
            this.xrLabel72.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel72.SizeF = new System.Drawing.SizeF(199.6243F, 38.47906F);
            this.xrLabel72.StylePriority.UseFont = false;
            this.xrLabel72.Text = "UBICACION PAGO";
            // 
            // xrLabel73
            // 
            this.xrLabel73.Dpi = 254F;
            this.xrLabel73.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel73.LocationFloat = new DevExpress.Utils.PointFloat(478.8958F, 1275.385F);
            this.xrLabel73.Name = "xrLabel73";
            this.xrLabel73.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel73.SizeF = new System.Drawing.SizeF(450.9785F, 36.14453F);
            this.xrLabel73.StylePriority.UseFont = false;
            this.xrLabel73.Text = "Guia Generada por kiosko";
            // 
            // xrLabel74
            // 
            this.xrLabel74.Dpi = 254F;
            this.xrLabel74.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel74.LocationFloat = new DevExpress.Utils.PointFloat(478.8958F, 1337.893F);
            this.xrLabel74.Name = "xrLabel74";
            this.xrLabel74.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel74.SizeF = new System.Drawing.SizeF(450.9783F, 36.14441F);
            this.xrLabel74.StylePriority.UseFont = false;
            this.xrLabel74.Text = "PAQUETES";
            // 
            // xrLabel76
            // 
            this.xrLabel76.Dpi = 254F;
            this.xrLabel76.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel76.LocationFloat = new DevExpress.Utils.PointFloat(478.8958F, 1446.442F);
            this.xrLabel76.Name = "xrLabel76";
            this.xrLabel76.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel76.SizeF = new System.Drawing.SizeF(450.9783F, 36.14453F);
            this.xrLabel76.StylePriority.UseFont = false;
            this.xrLabel76.Text = "PESO REAL";
            // 
            // xrLabel77
            // 
            this.xrLabel77.Dpi = 254F;
            this.xrLabel77.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel77.LocationFloat = new DevExpress.Utils.PointFloat(478.8958F, 1410.298F);
            this.xrLabel77.Name = "xrLabel77";
            this.xrLabel77.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel77.SizeF = new System.Drawing.SizeF(450.9783F, 36.14453F);
            this.xrLabel77.StylePriority.UseFont = false;
            this.xrLabel77.Text = "V. MERCANCIA";
            // 
            // xrLabel78
            // 
            this.xrLabel78.Dpi = 254F;
            this.xrLabel78.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel78.LocationFloat = new DevExpress.Utils.PointFloat(478.8958F, 1482.587F);
            this.xrLabel78.Name = "xrLabel78";
            this.xrLabel78.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel78.SizeF = new System.Drawing.SizeF(450.9783F, 36.14441F);
            this.xrLabel78.StylePriority.UseFont = false;
            this.xrLabel78.Text = "PESO VOLUMEN";
            // 
            // xrLabel79
            // 
            this.xrLabel79.Dpi = 254F;
            this.xrLabel79.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel79.LocationFloat = new DevExpress.Utils.PointFloat(478.8958F, 1663.425F);
            this.xrLabel79.Name = "xrLabel79";
            this.xrLabel79.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel79.SizeF = new System.Drawing.SizeF(450.9783F, 36.14441F);
            this.xrLabel79.StylePriority.UseFont = false;
            this.xrLabel79.Text = "DTO. FLETE MANEJO";
            // 
            // xrLabel80
            // 
            this.xrLabel80.Dpi = 254F;
            this.xrLabel80.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel80.LocationFloat = new DevExpress.Utils.PointFloat(478.8958F, 1591.137F);
            this.xrLabel80.Name = "xrLabel80";
            this.xrLabel80.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel80.SizeF = new System.Drawing.SizeF(450.9783F, 36.14453F);
            this.xrLabel80.StylePriority.UseFont = false;
            this.xrLabel80.Text = "FLETE MANEJO";
            // 
            // xrLabel81
            // 
            this.xrLabel81.Dpi = 254F;
            this.xrLabel81.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel81.LocationFloat = new DevExpress.Utils.PointFloat(478.8958F, 1627.281F);
            this.xrLabel81.Name = "xrLabel81";
            this.xrLabel81.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel81.SizeF = new System.Drawing.SizeF(450.9783F, 36.14465F);
            this.xrLabel81.StylePriority.UseFont = false;
            this.xrLabel81.Text = "DTO. FLETE";
            // 
            // xrLabel82
            // 
            this.xrLabel82.Dpi = 254F;
            this.xrLabel82.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel82.LocationFloat = new DevExpress.Utils.PointFloat(478.8958F, 1554.993F);
            this.xrLabel82.Name = "xrLabel82";
            this.xrLabel82.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel82.SizeF = new System.Drawing.SizeF(450.9783F, 36.14441F);
            this.xrLabel82.StylePriority.UseFont = false;
            this.xrLabel82.Text = "FLETE";
            // 
            // xrLabel83
            // 
            this.xrLabel83.Dpi = 254F;
            this.xrLabel83.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel83.LocationFloat = new DevExpress.Utils.PointFloat(478.8958F, 1518.732F);
            this.xrLabel83.Name = "xrLabel83";
            this.xrLabel83.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel83.SizeF = new System.Drawing.SizeF(450.9783F, 36.14441F);
            this.xrLabel83.StylePriority.UseFont = false;
            this.xrLabel83.Text = "PESO FACTURADO";
            // 
            // xrLabel84
            // 
            this.xrLabel84.Dpi = 254F;
            this.xrLabel84.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel84.LocationFloat = new DevExpress.Utils.PointFloat(478.8958F, 1764.294F);
            this.xrLabel84.Name = "xrLabel84";
            this.xrLabel84.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel84.SizeF = new System.Drawing.SizeF(450.9783F, 36.14453F);
            this.xrLabel84.StylePriority.UseFont = false;
            this.xrLabel84.Text = "VALOR DEL SERVICIO";
            // 
            // xrLabel85
            // 
            this.xrLabel85.Dpi = 254F;
            this.xrLabel85.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel85.LocationFloat = new DevExpress.Utils.PointFloat(478.8958F, 1728.149F);
            this.xrLabel85.Name = "xrLabel85";
            this.xrLabel85.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel85.SizeF = new System.Drawing.SizeF(450.9783F, 36.14441F);
            this.xrLabel85.StylePriority.UseFont = false;
            this.xrLabel85.Text = "UNIDADES LOGISTICAS";
            // 
            // xrLabel43
            // 
            this.xrLabel43.Dpi = 254F;
            this.xrLabel43.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel43.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1922.181F);
            this.xrLabel43.Name = "xrLabel43";
            this.xrLabel43.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel43.SizeF = new System.Drawing.SizeF(910.4974F, 36.14441F);
            this.xrLabel43.StylePriority.UseFont = false;
            this.xrLabel43.Text = "CEDULA / NIT: \t______________________________";
            // 
            // xrLabel40
            // 
            this.xrLabel40.Dpi = 254F;
            this.xrLabel40.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel40.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1862.174F);
            this.xrLabel40.Name = "xrLabel40";
            this.xrLabel40.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel40.SizeF = new System.Drawing.SizeF(910.4974F, 36.14453F);
            this.xrLabel40.StylePriority.UseFont = false;
            this.xrLabel40.Text = "NOMBRE COMPLETO:______________________________________";
            // 
            // xrLabel41
            // 
            this.xrLabel41.Dpi = 254F;
            this.xrLabel41.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel41.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1728.149F);
            this.xrLabel41.Name = "xrLabel41";
            this.xrLabel41.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel41.SizeF = new System.Drawing.SizeF(301.9557F, 36.14447F);
            this.xrLabel41.StylePriority.UseFont = false;
            this.xrLabel41.Text = "UNIDADES LOGISTICAS";
            // 
            // xrLabel42
            // 
            this.xrLabel42.Dpi = 254F;
            this.xrLabel42.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel42.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1764.294F);
            this.xrLabel42.Name = "xrLabel42";
            this.xrLabel42.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel42.SizeF = new System.Drawing.SizeF(301.9557F, 36.14447F);
            this.xrLabel42.StylePriority.UseFont = false;
            this.xrLabel42.Text = "VALOR DEL SERVICIO";
            // 
            // xrLabel35
            // 
            this.xrLabel35.Dpi = 254F;
            this.xrLabel35.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel35.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1518.732F);
            this.xrLabel35.Name = "xrLabel35";
            this.xrLabel35.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel35.SizeF = new System.Drawing.SizeF(301.9557F, 36.14447F);
            this.xrLabel35.StylePriority.UseFont = false;
            this.xrLabel35.Text = "PESO FACTURADO";
            // 
            // xrLabel36
            // 
            this.xrLabel36.Dpi = 254F;
            this.xrLabel36.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel36.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1554.992F);
            this.xrLabel36.Name = "xrLabel36";
            this.xrLabel36.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel36.SizeF = new System.Drawing.SizeF(301.9557F, 36.14447F);
            this.xrLabel36.StylePriority.UseFont = false;
            this.xrLabel36.Text = "FLETE";
            // 
            // xrLabel37
            // 
            this.xrLabel37.Dpi = 254F;
            this.xrLabel37.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel37.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1627.281F);
            this.xrLabel37.Name = "xrLabel37";
            this.xrLabel37.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel37.SizeF = new System.Drawing.SizeF(301.9557F, 36.14447F);
            this.xrLabel37.StylePriority.UseFont = false;
            this.xrLabel37.Text = "DTO. FLETE";
            // 
            // xrLabel38
            // 
            this.xrLabel38.Dpi = 254F;
            this.xrLabel38.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel38.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1591.137F);
            this.xrLabel38.Name = "xrLabel38";
            this.xrLabel38.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel38.SizeF = new System.Drawing.SizeF(301.9557F, 36.14447F);
            this.xrLabel38.StylePriority.UseFont = false;
            this.xrLabel38.Text = "FLETE MANEJO";
            // 
            // xrLabel39
            // 
            this.xrLabel39.Dpi = 254F;
            this.xrLabel39.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel39.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1663.426F);
            this.xrLabel39.Name = "xrLabel39";
            this.xrLabel39.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel39.SizeF = new System.Drawing.SizeF(301.9557F, 36.14447F);
            this.xrLabel39.StylePriority.UseFont = false;
            this.xrLabel39.Text = "DTO. FLETE MANEJO";
            // 
            // xrLabel34
            // 
            this.xrLabel34.Dpi = 254F;
            this.xrLabel34.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel34.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1482.587F);
            this.xrLabel34.Name = "xrLabel34";
            this.xrLabel34.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel34.SizeF = new System.Drawing.SizeF(301.9557F, 36.14447F);
            this.xrLabel34.StylePriority.UseFont = false;
            this.xrLabel34.Text = "PESO VOLUMEN";
            // 
            // xrLabel32
            // 
            this.xrLabel32.Dpi = 254F;
            this.xrLabel32.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel32.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1410.298F);
            this.xrLabel32.Name = "xrLabel32";
            this.xrLabel32.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel32.SizeF = new System.Drawing.SizeF(301.9557F, 36.14447F);
            this.xrLabel32.StylePriority.UseFont = false;
            this.xrLabel32.Text = "V. MERCANCIA";
            // 
            // xrLabel33
            // 
            this.xrLabel33.Dpi = 254F;
            this.xrLabel33.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel33.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1446.443F);
            this.xrLabel33.Name = "xrLabel33";
            this.xrLabel33.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel33.SizeF = new System.Drawing.SizeF(301.9557F, 36.14447F);
            this.xrLabel33.StylePriority.UseFont = false;
            this.xrLabel33.Text = "PESO REAL";
            // 
            // xrLabel30
            // 
            this.xrLabel30.Dpi = 254F;
            this.xrLabel30.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel30.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1337.893F);
            this.xrLabel30.Name = "xrLabel30";
            this.xrLabel30.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel30.SizeF = new System.Drawing.SizeF(301.9557F, 36.14447F);
            this.xrLabel30.StylePriority.UseFont = false;
            this.xrLabel30.Text = "PAQUETES";
            // 
            // xrLabel29
            // 
            this.xrLabel29.Dpi = 254F;
            this.xrLabel29.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel29.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1275.385F);
            this.xrLabel29.Name = "xrLabel29";
            this.xrLabel29.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel29.SizeF = new System.Drawing.SizeF(301.9557F, 36.14447F);
            this.xrLabel29.StylePriority.UseFont = false;
            this.xrLabel29.Text = "OBSERVACIONES";
            // 
            // xrLabel23
            // 
            this.xrLabel23.Dpi = 254F;
            this.xrLabel23.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel23.LocationFloat = new DevExpress.Utils.PointFloat(0F, 998.6923F);
            this.xrLabel23.Name = "xrLabel23";
            this.xrLabel23.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel23.SizeF = new System.Drawing.SizeF(249.039F, 38.479F);
            this.xrLabel23.StylePriority.UseFont = false;
            this.xrLabel23.Text = "UBICACION PAGO";
            // 
            // xrLabel24
            // 
            this.xrLabel24.Dpi = 254F;
            this.xrLabel24.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel24.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1037.171F);
            this.xrLabel24.Name = "xrLabel24";
            this.xrLabel24.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel24.SizeF = new System.Drawing.SizeF(199.6243F, 36.14447F);
            this.xrLabel24.StylePriority.UseFont = false;
            this.xrLabel24.Text = "DIRECCION";
            // 
            // xrLabel26
            // 
            this.xrLabel26.Dpi = 254F;
            this.xrLabel26.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel26.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1083.899F);
            this.xrLabel26.Name = "xrLabel26";
            this.xrLabel26.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel26.SizeF = new System.Drawing.SizeF(280.1665F, 36.14441F);
            this.xrLabel26.StylePriority.UseFont = false;
            this.xrLabel26.Text = "TELEFONO/ CELULAR";
            // 
            // xrLabel27
            // 
            this.xrLabel27.Dpi = 254F;
            this.xrLabel27.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel27.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1120.043F);
            this.xrLabel27.Name = "xrLabel27";
            this.xrLabel27.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel27.SizeF = new System.Drawing.SizeF(199.6243F, 36.14447F);
            this.xrLabel27.StylePriority.UseFont = false;
            this.xrLabel27.Text = "CONTACTO";
            // 
            // xrLabel28
            // 
            this.xrLabel28.Dpi = 254F;
            this.xrLabel28.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel28.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1214.433F);
            this.xrLabel28.Name = "xrLabel28";
            this.xrLabel28.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel28.SizeF = new System.Drawing.SizeF(301.9557F, 36.14447F);
            this.xrLabel28.StylePriority.UseFont = false;
            this.xrLabel28.Text = "DECLARA CONTENER";
            // 
            // xrLabel17
            // 
            this.xrLabel17.Dpi = 254F;
            this.xrLabel17.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(0F, 747.3381F);
            this.xrLabel17.Name = "xrLabel17";
            this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel17.SizeF = new System.Drawing.SizeF(209.9137F, 38.47906F);
            this.xrLabel17.StylePriority.UseFont = false;
            this.xrLabel17.Text = "DESTINATARIO";
            // 
            // xrLabel18
            // 
            this.xrLabel18.Dpi = 254F;
            this.xrLabel18.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(0F, 785.8171F);
            this.xrLabel18.Name = "xrLabel18";
            this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel18.SizeF = new System.Drawing.SizeF(199.6243F, 36.14447F);
            this.xrLabel18.StylePriority.UseFont = false;
            this.xrLabel18.Text = "CC O NIT:";
            // 
            // xrLabel19
            // 
            this.xrLabel19.Dpi = 254F;
            this.xrLabel19.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(0F, 821.9616F);
            this.xrLabel19.Name = "xrLabel19";
            this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel19.SizeF = new System.Drawing.SizeF(199.6243F, 36.14447F);
            this.xrLabel19.StylePriority.UseFont = false;
            this.xrLabel19.Text = "DIRECCION";
            // 
            // xrLabel20
            // 
            this.xrLabel20.Dpi = 254F;
            this.xrLabel20.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(0F, 858.106F);
            this.xrLabel20.Name = "xrLabel20";
            this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel20.SizeF = new System.Drawing.SizeF(199.6243F, 36.14447F);
            this.xrLabel20.StylePriority.UseFont = false;
            this.xrLabel20.Text = "DESTINO";
            // 
            // xrLabel22
            // 
            this.xrLabel22.Dpi = 254F;
            this.xrLabel22.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel22.LocationFloat = new DevExpress.Utils.PointFloat(0F, 894.2505F);
            this.xrLabel22.Name = "xrLabel22";
            this.xrLabel22.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel22.SizeF = new System.Drawing.SizeF(301.9557F, 36.14447F);
            this.xrLabel22.StylePriority.UseFont = false;
            this.xrLabel22.Text = "TELEFONO / CELULAR";
            // 
            // xrLabel16
            // 
            this.xrLabel16.Dpi = 254F;
            this.xrLabel16.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(0F, 645.9015F);
            this.xrLabel16.Name = "xrLabel16";
            this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel16.SizeF = new System.Drawing.SizeF(301.9557F, 36.14447F);
            this.xrLabel16.StylePriority.UseFont = false;
            this.xrLabel16.Text = "TELEFONO / CELULAR";
            // 
            // xrLabel14
            // 
            this.xrLabel14.Dpi = 254F;
            this.xrLabel14.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(0F, 609.757F);
            this.xrLabel14.Name = "xrLabel14";
            this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel14.SizeF = new System.Drawing.SizeF(199.6243F, 36.14447F);
            this.xrLabel14.StylePriority.UseFont = false;
            this.xrLabel14.Text = "ORIGEN";
            // 
            // xrLabel13
            // 
            this.xrLabel13.Dpi = 254F;
            this.xrLabel13.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(0F, 573.6125F);
            this.xrLabel13.Name = "xrLabel13";
            this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel13.SizeF = new System.Drawing.SizeF(199.6243F, 36.14447F);
            this.xrLabel13.StylePriority.UseFont = false;
            this.xrLabel13.Text = "DIRECCION";
            // 
            // xrLabel12
            // 
            this.xrLabel12.Dpi = 254F;
            this.xrLabel12.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(0F, 537.4681F);
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel12.SizeF = new System.Drawing.SizeF(199.6243F, 36.14447F);
            this.xrLabel12.StylePriority.UseFont = false;
            this.xrLabel12.Text = "CC O NIT:";
            // 
            // xrLabel11
            // 
            this.xrLabel11.Dpi = 254F;
            this.xrLabel11.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(0F, 498.9891F);
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel11.SizeF = new System.Drawing.SizeF(199.6243F, 38.47906F);
            this.xrLabel11.StylePriority.UseFont = false;
            this.xrLabel11.Text = "REMITENTE";
            // 
            // xrLabel10
            // 
            this.xrLabel10.Dpi = 254F;
            this.xrLabel10.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(0F, 397.7109F);
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel10.SizeF = new System.Drawing.SizeF(450.9784F, 38.47903F);
            this.xrLabel10.StylePriority.UseFont = false;
            this.xrLabel10.Text = "FECHA ESTIMADA DE ENTREGA";
            // 
            // xrLabel9
            // 
            this.xrLabel9.Dpi = 254F;
            this.xrLabel9.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(0F, 350.6718F);
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(301.9557F, 36.92267F);
            this.xrLabel9.StylePriority.UseFont = false;
            this.xrLabel9.Text = "REMESA";
            // 
            // xrLabel8
            // 
            this.xrLabel8.Dpi = 254F;
            this.xrLabel8.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(0F, 312.971F);
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(301.9557F, 37.70081F);
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.Text = "FECHA REMESA";
            // 
            // xrLabel7
            // 
            this.xrLabel7.Dpi = 254F;
            this.xrLabel7.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(0F, 275.2702F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(301.9557F, 37.70081F);
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.Text = "FECHA FACTURA";
            // 
            // xrLabel6
            // 
            this.xrLabel6.Dpi = 254F;
            this.xrLabel6.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(0F, 237.5693F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(301.9557F, 37.70087F);
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.Text = "FORMA DE PAGO";
            // 
            // xrLabel5
            // 
            this.xrLabel5.Dpi = 254F;
            this.xrLabel5.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 197.3395F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(301.9557F, 40.2299F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.Text = "FACTURA VENTA";
            // 
            // xrLabel4
            // 
            this.xrLabel4.Dpi = 254F;
            this.xrLabel4.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 96.34798F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(98.5573F, 48.49812F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.Text = "TEL:";
            // 
            // xrLabel3
            // 
            this.xrLabel3.Dpi = 254F;
            this.xrLabel3.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 52.81076F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(373.0625F, 43.53718F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.Text = "CIUDAD GENERA FACTURA:";
            // 
            // TopMargin
            // 
            this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel2,
            this.xrLabel1,
            this.xrPictureBox1,
            this.xrBarCode1});
            this.TopMargin.Dpi = 254F;
            this.TopMargin.HeightF = 386F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel2
            // 
            this.xrLabel2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "NIT")});
            this.xrLabel2.Dpi = 254F;
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(291.0417F, 304.3767F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(433.9166F, 58.42004F);
            this.xrLabel2.Text = "xrLabel2";
            // 
            // xrLabel1
            // 
            this.xrLabel1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "RazonSocial")});
            this.xrLabel1.Dpi = 254F;
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(291.0417F, 245.9567F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(433.9167F, 58.42001F);
            this.xrLabel1.Text = "xrLabel1";
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.Dpi = 254F;
            this.xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox1.Image")));
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(619.125F, 44.87336F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(338.6667F, 165.6058F);
            // 
            // xrBarCode1
            // 
            this.xrBarCode1.Dpi = 254F;
            this.xrBarCode1.LocationFloat = new DevExpress.Utils.PointFloat(55.5625F, 58.10252F);
            this.xrBarCode1.Module = 5.08F;
            this.xrBarCode1.Name = "xrBarCode1";
            this.xrBarCode1.Padding = new DevExpress.XtraPrinting.PaddingInfo(25, 25, 0, 0, 254F);
            this.xrBarCode1.SizeF = new System.Drawing.SizeF(521.2292F, 111.3366F);
            this.xrBarCode1.Symbology = code128Generator1;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel46,
            this.xrLabel45,
            this.xrLabel44});
            this.BottomMargin.Dpi = 254F;
            this.BottomMargin.HeightF = 700F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel46
            // 
            this.xrLabel46.Dpi = 254F;
            this.xrLabel46.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.xrLabel46.LocationFloat = new DevExpress.Utils.PointFloat(0F, 597.5217F);
            this.xrLabel46.Multiline = true;
            this.xrLabel46.Name = "xrLabel46";
            this.xrLabel46.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel46.SizeF = new System.Drawing.SizeF(962.9999F, 58.41998F);
            this.xrLabel46.StylePriority.UseFont = false;
            this.xrLabel46.StylePriority.UseTextAlignment = false;
            this.xrLabel46.Text = "Visite nuestro sitio en internet: www.tcc.com.co\r\n";
            this.xrLabel46.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel45
            // 
            this.xrLabel45.Dpi = 254F;
            this.xrLabel45.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel45.LocationFloat = new DevExpress.Utils.PointFloat(0F, 490.0347F);
            this.xrLabel45.Multiline = true;
            this.xrLabel45.Name = "xrLabel45";
            this.xrLabel45.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel45.SizeF = new System.Drawing.SizeF(962.9999F, 93.14655F);
            this.xrLabel45.StylePriority.UseFont = false;
            this.xrLabel45.Text = "LOS DESPACHOS RECIBIDOS EN DÍAS Y HORARIOS NO HABILES, AFETARÁN LA FECHA DE ENTRE" +
    "GA EN DESTINO\r\n";
            // 
            // xrLabel44
            // 
            this.xrLabel44.Dpi = 254F;
            this.xrLabel44.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel44.LocationFloat = new DevExpress.Utils.PointFloat(0F, 44.72595F);
            this.xrLabel44.Multiline = true;
            this.xrLabel44.Name = "xrLabel44";
            this.xrLabel44.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel44.SizeF = new System.Drawing.SizeF(962.9999F, 413.1757F);
            this.xrLabel44.StylePriority.UseFont = false;
            this.xrLabel44.Text = resources.GetString("xrLabel44.Text");
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(BluLogisticsService.Models.FacturaModel);
            // 
            // Factura
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.DataSource = this.bindingSource1;
            this.Dpi = 254F;
            this.Margins = new System.Drawing.Printing.Margins(37, 59, 386, 700);
            this.PageHeight = 2794;
            this.PageWidth = 1058;
            this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
