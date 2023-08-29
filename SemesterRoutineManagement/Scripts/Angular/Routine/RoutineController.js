app.controller('routineCtrl', function ($scope, routineService) {
    $scope.semesterList = [];
    var GenerateRoutine = function ()
    {
        data = {
            sessionOf: $scope.availableSession.Semester +1
        }
        routineService.GenerateRoutine(data).then(function (response) {
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
    var getAvailableSessionList = function () {
        routineService.getAvailableSession().then(function (response) {
            $scope.availableSessionList = response.data;
            console.log('response.data', response.data)
        })
    }
    getAvailableSessionList()

    $scope.onClick = function (expression, obj) {
        var data = []
        switch (expression) {
            case "Generate":
                getAvailableSessionList();
                GenerateRoutine();
                break;
            case "Regenerate":
                GenerateRoutine();
                break;
            case "getRoutines":
                console.log($scope.Semester)
                data =
                {
                    SessionId: $scope.Session.Id,
                    Term: $scope.Semester!=null? $scope.Semester.Id:null
                }
                routineService.getRoutine(data).then(function (response) {
                    $scope.RoutineListBySession = response.data
                });
                break;
            case "Remove":
                $scope.RoutineList.splice(obj, 1);
                break;
            case "SaveRoutine":
                data =
                {
                    SessionId: $scope.availableSession.Id,
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
         
            case "getSemester":
                if ($scope.Session.Semester == 0) {
                    $scope.semesterList = [{ Id: 1, Name: 'FirstYearFirstSemester' },
                    { Id: 3, Name: 'SecondYearFirstSemester' },
                    { Id: 5, Name: 'ThirdYearFirstSemester' },
                    { Id: 7, Name: 'FourthYearFirstSemester' }]
                }
                else {
                    $scope.semesterList
                        = [
                            { Id: 2, Name: 'FirstYearSecondSemester' },
                            { Id: 4, Name: 'SecondYearSecondSemester' },
                            { Id: 6, Name: 'ThirdYearSecondSemester' },
                            { Id: 8, Name: 'FourthYearSecondSemester' } ]    
                }
                break;
            case "Generate":
                GenerateRoutine();
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