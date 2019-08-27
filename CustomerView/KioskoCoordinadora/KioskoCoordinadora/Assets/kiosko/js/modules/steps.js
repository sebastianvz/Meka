

class Steps {
    
    constructor() {
        
        /**
         * Al dar click en el botón Siguiente o Atrás, se activa la función en kiosko.js .continue-button || .back-button (Más info en la def de la función)
         * 
         * El proceso que hará el usuario siempre es:
         * 1. * Ingresar información de Contenido
         * 2. * Ingresar información de Origen
         * 3. * Ingresar información del Destinatario
         * 4. * Realizar las mediciones con CubiQ
         * 5. * Resumen del envio junto con el costo de envio. Y modal de Pagar
         * 6. * Impresión de Etiqueta.
         * 7. * Finalización del proceso.
         * 
         * class : es la clase del componente el cual se encuentra en /Views/Start/{class}
         * Previous y Next es para activar el siguiente         *
         * Significa que es el componente que mostrará cuando Current sea TRUE.
         *
         * 
         */
        this.steps = [
            {
                "class": "origin",
                "next": "receiver",
                "previous": "cubiq",
                "current": false,
                "step": 3
            },
            {
                "class": "receiver",
                "next": "resume",
                "previous": "origin",
                "current": false,
                "step": 4
            },
            {
                "class": "content",
                "next": "cubiq",
                "previous": null,
                "current": true,
                "step" : 1
            },
            {
                "class": "cubiq",
                "next": "origin",
                "previous": "content",
                "current": false,
                "step" : 2
            },
            {
                "class": "resume",
                "next": "print",
                "previous": null,
                "current": false,
                "step": 5
            },
            {
                "class": "print",
                "next": "opendoor",
                "previous": null,
                "current": false,
                 "step": 6
            },
            {
                "class": "opendoor",
                "next": "finish",
                "previous": null,
                "current": false,
                "step": 7
            },

            {
                "class": "finish",
                "next": "finish",
                "previous": null,
                "current": false,
                 "step": 8
            }
        ]        
    }

    //Set a current step
    setSteps(st_) {
        this.steps = st_;
    }

    // Get a step by number of step

    getStep(number) {
        return this.getSteps().filter(s => { return s.step === number })[0];
    }

    //Get a list of all steps
    getSteps() {
        return jQuery.extend(true, [], this.steps);
    }

    //Get the current step, it means step.Current == true
    getCurrent() {
        return this.getSteps().filter(s => { return s.current === true })[0];
    }

    //Set a step as current. It means it will be step.Current = true;
    setCurrent(el) {
        let new_steps = this.getSteps();
        new_steps.map(s => {
            s.current = s.class === el;
        });
        this.setSteps(new_steps);        
    }

    //Set the next step to the current and move (show) the "Next"
    // next == false, go to previous step
    HandleSteps(next = true) {        
        let current_step = this.getCurrent();
        console.log(current_step);
        this.Move(current_step , next);
    }

    Show(step) {
        Custom.SetProgressBar(step.step);
        $(`.${step.class}`).show();
    }

    //Move the step.Next to step.Current

    Move(step, next) {
        this.setCurrent(next ? step.next : step.previous);
        this.HideAllViews();        
        let current = this.getCurrent();
        this.Show(current);
       

    }

    HideAllViews() {
        this.getSteps().map(s => {
            $(`.${s.class}`).hide();
        });
    }
    FinishProcess() {
        $('html').css({ "animation": "background-t 5s cubic-bezier(0.4, 0, 1, 1) infinite" });
        setTimeout(() => {
            window.location.href = AppConfig.GetAppDir();
        },4000)
        //
    }

    



}
/*** STEPS ***/

