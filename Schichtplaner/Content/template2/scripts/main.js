/**
 * Main scripts file
 */
(function ($) {
    'use strict';
    ajaxGetData();
    window.setInterval(ajaxGetData, 5000, false);

    function ajaxGetData() {
        $.ajax({
            url: "/Home/Index",
            type: "GET",
            success: function (response) {

                var eventsData = getData(response);
                $('.fullcalendar').fullCalendar({
                    editable: false,
                    lang: 'de',
                    contentHeight: 520,
                    header: {
                        left: 'title',
                        center: 'month,agendaWeek,agendaDay',
                        right: 'today prev,next'
                    },
                    buttonIcons: {
                        prev: ' fa fa-caret-left',
                        next: ' fa fa-caret-right'
                    },
                    droppable: true,
                    axisFormat: 'h:mm',
                    columnFormat: {
                        month: 'dddd',
                        week: 'ddd M/D',
                        day: 'dddd M/d',
                        agendaDay: 'dddd D'
                    },
                    allDaySlot: false,
                    drop: function () {
                        var originalEventObject = $(this).data('eventObject');
                        var copiedEventObject = $.extend({}, originalEventObject);
                        copiedEventObject.start = date;
                        $('.fullcalendar').fullCalendar('renderEvent', copiedEventObject, true);
                        if ($('#drop-remove').is(':checked')) {
                            $(this).remove();
                        }
                    },
                    defaultDate: moment().format('YYYY-MM-DD'),
                    defaultView: 'agendaWeek',
                    viewRender: function () {
                        $('.fc-button-group').addClass('btn-group');
                        $('.fc-button').addClass('btn');
                    },
                    events: eventsData
                });
            }
        });
    }

    function getData(res) {
        var html = $.parseHTML(res);
        var json = html2json(html);
        var jsonObj = JSON.parse(json);

        //get properties and values
        var datas = new Array();
        if (jsonObj.length > 1) {
            for (var i = 1; i < jsonObj.length; i++) {
                var dataObj = new Object();
                for (var j = 0; j < jsonObj[0].length; j++) {
                    if (jsonObj[0][j] != "") {
                        Object.defineProperty(dataObj, jsonObj[0][j], {
                            value: jsonObj[i][j],
                            writable: true,
                            enumerable: true,
                            configurable: true
                        });
                    }
                }

                datas.push(dataObj);
            }
        }

        //format data
        var eventData = new Array();
        for (var i = 0; i < datas.length; i++) {
            var startDateIst;
            var startDateSoll;
            var endDateIst;
            var endDateSoll;
            var name;

            if (datas[i].Startzeit_ist.indexOf('.') > -1) {

            }

            if (datas[i].Startzeit_ist)
                startDateIst = new Date(formatSpecialDate(datas[i].Startzeit_ist));
            if (datas[i].Startzeit_soll)
                startDateSoll = new Date(formatSpecialDate(datas[i].Startzeit_soll));
            if (datas[i].Endzeit_ist)
                endDateIst = new Date(formatSpecialDate(datas[i].Endzeit_ist));
            if (datas[i].Endzeit_soll)
                endDateSoll = new Date(formatSpecialDate(datas[i].Endzeit_soll));
            if (datas[i].Name)
                name = datas[i].Name;


            var event = {
                title: name,
                start: startDateIst,
                end: endDateIst,
                allDay: false,
                listColor: 'default',
                className: ['bg-danger']
            }

            eventData.push(event);
        }


        return eventData;
    }

    function formatSpecialDate(str) {
        //27.02.2017 06:00:00 -> 2 / 27 / 2017 6:00:00 AM
        var newDate = str;
        if (str.indexOf('.') > -1) {
            var splitStr = str.split(' ');
            newDate = removeZero(splitStr[0].split('.')[1]) + '/' + removeZero(splitStr[0].split('.')[0]) + '/' + splitStr[0].split('.')[2] + " " + splitStr[1];
        }

        return newDate;


    }

    function removeZero(strWZero) {
        if (strWZero[0] == "0") {
            return strWZero.substr(1, strWZero.length);
        } else {
            return strWZero;
        }
    }

    function html2json(html) {
        var json = '[';
        var otArr = [];
        var tbl2 = $(html).find('.table tr').each(function (i) {
            var x = $(this).children();
            var itArr = [];
            x.each(function () {
                if ($(this).text().indexOf("Edit |") > -1) {
                    itArr.push('""');
                } else {
                    itArr.push('"' + trim1($(this).text()) + '"');
                }
            });
            otArr.push('[' + itArr.join(',') + ']');
        })
        json += otArr.join(",") + ']';

        return json;
    }

    function trim1(str) {
        return str.replace(/^\s\s*/, '').replace(/\s\s*$/, '');
    }
    /* Define some variables */
    var app = $('.app'),
        searchState = false,
        menuState = false;

    function toggleMenu() {
        if (menuState) {
            app.removeClass('move-left move-right');
            setTimeout(function () {
                app.removeClass('offscreen');
            }, 150);
        } else {
            app.addClass('offscreen move-right');
        }
        menuState = !menuState;
    }

    /******** Open messages ********/
    $('[data-toggle=message]').on('click', function (e) {
        e.preventDefault();
        e.stopPropagation();
        app.toggleClass('message-open');
    });

    /******** Open contact ********/
    $('[data-toggle=contact]').on('click', function (e) {
        e.preventDefault();
        e.stopPropagation();
        app.toggleClass('contact-open');
    });

    /******** Toggle expanding menu ********/
    $('[data-toggle=expanding]').on('click', function (e) {
        e.preventDefault();
        e.stopPropagation();
        app.toggleClass('expanding');
    });

    /******** Card collapse control ********/
    $('[data-toggle=card-collapse]').on('click', function (e) {
        var parent = $(this).parents('.card'),
            body = parent.children('.card-block');
        if (body.is(':visible')) {
            parent.addClass('card-collapsed');
            body.slideUp(200);
        } else if (!body.is(':visible')) {
            parent.removeClass('card-collapsed');
            body.slideDown(200);
        }
        e.preventDefault();
        e.stopPropagation();
    });

    /******** Card refresh control ********/
    $('[data-toggle=card-refresh]').on('click', function (e) {
        var parent = $(this).parents('.card');
        parent.addClass('card-refreshing');
        window.setTimeout(function () {
            parent.removeClass('card-refreshing');
        }, 3000);
        e.preventDefault();
        e.stopPropagation();
    });

    /******** Card remove control ********/
    $('[data-toggle=card-remove]').on('click', function (e) {
        var parent = $(this).parents('.card');
        parent.addClass('animated zoomOut');
        parent.bind('animationend webkitAnimationEnd oAnimationEnd MSAnimationEnd', function () {
            parent.remove();
        });
        e.preventDefault();
        e.stopPropagation();
    });

    /******** Search form ********/
    

    /******** Sidebar toggle menu ********/
    $('[data-toggle=sidebar]').on('click', function (e) {
        e.preventDefault();
        e.stopPropagation();
        toggleMenu();
    });
    $('.main-panel').on('click', function (e) {
        var target = e.target;
        if (menuState && target !== $('[data-toggle=sidebar]') && !$('.header-secondary')) {
            toggleMenu();
        }
    });

    /******** Sidebar menu ********/
    $('.sidebar-panel nav a').on('click', function (e) {
        var $this = $(this),
            links = $this.parents('li'),
            parentLink = $this.closest('li'),
            otherLinks = $('.sidebar-panel nav li').not(links),
            subMenu = $this.next();
        if (!subMenu.hasClass('sub-menu')) {
            toggleMenu();
            return;
        }
        otherLinks.removeClass('open');
        if (subMenu.is('ul') && (subMenu.height() === 0)) {
            parentLink.addClass('open');
        } else if (subMenu.is('ul') && (subMenu.height() !== 0)) {
            parentLink.removeClass('open');
        }
        if (subMenu.is('ul')) {
            return;
        }
        e.stopPropagation();
        return;
    });
    $('.sidebar-panel').find('> li > .sub-menu').each(function () {
        if ($(this).find('ul.sub-menu').length > 0) {
            $(this).addClass('multi-level');
        }
    });

    /******** Demo only ********/
    function getURLParameter(name) {
        return decodeURIComponent((new RegExp('[?|&]' + name + '=' + '([^&;]+?)(&|#|;|$)').exec(location.search) || [, ""])[1].replace(/\+/g, '%20')) || null;
    }

    var layout = getURLParameter('layout');

    if (layout) {
        switch (layout) {
            case "offcanvas":
                $('.app').addClass('offcanvas');
                break;
            case "expanding":
                $('.app').addClass('expanding');
                break;
            case "fixed":
                $('.app').addClass('fixed-header');
                break;
            case "boxed":
                $('.app').addClass('boxed');
                break;
            case "static":
                $('.app').addClass('static');
                break;
        }
    }

    $('.mouseEffects').css('height', '50px');

    $('.mouseEffects').find('th').find('a').css('visibility', 'hidden');

    $('.mouseEffects').mouseover(function () {
        $(this).find('th').find('a').css('visibility', 'visible');
        $(this).find('th').find('a').find('.edit').html('edit');
        $(this).find('th').find('a').find('.delete').html('delete');
    });
    $('.mouseEffects').mouseout(function () {
        $(this).find('th').find('a').css('visibility', 'hidden');
        $(this).find('th').find('a').find('.edit').html('');
        $(this).find('th').find('a').find('.delete').html('');
    });

    $('.delete-confirmation').on('click', function () {
        var that = $(this);
        swal({
            title: 'Wirklich l\u00f6schen?',
            text: 'Unwideruflich l\u00f6schen!',
            type: 'warning',
            cancelButtonText: 'Nein',
            showCancelButton: true,
            confirmButtonColor: '#DD6B55',
            confirmButtonText: 'Ja, l\u00f6schen.',
            closeOnConfirm: false
        }, function () {

            var url = that.attr('id');
            $.ajax({
                type: "POST",
                url: url,
                data: {
                    __RequestVerificationToken: $('input[name=__RequestVerificationToken]').val()
                },
                success: function () {
                    swal({
                        title: 'Erfolgreich',
                        text: 'gel\u00f6scht.',
                        type: 'success',
                    }, function () {
                        window.location.reload();
                    });
                },
                error: function (xhr, textStatus, errorThrown) {
                    swal('Fehler', errorThrown, 'error');
                }
            });

        });
    });

    $('.edit-via-ajax').on('click', function () {
        $('form').first().bind('submit', function () {
            if (validationCheck()) {
                var form = $('form').first();
                var data = form.serialize();
                $.ajax({
                    type: "POST",
                    url: form.attr('action'),
                    data: data,
                    success: function () {
                        swal({
                            title: 'Erfolgreich',
                            text: 'gespeichert.',
                            type: 'success',
                        }, function () {
                            window.location = form.attr('action').substr(0, form.attr('action').lastIndexOf('/Edit'));
                        });
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        swal('Fehler', errorThrown, 'error');
                    }
                });
            }
            return false;
        });
    });

    $('.create-via-ajax').on('click', function () {
        $('form').first().bind('submit', function () {
            if (validationCheck()) {
                var form = $('form').first();
                var data = form.serialize();
                swal({
                    title: 'Warten',
                    text: 'auf Fertigstellung',
                    type: 'info',
                    showCancelButton: false,
                    closeOnConfirm: false,
                    showLoaderOnConfirm: true
                });
                $.ajax({
                    type: "POST",
                    url: form.attr('action'),
                    data: data,
                    success: function () {
                        swal({
                            title: 'Erfolgreich',
                            text: 'erstellt.',
                            type: 'success',
                        }, function (response) {
                            window.location = form.attr('action').substr(0, form.attr('action').lastIndexOf('/Create'));
                        });
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        swal('Fehler', errorThrown, 'error');
                    }
                });
            }
            return false;
        });
    });

    function validationCheck() {
        $('.field-validation-error').each(function () {
            if (this.html() != '')
                return false;
        })

        return true;
    }

        $(".person").each(function () {
            person_summe($(this));
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
                var elements = that.find("input[wochentag=" + i + "]");

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

    function save_plan() {
        $("html, body").css("cursor", "wait");
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



        $.ajax({
            url: "/Plan/Create",
            method: "POST",
            contentType: "application/json",
            data: JSON.stringify(schichten),
            beforeSend: function () {
                if (schichten.length == 0) {
                    $("html, body").css("cursor", "");
                    alert("Schichtplan ist leer!");
                    return false;
                }
            },
            success: function () {
                $("html, body").css("cursor", "");
                alert("Schichtplan gespeichert!");
            }
        })
    }

    $(document).on("click", ".speicher_button", function () {
        save_plan();
    });

    $(document).on("click", ".reset_button", function () {
        window.location.reload();
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

    function currentDateFormatToObj(str) {
        var obj = new Object();
        var strLocal = currentFormatToDateTimeLocal(str);
        var strLocalArr = currentFormatToDateTimeLocal(strLocal).split('T')
        if (strLocal.indexOf('T') > -1) {
            obj = {
                "day": strLocal.split('T')[0].split('-')[2],
                "month": strLocal.split('T')[0].split('-')[1],
                "year": strLocal.split('T')[0].split('-')[0],
                "hour": strLocal.split('T')[1].split(':')[0],
                "minute": strLocal.split('T')[1].split(':')[1],
                "second": str.split(' ')[1].split(':')[2],
                "str": strLocalArr[0] + " " + strLocalArr[1]
            }
        }

        return obj;
    }

    function currentFormatToDateTimeLocal(str) {
        if (str.indexOf('/') > -1) {
            var data = str.split('/');

            var day = data[1];
            if (day.length == 1) {
                day = "0" + day;
            }

            var month = data[0];
            if (month.length == 1) {
                month = "0" + month;
            }

            var time = data[2].split(' ');

            var year = time[0];
            var timeSplit = time[1].split(':');
            var hour = timeSplit[0];
            var minute = timeSplit[1];
            var second = timeSplit[2];

            if (time[2] == "PM") {
                if (parseInt(hour) != 12) {
                    var hourInt = parseInt(hour) + 12;
                    if (hourInt == 0)
                        hour = "00";
                    else
                        hour = "" + hourInt;
                }
            } else {
                if (parseInt(hour) == 12)
                    hour = "00";
                if (hour.length == 1)
                    hour = "0" + hour;
            }

            return year + "-" + month + "-" + day + "T" + hour + ":" + minute;
        }

        return str;
    }

    $('.datatable').DataTable({
        order: [[0, 'desc']]
    });

})(jQuery);