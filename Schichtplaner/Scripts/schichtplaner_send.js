function save_plan() {
    var schichten = [];
    $(".person").each(function () {
        var personal_id = $(this).attr("person-id");

        var values = $(this).parent().parent().find(".schicht_input");

        for (var i = 0; i < 7; i++) {
            var start = $(values).get(0 + i * 3);
            var end = $(values).get(1 + i * 3);
            var pause = $(values).get(2 + i * 3);

            if ($(start).val() != "" && $(pause).val() != "") {
                var pause_val = 0;
                if ($(pause).val() != "") pause_val = $(pause).val(); 
                var date = $(start).attr("date");
                schichten.push({
                    PersonalId: personal_id,
                    Startzeit_soll: moment(stringtotime($(start).val(), date), "DD.MM.YYYY HH:mm:ss"),
                    Endzeit_soll: moment(stringtotime($(end).val(), date), "DD.MM.YYYY HH:mm:ss"),
                    Startzeit_ist: moment(stringtotime($(start).val(), date), "DD.MM.YYYY HH:mm:ss"),
                    Endzeit_ist: moment(stringtotime($(end).val(), date), "DD.MM.YYYY HH:mm:ss"),
                    Pause: pause_val
                });
            }
        }
        
    });

    console.log(schichten);

    $.ajax({
        url: "/Plan/Create",
        method: "POST",
        contentType: "application/json",
        data: JSON.stringify(schichten),
    })
}

$(document).on("click", ".speicher_button", function () {
    save_plan();
});


function stringtotime(value, date) {
    var hours = 0;
    var minutes = 0;
    date = date.substr(0, 10);
    var datetime = 0;

    if (value.length == 3) {
        hours = value.substr(0, 1);
        minutes = value.substr(1, 2);
        datetime = date + ' ' + '0' + hours + ':' + minutes + ':00';

    }
    else {
        hours = value.substr(0, 2);
        minutes = value.substr(2, 2);
        datetime = date + ' ' + hours + ':' + minutes + ':00';

    }
    return datetime;

}