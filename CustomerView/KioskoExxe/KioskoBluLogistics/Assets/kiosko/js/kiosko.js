

$(document).ready(function () {

    /** VARS **/

    const APP_DIR = AppConfig.GetAppDir();

    /** LOAD **/

    const steps = new Steps();
    const _shipping = new Shipping();
    const _validation = new Validation();
    const _communication = new Communication();
    const _custom = new Custom();
    let idleInterval;
    
    let MeasureList = [];

    if (window.location.search === '?source=start') {
        //set current view
        $('.' + steps.getCurrent().class).show();
        GetDepartmentList(5);
        InitializeShipping();

        //idleInterval = setInterval(timerIncrement, 1000);
    }

    $('.welcome-button').click(function () {
        $('#modal_terms_and_conditions').modal('show');
    });

    $('.accept-terms-and-conditions').click(function () {
        let accept = $(this).attr('accept') === "true";
        if (accept) {
            StartKiosko();
        } 
        $('#modal_terms_and_conditions').modal('hide');
    });


    function StartKiosko() {
        $('#loader').show();       
        setTimeout(function () {            
            $(location).attr('href', APP_DIR + 'Home/Start?source=start');            
        }, 3000);
    }


    function InitializeShipping() {

        let shipping = _shipping.getShipping();        
        let initializeShippingRequest = _communication.InitializeShipping(shipping);

        initializeShippingRequest.done(function (response) {
            _shipping.updateShipping(response);

            GenerateShipping();
        });

        initializeShippingRequest.fail(function () {
            console.log("Error on initializeShippingRequest");
        });
    }

    $('.continue-button').click(function () {
        let shipping = _shipping.getShipping();
        let current_step = steps.getCurrent().class;
        _validation.SetShipping(shipping);

        if (_validation.IsValid(current_step)) {
            HandleShippingData(shipping, current_step);
            steps.HandleSteps();
        }
        
    });

    function HandleShippingData(shipping, view) {
        
        switch (view) {
            case "origin": HandleOriginData(shipping);
                break;
            case "receiver": HandleReceiverData(shipping);
                break;
            case "content": HandleContentData(shipping);
                break;
            case "cubiq":
                HandleCubiQData(shipping);
                _custom.SetResume(shipping);
                break;

          
           
        }

    }

    function HandleOriginData(shipping) {

        let dataForm = $('#origin_form').serializeArray();
        shipping.origin.IdentificationType = dataForm[0].value;
        shipping.origin.Identification = dataForm[1].value;
        shipping.origin.Name = dataForm[2].value;
        shipping.origin.Location.Address = dataForm[3].value;
        shipping.origin.Phone = dataForm[4].value;
        shipping.origin.Location.CityCode = "05001";
        if ($("#origin_email").prop('required')) {
            shipping.origin.Email = dataForm[5].value;
        } else {
            shipping.origin.Email = "sincorreo@tcc.com.co";
        } 
        //TODO: Fix this ->
        shipping.origin.Location.City = "Medellín";
        /// <--
        
        _shipping.updateShipping(shipping);
    }

    function HandleReceiverData(shipping) {
        let dataForm = $('#receiver_form').serializeArray();
      

        shipping.receiver.IdentificationType = dataForm[0].value;
        shipping.receiver.Identification = dataForm[1].value;
        shipping.receiver.Name = dataForm[2].value;
        shipping.receiver.Location.Department = dataForm[3].value;
        shipping.receiver.Location.CityCode = dataForm[4].value;
        shipping.receiver.Location.Address = dataForm[5].value;
        shipping.receiver.Phone = dataForm[6].value;
        shipping.receiver.Email = dataForm[7].value;
        shipping.receiver.Location.City = $('#receiver_city option:selected').text();
        _shipping.updateShipping(shipping);
    }

    function HandleContentData(shipping) {
        let dataForm = $('#content_form').serializeArray();

        shipping.content.Description = dataForm[0].value;
        shipping.content.Value = dataForm[1].value;

        _shipping.updateShipping(shipping);
    }

    function HandleCubiQData(shipping) {

        shipping.content.Measures = MeasureList;
        _shipping.updateShipping(shipping);
    }


    function HandleResumeData() {        
        $('#loader').show();
        let shipping = _shipping.getShipping();

        let request = _communication.GetCost(shipping);

        request.done(function (response) {
            
            shipping.payment.Cost = response.data;
            $('#resume_1').hide();
            $('#resume_2').show();
            $('#resume_title').html("DETALLE DE FACTURACIÓN");
            _custom.SetDimensionsAndCostResume(shipping);
            _shipping.updateShipping(shipping);
        });

        request.fail(function (e) {
            console.log("Error on cost service");
        });

        request.always(function () {
            $('#loader').hide();
        });


    }

    $('#retry_print').click(function () {
        HandlePrint();
    });
    
    function HandlePrint() {

        Utilities.animateCSS('#print_status', 'fadeInUp');
        let shipping = _shipping.getShipping();
        //----ETIQUETA------
        $('#print_status').html('Generando etiqueta');
        $('#guide_error_modal').modal('hide');
        //Generar la guia

        let generateGuideRequest = _communication.GenerateGuide(shipping);

        generateGuideRequest.done(function (response) {
            if (response.data.error.HasError) {
                console.log("Error on generateGuide");
                $('#guide_error_modal').modal('show');
                return;
            }
            shipping.guide = response.data.guide;
            _shipping.updateShipping(shipping);

            $('#print_status_icon').css("color", "green");
            $('#print_status').css("color", "green");
            $('#print_status_icon').removeClass('fa-spinner make-it-spinn').addClass('fa-check');
            $('#print_status').html('Etiqueta impresa correctamente');
            setTimeout(function () {
                $('#loader').show();
                $('#print_status_icon').hide();
                $('#print_status').hide();
                setTimeout(function () {
                    $('#loader').hide();
                    $('#finish_proccess_help').modal('show');
                }, 3000);
            }, 3000);
        });

        generateGuideRequest.fail(function () {
            $('#print_status_icon').removeClass('fa-spinner make-it-spinn').addClass('fa-times');           
            $('#print_status').html('Error al imprimir');
        });

        generateGuideRequest.always(function () {
            Utilities.animateCSS('#print_status', 'fadeInUp');
        });

        //---FACTURA----
    }
    

    $('#btn_get_measures').click(function () {
        $('#loader').show();
        let request = _communication.GetMeasures('package');
        request.done(function (response) {
            if (response.data.Error.HasError) {
                Modal.Show("error", "Error", response.data.Error.Message);
                return;
            }

            if (_custom.HandleMeasures(response.data)) {
                MeasureList = [];
                MeasureList.push(response.data);

                $('#btn_get_measures').hide();
                $('#btn_continue_measures').show();
            }
        });

        request.fail(function (e) {
            Modal.Show("error", "Error", "Hubo un error al obtener las medidas");
            console.log("fail getMeasures");
        });

        request.always(function () {
            $('#loader').hide();
        })
    });

    function GetDepartmentList(attemps) {
        if (window.location.pathname !== "/Home/Start")
            return;

        if (attemps === 0) {
            console.log("Error on departmentList");
            return;
        }
        let request = _communication.GetDepartmentList();
        request.done(function (response) {
            if (response === null) {
                GetDepartmentList(attemps - 1);
                return;
            }
            if (response.data === null) {
                GetDepartmentList(attemps - 1);
                return;
            }
            if (response.data.departmentList === null) {
                GetDepartmentList(attemps - 1);
                return;
            }
            var departmentList = response.data.departmentList.map(function (department) {
                return {
                    text: department.Name,
                    value: department.Code
                }
            });

            Utilities.addOptionsToSelect($('#receiver_department'), departmentList);

        });

        request.fail(function (e) {
            console.log("Fail");
        })
    }

    $('#receiver_department').change(function () {
        var optionSelected = $(this).find("option:selected");

        if (!optionSelected.val())
            return;

        var department_selected = optionSelected.val();
        $('#loader').show();
        let request = _communication.GetCityListByDepartment(department_selected);

        request.done(function (response) {
            if (response === null) {
                return;
            }
            if (response.data === null) {
                return;
            }
            if (response.data.cityList === null) {
                return;
            }
            var cityList = response.data.cityList.map(function (city) {
                return {
                    text: city.Name,
                    value: city.Code
                }
            });

            Utilities.addOptionsToSelect($('#receiver_city'), cityList);
        });

        request.fail(function (e) {
            console.log("GetCityListByDepartment fail");
        });

        request.always(function () {
            $('#loader').hide();
        })

    });


    $('#btn_do_payment').click(function () {
        $('#modal_payment_type_1').modal('show');

        //_custom.ShowPaymentTypeModal();
    });

    $('.confirm-btn').click(function () {

        HandleResumeData();
    });



    function HandlePayment(paymentMethod, retry = false) {

        let shipping = _shipping.getShipping();
        let modalStatus = $('#modal_payment_status');

        modalStatus.css({ "color": "#00397c" });
        modalStatus.html('Verificando pago <label id="modal_payment_status_timeout" style="display:none; color:red"></label> <i class="far fa-clock ml-2"></i>');
        $('#insert_card_image').show();     
        $('#modal-center-cs').css({
            'margin-top': '200px'
        });
    
        if (!retry) {
            shipping.payment.PaymentMethod = paymentMethod;
            _shipping.updateShipping(shipping);
        }
        _custom.HandlePayment(shipping.payment.PaymentMethod, shipping.payment.Cost.TotalCost);

        let timeoutStatusModal = $('#modal_payment_status_timeout');
        timeoutStatusModal.show();
        Utilities.TimeOut(timeoutStatusModal, 120000, function () {
            timeoutStatusModal.hide();
        });

        let paymentRequest = _communication.DoPayment(shipping);

        paymentRequest.done(function (response) {
            if (response.data.error.HasError) {
                modalStatus.css({ "color": "red" });
                $('#insert_card_image').hide();
                $('.modal-center-cs').css({
                    'margin-top': '0px'
                });
                modalStatus.html(response.data.error.Message + ' <label style="color:#00397c">Presione para reintentar.</label>');
                return;
            }

            shipping = _shipping.getShipping();
            shipping.payment = response.data.payment;
            _shipping.updateShipping(shipping);


            modalStatus.css({ "color": "green" });
            modalStatus.html("Su pago fue procesado con éxito.");
            setTimeout(() => {
                $('#loader').show();
                setTimeout(() => {
                    $('#modal_payment').modal('hide');
                    //HandleGenerateGuide();
                    HandlePrint();
                    steps.HandleSteps();
                    $('#loader').hide();
                }, 2000);
            }, 2000);

        });

        paymentRequest.fail(function (e) {
            console.log("Error on paymentRequest");
            modalStatus.css({ "color": "red" });
            $('#insert_card_image').hide();
            $('.modal-center-cs').css({
                'margin-top': '0px'
            });
            modalStatus.html('Hubo un error. Por favor intente con otro método de pago. <p style="color:#00397c">Presione para reintentar.</p>');
            return;
        });
    }

    

    $('#payment_cash').click(function () {
        $('#modal_payment_type_1').modal('hide');

        HandlePayment("cash");

        //let shipping = _shipping.getShipping();
        //shipping.payment.PaymentMethod = "cash";
        //_shipping.updateShipping(shipping);

        //let paymentRequest = _communication.DoPayment(shipping);

        //paymentRequest.done(function (response) {
        //    console.log(response);
        //});

        //paymentRequest.fail(function (e) {
        //    console.log("Error on paymentRequest");
        //});

    });
    $('#payment_pos').click(function () {

        $('#modal_payment_type_1').modal('hide');

        HandlePayment("pos");


        return;
        $('#modal_payment_type_1').modal('hide');
        $('#modal_payment_pos_process').modal('show');
        let shipping = _shipping.getShipping();
        shipping.payment.PaymentMethod = "pos";
        _shipping.updateShipping(shipping);

        let paymentRequest = _communication.DoPayment(shipping);

        paymentRequest.done(function (response) {
            if (response.data.error.HasError) {
                alert("Error en la comunicación " + response.data.error.Message);
                return;
            }
            $('#modal_payment_pos_process_message').html('<h4 style="color:green">Su pago fue procesado con éxito.</h4><i id="payment_check" style="color:green" class="fas fa-check fa-4x "></i>');
            
            setTimeout(function () {
                $('#modal_payment_pos_process').modal('hide');
                $('#loader').show();
                setTimeout(function () {
                    $('#loader').hide();
                    console.log("trying generate guide");
                    HandleGenerateGuide();
                }, 4000);
            }, 4000);
        });

        paymentRequest.fail(function (e) {
            console.log("Error on paymentRequest");
        });

    });

    function HandleGenerateGuide() {

        let shipping = _shipping.getShipping(); 
        let generateGuideRequest = _communication.GenerateGuide(shipping);
        console.log("handleGenGuide");
        generateGuideRequest.done(function (response) {
            console.log("generateGuideRequest done");
            console.log(response);
            _shipping.updateShipping(response);
            return;

            var message = '<strong><h4>Su envio ha sido confirmado. ' +
                'El número de guía para hacerle seguimiento es: ' + response.data.Code + '.</h4></strong>';

            $('#modal_resume_finish_message').html(message);

            $('#modal_resume_finish').modal('show');

            _communication.OpenDoor('ContainerReceiver');
            HandlePrintGuide(response.data);
            let shipping = _shipping.getShipping();
            shipping.guide = response.data;
            _shipping.updateShipping(shipping);
            HandleGenerateInvoice();
        });

        generateGuideRequest.fail(function (e) {
            console.log("Error on generateGuideRequest");
        });
    }


    function HandleGenerateInvoice() {
        let shipping = _shipping.getShipping();
        let generateInvoiceRequest = _communication.GenerateInvoice(shipping);

        generateInvoiceRequest.done(function () {
            HandlePrintBill();
        });

        generateInvoiceRequest.fail(function () {
            console.log("Error on generateInvoiceRequest");
        });
    }
    function HandlePrintBill() {
        let shipping = _shipping.getShipping();
        let invoice = shipping.guide.Code;
        let printBillRequest = _communication.PrintBill(invoice);

        printBillRequest.done(function (response) {
            console.log(response);
        });

        printBillRequest.fail(function (e) {
            console.log("Error on printBillRequest");
        });
    }

    function HandlePrintGuide(guide) {
        let printGuideRequest = _communication.PrintGuide(guide);

        printGuideRequest.done(function (response) {
            console.log(response);
        });

        printGuideRequest.fail(function (e) {
            console.log("Error on printGuideRequest");
        });
    }

    $('#finish_process').click(function () {
        $('#loader').show();
        setTimeout(function () {
            $(location).attr('href', APP_DIR);
        }, 3000);
    });


    $('.input-onlyletter').on('keypress', function (event) {
        _validation.AllowOnlyLetters(event);
    });

    $('.input-onlynumber').on('keypress', function (event) {
        _validation.AllowOnlyNumbers(event);
    });


    //Disable autocomplete in forms
    $('form').attr('autocomplete', 'off');
    $('input').attr('autocomplete', 'rutjfkde');



    $('#test_act').click(function () {
        HandleResumeData();

    });

    $(".resume").on("click", ".edit-resume", function () {
        let element = $(this).attr('id');

        let shipping = _shipping.getShipping();


    });

    let idleTime = 1;
    //Zero the idle timer on mouse movement.
    $(this).mousemove(function (e) {
        idleTime = 0;
    });

    $(this).keypress(function (e) {
        idleTime = 0;
    });


    function timerIncrement() {
        if (idleTime === 0) {
            idleTime = 1;
            return;
        }
        idleTime = idleTime + 1;
        if (idleTime === 45) {

            let message = 'La sesión se cerrará automáticamente en <label id="timer_test"></label> segundos.';
            Modal.Show('timeCount', '¿Desea continuar con el envio?', message);

            let element = document.getElementById('timer_test');

            Utilities.SetCounter(() => {

            }, element, 15);
           
        }
        if (idleTime > 60) {
            window.location.href = APP_DIR;
        }
    }

    function GenerateShipping() {

        var shipping = _shipping.getShipping();

        shipping.content.Description = "BOLSOS";
        shipping.content.Value = 20000;
        shipping.content.Type = "package";

        shipping.origin.Identification = "1036668468";
        shipping.origin.IdentificationType = "CC";
        shipping.origin.Name = "BRAYAN VALLE";
        shipping.origin.Email = "brayanvallejaramillo@gmail.com";
        shipping.origin.Phone = "3007444471";
        shipping.origin.Location.Address = "CALLE 36";
        shipping.origin.Location.City = "Medellín";
        shipping.origin.Location.CityCode = "5001";
        shipping.origin.Location.Department = "Antioquia";


        shipping.receiver.Identification = "1036668468";
        shipping.receiver.IdentificationType = "CC";
        shipping.receiver.Name = "MEKAGROUP SAS";
        shipping.receiver.Email = "info@mekagroupcol.com";
        shipping.receiver.Phone = "2507070";
        shipping.receiver.Location.Address = "CALLE 12 SUR";
        shipping.receiver.Location.City = "Medellín";
        shipping.receiver.Location.CityCode = "5001";
        shipping.receiver.Location.Department = "Antioquia";

        shipping.content.Measures = [{
            "Height": 10,
            "ImageBase64": null,
            "Length": 10,
            "Status": "STABLE",
            "Success": false,
            "VolumetricWeight": 10,
            "Weight": 10,
            "Width": 10
        }]; 

        _shipping.updateShipping(shipping);
        return shipping;
    }

    $('#open_door_modal').click(function () {

        $('#door_status').html('ABRIENDO COMPUERTA');
        $('#modal_message_test').html('EL RECIBIDOR DE PAQUETES SE ABRIRÁ');
        $('#finish_proccess_help').modal('hide');
        $('#modal_open_door').modal('show');

        let openDoorRequest = _communication.OpenDoor('ContainerReceiver');

        openDoorRequest.done(function () {
            $('#modal_message_test').html('POR FAVOR INGRESE SUS PAQUETES');
            $('#door_status').html('ABIERTA');     
            HandleDoorStatus();
        });

        openDoorRequest.fail(function () {
            $('#door_status').html('ERROR AL ABRIR LA COMPUERTA');         

        });
    });

    function HandleDoorStatus(attemps = 30000) {

        let doorStatusRequest = _communication.GetDoorStatus('ContainerReceiver');

        doorStatusRequest.done(function (response) {
            if (attemps > 2) {
                setTimeout(() => {
                    if (response.IsOpen || response.error.HasError) {
                        HandleDoorStatus(attemps - 1);
                    } else {
                        HandleDoorClosed();
                    }
                }, 1000)
            }
        });
    }
    
    function HandleOpenDoor() {
        let openDoorRequest = _communication.OpenDoor('ContainerReceiver');
        openDoorRequest.done((response) => {
            if (response.HasEror) {
                $('#door_status').html('HUBO UN ERROR AL ABRIR. PRESIONE PARA REINTENTAR');
                return;
            }
            $('#door_status').html('PUERTA ABIERTA. INGRESE SU PAQUETE <i class="fas fa-unlock-alt"></i>');
            HandleDoorStatus();
        });
        openDoorRequest.fail(() => {
            $('#door_status').html('HUBO UN ERROR AL ABRIR. PRESIONE PARA REINTENTAR');
            return;
        });
    }
    

    function HandleDoorClosed() {

        $('#door_status').html('PUERTA CERRADA CORRECTAMENTE');

        let shipping = _shipping.getShipping();

        let postShippingRequest = _communication.PostShipping(shipping);
        postShippingRequest.done(function (response) {

            console.log(response);
            setTimeout(() => {
                $('#modal_open_door').modal('hide');

                let message_1 = 'El envio programado por ' + shipping.origin.Name + ' hacia ' + shipping.receiver.Location.City + ' ha sido confirmado.';
                let message_2 = 'El numero de la guía para hacerle seguimiento es: ' + shipping.code;
                let message_3 = 'Cualquier duda/inquietud puede comunicarse al numero 3334455.';


                $('#finish_process_resume_1').html(message_1);
                $('#finish_process_resume_2').html(message_2);
                $('#finish_process_resume_3').html(message_3);
                steps.HandleSteps();

                _custom.CloseSession()
            }, 2000);
        });

        postShippingRequest.fail(function () {
            console.log("Error on post shipping");
        });
        

       
    }


    $('#btn_finish_process').click(function () {
        window.location.href = AppConfig.GetAppDir();
    });

    $('#cancel_shipping').click(function () {
        window.location.href = AppConfig.GetAppDir();
    });

});
