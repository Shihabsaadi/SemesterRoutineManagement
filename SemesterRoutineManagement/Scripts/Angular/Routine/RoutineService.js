app.service("routineService", ['$http', function ($http) {

    this.getSession = function () {
        return $http.get("/Session/GetSessionList");
    }
    this.getAvailableSession = function () {
        return $http.get("/Session/GetAvailableSessionList");
    }
    this.getRoutine = function (data) {
        return $http.post("/Routine/GetRoutineList", JSON.stringify(data));
    }
    this.GenerateRoutine = function (data) {
        return $http.post("/Routine/GenerateRoutine", JSON.stringify(data));
    }
    this.Save = function (data) {
        return $http.post('/Routine/Save', JSON.stringify(data))
    }
    this.deleteRoutine = function (data) {
        return $http.post('/Routine/DeleteRoutine', JSON.stringify(data))
    }
    
}]);