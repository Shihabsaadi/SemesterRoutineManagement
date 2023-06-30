app.controller('roomCtrl', function ($scope, roomService) {
    var getRoomList = function ()
    {
        roomService.getRoom().then(function (response) {
            $scope.roomList = response.data;
        })
    }
    getRoomList()
   
    $scope.onClick = function (expression, obj) {
        var data = []
        switch (expression) {
            case "Sync":
                sync();
                break;
            case "EditRoom":
                $scope.Id = obj.Id
                $scope.No = obj.No
                $scope.Status = obj.Status
                break;
            case "SaveRoom":
                data=
                    {
                        Id: $scope.Id,
                        No: $scope.No,
                        Status:$scope.Status
                }
                console.log('post', data)
                roomService.saveRoom(data).then(function (response) {

                    if (response.data) {
                        Swal.fire({
                            position: 'top-end',
                            type: 'success',
                            title: response.data.Message,
                            showConfirmButton: false,
                            timer: 1500
                        })
                        sync();
                    }

                });
                break;
             default:
        }
    }
    var sync = function () {
        $scope.Id = null
        $scope.No = null
        $scope.Status = null
        getRoomList()
    }
});