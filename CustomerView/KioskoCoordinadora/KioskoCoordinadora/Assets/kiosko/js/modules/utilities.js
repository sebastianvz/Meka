
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

    static resetSelector(select, options = null) {
        $(select).empty();
        Utilities.addOptionsToSelect(select, options, false);
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

    static async SetCounter(fn , timer, timeout) {
        timer.innerHTML = timeout;
        var i = 0;
        for (i = timeout; i > 0; i--) {
            timer.innerHTML = i;
            await new Promise(resolve => setTimeout(resolve, 1000));
        }

        fn();

    }

    /*** add dot on input**/
    static reverseNumber(input) {
        return [].map.call(input, function (x) {
            return x;
        }).reverse().join('');
    }

    static plainNumber(number) {
        return number.split('.').join('');
    }

    static splitInDots(input) {
        if (input.value == "")
            return;
        var value = input.value,
            plain = Utilities.plainNumber(value),
            reversed = Utilities.reverseNumber(plain),
            reversedWithDots = reversed.match(/.{1,3}/g).join('.'),
            normal = Utilities.reverseNumber(reversedWithDots);

        input.value = normal;
    }

    static resetForms() {
        $('form').trigger("reset");
    }



}