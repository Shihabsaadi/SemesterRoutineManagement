app.controller('teacherAppointmentCtrl', function ($scope, teacherAppointmentService) {
    var getTeacherList = function ()
    {
        teacherAppointmentService.getTeacher().then(function (response) {
            $scope.teacherList = response.data;
        })
    }
    getTeacherList()
    var getCourseList = function () {
        teacherAppointmentService.getCourse().then(function (response) {
            $scope.courseList = response.data;
        })
    }
    getCourseList()
    var getTeacherAppointmentList = function () {
        teacherAppointmentService.getTeacherAppointment().then(function (response) {
            $scope.teacherAppointmentList = response.data;
        })
    }
    getTeacherAppointmentList()
    $scope.onClick = function (expression, obj) {
        var data = []
        switch (expression) {
            case "Sync":
                sync();
                break;
            case "EditTeacherAppointment":
                $scope.Id = obj.Id
                $scope.Teacher = obj.TeacherId
                $scope.Course = obj.CourseId
                $scope.Status = obj.Status
                break;
            case "SaveTeacherAppointment":
                data=
                    {
                    Id: $scope.Id,
                    TeacherId: $scope.Teacher,
                    CourseId: $scope.Course,
                    Status: $scope.Status,
                    }
                teacherAppointmentService.save(data).then(function (response) {

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
        var sync = function () {
            $scope.Id = null
            $scope.Teacher = null
            $scope.Course = null
            $scope.Status = null
            getTeacherAppointmentList()
            getTeacherList()
            getCourseList()
        }
    }
});