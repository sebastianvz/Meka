class Shipping 
{

    
    //code = "";
    //origin = null;
    //receiver = null;
    //content = null;
    //payment = null;
    //shippingType = null;
    //tracking = null;
    //guide = null;
    constructor() {
        this.Cost = class {

           
            constructor() {
                this.MainCost = null;
                this.OtherCost = null;
                this.VariableCost = null;
                this.TotalCost = null;
            }

        }


        this.Payment = class {
           
          
            constructor(parent) {
                this.Cost = new parent.Cost();
                this.PaymentMethod = null;
                this.Invoice = null;
                this.Receipt = null;
                this.AuthorizationTransactionCode = null;
            }

        }


        this.Location = class {
           
            constructor() {
                this.Address = null;
                this.CityCode = null;
                this.City = null;
                this.Department = null;
            }

        }
        this.Origin = class {
           
            
            constructor(parent) {
                this.Location = new parent.Location();
                this.Identification = null;
                this.IdentificationType = null;
                this.Name = null;
                this.Email = null;
                this.Identification = null;
                this.IdentificationType = null;
                this.Name = null;
                this.Email = null;
                this.Phone = null; 

            }

        }


        this.Receiver = class {
            
          
            constructor(parent) {
                this.Location = new parent.Location();
                this.Identification = null;
                this.Name = null;
                this.Email = null;
                this.Phone = null;
            }

        }

        this.Content = class {
          
            constructor(parent) {
                this.Description = null;
                this.Value = null;
                this.Quantity = null;
                this.Measures = [];
                this.Type = null;
            }

        }

        this.ShippingType = class {
          
            constructor(parent) {
                this.KeyName = null;
                this.Name = null;
            }

        }

        this.Tracking = class {
           
            constructor(parent) {
                this.FinalDate = null;
                this.InitialDate = null;
            }
        }

        this.Guide = class {
           
            constructor(parent) {
                this.Code = null;
                this.Id = null;
                this.PdfGuide = null;
                this.Url = null;
            }
        }

        this.code="";
        this.origin = new this.Origin(this);
        this.receiver = new this.Receiver(this);
        this.content = new this.Content();
        this.payment = new this.Payment(this);
        this.shippingType = new this.ShippingType(this);
        this.tracking = new this.Tracking(this);
        this.guide = new this.Guide(this);
    }
    
    updateShipping(shipping) {
        this.origin = shipping.origin;
        this.receiver = shipping.receiver;
        this.content = shipping.content;
        this.payment = shipping.payment;
        this.guide = shipping.guide;
        this.shippingType = shipping.shippingType;
        this.tracking = shipping.tracking;
    }
    getShipping() {
        return jQuery.extend(true, {}, this);
    }  


    GenerateShipping() {

    }

}