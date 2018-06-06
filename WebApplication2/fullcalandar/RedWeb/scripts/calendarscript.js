var currentUpdateEvent;
var addStartDate;
var addEndDate;
var globalAllDay;
var addnewstartdate;

function updateEvent(event, element) {
    //alert(event.description);

    if ($(this).data("qtip")) $(this).qtip("destroy");

    currentUpdateEvent = event;

    $('#updatedialog').dialog('open');

    $("#eventName").val(event.title);
    $("#eventDesc").val(event.description);
    $("#eventId").val(event.id);
    $("#eventStart").text("" + event.start.toLocaleString());

    if (event.end === null) {
        $("#eventEnd").text("");
    }
    else {
        $("#eventEnd").text("" + event.end.toLocaleString());
    }

}

function updateSuccess(updateResult) {
    //alert(updateResult);
}

function deleteSuccess(deleteResult) {
    //alert(deleteResult);
}

function addSuccess(addResult) {
// if addresult is -1, means event was not added
//    alert("added key: " + addResult);

    if (addResult != -1) {
        $('#calendar').fullCalendar('renderEvent',
						{
						    title: $("#addEventName").val(),
						    start: addnewstartdate, //addStartDate,
						    end: addnewstartdate,//addEndDate,
						    id: addResult,
						    description: $("#addEventDesc").val(),
						    allDay: globalAllDay
						},
						true // make the event "stick"
					);


        $('#calendar').fullCalendar('unselect');
    }

}

function UpdateTimeSuccess(updateResult) {
    //alert(updateResult);
}


function selectDate(start, end, allDay) {

    $('#addDialog').dialog('open');


    $("#addEventStartDate").text("" + start.toLocaleString());
    $("#addEventEndDate").text("" + end.toLocaleString());


    addStartDate = start;
    addEndDate = end;
    globalAllDay = allDay;
    //alert(allDay);

}

function dateDiff(date1, date2) {
//    var diff = {}                           // Initialisation du retour
//    var tmp = date2 - date1;

//    tmp = Math.floor(tmp / 1000);             // Nombre de secondes entre les 2 dates
//    diff.sec = tmp % 60;                    // Extraction du nombre de secondes

//    tmp = Math.floor((tmp - diff.sec) / 60);    // Nombre de minutes (partie entière)
//    diff.min = tmp % 60;                    // Extraction du nombre de minutes

//    tmp = Math.floor((tmp - diff.min) / 60);    // Nombre d'heures (entières)
//    diff.hour = tmp % 24;                   // Extraction du nombre d'heures

//    tmp = Math.floor((tmp - diff.hour) / 24);   // Nombre de jours restants
//    diff.day = tmp;

    //    return diff;

    d1 =(new Date(date1)).getTime() / 86400000;
    d2 = (new Date(date2)).getTime() / 86400000;
    return new Number(d2 - d1).toFixed(0);
}



function AddDays(old_date, delta_days) {
//    var tzOff = new Date(date).getTimezoneOffset() * 60 * 1000;
//    var t = new Date(date).getTime();
//    t += (1000 * 60 * 60 * 24) * amount;
//    var d = new Date();
//    d.setTime(t);
//    var tzOff2 = d.getTimezoneOffset() * 60 * 1000;
//    if (tzOff != tzOff2) {
//        var diff = tzOff2 - tzOff;
//        t += diff;
//        d.setTime(t);
//    }
    //    return d;
    
    // Date plus plus quelques jours
    var split_dateold = old_date.substring(1, 10).split("-",3);
    var split_date = split_dateold;
    // Les mois vont de 0 a 11 donc on enleve 1, cast avec *1
    var new_date = new Date(split_date[2], split_date[1] * 1 - 1, split_date[0] * 1 + delta_days);
    var new_day = new_date.getDate();
    new_day = ((new_day < 10) ? '0' : '') + new_day; // ajoute un zéro devant pour la forme  
    var new_month = new_date.getMonth() + 1;
    new_month = ((new_month < 10) ? '0' : '') + new_month; // ajoute un zéro devant pour la forme  
    var new_year = new_date.getYear();
    new_year = ((new_year < 200) ? 1900 : 0) + new_year; // necessaire car IE et FF retourne pas la meme chose  
    var new_date_text = new_day + '-' + new_month + '-' + new_year;
    return new_date_text;
} 


function updateEventOnDropResize(event, allDay) {

    //alert("allday: " + allDay);
    var eventToUpdate = {
        id: event.id,
        start: event.start

    };

    if (allDay) {
        eventToUpdate.start.setHours(0, 0, 0);

    }

    if (event.end === null) {
        eventToUpdate.end = eventToUpdate.start;

    }
    else {
        eventToUpdate.end = event.end;
        if (allDay) {
            eventToUpdate.end.setHours(0, 0, 0);
        }
    }


    eventToUpdate.start = eventToUpdate.start.format("dd-MM-yyyy hh:mm:ss tt");
    eventToUpdate.end = eventToUpdate.end.format("dd-MM-yyyy hh:mm:ss tt");

    PageMethods.UpdateEventTime(eventToUpdate, UpdateTimeSuccess);
}

function eventDropped(event, dayDelta, minuteDelta, allDay, revertFunc) {

    if ($(this).data("qtip")) $(this).qtip("destroy");

    updateEventOnDropResize(event, allDay);



}

