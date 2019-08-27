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
    private XRPictureBox xrPictureBox1;
    private XRLabel xrLabel1;
    private XRLabel xrLabel2;
    private XRLabel xrLabel5;
    private XRLabel xrLabel6;
    private XRLabel xrLabel11;
    private XRLabel xrLabel23;
    private XRLabel xrLabel29;
    private XRLabel xrLabel44;
    private XRLabel xrLabel45;
    private XRLabel xrLabel46;
    private XRLabel xrLabel49;
    private XRLabel xrLabel50;
    private XRLabel xrLabel55;
    private CalculatedField calculatedField1;
    private XRLabel xrLabel21;
    private XRLabel xrLabel31;
    private XRLabel xrLabel25;
    private XRLabel xrLabel59;
    private XRLabel xrLabel62;
    private XRLabel xrLabel70;
    private XRLabel xrLabel75;
    private XRLabel xrLabel86;
    private XRLabel xrLabel3;
    private XRLabel xrLabel4;
    private XRLabel xrLabel47;
    private XRLabel xrLabel7;
    private CalculatedField calculatedField2;
    private XRLabel xrLabel8;
    private XRLabel xrLabel9;
    private XRLabel xrLabel10;
    private XRLabel xrLabel48;
    private CalculatedField calculatedField3;
    private XRLabel xrLabel12;
    private XRLabel xrLabel13;
    private XRLabel xrLabel14;
    private XRLabel xrLabel16;
    private XRLabel xrLabel17;
    private XRLabel xrLabel18;
    private XRLabel xrLabel20;
    private XRLabel xrLabel19;
    private XRLabel xrLabel22;
    private XRLabel xrLabel51;
    private XRLabel xrLabel24;
    private XRLabel xrLabel26;
    private XRLabel xrLabel27;
    private XRLabel xrLabel52;
    private XRLabel xrLabel53;
    private XRLabel xrLabel15;

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
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrLabel49 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel50 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel55 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel29 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel23 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrLabel46 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel45 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel44 = new DevExpress.XtraReports.UI.XRLabel();
            this.calculatedField1 = new DevExpress.XtraReports.UI.CalculatedField();
            this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel25 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel31 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel59 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel62 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel70 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel75 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel86 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel47 = new DevExpress.XtraReports.UI.XRLabel();
            this.calculatedField2 = new DevExpress.XtraReports.UI.CalculatedField();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel48 = new DevExpress.XtraReports.UI.XRLabel();
            this.calculatedField3 = new DevExpress.XtraReports.UI.CalculatedField();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel22 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel51 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel24 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel26 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel27 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel52 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel53 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel15,
            this.xrLabel53,
            this.xrLabel52,
            this.xrLabel27,
            this.xrLabel26,
            this.xrLabel24,
            this.xrLabel51,
            this.xrLabel22,
            this.xrLabel20,
            this.xrLabel19,
            this.xrLabel18,
            this.xrLabel17,
            this.xrLabel16,
            this.xrLabel14,
            this.xrLabel13,
            this.xrLabel12,
            this.xrLabel9,
            this.xrLabel10,
            this.xrLabel48,
            this.xrLabel8,
            this.xrLabel7,
            this.xrLabel47,
            this.xrLabel49,
            this.xrLabel50,
            this.xrLabel55,
            this.xrLabel29,
            this.xrLabel23,
            this.xrLabel11,
            this.xrLabel6,
            this.xrLabel5});
            this.Detail.Dpi = 254F;
            this.Detail.HeightF = 1365.671F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel49
            // 
            this.xrLabel49.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BluDetail.Vendedor")});
            this.xrLabel49.Dpi = 254F;
            this.xrLabel49.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrLabel49.LocationFloat = new DevExpress.Utils.PointFloat(451.1081F, 4.193704F);
            this.xrLabel49.Name = "xrLabel49";
            this.xrLabel49.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel49.SizeF = new System.Drawing.SizeF(301.9557F, 40.2299F);
            this.xrLabel49.StylePriority.UseFont = false;
            this.xrLabel49.StylePriority.UseTextAlignment = false;
            this.xrLabel49.Text = "FACTURA VENTA";
            // 
            // xrLabel50
            // 
            this.xrLabel50.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BluDetail.FormaPago")});
            this.xrLabel50.Dpi = 254F;
            this.xrLabel50.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrLabel50.LocationFloat = new DevExpress.Utils.PointFloat(451.1081F, 44.42365F);
            this.xrLabel50.Name = "xrLabel50";
            this.xrLabel50.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel50.SizeF = new System.Drawing.SizeF(301.9557F, 37.70087F);
            this.xrLabel50.StylePriority.UseFont = false;
            this.xrLabel50.StylePriority.UseTextAlignment = false;
            this.xrLabel50.Text = "FORMA DE PAGO";
            // 
            // xrLabel55
            // 
            this.xrLabel55.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BluDetail.NombreCliente")});
            this.xrLabel55.Dpi = 254F;
            this.xrLabel55.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrLabel55.LocationFloat = new DevExpress.Utils.PointFloat(44.83577F, 181.4891F);
            this.xrLabel55.Name = "xrLabel55";
            this.xrLabel55.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel55.SizeF = new System.Drawing.SizeF(564.0367F, 38.479F);
            this.xrLabel55.StylePriority.UseFont = false;
            this.xrLabel55.Text = "REMITENTE";
            // 
            // xrLabel29
            // 
            this.xrLabel29.Dpi = 254F;
            this.xrLabel29.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel29.LocationFloat = new DevExpress.Utils.PointFloat(18.52083F, 1145.739F);
            this.xrLabel29.Name = "xrLabel29";
            this.xrLabel29.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel29.SizeF = new System.Drawing.SizeF(233.164F, 36.14441F);
            this.xrLabel29.StylePriority.UseFont = false;
            this.xrLabel29.Text = "OBSERVACIONES";
            // 
            // xrLabel23
            // 
            this.xrLabel23.Dpi = 254F;
            this.xrLabel23.Font = new System.Drawing.Font("Tahoma", 10F);
            this.xrLabel23.LocationFloat = new DevExpress.Utils.PointFloat(44.83577F, 761.5097F);
            this.xrLabel23.Name = "xrLabel23";
            this.xrLabel23.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel23.SizeF = new System.Drawing.SizeF(122.039F, 54.35394F);
            this.xrLabel23.StylePriority.UseFont = false;
            this.xrLabel23.Text = "Cant:";
            // 
            // xrLabel11
            // 
            this.xrLabel11.Dpi = 254F;
            this.xrLabel11.Font = new System.Drawing.Font("Tahoma", 12F);
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(21.936F, 104.7599F);
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel11.SizeF = new System.Drawing.SizeF(306.186F, 64.93741F);
            this.xrLabel11.StylePriority.UseFont = false;
            this.xrLabel11.Text = "REMITENTE";
            // 
            // xrLabel6
            // 
            this.xrLabel6.Dpi = 254F;
            this.xrLabel6.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(21.936F, 44.42365F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(301.9557F, 37.70087F);
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.Text = "FORMA DE PAGO";
            // 
            // xrLabel5
            // 
            this.xrLabel5.Dpi = 254F;
            this.xrLabel5.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(21.936F, 4.193704F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(301.9557F, 40.2299F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.Text = "VENDEDOR";
            // 
            // TopMargin
            // 
            this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel4,
            this.xrLabel86,
            this.xrLabel75,
            this.xrLabel70,
            this.xrLabel62,
            this.xrLabel59,
            this.xrLabel31,
            this.xrLabel25,
            this.xrLabel21,
            this.xrLabel2,
            this.xrLabel1,
            this.xrPictureBox1,
            this.xrLabel3});
            this.TopMargin.Dpi = 254F;
            this.TopMargin.HeightF = 1034.229F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel2
            // 
            this.xrLabel2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BluDetail.Linea2")});
            this.xrLabel2.Dpi = 254F;
            this.xrLabel2.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(24.99977F, 494.8767F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(728.064F, 58.42001F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "xrLabel2";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel1
            // 
            this.xrLabel1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BluDetail.Linea1")});
            this.xrLabel1.Dpi = 254F;
            this.xrLabel1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(24.99977F, 436.4566F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(728.0641F, 58.42001F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "xrLabel1";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.Dpi = 254F;
            this.xrPictureBox1.ImageUrl = "D:\\Projects\\CubiQ-Kiosk\\CustomerView\\KioskoExxe\\KioskoBluLogistics\\Assets\\kiosko\\" +
    "img\\output-onlinepngtools.png";
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(183.749F, 205.1874F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(425.1235F, 219.6041F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel46,
            this.xrLabel45,
            this.xrLabel44});
            this.BottomMargin.Dpi = 254F;
            this.BottomMargin.HeightF = 770.2037F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel46
            // 
            this.xrLabel46.Dpi = 254F;
            this.xrLabel46.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.xrLabel46.LocationFloat = new DevExpress.Utils.PointFloat(0F, 583.1813F);
            this.xrLabel46.Multiline = true;
            this.xrLabel46.Name = "xrLabel46";
            this.xrLabel46.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel46.SizeF = new System.Drawing.SizeF(780.8516F, 58.41998F);
            this.xrLabel46.StylePriority.UseFont = false;
            this.xrLabel46.StylePriority.UseTextAlignment = false;
            this.xrLabel46.Text = "Visite nuestro sitio en internet: www.exxe.com.co\r\n";
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
            this.xrLabel45.SizeF = new System.Drawing.SizeF(800F, 93.14658F);
            this.xrLabel45.StylePriority.UseFont = false;
            this.xrLabel45.Text = "LOS DESPACHOS RECIBIDOS EN DÍAS Y HORARIOS NO HABILES, AFETARÁN LA FECHA DE ENTRE" +
    "GA EN DESTINO\r\n";
            // 
            // xrLabel44
            // 
            this.xrLabel44.Dpi = 254F;
            this.xrLabel44.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel44.LocationFloat = new DevExpress.Utils.PointFloat(0F, 24.99993F);
            this.xrLabel44.Multiline = true;
            this.xrLabel44.Name = "xrLabel44";
            this.xrLabel44.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel44.SizeF = new System.Drawing.SizeF(780.8516F, 413.1757F);
            this.xrLabel44.StylePriority.UseFont = false;
            this.xrLabel44.Text = resources.GetString("xrLabel44.Text");
            // 
            // calculatedField1
            // 
            this.calculatedField1.Name = "calculatedField1";
            // 
            // xrLabel21
            // 
            this.xrLabel21.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BluDetail.Linea3")});
            this.xrLabel21.Dpi = 254F;
            this.xrLabel21.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(25.00001F, 553.2966F);
            this.xrLabel21.Name = "xrLabel21";
            this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel21.SizeF = new System.Drawing.SizeF(728.0638F, 58.42004F);
            this.xrLabel21.StylePriority.UseFont = false;
            this.xrLabel21.StylePriority.UseTextAlignment = false;
            this.xrLabel21.Text = "xrLabel21";
            this.xrLabel21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel25
            // 
            this.xrLabel25.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BluDetail.Linea4")});
            this.xrLabel25.Dpi = 254F;
            this.xrLabel25.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.xrLabel25.LocationFloat = new DevExpress.Utils.PointFloat(25.00001F, 611.7167F);
            this.xrLabel25.Name = "xrLabel25";
            this.xrLabel25.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel25.SizeF = new System.Drawing.SizeF(728.0638F, 58.42004F);
            this.xrLabel25.StylePriority.UseFont = false;
            this.xrLabel25.StylePriority.UseTextAlignment = false;
            this.xrLabel25.Text = "xrLabel25";
            this.xrLabel25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel31
            // 
            this.xrLabel31.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BluDetail.Linea5")});
            this.xrLabel31.Dpi = 254F;
            this.xrLabel31.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.xrLabel31.LocationFloat = new DevExpress.Utils.PointFloat(25.00001F, 670.1367F);
            this.xrLabel31.Name = "xrLabel31";
            this.xrLabel31.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel31.SizeF = new System.Drawing.SizeF(728.0638F, 58.42004F);
            this.xrLabel31.StylePriority.UseFont = false;
            this.xrLabel31.StylePriority.UseTextAlignment = false;
            this.xrLabel31.Text = "xrLabel31";
            this.xrLabel31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel59
            // 
            this.xrLabel59.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BluDetail.Linea6")});
            this.xrLabel59.Dpi = 254F;
            this.xrLabel59.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.xrLabel59.LocationFloat = new DevExpress.Utils.PointFloat(24.99977F, 728.5568F);
            this.xrLabel59.Name = "xrLabel59";
            this.xrLabel59.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel59.SizeF = new System.Drawing.SizeF(728.0638F, 58.41998F);
            this.xrLabel59.StylePriority.UseFont = false;
            this.xrLabel59.StylePriority.UseTextAlignment = false;
            this.xrLabel59.Text = "xrLabel59";
            this.xrLabel59.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel62
            // 
            this.xrLabel62.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BluDetail.Linea7")});
            this.xrLabel62.Dpi = 254F;
            this.xrLabel62.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.xrLabel62.LocationFloat = new DevExpress.Utils.PointFloat(24.99977F, 786.9768F);
            this.xrLabel62.Name = "xrLabel62";
            this.xrLabel62.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel62.SizeF = new System.Drawing.SizeF(276.9559F, 58.41998F);
            this.xrLabel62.StylePriority.UseFont = false;
            this.xrLabel62.StylePriority.UseTextAlignment = false;
            this.xrLabel62.Text = "xrLabel59";
            this.xrLabel62.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel70
            // 
            this.xrLabel70.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BluDetail.Linea8")});
            this.xrLabel70.Dpi = 254F;
            this.xrLabel70.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.xrLabel70.LocationFloat = new DevExpress.Utils.PointFloat(345.1456F, 786.9768F);
            this.xrLabel70.Name = "xrLabel70";
            this.xrLabel70.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel70.SizeF = new System.Drawing.SizeF(407.9179F, 58.41998F);
            this.xrLabel70.StylePriority.UseFont = false;
            this.xrLabel70.StylePriority.UseTextAlignment = false;
            this.xrLabel70.Text = "xrLabel59";
            this.xrLabel70.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel75
            // 
            this.xrLabel75.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BluDetail.Resolucion")});
            this.xrLabel75.Dpi = 254F;
            this.xrLabel75.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.xrLabel75.LocationFloat = new DevExpress.Utils.PointFloat(26.16636F, 845.3969F);
            this.xrLabel75.Name = "xrLabel75";
            this.xrLabel75.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel75.SizeF = new System.Drawing.SizeF(726.8972F, 58.41998F);
            this.xrLabel75.StylePriority.UseFont = false;
            this.xrLabel75.StylePriority.UseTextAlignment = false;
            this.xrLabel75.Text = "xrLabel75";
            this.xrLabel75.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel86
            // 
            this.xrLabel86.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BluDetail.Factura")});
            this.xrLabel86.Dpi = 254F;
            this.xrLabel86.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.xrLabel86.LocationFloat = new DevExpress.Utils.PointFloat(432.5873F, 911.0531F);
            this.xrLabel86.Name = "xrLabel86";
            this.xrLabel86.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel86.SizeF = new System.Drawing.SizeF(320.4763F, 58.41992F);
            this.xrLabel86.StylePriority.UseFont = false;
            this.xrLabel86.Text = "xrLabel86";
            // 
            // xrLabel3
            // 
            this.xrLabel3.Dpi = 254F;
            this.xrLabel3.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(26.16636F, 911.0531F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(373.0625F, 58.41992F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.Text = "FACTURA N";
            // 
            // xrLabel4
            // 
            this.xrLabel4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BluDetail.FechaFactura")});
            this.xrLabel4.Dpi = 254F;
            this.xrLabel4.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(318.2558F, 969.473F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(434.8078F, 58.42004F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.Text = "xrLabel4";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel47
            // 
            this.xrLabel47.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Model.origin.Identification")});
            this.xrLabel47.Dpi = 254F;
            this.xrLabel47.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrLabel47.LocationFloat = new DevExpress.Utils.PointFloat(44.83577F, 219.9682F);
            this.xrLabel47.Name = "xrLabel47";
            this.xrLabel47.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel47.SizeF = new System.Drawing.SizeF(406.2725F, 58.42001F);
            this.xrLabel47.StylePriority.UseFont = false;
            this.xrLabel47.Text = "xrLabel47";
            // 
            // calculatedField2
            // 
            this.calculatedField2.Expression = "[BluDetail.CiudadDestino]-[BluDetail.DepartOrigen]";
            this.calculatedField2.Name = "calculatedField2";
            // 
            // xrLabel7
            // 
            this.xrLabel7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "calculatedField2")});
            this.xrLabel7.Dpi = 254F;
            this.xrLabel7.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(44.83577F, 278.3882F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(548.1616F, 58.42001F);
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.Text = "xrLabel7";
            // 
            // xrLabel8
            // 
            this.xrLabel8.Dpi = 254F;
            this.xrLabel8.Font = new System.Drawing.Font("Tahoma", 12F);
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(24.99977F, 352.9584F);
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(353.811F, 64.93741F);
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.Text = "DESTINATARIO";
            // 
            // xrLabel9
            // 
            this.xrLabel9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BluDetail.NombreCliente")});
            this.xrLabel9.Dpi = 254F;
            this.xrLabel9.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(44.83577F, 417.8958F);
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(564.0367F, 38.479F);
            this.xrLabel9.StylePriority.UseFont = false;
            this.xrLabel9.Text = "REMITENTE";
            // 
            // xrLabel10
            // 
            this.xrLabel10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Model.receiver.Identification")});
            this.xrLabel10.Dpi = 254F;
            this.xrLabel10.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(44.83577F, 456.3748F);
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel10.SizeF = new System.Drawing.SizeF(406.2725F, 58.42001F);
            this.xrLabel10.StylePriority.UseFont = false;
            this.xrLabel10.Text = "xrLabel47";
            // 
            // xrLabel48
            // 
            this.xrLabel48.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "calculatedField3")});
            this.xrLabel48.Dpi = 254F;
            this.xrLabel48.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.xrLabel48.LocationFloat = new DevExpress.Utils.PointFloat(44.83577F, 573.2148F);
            this.xrLabel48.Name = "xrLabel48";
            this.xrLabel48.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel48.SizeF = new System.Drawing.SizeF(548.1616F, 58.42001F);
            this.xrLabel48.StylePriority.UseFont = false;
            this.xrLabel48.Text = "xrLabel7";
            // 
            // calculatedField3
            // 
            this.calculatedField3.Expression = "[BluDetail.CiudadDestino]-[BluDetail.DepartDestino]";
            this.calculatedField3.Name = "calculatedField3";
            // 
            // xrLabel12
            // 
            this.xrLabel12.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BluDetail.DireccionDestino")});
            this.xrLabel12.Dpi = 254F;
            this.xrLabel12.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(44.83577F, 514.7949F);
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel12.SizeF = new System.Drawing.SizeF(548.1617F, 58.41998F);
            this.xrLabel12.StylePriority.UseFont = false;
            this.xrLabel12.Text = "xrLabel12";
            // 
            // xrLabel13
            // 
            this.xrLabel13.Dpi = 254F;
            this.xrLabel13.Font = new System.Drawing.Font("Tahoma", 12F);
            this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(25.00001F, 631.6349F);
            this.xrLabel13.Name = "xrLabel13";
            this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel13.SizeF = new System.Drawing.SizeF(353.811F, 64.93741F);
            this.xrLabel13.StylePriority.UseFont = false;
            this.xrLabel13.Text = "DETALLE";
            // 
            // xrLabel14
            // 
            this.xrLabel14.Dpi = 254F;
            this.xrLabel14.Font = new System.Drawing.Font("Tahoma", 10F);
            this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(44.83577F, 696.5723F);
            this.xrLabel14.Name = "xrLabel14";
            this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel14.SizeF = new System.Drawing.SizeF(353.811F, 64.93741F);
            this.xrLabel14.StylePriority.UseFont = false;
            this.xrLabel14.Text = "FLETE CONTADO";
            // 
            // xrLabel16
            // 
            this.xrLabel16.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BluDetail.Unidades")});
            this.xrLabel16.Dpi = 254F;
            this.xrLabel16.Font = new System.Drawing.Font("Tahoma", 10F);
            this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(214.3125F, 761.5097F);
            this.xrLabel16.Name = "xrLabel16";
            this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel16.SizeF = new System.Drawing.SizeF(122.039F, 54.354F);
            this.xrLabel16.StylePriority.UseFont = false;
            this.xrLabel16.Text = "CANT:";
            // 
            // xrLabel17
            // 
            this.xrLabel17.Dpi = 254F;
            this.xrLabel17.Font = new System.Drawing.Font("Tahoma", 10F);
            this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(44.83577F, 815.8636F);
            this.xrLabel17.Name = "xrLabel17";
            this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel17.SizeF = new System.Drawing.SizeF(169.4767F, 54.35388F);
            this.xrLabel17.StylePriority.UseFont = false;
            this.xrLabel17.Text = "Contenido:";
            // 
            // xrLabel18
            // 
            this.xrLabel18.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BluDetail.Contenido")});
            this.xrLabel18.Dpi = 254F;
            this.xrLabel18.Font = new System.Drawing.Font("Tahoma", 10F);
            this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(214.3125F, 815.8636F);
            this.xrLabel18.Name = "xrLabel18";
            this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel18.SizeF = new System.Drawing.SizeF(254F, 54.35388F);
            this.xrLabel18.StylePriority.UseFont = false;
            this.xrLabel18.Text = "xrLabel18";
            // 
            // xrLabel19
            // 
            this.xrLabel19.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BluDetail.ValorDeclarado")});
            this.xrLabel19.Dpi = 254F;
            this.xrLabel19.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(398.5222F, 870.2175F);
            this.xrLabel19.Name = "xrLabel19";
            this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel19.SizeF = new System.Drawing.SizeF(277.8125F, 58.41998F);
            this.xrLabel19.StylePriority.UseFont = false;
            this.xrLabel19.Text = "xrLabel19";
            // 
            // xrLabel20
            // 
            this.xrLabel20.Dpi = 254F;
            this.xrLabel20.Font = new System.Drawing.Font("Tahoma", 10F);
            this.xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(44.83577F, 874.2837F);
            this.xrLabel20.Name = "xrLabel20";
            this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel20.SizeF = new System.Drawing.SizeF(314.7058F, 54.35388F);
            this.xrLabel20.StylePriority.UseFont = false;
            this.xrLabel20.Text = "Valor Declarado:";
            // 
            // xrLabel22
            // 
            this.xrLabel22.Dpi = 254F;
            this.xrLabel22.Font = new System.Drawing.Font("Tahoma", 10F);
            this.xrLabel22.LocationFloat = new DevExpress.Utils.PointFloat(44.83577F, 928.6376F);
            this.xrLabel22.Name = "xrLabel22";
            this.xrLabel22.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel22.SizeF = new System.Drawing.SizeF(314.7058F, 54.35388F);
            this.xrLabel22.StylePriority.UseFont = false;
            this.xrLabel22.Text = "Valor flete:";
            // 
            // xrLabel51
            // 
            this.xrLabel51.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BluDetail.ValorFlete")});
            this.xrLabel51.Dpi = 254F;
            this.xrLabel51.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.xrLabel51.LocationFloat = new DevExpress.Utils.PointFloat(398.5222F, 928.6376F);
            this.xrLabel51.Name = "xrLabel51";
            this.xrLabel51.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel51.SizeF = new System.Drawing.SizeF(277.8125F, 54.354F);
            this.xrLabel51.StylePriority.UseFont = false;
            this.xrLabel51.Text = "xrLabel51";
            // 
            // xrLabel24
            // 
            this.xrLabel24.Dpi = 254F;
            this.xrLabel24.Font = new System.Drawing.Font("Tahoma", 10F);
            this.xrLabel24.LocationFloat = new DevExpress.Utils.PointFloat(44.83577F, 982.9915F);
            this.xrLabel24.Name = "xrLabel24";
            this.xrLabel24.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel24.SizeF = new System.Drawing.SizeF(314.7058F, 54.35388F);
            this.xrLabel24.StylePriority.UseFont = false;
            this.xrLabel24.Text = "Costo manejo:";
            // 
            // xrLabel26
            // 
            this.xrLabel26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BluDetail.Manejo")});
            this.xrLabel26.Dpi = 254F;
            this.xrLabel26.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.xrLabel26.LocationFloat = new DevExpress.Utils.PointFloat(398.5222F, 982.9915F);
            this.xrLabel26.Name = "xrLabel26";
            this.xrLabel26.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel26.SizeF = new System.Drawing.SizeF(277.8125F, 58.41998F);
            this.xrLabel26.StylePriority.UseFont = false;
            this.xrLabel26.Text = "xrLabel26";
            // 
            // xrLabel27
            // 
            this.xrLabel27.Dpi = 254F;
            this.xrLabel27.Font = new System.Drawing.Font("Tahoma", 12F);
            this.xrLabel27.LocationFloat = new DevExpress.Utils.PointFloat(45.41777F, 1041.412F);
            this.xrLabel27.Name = "xrLabel27";
            this.xrLabel27.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel27.SizeF = new System.Drawing.SizeF(256.5379F, 64.93726F);
            this.xrLabel27.StylePriority.UseFont = false;
            this.xrLabel27.Text = "TOTAL";
            // 
            // xrLabel52
            // 
            this.xrLabel52.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BluDetail.Total")});
            this.xrLabel52.Dpi = 254F;
            this.xrLabel52.Font = new System.Drawing.Font("Tahoma", 12F);
            this.xrLabel52.LocationFloat = new DevExpress.Utils.PointFloat(398.5222F, 1041.412F);
            this.xrLabel52.Name = "xrLabel52";
            this.xrLabel52.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel52.SizeF = new System.Drawing.SizeF(254F, 64.93713F);
            this.xrLabel52.StylePriority.UseFont = false;
            this.xrLabel52.Text = "xrLabel52";
            // 
            // xrLabel53
            // 
            this.xrLabel53.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BluDetail.Obs")});
            this.xrLabel53.Dpi = 254F;
            this.xrLabel53.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.xrLabel53.LocationFloat = new DevExpress.Utils.PointFloat(398.5222F, 1123.463F);
            this.xrLabel53.Name = "xrLabel53";
            this.xrLabel53.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel53.SizeF = new System.Drawing.SizeF(336.0208F, 58.42004F);
            this.xrLabel53.StylePriority.UseFont = false;
            this.xrLabel53.Text = "xrLabel53";
            // 
            // xrLabel15
            // 
            this.xrLabel15.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BluDetail.Obs")});
            this.xrLabel15.Dpi = 254F;
            this.xrLabel15.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(44.83577F, 1198.457F);
            this.xrLabel15.Name = "xrLabel15";
            this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel15.SizeF = new System.Drawing.SizeF(677.3333F, 58.42004F);
            this.xrLabel15.StylePriority.UseFont = false;
            this.xrLabel15.Text = "xrLabel15";
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(TCCService.Models.FacturaModel);
            // 
            // Factura
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.calculatedField1,
            this.calculatedField2,
            this.calculatedField3});
            this.DataSource = this.bindingSource1;
            this.Dpi = 254F;
            this.Margins = new System.Drawing.Printing.Margins(0, 0, 1034, 770);
            this.PageHeight = 3700;
            this.PageWidth = 800;
            this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
