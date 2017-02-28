$(document).ready(function () {
    $(".person").each(function () {
        person_summe($(this));
    });
});

$(document).on("blur", ".schicht_input", function () {

    // Prüfen ob Eingabe gültig
    var date = $(this).attr("date");
    var value = $(this).val();


    // Gegebenenfalls umformen

    // Gesamt Menge ausrechnen
    person_summe($(this));

    

});

function person_summe(thut) {
    var that = thut.parent().parent();
    
    var sum_unix = 0;
    try {
        for (var i = 0; i < 7; i++) {
                var elements = that.find("input[wochentag="+i+"]");

                var start = elements[0];
                var end = elements[1];
                var pause = elements[2];
                if ($(start).val() !== "" && $(end).val() !== "") {
                    var start_date = value2datetimestr($(start).val(), $(start).attr("date"));
                    var end_date = value2datetimestr($(end).val(), $(end).attr("date"));
                    var pause_time = value2minutes($(pause).val());

                    var start_unix = start_date.toDate().getTime() / 100;
                    var end_unix = end_date.toDate().getTime() / 100;
                    var pause_unix = pause_time * 60;
                    
                } else {
                    var start_unix = 0;
                    var end_unix = 0;
                    var pause_unix = 0;
                }

                sum_unix += end_unix - start_unix - pause_unix;             
        }
        var tr = $(that);
        tr.find(".arbwo").text(durationUnix2Time(sum_unix));

        var max = parseInt(tr.find(".mawo").attr("max")) * 36000;

        var mehrarbeitwoche = max - sum_unix;
        tr.find(".mawo").text((durationUnix2Time(mehrarbeitwoche)));
    } catch (err) {
        // Fehlermeldung ausgeben
        console.log(err);
    }
    
}

function durationUnix2Time(unix) {
    
    var unix_in_minutes = Math.abs(unix) / 10 / 60;

    var hours = parseInt(unix_in_minutes / 60);
    var minutes = parseInt((unix_in_minutes - hours * 60) % 60);

    var string = (hours >= 10 ? hours : "0" + hours) + ":" + (minutes >= 10 ? minutes : "0" + minutes);
    if (unix > 0) {
        return string;
    }
    else {
        return '-' + string;
    }
    
}

function value2minutes(value) {
    var time = "";
    if (value.length == 2) {
        time = value;
    }
    else if (value.length == 3) {
        var hours = value.substr(0, 1)
        var minutes = value.substr(1, 2);
        time = parseInt(hours) * 60 + parseInt(minutes);
    }
    return time * 10;
}
function value2datetimestr(value, date) {
    var date = date.substr(0, 10);
    var time = "";
    if (value.length == 3) {
        time = '0' + value.substr(0, 1) + ':' + value.substr(1, 2);
        return moment(date + ', ' + time + ':00', "DD.MM.YYYY HH:mm:ss");
    }
    else if (value.length == 4) {
        time = value.substr(0, 2) + ':' + value.substr(2, 2);
        return moment(date + ', ' + time + ':00', "DD.MM.YYYY HH:mm:ss");
    }
    else return false;
}