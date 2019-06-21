$(document).ready(function () {
    ReRealTime();
    //ReRealDate();
    setInterval(ReRealTime, 1000);
});
function ReRealTime() {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "GetRealTime",
        success: procRealTime
    });
}
function procRealTime(data) {
    var show = document.getElementById("showtime");
    var currentdate = data;
    show.innerHTML = currentdate;

    //ReRealDate(data)
}

function ReRealDate(data) {
    var show_date = document.getElementById("showdays");
    setInterval(function () {
        var date = new Date(data);
        var seperator1 = "-";
        var seperator2 = ":";
        var month = date.getMonth() + 1;
        var strDate = date.getDate();
        var hours = date.getHours();
        var min = date.getMinutes();
        var second = date.getSeconds();
        if (month >= 1 && month <= 9) {
            month = "0" + month;
        }
        if (strDate >= 0 && strDate <= 9) {
            strDate = "0" + strDate;
        }
        if (hours >= 0 && hours <= 9) {
            hours = "0" + hours;
        }
        if (min >= 0 && min <= 9) {
            min = "0" + min;
        }
        if (second >= 0 && second <= 9) {
            second = "0" + second;
        }
        var currentdate = date.getFullYear() + seperator1 + month + seperator1 + strDate;
        $('#showdays').empty();
        show_date.innerHTML = currentdate;
    }, 500);
}
