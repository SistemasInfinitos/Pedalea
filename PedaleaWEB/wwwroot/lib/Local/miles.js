var numero_miles = "";

function formatNumber(n) {

    const array_number = n.split(',');
    var complemento = "";
    n = String(array_number[0]).replace(/\D/g, "");
    if (array_number.length > 1) {
        m = String(array_number[1]).replace(/\D/g, "");
        complemento = `,${m}`;
    }
    return n === '' ? n : String(n).replace(/\B(?=(\d{3})+(?!\d))/g, ".") + complemento;
}

function miles(id) {

    numero_miles = formatNumber($(`#${id}`).val());
    $(`#${id}`).val(numero_miles);
}


//var numero_miles = "";

//function formatNumber(n) {
//    n = String(n).replace(/\D/g, "");
//    return n === '' ? n : String(n).replace(/\B(?=(\d{3})+(?!\d))/g, ".");
//}

//function miles(id) {
//    numero_miles = formatNumber($('#' + id + '').val());
//    $('#' + id + '').val(numero_miles);
//}

function addCommas(nStr) {
    nStr += '';
    x = nStr.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + '.' + '$2');
    }
    return x1 + x2;
}

function quitCommas(nStr) {
    nStr.toString();
    var s = nStr.replace(/\./g, "");
    return s;
}