app.controller('studentCourseEnrollmentCtrl', function ($scope, studentCourseEnrollmentService) {

    var getSessionList = function () {
        studentCourseEnrollmentService.getSession().then(function (response) {
            $scope.sessionList = response.data;
        })
    }
    getSessionList()
    var getCourseList = function () {
        studentCourseEnrollmentService.getCourse().then(function (response) {
            $scope.courseList = response.data;
        })
    }
    getCourseList()
   
    var getAvailableStudentList = function () {
        data = {
            SessionId: $scope.Session,
            CourseId: $scope.Course
        }
        studentCourseEnrollmentService.getAvailableStudentList(data).then(function (response) {
            $scope.studentList = response.data;
        })
    }
    var getStudentCourseEnrollmentList = function () {
        studentCourseEnrollmentService.getStudentCourseEnrollment().then(function (response) {
            $scope.studentCourseEnrollmentList = response.data;
        })
    }
    getStudentCourseEnrollmentList()
    $scope.onClick = function (expression, obj) {
        var data = []
        switch (expression) {
            case "Sync":
                //sync();
                break;
            case "EditStudentCourseEnrollment":
                //$scope.Id = obj.Id
                //$scope.Teacher = obj.TeacherId
                //$scope.Course = obj.CourseId
                //$scope.Status = obj.Status
                break;
            case "SaveStudentCourseEnrollment":
                var selectedItems = $scope.studentList.filter(function (item) {
                    return item.Selected
                }).map(function (item) {
                    return item.Id;
                });

                data=
                    {
                    SessionId: $scope.Session,
                    CourseId: $scope.Course,
                    Status: true,
                    StudentIds: selectedItems
                    }
                studentCourseEnrollmentService.save(data).then(function (response) {
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
            case "DeleteStudentCourseEnrollment":

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
                        studentCourseEnrollmentService.deleteStudentCourseEnrollment(data).then(function (response) {
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
                    }
                    else {
                        // Cancelled the delete action
                        console.log("Delete cancelled");
                    }
                });
                break;
             default:
        }
        var sync = function () {
            $scope.Id = null
            $scope.Session = null
            $scope.Course = null
            $scope.selectedItems = null
            getStudentCourseEnrollmentList()
            getSessionList()
            getCourseList()
        }
    }
    $scope.onChange = function (expression, obj) {
        var data = []
        switch (expression) {
            case "getAvailableStudents":
                getAvailableStudentList();
                break;
            
            default:
        }
        //var sync = function () {
        //    $scope.Id = null
        //    $scope.Teacher = null
        //    $scope.Course = null
        //    $scope.Status = null
        //    getStudentCourseEnrollmentList()
        //    getTeacherList()
        //    getCourseList()
        //}
    }
});