function eventResized(event, dayDelta, minuteDelta, revertFunc) {

    if ($(this).data("qtip")) $(this).qtip("destroy");

    updateEventOnDropResize(event);

}



function change(time) {
    var r = time.match(/^\s*([0-9]+)\s*-\s*([0-9]+)\s*-\s*([0-9]+)(.*)$/);
    var val = r[3] + "-" + r[2] + "-" + r[1];
    return val;

}

function checkForSpecialChars(stringToCheck) {
    var pattern = /[^A-Za-z0-9 ]/;
    return pattern.test(stringToCheck); 
}

$(document).ready(function () {

    // update Dialog
    $('#updatedialog').dialog({
        autoOpen: false,
        width: 470,
        buttons: {
            "update": function () {
                //alert(currentUpdateEvent.title);
                var eventToUpdate = {
                    id: currentUpdateEvent.id,
                    title: $("#eventName").val(),
                    description: $("#eventDesc").val()
                };

                if (checkForSpecialChars(eventToUpdate.title) || checkForSpecialChars(eventToUpdate.description)) {
                    alert("please enter characters: A to Z, a to z, 0 to 9, spaces");
                }
                else {
                    PageMethods.UpdateEvent(eventToUpdate, updateSuccess);
                    $(this).dialog("close");

                    currentUpdateEvent.title = $("#eventName").val();
                    currentUpdateEvent.description = $("#eventDesc").val();
                    $('#calendar').fullCalendar('updateEvent', currentUpdateEvent);
                }

            },
            "delete": function () {

                if (confirm("do you really want to delete this event?")) {

                    PageMethods.deleteEvent($("#eventId").val(), deleteSuccess);
                    $(this).dialog("close");
                    $('#calendar').fullCalendar('removeEvents', $("#eventId").val());
                }

            }

        }
    });

    //add dialog
    $('#addDialog').dialog({
        autoOpen: false,
        width: 470,
        buttons: {
            "Add": function () {

                //alert("sent:" + addStartDate.format("dd-MM-yyyy hh:mm:ss tt") + "==" + addStartDate.toLocaleString());
                var eventToAdd = {
                    title: $("#addEventName").val(),
                    description: $("#addEventDesc").val(),
                    start: addStartDate.format("dd-MM-yyyy hh:mm:ss tt"),
                    end: addEndDate.format("dd-MM-yyyy hh:mm:ss tt")

                };
                var eventToAdd2 = {
                    title: $("#addEventName").val(),
                    description: $("#addEventDesc").val(),
                    start: new Date(""), //addStartDate.format("dd-MM-yyyy hh:mm:ss tt"),
                    end: new Date("") // end: addEndDate.format("dd-MM-yyyy hh:mm:ss tt")

                };

                if (checkForSpecialChars(eventToAdd.title) || checkForSpecialChars(eventToAdd.description)) {
                    alert("please enter characters: A to Z, a to z, 0 to 9, spaces");
                }
                else {
                    //alert("sending " + eventToAdd.title);
                    var diffe;
                    var max;
                    var pas;
                    var dat1 = eventToAdd.start;
                    var formatted = change(dat1);
                    //var dt1=dat1.format('yyyy-MM-dd hh:mm:ss')
                    var dat2 = eventToAdd.end;
                    var formatted2 = change(dat2);
                    diffe = dateDiff(formatted, formatted2);
                    max = parseInt(diffe) + 1;
                    for (pas = 0; pas < max; pas++) {
                        var newdate;
                        newdate = eventToAdd.start;
                        new Date(newdate).setDate((new Date(newdate)).getDate() + pas);
                        //newdate.setDate(newdate.getDate() + pas);
                        

                        eventToAdd2.title = eventToAdd.title;
                        eventToAdd2.description = eventToAdd.description;
                        var newvaldate;
                        newvaldate = AddDays(eventToAdd.start, pas);
                        eventToAdd2.start = new Date(newvaldate).format("dd-MM-yyyy hh:mm:ss tt"); //format("dd-MM-yyyy hh:mm:ss tt");
                        addnewstartdate = eventToAdd2.start;
                        eventToAdd2.end = new Date(newvaldate).format("dd-MM-yyyy hh:mm:ss tt"); //newvaldate.format("dd-MM-yyyy hh:mm:ss tt");
                        addnewstartdate = eventToAdd2.end;
                       // PageMethods.addEvent(eventToAdd2, addSuccess);
                    }
                    PageMethods.addEvent(eventToAdd, addSuccess);

                    $(this).dialog("close");
                }

            }

        }
    });




    var date = new Date();
    var d = date.getDate();
    var m = date.getMonth();
    var y = date.getFullYear();

    var calendar = $('#calendar').fullCalendar({
        theme: true,
        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'month,agendaWeek,agendaDay'
        },
        eventClick: updateEvent,
        selectable: true,
        selectHelper: true,
        select: selectDate,
        editable: true,
        events: "JsonResponse.ashx",
        eventDrop: eventDropped,
        eventResize: eventResized,
        eventRender: function (event, element) {
            //alert(event.title);
            element.qtip({
                content: event.description,
                position: { corner: { tooltip: 'bottomLeft', target: 'topRight'} },
                style: {
                    border: {
                        width: 1,
                        radius: 3,
                        color: '#2779AA'

                    },
                    padding: 10,
                    textAlign: 'center',
                    tip: true, // Give it a speech bubble tip with automatic corner detection
                    name: 'cream' // Style it according to the preset 'cream' style
                }

            });
        }

    });

});

