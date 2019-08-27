class Communication
{
   
    constructor() {
    
        this.service = AppConfig.GetKioskoCore();
        this.endpoint = {
            getMeasures: 'getMeasures/',
            getDepartmentList: 'getDepartmentList',
            getCityListByDepartment: 'getCitiesList',
            getCost: 'getCost',
            getInventory: 'getInventory',
            requestPayment: 'requestPayment',
            generateGuide: 'generateGuide',
            printGuide: 'printGuide',
            printBill: 'printBill',
            openDoor: 'openDoor',
            getDoorStatus: 'getDoorStatus',
            initializeShipping: 'initializeShipping',
            postShipping: 'postShipping',
            isMaxDeclaredValue: 'isMaxDeclaredValue'
        };
    
    }
    //KioskoCore service and Communication
    //development endpoint
    

    /**
     * IMPORTANT!: Use this function when you need to send Shipping object to server
     * @param {any} shipping
     */
    Serialize(shipping) {
        let _s = {
            code: "",
            origin: [],
            receiver: [],
            content: [],
            payment: [],
            guide: [],
            error: [],
            tracking: []
        };
        _s.code = shipping.code;
        _s.origin = shipping.origin;
        _s.receiver = shipping.receiver;
        _s.content = shipping.content;
        _s.payment = shipping.payment;
        _s.guide = shipping.guide;
        _s.tracking = shipping.tracking;
        _s.error = shipping.error;
        _s.shippingType = shipping.shippingType;
        return JSON.stringify(_s);
    }
    
    GetMeasures(mode) {
        return Utilities.DoRequest({
            url: this.service + this.endpoint.getMeasures + mode
        });
    }

    InitializeShipping(shipping) {
        let data = this.Serialize(shipping);
        return Utilities.DoRequest({
            url: this.service + this.endpoint.initializeShipping,
            async: true,
            type: "POST",
            data : data
        });

    }

    GetDepartmentList() {
        return Utilities.DoRequest({
            url: this.service + this.endpoint.getDepartmentList,
            async: true
        });
    }


    IsMaxDeclaredValue(value) {
        return Utilities.DoRequest({
            url: this.service + this.endpoint.isMaxDeclaredValue + "/" + value,
            async: true
        });
    }
    GetCityListByDepartment(departmentCode) {
        return Utilities.DoRequest({
            url: this.service + this.endpoint.getCityListByDepartment + "/" + departmentCode,
            async: true
        });
    }   

    GetCost(shipping) {  
        let data = this.Serialize(shipping);
        return Utilities.DoRequest({
            url: this.service + this.endpoint.getCost,
            type: "POST",
            data: data
        });
    }

    DoPayment(shipping) {
        let data = this.Serialize(shipping);
        return Utilities.DoRequest({
            url: this.service + this.endpoint.requestPayment,
            type: "POST",
            data: data
        });
    }

    GenerateGuide(shipping) {
        let data = this.Serialize(shipping);
        return Utilities.DoRequest({
            url: this.service + this.endpoint.generateGuide,
            type: "POST",
            data: data
        });
    }

    PrintGuide(guide) {
        return Utilities.DoRequest({
            url: this.service + this.endpoint.printGuide,
            type: "POST",
            data: JSON.stringify(guide)
        });
    }

    GetInventory() {
        return Utilities.DoRequest({
            url: this.service + this.endpoint.getInventory,
            async: true
        });
    }  

    PrintBill(shipping) {
        let data = this.Serialize(shipping);
        return Utilities.DoRequest({
            url: this.service + this.endpoint.printBill,
            type: "POST",
            data:data
        });
    }

    OpenDoor(door) {
        return Utilities.DoRequest({
            url: this.service + this.endpoint.openDoor + '/' + door
        });
    }

    GetDoorStatus(door) {
        return Utilities.DoRequest({
            url: this.service + this.endpoint.getDoorStatus + '/' + door
        });
    }

    //Send shipping to CubiQManager
    PostShipping(shipping) {
        let data = this.Serialize(shipping);
        return Utilities.DoRequest({
            url: this.service + this.endpoint.postShipping,
            type: "POST",
            data: data
        });
    }


}