// CREDITS: 
// Automatic Page Refresher by Peter Gehrig and Urs Dudli www.24fun.com 
// Permission given to use the script provided that this notice remains as is. 
// Additional scripts can be found at http://www.hypergurl.com 
// Configure refresh interval (in seconds) 
// Shall the coundown be displayed inside your status bar? Say "yes" or "no" below: 
// Do not edit the code below 
var starttime ;
var nowtime ;
var reloadseconds=0 ;
var secondssinceloaded = 0;
var displaycountdown = "no";
var sessionStatus;
var refreshinterval = 10;
function starttime() 
{
    starttime = new Date();
    starttime = starttime.getTime();
    countdown();
} 

function countdown() 
{
    nowtime = new Date();
    nowtime = nowtime.getTime();
    secondssinceloaded = (nowtime - starttime) / 1000;
    reloadseconds = Math.round(refreshinterval - secondssinceloaded);
    var rand_no = Math.random();
    if (secondssinceloaded < refreshinterval) {
        var timer = setTimeout("countdown()", 1000);
        if (displaycountdown == "yes") {
            window.status = "Page refreshing in " + reloadseconds + " seconds";
        }
    }
    else 
    {
        starttime = new Date();
        starttime = starttime.getTime();
        clearTimeout(timer);
        //window.location.reload(true);
        window.location.href = '/Vehicles/Index?' + rand_no;
    }


}

window.onload = starttime;
