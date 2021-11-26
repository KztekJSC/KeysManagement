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
        var frm = $("#frmCDKey");

        var quan = frm.find("#Quantity").val();

        if (quan !== '' && quan !== null && quan !== '0') {
          
            var model = {
                Quantity: quan,
                App: frm.find("#AppId").val(),
                ProjectId: frm.find("#ProjectId").val(),
                CustomerId: frm.find("#CustomerId").val()
            }

            JSHelper.AJAX_HttpPost('/Admin/CDKey/Save', model)
                .done(function (data) {
                    if (data.isSuccess) {

                        toastr.success(data.Message);

                        location.href = '/Admin/CDKey/Index';

                    } else {
                        toastr.error(data.Message);
                    }
                });
        } else {
            toastr.error("Vui lòng nhập số lượng!");
        }
    },
   
}