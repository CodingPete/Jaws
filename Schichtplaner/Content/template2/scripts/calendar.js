/**
 * Calendar app page
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

            if (datas[i].Startzeit_ist)
                startDateIst = new Date(datas[i].Startzeit_ist);
            if (datas[i].Startzeit_soll)
                startDateSoll = new Date(datas[i].Startzeit_soll);
            if (datas[i].Endzeit_ist)
                endDateIst = new Date(datas[i].Endzeit_ist);
            if (datas[i].Endzeit_soll)
                endDateSoll = new Date(datas[i].Endzeit_soll);
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
                    itArr.push('"' + $(this).text().substr(13, $(this).text().length - 22) + '"');
                }
            });
            otArr.push('[' + itArr.join(',') + ']');
        })
        json += otArr.join(",") + ']'

        return json;
    }   
})(jQuery);
