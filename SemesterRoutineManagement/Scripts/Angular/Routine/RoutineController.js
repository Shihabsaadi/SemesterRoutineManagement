app.controller('routineCtrl', function ($scope, routineService) {
    var GenerateRoutine = function ()
    {
        routineService.GenerateRoutine().then(function (response) {
            $scope.RoutineList = response.data;
            console.log($scope.RoutineList)
        })
    }
    var getSessionList = function () {
        routineService.getSession().then(function (response) {
            $scope.sessionList = response.data;
        })
    }
    getSessionList()
    $scope.onClick = function (expression, obj) {
        var data = []
        switch (expression) {
            case "Generate":
                GenerateRoutine();
                break;
            case "Regenerate":
                GenerateRoutine();
                break;
            case "Remove":
                $scope.RoutineList.splice(obj, 1);
                break;
            case "SaveRoutine":
                data =
                {
                    SessionId: $scope.Session,
                    Routines: $scope.RoutineList
                }
                routineService.Save(data).then(function (response) {
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
            case "Delete":
                swal.fire({
                    title: 'Are you sure?',
                    type: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Yes'
                }).then(function (willDelete) {
                    console.log('willdelete', willDelete)
                    if (willDelete.value == true) {
                        data = { Id: obj.Id }
                        routineService.deleteRoutine(data).then(function (response) {
                            if (response.data) {
                                swal.fire(
                                    'Deleted!',
                                    'success'
                                )
                                sync();
                            }
                        });
                    }
                    else {
                        // Cancelled the delete action
                        console.log("Delete cancelled");
                    }
                });
                break;
        }
    }
    $scope.onChange = function (expression, obj) {
        switch (expression) {
            case "getRoutines":
                data =
                {
                    SessionId: $scope.Session,
                }
                routineService.getRoutine(data).then(function (response) {
                    $scope.RoutineListBySession=response.data
                });
                break;
            default:
        }
    }
    var sync = function () {
        data =
        {
            SessionId: $scope.Session,
        }
        routineService.getRoutine(data).then(function (response) {
            $scope.RoutineListBySession = response.data
        });
        $scope.RoutineList = null;
    }
});