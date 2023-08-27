app.controller('sessionCtrl', function ($scope, $filter, sessionService) {
    var getSessionList = function ()
    {
        sessionService.getSession().then(function (response) {
            $scope.SessionList = response.data;
        })
    }
    $scope.ToDate = function (value) {
        var a;
        if (typeof value === 'string') {
            a = /\/Date\((\d*)\)\//.exec(value);
            if (a) {
                return $filter('date')(new Date(+a[1]), "yyyy-MM-dd HH:mm:ss");
            }
        }
        return value;
    }
    var ToMonths = function (value) {
        var a;
        if (typeof value === 'string') {
            a = /\/Date\((\d*)\)\//.exec(value);
            if (a) {
                return $filter('date')(new Date(+a[1]), "MM");
            }
        }
        return value;
    }
    var ToDays = function (value) {
        var a;
        if (typeof value === 'string') {
            a = /\/Date\((\d*)\)\//.exec(value);
            if (a) {
                return $filter('date')(new Date(+a[1]), "dd");
            }
        }
        return value;
    }
    var ToYears = function (value) {
        var a;
        if (typeof value === 'string') {
            a = /\/Date\((\d*)\)\//.exec(value);
            if (a) {
                return $filter('date')(new Date(+a[1]), "yyyy");
            }
        }
        return value;
    }
    var ToHours = function (value) {
        var a;
        if (typeof value === 'string') {
            a = /\/Date\((\d*)\)\//.exec(value);
            if (a) {
                return $filter('date')(new Date(+a[1]), "HH");
            }
        }
        return value;
    }
    var ToMinutes = function (value) {
        var a;
        if (typeof value === 'string') {
            a = /\/Date\((\d*)\)\//.exec(value);
            if (a) {
                return $filter('date')(new Date(+a[1]), "mm");
            }
        }
        return value;
    }
    var ToSeconds = function (value) {
        var a;
        if (typeof value === 'string') {
            a = /\/Date\((\d*)\)\//.exec(value);
            if (a) {
                return $filter('date')(new Date(+a[1]), "ss");
            }
        }
        return value;
    }
    getSessionList()
    $scope.semesterList = ['FirstSemester', 'SecondSemester']
    $scope.onClick = function (expression, obj) {
        var data = []
        switch (expression) {
            case "Sync":
                sync();
                break;
            case "EditSession":
                var year = ToYears(obj.Date)
                var date = ToDays(obj.Date)
                var month = ToMonths(obj.Date) - 1
                var hours = ToHours(obj.Date)
                var second = ToSeconds(obj.Date)
                var minutes = ToMinutes(obj.Date)
                $scope.Date = {
                    value: new Date(year, month, date, hours, minutes)
                };
                console.log($scope.Date)

                $scope.Semester = $scope.semesterList[obj.Semester]
                $scope.Id = obj.Id
                $scope.Status = obj.Status
                break;
            case "SaveSession":
                data=
                    {
                        Id: $scope.Id,
                        Semester: $scope.Semester,
                        Date: $scope.Date.value,
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
            $scope.Date = null
            $scope.Id = null
            $scope.Semester = null
            $scope.Status = null
            getSessionList()
    }
});