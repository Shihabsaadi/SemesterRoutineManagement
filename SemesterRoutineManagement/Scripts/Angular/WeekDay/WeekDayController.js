app.controller('weekDayCtrl', function ($scope, $filter, weekDayService) {
    var getWeekDayList = function ()
    {
        weekDayService.getWeekDay().then(function (response) {
            $scope.weekDayList = response.data;
        })
    }
 
    getWeekDayList()
    $scope.onClick = function (expression, obj) {
        var data = []
        switch (expression) {
            case "Sync":
                sync();
                break;
            case "EditWeekDay":
                $scope.Id = obj.Id
                $scope.Name = obj.Name
                $scope.ShortName = obj.ShortName
                $scope.Sort = obj.Sort
                $scope.Status = obj.Status
                break;
            case "SaveWeekDay":
                data=
                    {
                        Id: $scope.Id,
                        Name: $scope.Name,
                        ShortName: $scope.ShortName,
                        Sort: $scope.Sort,
                        Status:$scope.Status
                }
                weekDayService.saveWeekDay(data).then(function (response) {

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
        $scope.Id = null
        $scope.Name = null
        $scope.ShortName = null
        $scope.Sort = null
        $scope.Status = null
        getWeekDayList()
    }
});