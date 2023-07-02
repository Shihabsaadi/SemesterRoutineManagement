app.controller('sessionCtrl', function ($scope, sessionService) {
    var getSessionList = function ()
    {
        sessionService.getSession().then(function (response) {
            $scope.SessionList = response.data;
            console.log($scope.SessionList)
        })
    }
    getSessionList()
   
    $scope.onClick = function (expression, obj) {
        var data = []
        switch (expression) {
            case "Sync":
                sync();
                break;
            case "EditSession":
                $scope.Name = obj.Name
                $scope.Id = obj.Id
                $scope.Status = obj.Status
                break;
            case "SaveSession":
                data=
                    {
                        Id: $scope.Id,
                        Name: $scope.Name,
                        Status:$scope.Status
                    }
                sessionService.saveSession(data).then(function (response) {

                    if (response.data.Success) {
                        Swal.fire({
                            position: 'top-end',
                            type: 'success',
                            title: response.data.Message,
                            showConfirmButton: false,
                            timer: 1500
                        })
                        sync();
                    }
                    else {
                        Swal.fire({
                            position: 'top-end',
                            type: 'error',
                            title: response.data.Message,
                            showConfirmButton: false,
                            timer: 1500
                        })
                    }

                });
                break;
             default:
        }
    }
    var sync = function () {
            $scope.Name = null
            $scope.Id = null
            $scope.Status = null
            getSessionList()
    }
});