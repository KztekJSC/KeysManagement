

$(() => {
    let connection = new signalR.HubConnectionBuilder().withUrl("/signalServer").build()

    connection.start()

    connection.on("refreshEmployees", function () {
        SignalrController.GetId();
    })

    SignalrController.GetId();

   
})

var SignalrController = {
    GetId: function() {
        $.ajax({
            url: '/Admin/Home/GetId',
            method: 'GET',
            success: (result) => {
                SignalrController.LoadData(result);
            },
            error: (error) => {
                console.log(error)
            }
        })
    },
    LoadData: function (id) {
        $.ajax({
            url: '/Admin/Home/GetData',
            data: { id: id },
            method: 'GET',
            success: (result) => {

                $("#spPlate").text(result.plateIn);
                $("#spTime").text(result.dateTimeIn);
                $("#spGroup").text(result.groupId);
                $("#spGate").text(result.gateIn);

                $("#imgVehicle a").attr('href', result.picVehicleIn)
                $("#imgVehicle img").attr('src', result.picVehicleIn)

                $("#imgAll a").attr('href', result.picAllIn)
                $("#imgAll img").attr('src', result.picAllIn)

            },
            error: (error) => {
                console.log(error)
            }
        })
    }
}