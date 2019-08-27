


$(document).ready(function () {

    //Vars
    const steps = new Steps();
    const _shipping = new Shipping();
    const _validation = new Validation();
    const _communication = new Communication();
    const _custom = new Custom();

    const APP_DIR = AppConfig.GetAppDir();
    const APP_ADMIN = AppConfig.GetKioskoAdmin();

    let idleInterval = null;
    let optionSelected = "";
    let MeasureList = [];
    let flagPos = false;
    let flagCash = false;
    let flagUpdate = false;


    $('.welcome-button').click(function () {    

        let tycAccepted = $('#checkbox_autocomplete_origin').prop("checked");
        
        if (!tycAccepted) {
            Modal.Show('error2', 'Error', 'Debe aceptar los términos y condiciones para continuar');
            return;
        }

        $('#loader').show();
        setTimeout(function () {
            $(location).attr('href', APP_DIR + 'Home/Start?source=start');
        }, 3000);
    
    });


    $('#processInitWithJustPos').click(function () {
        flagPos = true;
    });

    $('#processInitWithJustCash').click(function () {
        flagCash = true;
    });

    
    StartKiosko();
    function StartKiosko() {
        //Si la url no es la que redirecciona $('.welcome-button');
        if (window.location.search !== '?source=start')
            return;


        InitializeShipping();
        idleInterval = setInterval(timerIncrement, 1000);

        flagPos = false;

        let requestInventory = _communication.GetInventory();
        GetDepartmentList(5);

        function checkLocation(Inventaries, location) {
            return Inventaries.filter(inventary => inventary.Location.includes(location));
        };



        requestInventory.done(function (response) {

            let InventaryArray = response
            InventarySmartHopperValue = checkLocation(InventaryArray, "SMARTHOPPER").map(inventary => inventary.Value);
            InventarySmartHopperQuantity = checkLocation(InventaryArray, "SMARTHOPPER").map(inventary => inventary.Inventory);
            InventaryF56Value = checkLocation(InventaryArray, "F56").map(inventary => inventary.Value);
            InventaryF56Quantity = checkLocation(InventaryArray, "F56").map(inventary => inventary.Inventory);

            let sumHopper = 0;
            for (let i = 0; i < InventarySmartHopperValue.length; i++) {
                sumHopper += InventarySmartHopperValue[i] * InventarySmartHopperQuantity[i];
            };

            let sumF56 = 0;
            for (let i = 0; i < InventaryF56Value.length; i++) {
                sumF56 += InventaryF56Value[i] * InventaryF56Quantity[i];
            };

            if (sumF56 < 10000 || sumHopper < 4000) {
                Modal.Show("withoutMoney", "En este momento el módulo solo acepta pagos con tarjeta", "¿Desea continuar con el proceso?");
            };
        });

        requestInventory.fail(function () {
            Modal.Show("withoutMoney", "En este momento el módulo solo acepta pagos con tarjeta", "¿Desea continuar con el proceso?");       
        });


        //Inicializar kiosko para el usuario
        _custom.ProgressBar();

        $('#footer_left').hide();
        $('#footer_right').show();
        $('.footer').show();
        $('#loader').hide();
        $('input').attr('autocomplete', 'off');
        $('input').addClass('form-control-focus');
        $('select').addClass('form-control-focus');
        $('#footer_customer_info').hide();
        $('#footer-steps').show();    
        
        $('.' + steps.getCurrent().class).show();
    }

    if (!window.navigator.onLine) {

        if (window.location.href !== APP_DIR) {
            $(location).attr('href', APP_DIR);
        };
        clearInterval(idleInterval);
        $('#maintenanceImg').show();

    }

    //Botón de siguiente
    $('.continue-button').click(function () {

        let continueButtonEnabled = $('.continue-button').attr('enabled') === 'true';
        if (!continueButtonEnabled)
            return;
        let current_step = steps.getCurrent().class;
        
        

        let shipping = _shipping.getShipping();        

        let addressReceiver = document.getElementsByName("receiver_address")[0].value;
        let confirmationAddressReceiver = document.getElementsByName("receiver_address_copy")[0].value;

        if (addressReceiver !== confirmationAddressReceiver) {

            Modal.Show('error', 'Error', 'Las direcciones no coinciden. Modifique los campos para continuar');
            return;
            
        };


        _validation.SetShipping(shipping);
        if (_validation.IsValid(current_step)) {
            if (current_step !== "print") {
                $('#footer_left').show();
            }
            HandleShippingData(shipping, current_step);      
            steps.HandleSteps();
        };
        
    });


    $(document).on('keypress', function (e) {
        let shipping = _shipping.getShipping();
        let current_step = steps.getCurrent().class;
        
        if (e.which == 13) {
            if (current_step !== "resume" && current_step !== "opendoor" && current_step !== "print" && current_step !== "finish" ) {
                let continueButtonEnabled = $('.continue-button').attr('enabled') === 'true';
                if (!continueButtonEnabled)
                    return;

                
                
                let addressReceiver = document.getElementsByName("receiver_address")[0].value;
                let confirmationAddressReceiver = document.getElementsByName("receiver_address_copy")[0].value;

                if (addressReceiver !== confirmationAddressReceiver) {

                    Modal.Show('error', 'Error', 'Las direcciones no coinciden. Modifique los campos para continuar');
                    return;

                }

                _validation.SetShipping(shipping);
                if (_validation.IsValid(current_step)) {
                    if (current_step !== "print") {
                        $('#footer_left').show();
                    }
                    HandleShippingData(shipping, current_step);
                    steps.HandleSteps();
                }
            }
        }
    });


    $('.back-button').click(function () {

        let addressReceiver = document.getElementsByName("receiver_address")[0].value;
        let confirmationAddressReceiver = document.getElementsByName("receiver_address_copy")[0].value;
        let current_step = steps.getCurrent().class;
        if (current_step === 'cubiq') {
            $('#footer_left').hide();
        }

        if (addressReceiver !== confirmationAddressReceiver) {

            Modal.Show('error', 'Error', 'Las direcciones no coinciden. Modifique los campos para continuar');
            return;

        };

        $('.continue-button').attr('enabled', 'true');
        steps.HandleSteps(false);
    });


    function HandleShippingData(shipping, view) {
        switch (view) {
            case "origin": HandleOriginData(shipping);
                break;
            case "receiver":
                HandleReceiverData(shipping);
                _custom.SetResume(shipping);
                HandleResumeData();
                break;
            case "content": HandleContentData(shipping);
                $('.continue-button').attr('enabled', false);
                break;
            case "cubiq":
                HandleCubiQData(shipping);
                break;     

            case "print":
                $('#footer_right').hide();

               
                if (shipping.content.Type === "package") {
                    setTimeout(() => {
                        HandleOpenDoor();
                    }, 5000);
                } else {
                    HandleOpenLabel();
                }
                   
                break;

            case "finish":
                $('#loader').show();
                steps.FinishProcess();
                break;
        }
    }
    function InitializeShipping() {

        let shipping = _shipping.getShipping();        
        let initializeShippingRequest = _communication.InitializeShipping(shipping);

        initializeShippingRequest.done(function (response) {
            _shipping.updateShipping(response);

            //For test
            //GenerateShipping();

        });

        initializeShippingRequest.fail(function () {
            console.log("Error on initializeShippingRequest");
        });
    }

    //Función de prueba::
    function GenerateShipping() {

        var shipping = _shipping.getShipping();

        shipping.content.Description = "BOLSOS";
        shipping.content.Value = 20000;
        shipping.content.Type = "box";

        shipping.origin.Identification = "1036668468";
        shipping.origin.IdentificationType = "CC";
        shipping.origin.Name = "BRAYAN VALLE";
        shipping.origin.Email = "brayanvallejaramillo@gmail.com";
        shipping.origin.Phone = "3007444471";
        shipping.origin.Location.Address = "CALLE 36";
        shipping.origin.Location.City = "Medellín";
        shipping.origin.Location.CityCode = "5001";
        shipping.origin.Location.Department = "Antioquia";
        

        shipping.receiver.Identification = "102522";
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


        shipping.guide.Code = "TEST";
        _shipping.updateShipping(shipping);
        shipping = _shipping.getShipping();
        //let request = _communication.PostShipping(shipping);
        return shipping;
    }

    function HandleOpenLabel() {
        $('#PutContentInside').html("INGRESE EL SOBRE");
        $('#DoorWillOpen').hide()
        $('#door_status').html("Por favor ingrese el sobre y presione aquí para continuar");
    }
    

    function HandleOriginData(shipping) {

        let dataForm = $('#origin_form').serializeArray();
        shipping.origin.IdentificationType = "CC";
        shipping.origin.Name = dataForm[0].value;
        shipping.origin.Identification = dataForm[1].value;
        shipping.origin.Location.Department = $('#origin_department option:selected').text();
        shipping.origin.Location.CityCode = dataForm[3].value;
        shipping.origin.Phone = dataForm[4].value;
        shipping.origin.Location.Address = dataForm[5].value;
        shipping.origin.Location.City = $('#origin_city option:selected').text();
        _shipping.updateShipping(shipping);

    }

    function HandleReceiverData(shipping) {
        let dataForm = $('#receiver_form').serializeArray();     

        shipping.receiver.IdentificationType = "CC";        
        shipping.receiver.Name = dataForm[0].value;
        shipping.receiver.Identification = dataForm[1].value;
        shipping.receiver.Location.Department = $('#receiver_department option:selected').text();
        shipping.receiver.Location.CityCode = dataForm[3].value;
        shipping.receiver.Phone = dataForm[4].value;
        shipping.receiver.Location.Address = dataForm[5].value;        
        shipping.receiver.Location.City = $('#receiver_city option:selected').text();

        _shipping.updateShipping(shipping);
    }

    function HandleContentData(shipping) {

        let dataForm = $('#content_form').serializeArray();

        let shipping_value = dataForm[2].value;
        let new_value = shipping_value.replace('$', '');
        new_value = new_value.replace(/\s+/g, '');
        new_value = new_value.replace(/\./g, '')
        shipping.content.Description = dataForm[1].value;
        shipping.content.Value = new_value;
        shipping.content.Type = dataForm[0].value;
        shipping.shippingType.Name = dataForm[0].value
        shipping.shippingType.KeyName = dataForm[0].value

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
            TotalCostVal = response.data.TotalCost;
            $('#resume_total_cost').html('<strong>Total a pagar: $</strong>' + shipping.payment.Cost.TotalCost);
            if (flagPos && TotalCostVal > 55000) {
                Modal.Show("noPaymentMethodAvailable", "En el momento no podemos continuar con el proceso", "Disculpe las molestias");
                return;
            }
            if (TotalCostVal === 0) {
                console.log("valor en 0 del costo")
                Modal.Show("totalCostError", "Error en el sistema", "intentelo nuevamente");
            }
            if (TotalCostVal > 55000 && !flagUpdate) {
                Modal.Show("justCashMethod", "El proceso solo puede continuar con pagos en efectivo", "¿Desea continuar con el proceso?");
            }
            
            _shipping.updateShipping(shipping);
        });

        request.fail(function (e) {
            console.log("Error on cost service");
        });

        request.always(function () {
            $('#loader').hide();
        });

    }

   
    function HandlePayment(paymentMethod, retry = false) {

        let shipping = _shipping.getShipping();
        let modalStatus = $('#modal_payment_status');

        modalStatus.css({ "color": "#00397c" });
        modalStatus.html('Verificando pago <label id="modal_payment_status_timeout" style="display:none;></label> <i class="far fa-clock ml-2"></i>');
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
                $('#modal_payment').modal('hide');
                $('#errorPaymentModal').modal({ backdrop: 'static', keyboard: false });
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
                    steps.HandleSteps()
                    $('#loader').hide();
                    $('#print_guide_btn').show();
                    $('#print-guide-text').removeClass('slideInUp');             
                    $('#print-guide-text').html('');
                    $('#warningPrint').show();
                    $('#print_guide_btn').html("PRESIONE PARA IMPRIMIR ETIQUETA");
                    $('#print_guide_image').hide();
                }, 2000);
            }, 2000);

        });

        paymentRequest.fail(function (e) {
            $('#modal_payment').modal('hide');
            $('#errorPaymentModal').modal({ backdrop: 'static', keyboard: false });
            return;
        });
    }

    // Para simular la impresión de la etiqueta y recibo
    const TestWithOutPrint = () => {
        let shipping = _shipping.getShipping();
        let generateGuideRequest = _communication.GenerateGuide(shipping);

        //handle UI actions//
        $('#print_guide_btn').hide();
        $('#print_guide_container').show();
        $('#print-guide-text').addClass('slideInUp');
        $('#print-guide-text').html(' <i class="fas fa-spinner make-it-spinn" style="color:white"></i> IMPRIMIENDO ETIQUETA');
        $('#warningPrint').hide();
        $('#footer_right').hide();

        generateGuideRequest.done(function (response) {
            if (response.data.error.HasError) {
                $('#print_guide_btn').html("Hubo un error. Presione para reintentar");
                $('#print_guide_btn').attr('enable_print', "true");
                return;
            }

            $('#finish_guide_code').html(response.data.Code);
            shipping = _shipping.getShipping();
            shipping = response.data;
            _shipping.updateShipping(shipping);
        });

        generateGuideRequest.fail(function (e) {
            console.log("Error on generateGuideRequest");
        });

        setTimeout(() => {
            $('#print-guide-text').removeClass('slideInUp');
            setTimeout(() => {
                $('#print-guide-text').addClass('slideInUp');
                $('#print-guide-text').html(' <i class="fas fa-check" style="color:limegreen"></i> ETIQUETA IMPRESA CORRECTAMENTE');
                $('#print_guide_image').show();
                $('#footer_right').show();
            }, 3000);
        }, 3000);
        

    };


    function HandleGenerateGuide() {
        let shipping = _shipping.getShipping();

        let generateGuideRequest = _communication.GenerateGuide(shipping);

        //handle UI actions//
        $('#print_guide_btn').hide();
        $('#print_guide_container').show();
        $('#print-guide-text').addClass('slideInUp');
        $('#print-guide-text').html(' <i class="fas fa-spinner make-it-spinn" style="color:white"></i> IMPRIMIENDO ETIQUETA');
        $('#warningPrint').hide();
       
        generateGuideRequest.done(function (response) {
            if (response.data.error.HasError) {
                $('#print_guide_btn').html("Hubo un error. Presione para reintentar");
                $('#print_guide_btn').attr('enable_print', "true");
                return;
            }

            $('#finish_guide_code').html(response.data.Code);
            shipping = _shipping.getShipping();
            shipping = response.data;
            _shipping.updateShipping(shipping);     
            HandlePrintGuide();


        });

        generateGuideRequest.fail(function (e) {
            console.log("Error on generateGuideRequest");
        });

    }

    function HandlePrintBill() {

        let shipping = _shipping.getShipping();
        let printBillRequest = _communication.PrintBill(shipping);

        printBillRequest.done(function (response) {
            console.log(response);
        });

        printBillRequest.fail(function (e) {
            console.log("Error on printBillRequest");
        });

    }

    function HandlePrintGuide() {
        shipping = _shipping.getShipping();        
        let printGuideRequest = _communication.PrintGuide(shipping);

        printGuideRequest.done(function (response) {
            if (response.data.HasError) {                
                $('#print-guide-text').html("Hubo un error. Comuníquese al 018000520555 para solicitar ayuda");
                $('#print-guide-text').attr('style', "margin-top: 200px;border: 0px !important; font-size: 40px;");
                return;
            }
            setTimeout(() => {
                $('#print-guide-text').removeClass('slideInUp');
                setTimeout(() => {
                    $('#print-guide-text').addClass('slideInUp');
                    $('#print-guide-text').html(' <i class="fas fa-check" style="color:limegreen"></i> ETIQUETA IMPRESA CORRECTAMENTE');
                    $('#print_guide_image').show();
                    $('#footer_right').show();
                }, 1000);
            }, 1000);
            
        });

        printGuideRequest.fail(function (e) {
            console.log("Error on printGuideRequest");
        });
    }


    function HandleReprintGuide() {
        shipping = _shipping.getShipping();
        let printGuideRequest = _communication.PrintGuide(shipping);

        $('#print_guide_btn').hide();
        $('#print_guide_container').show();
        $('#print-guide-text').addClass('slideInUp');
        $('#print-guide-text').html(' <i class="fas fa-spinner make-it-spinn" style="color:white"></i> IMPRIMIENDO ETIQUETA');
        $('#warningPrint').hide();

        printGuideRequest.done(function (response) {
            if (response.data.HasError) {
                $('#print-guide-text').html("Hubo un error. Comuníquese al 018000520555 para solicitar ayuda");
                $('#print-guide-text').attr('style', "margin-top: 200px;border: 0px !important; font-size: 40px;");
                return;
            }
            //UI effect
            setTimeout(() => {
                $('#print-guide-text').removeClass('slideInUp');
                setTimeout(() => {
                    $('#print-guide-text').addClass('slideInUp');
                    $('#print-guide-text').html(' <i class="fas fa-check" style="color:limegreen"></i> ETIQUETA IMPRESA CORRECTAMENTE');
                    $('#print_guide_image').show();
                    $('#footer_right').show();
                }, 1000);
            }, 1000);

        });

        printGuideRequest.fail(function (e) {
            console.log("Error on printGuideRequest");
        });
    }

    $('#door_status').click(function () {

        let shipping = _shipping.getShipping();

        if (shipping.content.Type === "package") {
            HandleOpenDoor();
        } else {
            $('#loader').show();
            HandlePrintBill();
            HandleFinishProccess();
            setTimeout(() => {
                $('#loader').hide();
                $('#footer_left').hide();
                $('#footer_right').show();
                $('#footer_retry_all_process').show();
                $('#text_next').html("FINALIZAR");
                steps.HandleSteps();
            }, 3000);
        }
    });


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

    function HandleDoorStatus(attemps = 30000) {

        let doorStatusRequest = _communication.GetDoorStatus('ContainerReceiver');

        doorStatusRequest.done(function (response) {
            if (attemps > 2) {
                setTimeout(() => {
                    if (response.IsOpen || response.hasError) {
                        HandleDoorStatus(attemps - 1);
                    } else {
                        HandleDoorClosed();
                    }
                }, 1000)
            }
        });
    }

    function HandleDoorClosed() {

        HandlePrintBill();
        $('#door_status').html('PUERTA CERRADA CORRECTAMENTE');
        $('#loader').show();
        HandleFinishProccess();
        setTimeout(() => {
            $('#loader').hide();
            $('#footer_right').show();
            $('#footer_retry_all_process').show();
            $('#text_next').html("FINALIZAR");
            steps.HandleSteps();
        }, 3000);
    }

    

    function HandleFinishProccess() {      

        let shipping = _shipping.getShipping();
        let request = _communication.PostShipping(shipping);

        request.done(function (response) {
            _custom.CloseSession();
        });

        request.fail(function () {
            console.log("PostShipping request failed");
        });
    } 

    $('#btn_get_measures').click(function () {
        
        $('#loader').show();
        let shipping = _shipping.getShipping();
        let request = _communication.GetMeasures(shipping.content.Type);
        request.done(function (response) {
            if (response.data.Error.HasError) {
                Modal.Show('error', 'Error', response.data.Error.Message);   
                return;
            }

            if (_custom.HandleMeasures(response.data)) {
                MeasureList = [];
                MeasureList.push(response.data);
                $('.continue-button').attr('enabled', 'true');
            }
        });

        request.fail(function (e) {
            Modal.Show('error', 'Error', 'Hubo un error al obtener las medidas. Por favor intente nuevamente');
        });

        request.always(function () {
            $('#loader').hide();
        })
    });

    function GetDepartmentList(attemps) {

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
            Utilities.addOptionsToSelect($('#origin_department'), departmentList);
            document.getElementById("origin_department").options[1].selected = true;

            var department_selected = document.getElementById("origin_department").options[1].value;
            $('#loader').show();
            let requestOrigin = _communication.GetCityListByDepartment(department_selected);

            requestOrigin.done(function (response) {
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
                citySelector = '#origin_city';
                Utilities.addOptionsToSelect($(citySelector), cityList);
                document.getElementById("origin_city").options[83].selected = true;

            });

            //Receiver
            document.getElementById("receiver_department").options[0].selected = true;

            var department_selected_receiver = document.getElementById("receiver_department").options[0].value;
            $('#loader').show();
            let requestReceiver = _communication.GetCityListByDepartment(department_selected_receiver);

            requestReceiver.done(function (response) {
                if (response === null) {
                    return;
                }
                if (response.data === null) {
                    return;
                }
                if (response.data.cityList === null) {
                    return;
                }
                var cityListReceiver = response.data.cityList.map(function (city) {
                    return {
                        text: city.Name,
                        value: city.Code
                    }
                });
                citySelectorReceiver = '#receiver_city';
                Utilities.addOptionsToSelect($(citySelectorReceiver), cityListReceiver);
                document.getElementById("receiver_city").options[0].selected = true;

            });

            requestReceiver.fail(function (e) {
                console.log("GetCityListByDepartment fail");
            });

            requestReceiver.always(function () {
                $('#loader').hide();
            })

        });

        request.fail(function (e) {
            console.log("Fail");
        })
    }

    $('.department_list').change(function () {
        let selector = $(this).attr('id');
        optionSelected = $('#' + selector).find("option:selected");

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

            citySelector = selector === 'origin_department' ? '#origin_city' : '#receiver_city';
            Utilities.addOptionsToSelect($(citySelector), cityList);
        });

        request.fail(function (e) {
            console.log("GetCityListByDepartment fail");
        });

        request.always(function () {
            $('#loader').hide();
        })

    });


    $('#btn_do_payment').click(function () {

        if (flagPos) {
            HandlePayment('pos');
            clearInterval(idleInterval);
            _custom.HandlePaymentPos();
        } else if (flagCash) {
            HandlePayment('cash');
            clearInterval(idleInterval);
            _custom.HandlePaymentPos();
        }else {
            _custom.ShowPaymentTypeModal();
        }
        
    });


    $('.btn_payment_method').click(function () {
        let paymentMethod = $(this).attr('id').replace('btn_payment_', '');
        HandlePayment(paymentMethod);
    });

   
    $('#print_guide_btn').click(function () {

        if ($(this).attr('enable_print') === "true")
        {
            $(this).attr('enable_print', false);
            HandleGenerateGuide();
        }

        //TestWithOutPrint();
        
    });

    $('#retry_print_guide_btn').click(function () {

        HandleReprintGuide();

    });


    // Shortcut para ingresar al kiosko admin
    document.onkeyup = function (e) {
        if (e.ctrlKey && e.altKey && e.which == 89) {
            $(location).attr('href', APP_ADMIN);
        };
    };


    // Conexión Internet y mantenimiento 

    if (!window.navigator.onLine) {
        $('#maintenanceImg').show();
    }

    // Para pruebas sin el kiskoadmin
    const maintenanceFlag = false;

    const MaintenanceOn = () => {
        clearInterval(idleInterval);
        $('#maintenanceImg').show();
    };
        
    const MaintenanceOff = () => {
        if (!maintenanceFlag) {
            $('#maintenanceImg').hide();
            $(location).attr('href', APP_DIR);
        };
    };

    window.addEventListener('offline', function (e) { MaintenanceOn(); });
    window.addEventListener('online', function (e) { MaintenanceOff(); });

    if (maintenanceFlag) {
        $('#maintenanceImg').show();
    };


    // Termina el proceso con un click y regresa a pantalla de inicio

    $('#finish_process').click(function () {
        $('#loader').show();
        setTimeout(function () {
            $(location).attr('href', APP_DIR);
        }, 2000);
    });


    // Validaciones

    $('.input-onlyletter').on('keypress', function (event) {
        _validation.AllowOnlyLetters(event);
    });

    $('.input-onlynumber').on('keypress', function (event) {
        _validation.AllowOnlyNumbers(event);
    });

    $('.input-onlynumberAndDash').on('keypress', function (event) {
        _validation.AllowOnlyNumbersAndDash(event);
    });

    // Deshabilitar autocompletado de los formularios

    $('form').attr('autocomplete', 'off');
    $('input').attr('autocomplete', 'rutjfkde');


    $('.btn_retry_all_process').click(function () {
        HandleRetryProcess();
    });

    function HandleRetryProcess() {

        _custom.setRetryProcess(true);
        let shipping = _shipping.getShipping();
        Utilities.resetForms();

        $('#origin_identification_type').val(shipping.origin.IdentificationType);
        $('#origin_identification_number').val(shipping.origin.Identification);
        $('#origin_name').val(shipping.origin.Name);
        $('#origin_address').val(shipping.origin.Location.Address);
        $('#origin_phone').val(shipping.origin.Phone);
        $('#origin_email').val(shipping.origin.Email);
        $('.finish').hide();
        $('#footer_right').show();
        $('#footer_left').hide();
        $('#footer_retry_all_process').hide();
        $('#footer_left').hide();
        $('#text_next').html("CONTINUAR");
        $('#current_cubiq_image').attr('src', '/Assets/kiosko/img/IMAGEN-BASCULA-558X480.png');
        $('#measure_height').html(' ');
        $('#measure_width').html(' ');
        $('#measure_length').html(' ');
        $('#measure_weight').html(' ');
        $('#print_guide_btn').attr('enable_print', true);

        $('#btn_get_measures').html(' OPRIMA PARA CALCULAR PESO<i class="fas fa-arrow-down ml-2"></i>');

        _shipping.updateShipping(new Shipping());


        // Get CONTENT step
        let contentStep = steps.getStep(1);
        steps.setCurrent(contentStep.class);
        steps.HideAllViews();    
        steps.Show(contentStep);    
        return;
    }


    // No permite que el usuario digite un valor superior al maximo permitido

    $('#content_value').on('keyup', (e) => {

        let current_val = $('#content_value')[0].value;

        let new_value = current_val.replace('$', '');
        new_value = new_value.replace(/\s+/g, '');
        new_value = new_value.replace(/\./g, '')
        HandleIsMaxDeclaredValue(new_value);
    });


    function HandleIsMaxDeclaredValue(value) {

        if ($('#content_shipping_type').val() == 'envelope') {

            if (value > 200001) {
                $('#exceededValue').modal('show');
                $('#content_value').val("");
            }

        } else {

            let request = _communication.IsMaxDeclaredValue(value);
            request.done(function (response) {

                if (response.isMaxDeclaredValue) {

                    $('#exceededValue').modal('show');
                    $('#content_value').val("");
                }

            });
            request.fail(function () {
                console.log("Error on HandleIsMaxDeclaredValue");
            });

        }
        
    };


    // Inactividad en la UI -- Muestra Modal

    let idleTime = 1;

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

        if (idleTime === 120) {

            let message = 'La sesión se cerrará automáticamente en <label id="timer_test"></label> segundos.';
            Modal.Show('timeCount', '¿Desea continuar con el envio?', message);

            let element = document.getElementById('timer_test');

            Utilities.SetCounter(() => {

            }, element, 15);

        }
        if (idleTime > 135) {
            window.location.href = APP_DIR;
        }
    }


    //Edit resume data

    $('.edit-data').click(function () {
        let viewToEdit = $(this).attr('id');

        //hide resume view
        $('.resume').hide();

        //hide the viewToEdit

        viewToShow = viewToEdit.replace('edit_', '');
        $('.' + viewToShow).show();

        //hide progress bar
        $('#progress_bar').hide();

        //Relocate btn
        $("footer").attr("style", "margin-bottom:50px;");

        //show edit btn
        $('#btn_update_data').show();
        $('#btn_update_data').attr('data-to-edit', viewToShow);

    });


    $('body').on('click', '#btn_retry_payment', function () {

        $('#common_modal').modal('hide');
        $('.modal-backdrop').remove();
        if (flagPos) {
            HandlePayment('pos');
            clearInterval(idleInterval);
            _custom.HandlePaymentPos();
        } else {
            $('#modal_payment_type').modal('show');
        };

    });


    $('#btn_update_data').click(function () {

        current_step = viewToShow
        if (_validation.IsValid(current_step)) {

            let view = $('#btn_update_data').attr('data-to-edit');

            let shipping = _shipping.getShipping();
            switch (view) {
                case 'origin': HandleOriginData(shipping);
                    break;
                case 'receiver': HandleReceiverData(shipping);
                    break;
                case 'content': HandleContentData(shipping);
                    break;
            }

            $('.' + view).hide();
            $('.resume').show();
            $("footer").attr("style", "margin-bottom:0px;");
            $('#btn_update_data').hide();
            HandleResumeData();
            flagUpdate = true;
            $('#progress_bar').show();
            _custom.SetResume(shipping);

        }       
    });


   
});
