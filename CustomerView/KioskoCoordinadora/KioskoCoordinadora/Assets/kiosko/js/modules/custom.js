class Custom {
    
    
    constructor() {
        this.RETRY_PROCESS = false;
    }
    HandleMeasures(measure) {

        $('#measure_height').html(measure.Height + " cm");
        $('#measure_width').html(measure.Width + " cm");
        $('#measure_length').html(measure.Length + " cm");
        $('#measure_weight').html(measure.Weight + " kg");
        $('#current_cubiq_image').attr('src', measure.ImageBase64);
        $('#btn_get_measures').html("MEDIDAS TOMADAS CORRECTAMENTE. PRESIONE SIGUIENTE PARA CONTINUAR");
        return true;
    }

    HandlePayment(paymentMethod, cost) {
        switch (paymentMethod) {
            case 'cash': this.HandlePaymentCash(cost);
                break;
            case 'pos': this.HandlePaymentPos();
                break;
        }

        $('#modal_payment_type').modal('hide');
        $('#modal_payment').modal('hide');
        $('#loader').show();
        setTimeout(() => {
            $('#modal_payment').modal({ backdrop: 'static', keyboard: false });
            $('#loader').hide();
        }, 1000);
    }

    HandlePaymentCash(cost) {
        $('#modal_payment_title').html(`POR FAVOR INGRESE EL TOTAL DE $ ${cost} Y SIGA LAS INSTRUCCIONES`);
        $('#modal_payment_body').html(`<img class="img-responsive" src="${AppConfig.GetAppDir()}/Assets/kiosko/img/insert_card.gif" style="width:100%" height="480" id="insert_card_image" />`);
    }
    HandlePaymentPos() {
        $('#modal_payment_title').html('POR FAVOR INGRESE SU TARJETA Y SIGA LAS INSTRUCCIONES')
        $('#modal_payment_body').html(`<img class="img-responsive" src="${AppConfig.GetAppDir()}/Assets/kiosko/img/insert_card.gif" style="width:100%" height="480" id="insert_card_image" />`);
    }
    ShowPaymentTypeModal() {
        $('#modal_payment_type').modal('show');
    }


    SetResume(shipping) {

        $('#footer_left').hide();
        $('#footer_right').hide();

        //origin info
        let origin_table = $('#resume_origin_table');
        let origin_data = [           
            {
                'title': "Identificación",
                'data': shipping.origin.Identification
            },   
            {
                'title': "Nombre",
                'data': shipping.origin.Name
            },
            {
                'title': "Ubicación",
                'data': shipping.origin.Location.City + " - " + shipping.origin.Location.Department
            },
            {
                'title': "Dirección",
                'data': shipping.origin.Location.Address
            },
            {
                'title': "Teléfono",
                'data': shipping.origin.Phone
            }
        ]


        //receiver info
        let receiver_table = $('#resume_receiver_table');
        let receiver_data = [           
            {
                'title': "Identificación",
                'data': shipping.receiver.Identification
            },
            {
                'title': "Nombre",
                'data': shipping.receiver.Name
            },
            {
                'title': "Ubicación",
                'data': shipping.receiver.Location.City + " - " + shipping.receiver.Location.Department 
            },
            {
                'title': "Dirección",
                'data': shipping.receiver.Location.Address
            },
            {
                'title': "Teléfono",
                'data': shipping.receiver.Phone
            }
        ]

        //content info
        let content_table = $('#resume_content_table');
        let content_data = [
            {
                'title': "Contenido",
                'data': shipping.content.Description
            },
            {
                'title': "Valor declarado",
                'data': '$ ' + shipping.content.Value
            }
        ]
        
        this.SetResumeDataToTable(origin_data, origin_table);
        this.SetResumeDataToTable(receiver_data, receiver_table);
        this.SetResumeDataToTable(content_data, content_table);
    }

    SetResumeDataToTable(data, table) {
        let data_table = "";
        data.forEach(function (d) {
            data_table += '<tr> ' +
                '<td><p class="ml-4 mt-2" style="color:dimgrey"><strong>' +d.title + ': </strong> ' + d.data +'</p></td>' +
                '</tr>';
        });

        table.html(data_table);
    }

    //Contador para cerrar la sesión al finalizar todo el proceso del Kiosko
    async CloseSession() {
        this.setRetryProcess(false);
        let _timer = document.getElementById('timer');
        _timer.innerHTML = 15;
        var i = 15;
        for (i = 15; i > 0; i--) {
            if (this.RETRY_PROCESS) {
                return false;
            }
            console.log("Retry process : ", this.RETRY_PROCESS);
            _timer.innerHTML = i;
            await new Promise(resolve => setTimeout(resolve, 1000));
        }


        if (!this.RETRY_PROCESS) {
            window.location.href = AppConfig.GetAppDir();
        }
    }

    SetDimensionsAndCostResume(shipping) {
        //set dimensions
        let resume_measure_table = $('#resume_measure_table');
        let table_data = "";
        let i = 1;
        let measures = shipping.content.Measures;
        
        measures.forEach(function (measure) {
            let data = [
                'Paquete ' + i++,
                '<img src="/Assets/kiosko/img/box_sm.png" />',
                measure.Height,
                measure.Width,
                measure.Length,
                measure.VolumetricWeight,
                measure.Width
            ];
            let table_data_1 = '<tr>';
            data.forEach(function (el) {
                table_data_1 += '<td>' + el + '</td>';
            });
            table_data_1 += '</tr>';

            table_data += table_data_1;
        });
        resume_measure_table.html(table_data);

        //set cost

        let resume_cost_table = $('#resume_cost_table');

        let resume_cost_data = [
            {
                'title': 'FLETE',
                'data': shipping.payment.Cost.MainCost
            },
            {
                'title': 'FLETE MANEJO',
                'data': shipping.payment.Cost.VariableCost
            },
            {
                'title': 'TOTAL COSTO',
                'data': shipping.payment.Cost.TotalCost
            },
        ];

        let resume_cost_table_data;
        resume_cost_data.forEach(function (el) {
            resume_cost_table_data += '<tr><td>' + el.title + '</td>' + '<td>'+el.data+'</td></tr>'
        });    
        resume_cost_table.html(resume_cost_table_data);

    }

    ProgressBar() {
        $("#progress_bar").show();
        Custom.SetProgressBar(1);
    }

    static SetProgressBar(item) {
        $('.progress_bar').css({ "background-color": "transparent" });
        $('#progress_bar_' + item).css({ "background-color": "white" });

    }

    CleanProgressBar() {
        $('.progress_bar').css({ "background-color": "transparent" });
    }


    setRetryProcess(retry ){
        this.RETRY_PROCESS = retry
    }

}



$('#origin_payment').click(function () {
    $('#modal_payment_type').modal('hide');
    $('#modal_payment_type_1').modal({ backdrop: 'static', keyboard: false });
});



$('#check_no_email').change(function () {
    var checked = $(this).is(':checked');
    $("#origin_email").prop('required', !checked);
    $("#origin_email").prop('disabled', checked);
    
});


$('#content_value').bind('change paste keyup', function (e) {
    var input = $(this);

    if (!input.val())
        return;

    var integerValue = $(this).val().replace('$', '');
    $(this).val(integerValue);

    if (!isNaN(Number(e.key)) || (e.keyCode === 46 || 8)) {
        Utilities.splitInDots(this);
        input.val('$ ' + input.val());
    }
});

$('#view_not_transportable_content').click(function () {
    $('#not_transportable_content_modal').modal('show');
});

$('.cancel-process-button').click(function () {
    $('#cancel_process_modal').modal('show');
});

$('.btn-cancel-process').click(function () {
    if ($(this).attr('exit') === 'true') {
        window.location.href = AppConfig.GetAppDir();
    } 
});

