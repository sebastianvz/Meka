class Custom {

    VOLUMETRIC_WEIGHT_RESTRICTION = 10;

    HandleMeasures(measure) {
        if (measure.VolumetricWeight > this.VOLUMETRIC_WEIGHT_RESTRICTION) {
            Steps.FinishProcess();
            this.CloseSession();
            return false;
        }
        
        $('#measure_height').html(measure.Height + " cm");
        $('#measure_width').html(measure.Width + " cm");
        $('#measure_length').html(measure.Length + " cm");
        $('#measure_volumetric_weight').html(measure.VolumetricWeight + " m3");
        $('#measure_weight').html(measure.Weight + " kg");
        $('#current_cubiq_image').attr('src', measure.ImageBase64);

        $('#btn_get_measures').hide();
        $('#cubiq_more_units').show();
        return true;        
    }

    ShowPaymentTypeModal() {
        $('#modal_payment_type').modal('show');
    }

    HandlePaymentCash($cash) {
        $('#modal_payment_title').html('POR FAVOR INGRESE EL TOTAL DE ' + $cash + 'Y SIGA LAS INSTRUCCIONES.');
       // $('#modal_payment_body').html('<img class="img-responsive" src="/Assets/kiosko/img/insert_card.gif" style="width:100%" height="480" id="insert_card_image" />'); 
    }

    HandlePaymentPos($cash) {
        $('#modal_payment_title').html('POR FAVOR INGRESE SU TARJETA Y SIGA LAS INSTRUCCIONES.');
        // $('#modal_payment_body').html('<img class="img-responsive" src="/Assets/kiosko/img/insert_card.gif" style="width:100%" height="480" id="insert_card_image" />'); 
    }

    HandlePayment(paymentMethod, $cost) {

               
        switch (paymentMethod) {
            case 'cash': this.HandlePaymentCash($cost);
                break;
            case 'pos': this.HandlePaymentPos($cost);
                break;
        }

        $('#modal_payment_type').modal('hide');
        $('#modal_payment').modal('hide');
        $('#loader').show();
        setTimeout(() => {
            $('#modal_payment').modal('show');
            $('#loader').hide();
        },1000);
    }


    SetResume(shipping) {

        //origin info
        let origin_table = $('#resume_origin_info');
        let origin_data = [
            {
                'title': "TIPO DE IDENTIFICACIÓN",
                'data': shipping.origin.IdentificationType,
                'identifier' : 'origin_identification_type'
            },
            {
                'title': "IDENTIFICACIÓN",
                'data': shipping.origin.Identification,
                'identifier': 'origin_identification'
            },
            {
                'title': "NOMBRE O RAZÓN SOCIAL",
                'data': shipping.origin.Name
            },
            {
                'title': "CIUDAD",
                'data': shipping.origin.Location.City
            },
            {
                'title': "DIRECCIÓN",
                'data': shipping.origin.Location.Address
            },
            {
                'title': "TELÉFONO",
                'data': shipping.origin.Phone
            },
            {
                'title': "CORREO",
                'data': shipping.origin.Email
            }
        ]


        //receiver info
        let receiver_table = $('#resume_receiver_info');
        let receiver_data = [
            {
                'title': "TIPO DE IDENTIFICACIÓN",
                'data': shipping.receiver.IdentificationType
            },
            {
                'title': "IDENTIFICACIÓN",
                'data': shipping.receiver.Identification
            },
            {
                'title': "NOMBRE O RAZÓN SOCIAL",
                'data': shipping.receiver.Name
            },
            {
                'title': "CIUDAD",
                'data': shipping.receiver.Location.City
            },
            {
                'title': "DIRECCIÓN",
                'data': shipping.receiver.Location.Address
            },
            {
                'title': "TELÉFONO",
                'data': shipping.receiver.Phone
            },
            {
                'title': "CORREO",
                'data': shipping.receiver.Email
            }
        ]

        //content info
        let content_table = $('#resume_content_info');
        let content_data = [
            {
                'title': "VALOR DECLARADO",
                'data': shipping.content.Value
            },
            {
                'title': "OBSERVACIONES",
                'data': shipping.content.Description
            }
        ]
        
        this.SetResumeDataToTable(origin_data, origin_table);
        this.SetResumeDataToTable(receiver_data, receiver_table);
        this.SetResumeDataToTable(content_data, content_table);
    }

    SetResumeDataToTable(data, table) {
        let data_table = "";
        data.forEach(function (d) {
            data_table += '<li class="list-group-item">' + d.title + " : " + d.data + '</li>';
        });

        table.html(data_table);
    }
    //Contador para cerrar la sesión al finalizar todo el proceso del Kiosko
    async CloseSession() {
        let _timer = document.getElementById('timer');
        _timer.innerHTML = 20;
        var i = 20;
        for (i = 20; i > 0; i--) {
            _timer.innerHTML = i;
            await new Promise(resolve => setTimeout(resolve, 1000));
        }

        window.location.href = AppConfig.GetAppDir();
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
                '<img src="' + AppConfig.GetAppDir() +'/Assets/kiosko/img/box_sm.png" />',
                measure.Height,
                measure.Width,
                measure.Length,
                measure.VolumetricWeight,
                measure.Weight
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
}


$('#origin_payment').click(function () {
    $('#modal_payment_type').modal('hide');
    $('#modal_payment_type_1').modal('show');
});

//Useless
$('.cubicate_more').click(function () {
    $('#btn_get_measures').show();
    $('#cubiq_more_units').hide();
});


$('#check_no_email').change(function () {
    var checked = $(this).is(':checked');
    $("#origin_email").prop('required', !checked);
    $("#origin_email").prop('disabled', checked); 
});


