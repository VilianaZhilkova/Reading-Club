$(function () {
    var timeOffsetMS = new Date().getTimezoneOffset() * 60000;
    $(".date-time").each(function () {
        var date = new Date($(this).attr("date-time"));
        date.setTime(date.getTime() - timeOffsetMS);

        $(this).html(date.toDateString() + " " + date.toLocaleTimeString());
    });
});