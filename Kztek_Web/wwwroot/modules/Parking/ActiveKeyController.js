$(function () {

    $("#txtUserCode").select().focus();

    ActiveKeyController.LoadCDKey();

    $("body").on("change", "#ddlApp", function () {
        var str = '';
        var cmd = $(this);
        cmd.parent().find('ul.multiselect-container li.active').each(function () {
            var _cmd = $(this);
            str += _cmd.find('input[type=checkbox]').val() + ',';
        });
        $('#strapp').val(str);

        ActiveKeyController.LoadCDKey();
    });

    $("body").on("click", "#chkAllHeader", function () {
        var checkall = $(this).is(":checked");
        var str = "";
        $('#lpCDKey tr').each(function () {
            var cmd = $(this).find(".chkItem");
            if (checkall) {
                cmd.prop('checked', 'checked');
                if (str === "") {
                    str += cmd.val();
                } else {
                    str += ',' + cmd.val();
                }

            } else {
                cmd.prop('checked', '');
            }
        });

        if (!checkall) {
            str = "";
        }

        $("#strCode").val(str);

    });

    $("body").on("click", ".chkItem", function () {
        var cmd = $(this);
        var code = cmd.val();
        var str = $("#strCode").val();

        if (cmd.is(":checked")) {
            cmd.prop('checked', 'checked');
            if (str === "") {
                str += code;
            } else {
                str += ',' + code;
            }

            $("#strCode").val(str);
        } else {
            cmd.prop('checked', '');
            ActiveKeyController.RemoveCardChoose(str, code);
        }
    });

    $("body").on("click", ".btnActive", function () {    
        ActiveKeyController.Save();
    });
})

var ActiveKeyController = {
    LoadCDKey: function () {
        var model = {
            app: $("#strapp").val(),         
            codes: $("#strCode").val()
        }

        JSHelper.AJAX_LoadDataPOST('/Admin/ActiveKey/Partial_CDKey', model)
            .success(function (data) {
                $('#divCDKey').html('');
                $('#divCDKey').html(data);             
            });
    },  
    LoadActiveKey: function () {
        var model = {
            codes: $("#strCode").val()
        }

        JSHelper.AJAX_LoadDataPOST('/Admin/ActiveKey/Partial_CDKey', model)
            .success(function (data) {
                $('#divCDKey').html('');
                $('#divCDKey').html(data);
            });
    },
    RemoveChoose: function (str, code) {
        var model = {
            strCode: str,
            code: code
        }
        JSHelper.AJAX_LoadDataPOST('/Admin/ActiveKey/RemoveChoose', model)
            .success(function (data) {
                $("#strCode").val(data);
            });
    },  
    Save: function () {
        var model = {
            CDKey: $("#strCode").val(),
            CustomerId: $("#CustomerId").val(),
            ProductId: $("#ProductId").val(),
            UserCode: $("#txtUserCode").val()
        }

        JSHelper.AJAX_LoadDataPOST('/Admin/ActiveKey/Save', model)
            .success(function (data) {
                if (data.isSuccess) {
                    toastr.success(data.message);

                    ActiveKeyController.LoadActiveKey();

                    $("#strCode").val("");

                    ActiveKeyController.LoadCDKey();
                } else {
                    toastr.error(data.message);
                }
            });
    }
}