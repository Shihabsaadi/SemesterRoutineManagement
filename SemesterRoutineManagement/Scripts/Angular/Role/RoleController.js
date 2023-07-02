app.controller('roleCtrl', function ($scope, roleService) {
    var getRoleList = function ()
    {
        roleService.getRole().then(function (response) {
            $scope.RoleList = response.data;
            console.log($scope.RoleList)
        })
    }
    getRoleList()
   
    $scope.onClick = function (expression, obj) {
        var data = []
        switch (expression) {
            case "Sync":
                sync();
                break;
            case "EditRole":
                $scope.Name = obj.Name
                $scope.Id = obj.Id
                $scope.Status = obj.Status
                break;
            case "SaveRole":
                data=
                    {
                        Id: $scope.Id,
                        Name: $scope.Name,
                        Status:$scope.Status
                    }
                roleService.saveRole(data).then(function (response) {

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
            getRoleList()
    }
});