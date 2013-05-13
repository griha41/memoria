var Util = {
    modal: function (url, e) {
        var page = this.post(url, null, function (html) {
            $("body").append(html);
        });
    },
    absoluteUrl: function (url) {
        return (baseUrl + url).replace(/([a-zA-Z])\/\//, "$1/");
    },
    url: function (url, params) {
        if (params != null) {
            url += (url.indexOf("?") > -1 ? "&" : "?") + $.param(params);
        }
        if (url.indexOf("http") > -1)
            return url;
        else
            return Util.absoluteUrl(url);
    },
    redirect: function (url, params) {
        location.href = Util.url(url, params);
    },
    error: function (title, message) {
        $('#modalMessage #modalMessageTitle').text(title);
        $('#modalMessage #modalMessageText').text(message);
        $('#modalMessage #modalMessageCancel').hide();
        $('#modalMessage #modalMessageAccept').off('click');
        $('#modalMessage #modalMessageAccept').click(function () { $('#modalMessage').hide(); });
        $('#modalMessage').show();
    },
    confirm: function (title, message, onOk, onCancel) {
        $('#modalMessage #modalMessageTitle').text(title);
        $('#modalMessage #modalMessageText').text(message);
        $('#modalMessage #modalMessageCancel').show();
        if (onOk == null) { onOk = function () { $('#modalMessage').hide(); } }
        if (onCancel == null) { onCancel = function () { $('#modalMessage').hide(); } }
        $('#modalMessage #modalMessageAccept').off('click');
        $('#modalMessage #modalMessageAccept').click(onOk);
        $('#modalMessage #modalMessageCancel').off('click');
        $('#modalMessage #modalMessageCancel').click(onCancel);
        $('#modalMessage').show();
    },
    post: function (url, parameters, onResponse, onError, onComplete) {
        $.ajax({
            type: 'POST',
            url: this.absoluteUrl(url),
            data: parameters,
            success: onResponse,
            error: function (html) {
                var error = eval("(" + html.responseText + ")");
                if (typeof (onError) == "function") { onError(error); }
                else { alert(error.Title, error.Message); }
            },
            complete: onComplete
        });
    },
    appendAction: function (selector, url, parameters) {
        this.post(url, parameters, function (html) {
            $(selector).append(html);

        });
    },
    replaceAction: function (selector, url, parameters) {
        this.post(url, parameters, function (html) {
            $(selector).html(html);
        });
    },
    addPoints: function (str) {
        //Borro todos los puntos.
        while (str.indexOf(".") > -1) str = str.replace(".", "");
        if (str.length < 4) return str;
        var cursor = 0;
        var strTmp = "";
        while (str.length - cursor > 3) {
            cursor = cursor + 3;
            strTmp = "." + str.substr(str.length - cursor, 3) + strTmp;
        }
        if (str.length - cursor > 0) strTmp = str.substr(str.length - cursor - (str.length - cursor), str.length - cursor) + strTmp;
        return strTmp;
    }
    , rutFormat: function (rut) {
        //[Puntos|SinPuntos],[Guion|SinGuion],[DigitoVerificador|SinDigitoVerificador]
        //PuntosGuion|PuntosSinGuion|SinPuntosGuion|SinPuntosSinGuion
        SinPuntosSinGuion = new RegExp("^[0-9]{1,8}[0-9kK]$");
        SinPuntosGuion = new RegExp("^[0-9]{1,8}-[0-9kK]$");
        PuntosGuion = new RegExp("^[0-9]{1,3}(\\.[0-9]{3}){1,2}-[0-9kK]$");
        PuntosSinGuion = new RegExp("^[0-9]{1,3}(\\.[0-9]{3}){1,2}[0-9kK]$");

        SinPuntosSinGuionSinDigitoVerificador = new RegExp("^[0-9]{1,8}$");
        PuntosSinGuionSinDigitoVerificador = new RegExp("^[0-9]{1,3}(\\.[0-9]{3}){1,2}$");

        if (SinPuntosSinGuion.test(rut))
            return Util.addPoints(rut.substr(0, rut.length - 1)) + "-" + rut.substr(rut.length - 1); // "SinPuntosSinGuion";
        if (SinPuntosGuion.test(rut))
            return Util.addPoints(rut.substr(0, rut.length - 2)) + "-" + rut.substr(rut.length - 1);
        if (PuntosGuion.test(rut))
            return rut;
        if (PuntosSinGuion.test(rut)) {
            return Util.addPoints(rut.substr(0, rut.length - 1)) + "-" + rut.substr(rut.length - 1);
        }
        if (SinPuntosSinGuionSinDigitoVerificador.test(rut)) {
            return Util.addPoints(rut) + "-" + this.rutDigit(rut);
        }
        if (PuntosSinGuionSinDigitoVerificador.test(rut)) {
            return Util.addPoints(rut) + "-" + this.rutDigit(rut);
        }


        return rut;
    }
    , rutDigit: function (T) {
        //En su defecto retorna k, aunque no tenga el formato adecuado
        //http://www.dcc.uchile.cl/~mortega/microcodigos/validarrut/javascript.html
        var M = 0, S = 1; for (; T; T = Math.floor(T / 10))
            S = (S + T % 10 * (9 - M++ % 6)) % 11; return S ? S - 1 : 'k';
    }
    , isRut: function (valor) {
        if (valor.length < 1) return false;
        rutx = new RegExp("^[0-9]{1,3}(\\.?[0-9]{3}){0,2}(-[0-9kK])?$");
        if (!rutx.test(valor)) return false;
        while (valor.indexOf(".") > -1) { valor = valor.replace(".", ""); }

        if (valor == null || valor.length == 0) return false;
        var IgStringVerificador, IgN, IgSuma, IgDigito, IgDigitoVerificador, rut;
        rut = valor;
        for (i = 10 - rut.length; i > 0; i--) rut = '0' + rut;
        IgStringVerificador = '32765432';
        IgSuma = 0;
        for (IgN = 0; IgN < 8 && IgN < rut.length; IgN++)
            IgSuma = eval(IgSuma + '+' + rut.substr(IgN, 1) + '*' + IgStringVerificador.substr(IgN, 1) + ';');
        IgDigito = 11 - IgSuma % 11;
        if (IgDigito == 10) {
            IgDigitoVerificador = 'K';
        }
        else {
            if (IgDigito == 11) {
                IgDigitoVerificador = '0';
            }
            else {
                IgDigitoVerificador = IgDigito;
            }
        }
        if (rut.substr(rut.length - 1).toUpperCase() == IgDigitoVerificador) { return true; }
        else { return false; }
    }
    , getDialog: function (code) {
        if (stringJson[code] == null)
            return code;
        else
            return stringJson[code];
    },
    isEmail: function (valor) {
        var resultado = false;
        var Valida = new RegExp("(^[0-9a-zA-Z]+(?:[._][0-9a-zA-Z]+)*)@([0-9a-zA-Z]+(?:[._-][0-9a-zA-Z]+)*\.[0-9a-zA-Z]{2,3})$");
        resultado = Valida.test(valor);
        return resultado;
    },
    isPhone: function (value) {
        var valid = new RegExp("^0?[2-9]?[-\ ]?[0-9]{5,6}$");
        return !valid.test(value);
    },
    isCellPhone: function (value) {
        var valid = new RegExp("^0?[5-9]{1,2}[-\ ]?[0-9]{7}$");
        return !valid.test(value);
    },
    isCC: function (value) {
        if (value == null && value.length < 10) return false;
        else {
            return Util.isNumeric(value);
        }
    },
    isCreditCard: function (type, number) {
        if (number == null || number.length == 0) return false;
        // quito los espacios
        number = number.replace(/\s/g, "");
        //checkeo si es un número entre 13 y 19 caracteres
        var cardexp = /^[0-9]{13,19}$/;
        if (!cardexp.exec(number)) { return false; }
        // Reviso el largo
        if (Number(cards[type].length) != number.length) { return false; }
        // Reviso los prefijos
        var prefixes = cards[type].prefixes.split(',');
        for (var e in prefixes) {
            if (number.startsWith(prefixes[e])) { break; }
            if (e == (prefixes.length - 1)) { return false; }
        }
        //reviso si necesita código de validación
        if (cards[type].checkdigit) {
            var checksum = 0;
            var mychar = "";
            var j = 1;
            var calc;
            var cardNumber = number;
            for (i = cardNumber.length - 1; i >= 0; i--) {

                // Extract the next digit and multiply by 1 or 2 on alternative digits.
                calc = Number(cardNumber.charAt(i)) * j;

                // If the result is in two digits add 1 to the checksum total
                if (calc > 9) {
                    checksum = checksum + 1;
                    calc = calc - 10;
                }

                // Add the units element to the checksum total
                checksum = checksum + calc;

                // Switch the value of j
                if (j == 1) { j = 2 } else { j = 1 };
            }

            // All done - if checksum is divisible by 10, it is a valid modulus 10.
            // If not, report an error.
            if (checksum % 10 != 0) {
                return false;
            }
        }
        return true;
    },
    isNull: function (value) {
        if (value == null || value == undefined) return true;
        if (value.length == 0) return true;
        return false;
    },
    isDate: function (value) {
        var str, mes, dia, anyo, febrero, fecha;
        fecha = value;
        expr = /^[0-9]{1,2}\/[0-9]{1,2}\/[0-9]{4}$/;
        str = value;

        if ((m = str.match(expr)) == null) {
            return false;
        }
        else {
            dia = fecha.split("/")[0];
            mes = fecha.split("/")[1];
            anyo = fecha.split("/")[2];

            if (anyoBisiesto(anyo)) { febrero = 29; } else { febrero = 28; }
            if ((mes < 1) || (mes > 12)) {
                esFecha1 = false;
            }
            else {
                if ((mes == 2) && ((dia < 1) || (dia > febrero))) {
                    esFecha1 = false;
                }
                else {
                    if (((mes == 1) || (mes == 3) || (mes == 5) || (mes == 7) || (mes == 8) || (mes == 10) || (mes == 12)) && ((dia < 1) || (dia > 31))) {
                        esFecha1 = false;
                    }
                    else {
                        if (((mes == 4) || (mes == 6) || (mes == 9) || (mes == 11)) && ((dia < 1) || (dia > 30))) {
                            esFecha1 = false;
                        }
                        else {
                            esFecha1 = true;
                        }
                    }
                }
            }
        }
        return (esFecha1);
    },
    isNumeric: function (value) {
        var valid = new RegExp("^[0-9]*((\,)([0-9]){3,3})*[0-9]*((\.)([0-9])*)?$");
        return valid.test(value);
    },
    keydown: {
        numeric: function (e) {
            var keyPressed = (e.which) ? e.which : event.keyCode
            return !(keyPressed > 31 && (keyPressed < 48 || keyPressed > 57));
        }
    },
    getParams: function (container) {
        var inputs = $(container + ' input').not($(container + ' section[id] input ')).toArray();
        var selects = $(container + ' select').not($(container + ' section[id] select ')).toArray();
        var params = {};
        for (var e in inputs) {
            var input = $(inputs[e]);
            params[input.attr('name')] = input.val();
        }
        for (var e in selects) {
            var select = $(selects[e]);
            params[select.attr('name')] = select.val();
        }
        return params;
    },
    sendForm: function (form, onResponse, onError) {
        var params = Util.getParams(form);
        var url = $(form).attr('action');
        Util.post(url, params, onResponse, onError);
    },
    showError: function (selector, errorCode) {
        alert(errorCode);
    },
    clear: function (selector) {
        var inputs = $(selector + ' input').not($(selector + ' section[id] input ')).toArray();
        var selects = $(selector + ' select').not($(selector + ' section[id] select ')).toArray();
        for (var e in inputs)
        { $(inputs[e]).val(''); }
        for (var e in selects)
        { $(selects[e]).val($('option', selects[e]).first().val()) }
    }
};

cards = {
    "1": { name: "Visa",
        length: "13,16",
        prefixes: "4",
        checkdigit: true
    },
    "2": { name: "MasterCard",
        length: "16",
        prefixes: "51,52,53,54,55",
        checkdigit: true
    },
    "3": { name: "Diners",
        length: "14,16",
        prefixes: "305,36,38,54,55",
        checkdigit: true
    },
    "5": { name: "AmericanExpress",
        length: "15",
        prefixes: "34,37",
        checkdigit: true
    }
};

//window.alert = Util.error;
//window.confirm = Util.confirm;

if (typeof String.prototype.startsWith != 'function') {
  // see below for better implementation!
  String.prototype.startsWith = function (str){
    return this.indexOf(str) == 0;
  };
}