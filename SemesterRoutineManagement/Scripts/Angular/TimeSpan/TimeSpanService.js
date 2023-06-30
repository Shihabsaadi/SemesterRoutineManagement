app.service("timeSpanService", ['$http', function ($http) {
    this.getTimeSpan = function()
    {
        return $http.get("/TimeSpan/GetTimeSpanList");
    }
    this.saveTimeSpan = function (data) {
        return $http.post('/TimeSpan/SaveTimeSpan', JSON.stringify(data))
    }
    this.deleteTimeSpan = function (data) {
        return $http.post('/TimeSpan/DeleteTimeSpan', JSON.stringify(data))
    }
    
}]);