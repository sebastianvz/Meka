class Shipping {
    Cost = class {
        MainCost = null;
        OtherCost = null;
        VariableCost = null;
        TotalCost = null;
        constructor() {

        }

    }


    Payment = class {
        PaymentMethod = null;
        Invoice = null;
        Receipt = null;
        AuthorizationTransactionCode = null;
        Cost = null;
        constructor(parent) {
            this.Cost = new parent.Cost();
        }

    }


    Location = class {
        Address = null;
        CityCode = null;
        City = null;
        Department = null;
        constructor() {

        }

    }
    Origin = class {
        Identification = null;
        IdentificationType = null;
        Name = null;
        Email = null;
        Phone = null;
        Location = null;
        constructor(parent) {
            this.Location = new parent.Location();

        }

    }


    Receiver = class {
        Identification = null;
        Name = null;
        Email = null;
        Phone = null;
        Location = null;
        constructor(parent) {
            this.Location = new parent.Location();
        }

    }

    Content = class {
        Description = null;
        Value = null;
        Quantity = null;
        Measures = [];
        Type = null;
        constructor(parent) {
        }

    }

    ShippingType = class {
        KeyName = null;
        Name = null;
        constructor(parent) {
        }

    }

    Tracking = class {
        FinalDate = null;
        InitialDate = null;
        constructor(parent) {
        }
    }

    Guide = class {
        Code = null;
        Id = null;
        PdfGuide = null;
        Url = null;
        constructor(parent) {
        }
    }

    Error = class {
        HasError = null;
        Message = null;
    }

    code = "";
    origin = null;
    receiver = null;
    content = null;
    payment = null;
    shippingType = null;
    tracking = null;
    guide = null;
    error = null;
    constructor() {
        this.origin = new this.Origin(this);
        this.receiver = new this.Receiver(this);
        this.content = new this.Content();
        this.payment = new this.Payment(this);
        this.shippingType = new this.ShippingType(this);
        this.tracking = new this.Tracking(this);
        this.guide = new this.Guide(this);
        this.error = new this.Error(this);
    }

    updateShipping(shipping) {
        this.origin = shipping.origin;
        this.receiver = shipping.receiver;
        this.content = shipping.content;
        this.payment = shipping.payment;
        this.guide = shipping.guide;
        this.shippingType = shipping.shippingType;
        this.tracking = shipping.tracking;
        this.error = shipping.error;
    }
    getShipping() {
        return jQuery.extend(true, {}, this);
    }


    GenerateShipping() {

    }

}