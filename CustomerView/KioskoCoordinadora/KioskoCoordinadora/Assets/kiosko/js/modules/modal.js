class Modal {

    static Show(type, title, body) {
        const modal = $('#' + type + '_message_modal');
        $('#' + type + '_message_modal_body').html(body);
        $('#' + type + '_message_modal_title').html(title);
        modal.modal({ backdrop: 'static', keyboard: false });
    }



    static ShowPaymentErrorModal() {

        let header = `<img src="/Assets/kiosko/img/warning-ico.PNG"/>`;
        let body = `
                <div style="display:flex">
                    <img src="/Assets/kiosko/img/circle-ico.PNG"/>
                    <h4>Ha ocurrido un error al realizar el pago. Por favor vuelva a intentarlo.</h4>
                </div>
            `;
 
        let footer = '<button type="button" id="btn_retry_payment" class="btn default-button-sm btn-round " data-dismiss="modal" data-backdrop="false">REGRESAR</button>';

        Modal.ShowCommonModal(header, body, footer);

    }

    static ShowCommonModal(header, body, footer) {

        let modal = $('#common_modal');     

        let _body = modal.find('.modal-body');
        let _footer = modal.find('.modal-footer');
        let _header = modal.find('.modal-header');
        _body.html(body);
        _footer.html(footer);
        _header.html(header);

        modal.modal({ backdrop: 'static', keyboard: false });
    }
   
}