class Modal {

    static Show(type, title, body) {
        const modal = $('#' + type + '_message_modal');
        $('#' + type + '_message_modal_body').html(body);
        $('#' + type + '_message_modal_title').html(title);
        modal.modal('show');
    }


   
}