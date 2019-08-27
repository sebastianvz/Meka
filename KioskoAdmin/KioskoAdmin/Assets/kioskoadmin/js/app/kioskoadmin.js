$(document).ready(function () {

    // jQuery plugin to prevent double submission of forms
    jQuery.fn.preventDoubleSubmission = function () {
        $(this).on('submit', function (e) {
            var $form = $(this);

            if ($form.data('submitted') === true) {
                // Previously submitted - don't submit again
                e.preventDefault();
            } else {
                // Mark it so that the next submit can be ignored
                $form.data('submitted', true);
            }
        });

        // Keep chainability
        return this;
    };

    const _communication = new Communication();
    $('.kiosko-admin-action').click(function (event) {
        event.preventDefault();

        var action = $(this).attr('id');
        if (!action) {
            return;
        }

        HandleAdminAction(action);
    });

    $('.kiosko-admin-action-open-box').click(function () {
        var action = $(this).attr('action');
        if (!action) {
            return;
        }
        HandleOpenBoxAction(action);
    });
   
    $('.fa-power-off').click(function () {
        $('#logOut_process_modal').modal('show');
    });

    $('#logOutId').click(function () {
        $(location).attr('href', "http://localhost:49908/");
    });



    $('body').on('click', '.modal-link', function (e) {
        e.preventDefault();

        $("#modal_empty").remove();

        $.get($(this).data("targeturl"), function (data) {

            $("#modalbody").html(data);

            $("#modal-container").modal('show');


            //$('<div id="modal-container" class="modal fade"><div class="modal-dialog modal-dialog-centered" role="document">' +
            //  '<div class="modal-content" id= "modalbody" >' +
            //        data + '</div></div></div>').modal('show');


        }).done();
    });

    $('#btnsubmit').on('click', function () {
        $(this).val('Please wait ...')
          .attr('disabled', 'disabled');
        $('#cash_handling').submit();
    });


    function HandleAdminAction(action) {
        if (action == "take_boxes ") {
            let request = _communication.RemoveBoxes();
        } else {
            $('#modal_' + action).modal('show');
        }
    }

    function HandleOpenBoxAction(action) {
        
        let request = _communication.OpenDoor(action);

        request.done(function (response) {
            console.log(response);
        });


        request.fail(function () {
            console.log("HandleOpenBox fail");
        });

    }

});




    