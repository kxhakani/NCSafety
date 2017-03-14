// Toggle Search Display
function ToggleDisplay(id, btn) {
    var s = document.getElementById(id);
    var b = document.getElementById(btn);
    if (s.style.display == 'none') {
        s.style.display = 'block';
        b.value = 'Close Search';
    }
    else {
        s.style.display = 'none';
        b.value = 'Search';
    }
}

// Create Element from Html
function createElementFromHtml(html) {
    var div = document.createElement('select');
    div.innerHTML = html;
    return div.childNodes;
}

//Add Row
function addRow(id) {

    var table = document.getElementById(id);

    var rowCount = table.rows.length;
    var row = table.insertRow(rowCount);

    var dropDown = createElementFromHtml('<input type="button" value="hi"/>');


    row.insertCell(0).innerHTML = "<select id='HazardID' class='form-control' name='HazardID' style='width:210px;margin-right:10px'><option value=''>Select a hazard...</option><option value='2'>Food spill</option><option value='1'>Guard Missing</option></select><span class='field-validation-valid text-danger' data-valmsg-for='HazardID' data-valmsg-replace='true'></span>";
    row.insertCell(1).innerHTML = '<div class="checkbox" style="text-align:center"><input class="check-box" data-val="true" data-val-required="The Good field is required." id="isGood" name="isGood" value="true" type="checkbox"><input name="isGood" value="false" type="hidden"><span class="field-validation-valid text-danger" data-valmsg-for="isGood" data-valmsg-replace="true"></span></div>';
    row.insertCell(2).innerHTML = '<div class="checkbox" style="text-align:center"><input class="check-box" data-val="true" data-val-required="The Fault field is required." id="isFault" name="isFault" value="true" type="checkbox"><input name="isFault" value="false" type="hidden"><span class="field-validation-valid text-danger" data-valmsg-for="isFault" data-valmsg-replace="true"></span></div>';
    row.insertCell(3).innerHTML = '<input class="form-control text-box single-line" id="itemComment" name="itemComment" style="width:300px" value="" type="text"><span class="field-validation-valid text-danger" data-valmsg-for="itemComment" data-valmsg-replace="true"></span>';        
    row.insertCell(4).innerHTML = '<div class="row"><div class="col-lg-10"><input class="form-control datetimepicker" id="DueDateID" name="DueDateID" placeholder="2017-03-12" value="" style="" type="text"></div></div><span class="field-validation-valid text-danger" data-valmsg-for="itemCorrActionDue" data-valmsg-replace="true"></span>';      
    row.insertCell(5).innerHTML = '<div class="row"><div class="col-lg-10"><input class="form-control datetimepicker" id="CompDateID" name="CompDateID" placeholder="Not completed" value="" style="" type="text"></div></div><span class="field-validation-valid text-danger" data-valmsg-for="itemCorrActionCompleted" data-valmsg-replace="true"></span>';

    row = table.insertRow(rowCount + 1);

}

function hello() {
    alert("hello");
}

//Bootstrap3DatePicker
$(function () {
    $('.datetimepicker').datetimepicker({
        format: 'YYYY/MM/DD',
        viewMode: 'years'
    });
});

// Selectivity Drop Down List
$(function () {
    $(".ddl").each(function () {
        $(this).selectivity({
            allowClear:true
        })
    })
})

// Clear ddl entry
$(function () {
    $(".btnClear").click(function () {
        $(this).closest('td').find('.ddl').selectivity('clear');
        $(this).css("display", "none");
    });
});

//Toggle Clear Ddl Button
$(function () {
    $(".ddl").change(function () {
        var btn = $(this).closest('td').find('.btnClear');
        $(btn).css("display", "inline");
            
    });
});