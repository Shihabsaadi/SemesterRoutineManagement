app.controller('userCtrl', function ($scope, userService) {
    $scope.editUser = false;
    var getRoleList = function ()
    {
        userService.getRole().then(function (response) {
            $scope.roleList = response.data;
        })
    }
    getRoleList()
    var getUserList = function () {
        userService.getUser().then(function (response) {
            $scope.userList = response.data;
        })
    }
    getUserList()
    $scope.onClick = function (expression, obj) {
        var data = []
        switch (expression) {
            case "Sync":
                sync();
                break;
            case "EditUser":
                console.log('obj', obj)
                $scope.Name = obj.Name
                $scope.Id = obj.Id
                $scope.Role = obj.Role
                $scope.Status = obj.Status
                $scope.Phone = obj.phone
                $scope.Email = obj.Email
                $scope.editUser = true;
                break;
            case "SaveUser":
                data=
                    {
                        Id: $scope.Id,
                        Name: $scope.Name,
                        Status: $scope.Status,
                        Email: $scope.Email,
                        Role : $scope.Role,
                        phone: $scope.Phone,
                        UserName: $scope.UserName,
                    }
                userService.saveUser(data).then(function (response) {
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
        $scope.editUser = false;
        $scope.Name = null
        $scope.Id = null
        $scope.Role = null
        $scope.Status = null
        $scope.Phone = null
        $scope.Email = null
        getRoleList()
        getUserList()
    }
});