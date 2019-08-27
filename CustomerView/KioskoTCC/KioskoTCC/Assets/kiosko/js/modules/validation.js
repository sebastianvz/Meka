

class Validation {

    shipping = null;
    constructor() {
    }

    SetShipping(_shipping) {
        this.shipping = _shipping
    }

    static IsFormValid(form) {
        let valid = true;
        $("form#" + form + " :input").each(function () {

            if ($(this).prop('required')) {
                if ($(this).attr('type') === 'email') {
                    if (!Validation.IsEmailValid(($(this).val()))) {
                        valid = false;
                        $(this).addClass("highlight-button");
                        $(this).attr('placeholder', 'Falta diligenciar este campo');
                        setTimeout(function () {
                            Modal.Show('error', "¡Atención!", "Debe ingresar un email válido");
                        }, 1);
                        return false;
                    }
                }


                if ($(this).attr('isPhone')) {
                    if (!Validation.IsPhoneValid(($(this).val()))) {

                        valid = false;
                        $(this).addClass("highlight-button");
                        $(this).attr('placeholder', 'Falta diligenciar este campo');
                        setTimeout(function () {
                            Modal.Show('error', "¡Atención!", "El número debe tener diez dígitos");
                        }, 1);
                        
                        return false;
                    }
                }

                if (!$(this).val()) {
                    valid = false;
                    $(this).addClass("highlight-button");
                    $(this).attr('placeholder', 'Falta diligenciar este campo');
                    return false;
                }
            }
            

            
        });
        return valid;
    }

    IsValid(form) {

        if (form === "origin" || form === "receiver" || form === "content") {
            if (!Validation.IsFormValid(form + '_form')) {
                Modal.Show('error', "¡Atención!", "Algunos campos están vacios o incompletos. Intente nuevamente");
                return false;
            }
            return true;
        }

        if (form === "cubiq") {
            return true;
        }

        if (form === "resume") {
            return true;    
        }

        return true;
        
    }

 
    static IsPhoneValid(phone) {
        return (phone.length === 10);
    }

    static IsEmailValid(email) {
        var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(String(email).toLowerCase());
    }

    AllowOnlyLetters(event) {
        this.AllowRegExp(event, "^[a-zA-Z ]+$")
    }

    AllowOnlyNumbers(event) {
        this.AllowRegExp(event, "^[0-9]*$")
    }

    AllowRegExp(event , regex) {
        var regex = new RegExp(regex);
        var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
        if (!regex.test(key)) {
            event.preventDefault();
            return false;
        }
    }


}