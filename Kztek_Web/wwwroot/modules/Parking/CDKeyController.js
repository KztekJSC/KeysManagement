$(function () {
    //mở modal group
    $("body").on("click", ".btnCreateKey", function () {
        CDKeyController.ModalKey();
    })
    //đóng model
    $("body").on("click", "#ModalCDKey #btnClose", function () {
        $("#ModalCDKey").modal("hide");
    })

    //cập nhật
    $("body").on("click", "#ModalCDKey #btnCompleted", function () {
        CDKeyController.Save();
    })


    $("body").on("change", "#AppId", function () {
        $("#appId").val($(this).val())
    })

    $("body").on("change", "#CustomerId", function () {
        $("#cId").val($(this).val())
    })

    $("body").on("change", "#ProjectId", function () {
        $("#pId").val($(this).val())
    })
})
var CDKeyController = {
    ModalKey: function () {
        var model = {
            idboxrender: "boxModal",
            url: '/Admin/CDKey/Modal_CreateKey',
            idmodal: "ModalCDKey"
        }
        JSHelper.Modal_Open(model);
    },

    Save: function () {

        $("#boxLoding").css("display", "");

        $("#btnCompleted").prop("disabled", true);

        $("#btnClose").prop("disabled", true);

        var frm = $("#frmCDKey");

        var quan = frm.find("#Quantity").val();

        var model = {
            Quantity: quan,
            App: frm.find("#AppId").val(),
            ProjectId: frm.find("#ProjectId").val(),
            CustomerId: frm.find("#CustomerId").val()
        }

        JSHelper.AJAX_HttpPost('/Admin/CDKey/Save', model)
            .done(function (data) {
                if (data.isSuccess) {

                    toastr.success(data.message);

                    location.href = '/Admin/CDKey/Index';

                } else {
                    toastr.error(data.message);
                }

                $("#boxLoding").css("display", "none");

                $("#btnCompleted").prop("disabled", false);

                $("#btnClose").prop("disabled", false);
            });
    },
   
}