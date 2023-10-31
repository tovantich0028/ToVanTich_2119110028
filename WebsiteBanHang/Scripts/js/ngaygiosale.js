
var h;
var m;
var s;
function init() {
   
    h = '15';
    m = '15';
    s = '60';
    clock();
}
function clock() {
    s--;
    if (s == 0) {
        s = 59;
        m--;
        if (m == 0) {
            m = 59;
            h--;
            if (h == 0) {
                h = 24;
                
            }

        }
    }
   
    change('seconds', s);
    change('minutes', m);
    change('hours', h);
    
    setTimeout(clock, 1000);
}
function change(id, val) {
    if (val < 10) {
        val = '0' + val;
    }
    document.getElementById(id).innerHTML = val;
}
window.onload = init();