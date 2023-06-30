app.service("studentCourseEnrollmentService", ['$http', function ($http) {

    this.getSession = function () {
        return $http.get("/Session/GetSessionList");
    }
    this.getCourse = function () {
        return $http.get("/StudentCourseEnrollment/GetCourseList");
    }
    this.getAvailableStudentList = function (data) {
        return $http.post("/StudentCourseEnrollment/GetAvailableStudentList", JSON.stringify(data));
    }
  
    this.getStudentCourseEnrollment = function()
    {
        return $http.get("/StudentCourseEnrollment/GetStudentCourseEnrollmentList");
    }
    //Save 
    this.save = function (data) {
        return $http.post('/StudentCourseEnrollment/Save', JSON.stringify(data))
    }
    this.deleteStudentCourseEnrollment = function (data) {
        return $http.post('/StudentCourseEnrollment/DeleteStudentCourseEnrollment', JSON.stringify(data))
    }
}]);