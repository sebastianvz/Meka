class Communication
{
    //kioskocore service
    service = AppConfig.GetKioskoCoreDir();

    endpoint = {
        getMeasures: 'getMeasures',
        getDepartmentList: 'getDepartmentList',
        getCityListByDepartment: 'getCitiesList',
        getCost: 'getCost',
        getInventory: 'getInventory',
        requestPayment: 'requestPayment',
        generateGuide: 'generateGuide',
        generateInvoice : 'generateInvoice',
        printGuide: 'printGuide',
        printBill: 'printBill',
        openDoor: 'openDoor',
        getDoorStatus: 'getDoorStatus',
        postShipping: 'postShipping',
        initializeShipping: 'initializeShipping',
        isMaxDeclaredValue: 'isMaxDeclaredValue'
    }
    
    GetMeasures(mode) {
        return Utilities.DoRequest({
            url: this.service + this.endpoint.getMeasures + "/" +mode
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

    IsMaxDeclaredValue(value) {
        return Utilities.DoRequest({
            url: this.service + this.endpoint.isMaxDeclaredValue + "/" + value,
            async: true
        });
    }

    GetDepartmentList() {
        return Utilities.DoRequest({
            url: this.service + this.endpoint.getDepartmentList,
            async: true
        });
    }    

    GetInventory() {
        return Utilities.DoRequest({
            url: this.service + this.endpoint.getInventory,
            async: true
        });
    }  

    GetCityListByDepartment(departmentCode) {
        return Utilities.DoRequest({
            url: this.service + this.endpoint.getCityListByDepartment + "/" + departmentCode,
            async: true
        });
    }


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
        return JSON.stringify(_s);
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

    GenerateInvoice(shipping) {
        let data = this.Serialize(shipping);
        return Utilities.DoRequest({
            url: this.service + this.endpoint.generateInvoice,
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

    PrintBill(invoice) {
        return Utilities.DoRequest({
            url: this.service + this.endpoint.printBill + "/" + invoice,
            type: "POST"
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

    PostShipping(shipping) {
        let data = this.Serialize(shipping);
        return Utilities.DoRequest({
            url: this.service + this.endpoint.postShipping,
            type: "POST",
            data: data
        });
    }

}