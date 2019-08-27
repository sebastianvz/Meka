class ShippingTest
{


    constructor() {


    }

    Run() {
        this.GenerateShipping()
    }

    GenerateShipping() {

        var shipping = new Shipping();

        shipping.content.Description = "BOLSOS";
        shipping.content.Value = 20000;
        shipping.content.Type = "box";

        shipping.origin.Identification = "1036668468";
        shipping.origin.IdentificationType = "CC";
        shipping.origin.Name = "BRAYAN VALLE";
        shipping.origin.Email = "brayanvallejaramillo@gmail.com";
        shipping.origin.Phone = "3007444471";
        shipping.origin.Location.Address = "CALLE 36";

        shipping.receiver.Identification = "102522";
        shipping.receiver.IdentificationType = "NIT";
        shipping.receiver.Name = "MEKAGROUP SAS";
        shipping.receiver.Email = "info@mekagroupcol.com";
        shipping.receiver.Phone = "2507070";
        shipping.receiver.Location.Address = "CALLE 12 SUR";

        $('#origin_identification_type').val(shipping.origin.IdentificationType);
        $('#origin_identification_number').val(shipping.origin.Identification);
        $('#origin_name').val(shipping.origin.Name);
        $('#origin_address').val(shipping.origin.Location.Address);
        $('#origin_phone').val(shipping.origin.Phone);
        $('#origin_email').val(shipping.origin.Email);

        $('#receiver_identification_type').val(shipping.receiver.IdentificationType);
        $('#receiver_identification_number').val(shipping.receiver.Identification);
        $('#receiver_name').val(shipping.receiver.Name);
        $('#receiver_address').val(shipping.receiver.Location.Address);
        $('#receiver_phone').val(shipping.receiver.Phone);
        $('#receiver_email').val(shipping.receiver.Email);

        $('#content_description').val(shipping.content.Description);
        $('#content_value').val(shipping.content.Value);
    }

}