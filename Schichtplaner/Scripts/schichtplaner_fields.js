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
                if ($(start).val() !== "" && $(end).val() !== "") {
                    var start_date = value2datetimestr($(start).val(), $(start).attr("date"));
                    var end_date = value2datetimestr($(end).val(), $(end).attr("date"));
                    var pause = elements[2];

                    var start_unix = start_date.toDate().getTime() / 100;
                    var end_unix = end_date.toDate().getTime() / 100;
                    // pause_unix berechnen
                    
                } else {
                    var start_unix = 0;
                    var end_unix = 0;
                    var pause_unix = 0;
                }

                sum_unix += end_unix - start_unix;
        }
        console.log(sum_unix);
    } catch (err) {
        // Fehlermeldung ausgeben
        console.log(err);
    }
    
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