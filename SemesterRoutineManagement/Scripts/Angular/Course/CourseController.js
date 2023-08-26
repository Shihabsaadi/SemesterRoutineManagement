app.controller('courseCtrl', function ($scope, courseService) {
    var getCourseList = function ()
    {
        courseService.getCourse().then(function (response) {
            $scope.CourseList = response.data;
        })
    }
    getCourseList()
   
    $scope.onClick = function (expression, obj) {
        var data = []
        switch (expression) {
            case "Sync":
                sync();
                break;
            case "EditCourse":
                $scope.Name = obj.Name
                $scope.Id = obj.Id
                $scope.Status = obj.Status
                $scope.ShortName = obj.ShortName
                $scope.Code = obj.Code

                break;
            case "SaveCourse":
                data=
                    {
                        Id: $scope.Id,
                        Name: $scope.Name,
                        ShortName:$scope.ShortName,
                        Status: $scope.Status,
                        Code: $scope.Code
                    }
                courseService.saveCourse(data).then(function (response) {

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
        }
    }
    var sync = function () {
            $scope.Name = null
            $scope.Id = null
            $scope.Status = null
            $scope.ShortName = null
            getCourseList()
    }
});