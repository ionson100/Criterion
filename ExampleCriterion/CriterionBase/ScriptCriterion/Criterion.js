jQuery.extend({
    getUrlVars: function () {
        var vars = [], hash;
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }
        return vars;
    },
    getUrlVar: function (name) {
        return jQuery.getUrlVars()[name];
    }
});

var openpageion = '';
$(function () {
    $("#criterionformmodal").dialog({ autoOpen: false, modal: true, title: "Справка:", height: 400, width: 500 });
     if ($.getUrlVars()['open']) { openpageion = $.getUrlVars()['open'][0]; }
});
function updateURLParameterS(url, param, paramVal) {
    var newAdditionalUrl = "";
    var tempArray = url.split("?");
    var baseUrl = tempArray[0];
    var additionalUrl = tempArray[1];
    var temp = "";
    if (additionalUrl) {
        tempArray = additionalUrl.split("&");
        for (var i = 0; i < tempArray.length; i++) {
            if (tempArray[i].split('=')[0] != param) {
                newAdditionalUrl += temp + tempArray[i];
                temp = "&";
            }
        }
    }
    var rowsTxt = temp + "" + param + "=" + paramVal;
    return baseUrl + "?" + newAdditionalUrl + rowsTxt;
}

function onchangeDropDownListBettwen(aa) {
    var ses = aa.value;
    var io = aa.id.substr(aa.id.indexOf('-')+1);
    var textbox1 = $("#ot-"+io);
    var textbox2 = $("#do-" + io);
   
    if (ses == 2) {
        $(textbox1).hide();
        $(textbox2).css("width", "60px");
    }
    if(ses==1) {
        $(textbox1).show();
        $(textbox2).css("width", "");
    }
    $(textbox1).val('');
    $(textbox2).val('');
}

function clearAll() {
    $('#Criterion select option:selected').each(function () {
        this.selected = false;
    });
    $('#Criterion :input').each(function () {
        if ($(this).attr('data-ssd') != 1 && $(this).attr('id') != 'dictionarycriterion')
        $(this).val('');
        this.checked = false;
       

    });
  
}

function pizdaticusButton() {

    var where = "";
    var urlbefore = updateURLParameterS(window.location.toString(), "where", where);
    var d = $('#dictionarycriterion').val();
    if (d == '') return;
    var tt = d.toString().split(';');
    for (var i = 0; i < tt.length; i++) {
        var id = tt[i].toString().split('-')[0];
        var areal = tt[i].toString().split('-')[1];
        if (areal == 'listboxmultiple') {
            var str = "";
            $('#' + areal + '-' + id + ' select option:selected').each(function (ii) {
                if (this.value != '')
                    str = str + ((ii == 0) ? '' : ',') + this.value;
            });
            if (str != '')
                where = where + id + ':' + str + ';';

        }
        if (areal == 'checkboxlist') {
            var str1 = "";

            $('#' + areal + '-' + id + ' input:checkbox:checked').each(function (ii) {

                if ($(this).val() != '')
                    str1 = str1 + ((ii == 0) ? '' : ',') + $(this).val();
            });
            if (str1 != '')
                where = where + id + ':' + str1 + ';';
        }
        if (areal == 'boolcheckbox') {

            if ($('#' + id).prop('checked'))
                where = where + id + ':true;';
        }
        if (areal == 'booldropdown') {

            if ($('#' + id).val() != '')
                where = where + id + ':' + $('#' + id).val() + ';';
        }
        if (areal == 'dropdown' || areal == 'listbox') {

            if ($('#' + id).val() != '')
                where = where + id + ':' + $('#' + id).val() + ';';
        }
        if (areal == 'radiobuttonlist') {
            if ($(' #radiobuttonlist-' + id + ' input:radio:checked').size() > 0) {
                var listvlue = $(' #radiobuttonlist-' + id + ' input:radio:checked').prop('value');
                where = where + id + ':' + listvlue + ';';
            }
        }
        if (areal == 'slider') {

            var ot = $('#ot-' + id).val();
            var dod = $('#do-' + id).val();
            if (ot != '' && dod != '') {
                where = where + id + ':' + ot + ',' + dod + ';';
            }
        }
        if (areal == 'betweendate' || areal == 'between') {
            var ot1 = $('#ot-' + id).val();
            var dod1 = $('#do-' + id).val();
            var ee = $("#manager-" + id).val();

            if (ee == '1' && ot1 != '' && dod1 != '') {
                where = where + id + ':1,' + ot1 + ',' + dod1 + ';';
            }
            if (ee == '2' && dod1 != '') {
                where = where + id + ':2,' + dod1 + ';';
            }
        }
        if (areal == "custom") {
            var value = "";
            $('[data-custom]').each(function() {
                if($(this).attr('data-custom')==id) {
                    value = $(this).val();
                }
            });
            if(value!='')
            where = where+id + ':' + value;
        }


    }
  //  typeguid
    urlbefore = updateURLParameterS(urlbefore, "where", where);
    urlbefore = updateURLParameterS(urlbefore, "open", openpageion);
    window.location = urlbefore.toString();
}



jQuery(document).ready(function () {
     openpageion = $('#open').val();
    jQuery('.spoiler-head').click(function () {
        jQuery(this).toggleClass("open").toggleClass("closed").next().toggle();
        if ($(this).next().is(':hidden') == false) {
            openpageion = openpageion + jQuery(this).attr('data-name');
        } else {
            openpageion = openpageion.replace(jQuery(this).attr('data-name').toString(), "");
        }
        $('#open').val(openpageion);
        
    });

});


function imageHelp(id) {
    $('#criterionformmodal').load('/Criterion/CriterionHome/Index/' + id + '?type=' + $('#typeguid').val(), null, function () {
        $("#criterionformmodal").dialog("open");
    });
}

$(function() {
    $('[data-core]').each(function() {
        $(this).on('change', function() {
            pizdaticus2();
        });
    });

    $('#button-manager').on('click', function() {
        pizdaticusButton();
        return false;
    });
    $('#link-manager').on('click', function() {
        clearAll();
    });
    

    $("[id |= 'datapicker'] input").datepicker({ dateFormat: "yy-mm-dd" });
    $("[id |= 'datapicker'] select").on("change", function () {
        onchangeDropDownListBettwen(this, 'datapicker');
    });
    $("[id |= 'simplebetween'] select").on("change", function () {
        onchangeDropDownListBettwen(this, 'simplebetween');
    });
});
function pizdaticus2() {
    //pizdaticusButton();
}