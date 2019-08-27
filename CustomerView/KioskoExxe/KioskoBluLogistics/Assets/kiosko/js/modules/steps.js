

class Steps {
    steps = [];
    constructor() {
        this.steps = [
            {
                "class": "origin",
                "next": "receiver",
                "previous": null,
                "current" : true
            },
            {
                "class": "receiver",
                "next": "content",
                "previous": "origin",
                "current": false
            },
            {
                "class": "content",
                "next": "cubiq",
                "previous": "receiver",
                "current": false
            },
            {
                "class": "cubiq",
                "next": "resume",
                "previous": "content",
                "current": false
            },

            {
                "class": "resume",
                "next": "print",
                "previous": null,
                "current": false
            },
             {
                 "class": "print",
                 "next": "finish",
                 "previous": null,
                 "current": false,
                 "step": 6
            },  
            {
                "class": "finish",
                "next": "",
                "previous": "print",
                "current": false,
                "step": 7
            },  

            {
                "class": "end",
                "next": null,
                "previous": null,
                "current": false
            }
        ]        
    }

    setSteps(st_) {
        this.steps = st_;
    }

    getSteps() {
        return jQuery.extend(true, [], this.steps);
    }

    getCurrent() {
        return this.getSteps().filter(s => { return s.current === true })[0];
    }

    setCurrent(el) {
        let new_steps = this.getSteps();
        new_steps.map(s => {
            s.current = s.class === el;
        });
        this.setSteps(new_steps);        
    }
    
    HandleSteps() {        
        let current_step = this.getCurrent();
        this.Move(current_step);
    }

    Move(step, next = true) {
        this.setCurrent(next ? step.next : step.previous);
        this.getSteps().map(s => {
            $(`.${s.class}`).hide();
        });
        
        $(`.${this.getCurrent().class}`).show();

    }

    static FinishProcess() {
        $('.origin').hide();
        $('.receiver').hide();
        $('.cubiq').hide();
        $('.content').hide();

        $('.end').show();
    }



}
/*** STEPS ***/

