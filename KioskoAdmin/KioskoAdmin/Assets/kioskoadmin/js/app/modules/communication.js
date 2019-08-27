class Communication {

   // service = "http://localhost/kioskocore/api/";
   // service = "http://localhost:51904/api/";

    service = "http://localhost/kioskoadmin/api/";
    endpoint = {
        openDoor: 'openDoor',
        HandleCash: 'handleCash',
        RemoveBoxes:'removeboxes',
    }


    getSession() {
        let user = {
            id: $('#session_user_id').attr('value'),
            email: $('#session_user_name').attr('value'),
            session: $('#session_key').attr('value'),
        }
        return JSON.stringify(user);       

    }

    
    HandleCash(_data){
        return Utilities.DoRequest({
            url: this.service + this.endpoint.HandleCash,
            type: "post",
            data:_data

        });


    }

    RemoveBoxes() {
        return Utilities.DoRequest({
            url: this.service + this.endpoint.RemoveBoxes ,
            type: "post",
            data: this.getSession()
        });
    }


    OpenDoor(door) {
        return Utilities.DoRequest({
            url: this.service + this.endpoint.openDoor + '/' + door,
            type: "post",
            data: this.getSession()
        });
    }

    GetDoorStatus(door) {
        return Utilities.DoRequest({
            url: this.service + this.endpoint.getDoorStatus + '/' + door
        });
    }




}