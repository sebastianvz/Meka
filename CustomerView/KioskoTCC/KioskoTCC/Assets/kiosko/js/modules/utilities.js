class Utilities {

    

    static DoRequest(params) {

        if (params.async === null)
            params.async = true;

        if (params.type === null)
            params.type = "GET";
        
        return $.ajax({
            url: params.url,
            type: params.type,
            async: params.async,
            data: params.data,
            contentType: "application/json"
        });
        
    }
    static TimeOut(element, timeout, fnDone, done = false) {
        
        if (done) {
            fnDone();
            return;
        }
        setTimeout(() => {
            element.html(timeout / 1000);
            this.TimeOut(element, timeout - 1000, fnDone, timeout === 0);
        }, 1000);
        
    }

    static addOptionsToSelect(select, data , reset = true) {
        if (!data || data == null)
            return;
        
        if (reset) {
            Utilities.resetSelector(select);
        }
        
        data.map(function (data) {
            $(select).append($('<option>', {
                value: data.value,
                text: data.text
            }));
        })
    }

    static async SetCounter(fn, timer, timeout) {
        timer.innerHTML = timeout;
        var i = 0;
        for (i = timeout; i > 0; i--) {
            timer.innerHTML = i;
            await new Promise(resolve => setTimeout(resolve, 1000));
        }

        fn();

    }

    static resetSelector(select, options = null) {
        $(select).empty();
        Utilities.addOptionsToSelect(select, options, false);
    }


    static animateCSS(element, animationName, callback) {
        const node = document.querySelector(element)
        node.classList.add('animated', animationName)

        function handleAnimationEnd() {
            node.classList.remove('animated', animationName)
            node.removeEventListener('animationend', handleAnimationEnd)

            if (typeof callback === 'function') callback()
        }

        node.addEventListener('animationend', handleAnimationEnd)
    }


}