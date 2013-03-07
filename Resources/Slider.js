$(function () {
    $("##id#_criterion").slider({ min: #min#, max: #max#, step: #step#, values: [#min1#, #max1#], slide:function (event, ui) {
       // alert(ui.values.toString().split(',')[0]);
        $('#ot-#id#').val(ui.values.toString().split(',')[0]);
        $('#do-#id#').val(ui.values.toString().split(',')[1]);
    }, change:function (event, ui) {
       // $('##id#-hiion').val(ui.values.toString());
        pizdaticus2();
    } });
})